using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using VDCA.Models;
using VDCA.Utils;
using VDCA.Views;

namespace VDCA.ViewModels
{
    public abstract partial class SelectViewModel : BindableObject
    {
        private readonly string LOGTAG = "SelectViewModel";
        // Define bindable properties for margin to change to fix the gird in landscape mode mdail 3-19-25
        public static readonly BindableProperty GridMarginProperty =
            BindableProperty.Create(nameof(GridMargin), typeof(Thickness), typeof(SelectViewModel), new Thickness(0, 0, 0, 0));
        //Define bindable properties for margin to change to fix the gird in landscape mode mdail 3-19-25
        public Thickness GridMargin
        {
            get => (Thickness)GetValue(GridMarginProperty);
            set => SetValue(GridMarginProperty, value);
        }
        //Categories
        public ObservableCollection<Categories> CategoriesCollection { get; set; }
        //Used to track the Selected Categories 
        public ObservableCollection<Categories> SelectedCategories { get; set; }
        public SelectView sv;
        public ICommand SelectCommand { get; set; }
        public ICommand DeselectCommand { get; set; }
        public ICommand StartCommand { get; set; }
        public ICommand CollectionViewSelectionChanged { get; set; }
        public SelectViewModel()
        {
            CollectionViewSelectionChanged = new AsyncCommand(OnSelectionChanged);
            SelectCommand = new Command(async () => await OnSelectCommandExecuted());
            DeselectCommand = new Command(async () => await OnDeselectCommandExecuted());
            StartCommand = new Command(async () => await OnStartCommandExecuted());
        }
        public async Task OnSelectCommandExecuted()
        {
            await SetAllSelected();
        }
        public async Task OnDeselectCommandExecuted()
        {
            await SetAllDeselected();
        }
        public async Task OnStartCommandExecuted()
        {
            await CardsForSelectedSubjects();
        }
        private async Task OnSelectionChanged()
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                if (SelectView.CatalogCollectionView.SelectedItem != null)
                {
                    Categories current = (Categories)SelectView.CatalogCollectionView.SelectedItem;
                    SelectedCategories ??= [];
                    if (current != null)
                    {
                        int index = CategoriesCollection.IndexOf(current);
                        if (index != -1)
                        {
                            CategoriesCollection[index].ToggleSelected();
                            if (CategoriesCollection[index].ItIsChecked)
                            {
                                SelectedCategories.Add(CategoriesCollection[index]);
                            }
                            else
                            {
                                if (SelectedCategories.Count > 0)
                                {
                                    bool worked = SelectedCategories.Remove(current);
                                    if (!worked)
                                    {
                                        Console.WriteLine(LOGTAG + " Failed to remove category " + current.CategoryName);
                                    }
                                }
                            }
                        }
                    }
                    SelectView.CatalogCollectionView.SelectedItem = null;
                }
            });
        }
        //connect the vm to v and help the binding of the collection view to the vm
        public void SetView(SelectView _sv)
        {
            sv = _sv;
        }
        // Abstract method that must be overridden in derived classes mdail 9-5-24
        public abstract Task CardsForSelectedSubjects();
        // Abstract method that must be overridden in derived classes then call the base class method SaveSelectedCats(string SavedFileName) mdail 3-5-24
        public async Task SaveSelectedCats()
        {
            if (sv.IsFlashcards)
            {
                await SaveSelectedCats(Constants.SavedFlashCats);
            }
            else{
                await SaveSelectedCats(Constants.SavedPracticeCats);
            }
        }
        public async Task SetSelected()
        {
            if (sv.IsFlashcards)
            {
                await SetSelectedCategories(Constants.SavedFlashCats);
            }
            else{
                await SetSelectedCategories(Constants.SavedPracticeCats);
            }
        }
        // If I add run task to the select all and deselect all they slow down far too much, these work better mdail 3-5-24
        public async Task SetAllSelected()
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                SelectedCategories ??= [];
                SelectedCategories.Clear();
                if (CategoriesCollection != null && CategoriesCollection.Count > 0)
                {
                    var tasks = CategoriesCollection.Select(async category =>
                    {
                        if (category != null)
                        {
                            category.ItIsChecked = true;
                            int index = CategoriesCollection.IndexOf(category);
                            if (index != -1)
                            {
                                if (CategoriesCollection[index].ItIsChecked)
                                {
                                    await AddCategoryToSelectedCategories(CategoriesCollection[index]);
                                }
                            }
                        }
                    });
                    await Task.WhenAll(tasks);
                }
            });
        }
        // If I add run task to the select all and deselect all they slow down far too much, these work better mdail 3-5-24
        public async Task SetAllDeselected()
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                SelectedCategories ??= [];
                if (CategoriesCollection != null && CategoriesCollection.Count > 0)
                {
                    var tasks = CategoriesCollection.Select(async category =>
                    {
                        if (category != null)
                        {
                            await SetCategoryDeselected(category);
                        }
                    });
                    await Task.WhenAll(tasks);
                    SelectedCategories.Clear();
                }
            });
        }
        // Method to save selected categories to a file
        public async Task SaveSelectedCats(string SaveToFileName)
        {
            var json = JsonSerializer.Serialize(SelectedCategories);
            await File.WriteAllTextAsync(SaveToFileName, json);
        }
        public async Task SetSelectedCategories(string SavedFileName)
        {
            if (File.Exists(SavedFileName))
            {
                var json = await File.ReadAllTextAsync(SavedFileName);
                var savedCategories = JsonSerializer.Deserialize<ObservableCollection<Categories>>(json);
                if (savedCategories != null)
                {
                    if (savedCategories.Count > 0)
                    {
                        // Clear the current SelectedCategories
                        SelectedCategories.Clear();
                        // Update CategoriesCollection based on the saved categories
                        var tasks = savedCategories.Select(async savedCategory =>
                        {
                            var category = CategoriesCollection.FirstOrDefault(c => c.CategoryName == savedCategory.CategoryName);
                            if (category != null)
                            {
                                category.ItIsChecked = savedCategory.ItIsChecked;
                                if (category.ItIsChecked)
                                {
                                    await AddCategoryToSelectedCategories(category);
                                }
                            }
                        });
                        await Task.WhenAll(tasks);
                    }
                    else
                    {
                        await SetAllSelected();
                    }
                }
            }
            else
            {
                await SetAllSelected();
            }
        }
        private static async Task SetCategoryDeselected(Categories category)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                if (category != null)
                {
                    category.ItIsChecked = false;
                }
            });
        }

        private async Task AddCategoryToSelectedCategories(Categories category)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                if (category != null && !SelectedCategories.Contains(category))
                {
                    SelectedCategories.Add(category);
                }
            });
        }

    }
}