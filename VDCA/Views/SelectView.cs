using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;
using VDCA.Utils;
using VDCA.ViewModels;

namespace VDCA.Views;

public partial class SelectView : ContentPage
{
    //the ViewModel is set to = the name of the bindingcontext in the xaml file mdail 10-16-24
    public SelectViewModel ViewModel;
    private static CollectionView CatalogCollectionViewLocal;
    private static Grid SelectGridLocal;
    public InformationAlert InformationSelectAlert;
    public QuestionAlert QuestionSelectAlert;
    public ProgressBarOverlay ProgressBarSelectOverlay;
    public bool IsFlashcards;
    public DisplayOrientation Orientation = DisplayOrientation.Unknown;
    public DisplayOrientation PreviousOrientation = DisplayOrientation.Unknown;

    public static CollectionView CatalogCollectionView
    {
        get => CatalogCollectionViewLocal;
        private set => CatalogCollectionViewLocal = value;
    }

    public static Grid SelectGrid
    {
        get => SelectGridLocal;
        private set => SelectGridLocal = value;
    }

    public SelectView()
    {
        this.SizeChanged += OnSizeChanged;
        Constants.FROM_QUIZ = false;
        Orientation = DeviceDisplay.MainDisplayInfo.Orientation;
        PreviousOrientation = Orientation;
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior
        {
            IsVisible = true,
            IconOverride = "chevron_left_white.png",
            Command = new Command(async () => await SelectView.OnBackButtonClicked())
        });
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (ProgressBarSelectOverlay != null)
        {
            await ShowProgressBarUtil.ShowProgressBar(ProgressBarSelectOverlay);
        }
        await ViewModel.SetSelected();
        if (ProgressBarSelectOverlay != null)
        {
            await ShowProgressBarUtil.HideProgressBar(ProgressBarSelectOverlay);
        }
    }

    public static void SetSelectView(CollectionView catCollection, Grid theGrid)
    {
        CatalogCollectionView = catCollection;
        SelectGrid = theGrid;
    }

    public void SetContextViews(InformationAlert informationAlert = null, ProgressBarOverlay progressBarQuizOverlay = null)
    {
        if (informationAlert != null)
        {
            InformationSelectAlert = informationAlert;
        }
        if (progressBarQuizOverlay != null)
        {
            ProgressBarSelectOverlay = progressBarQuizOverlay;
        }
    }

    public void ShowInformationAlert(string title, string message)
    {
        if (ProgressBarSelectOverlay != null && ProgressBarSelectOverlay.IsVisible)
        {
            _ = ShowProgressBarUtil.HideProgressBar(ProgressBarSelectOverlay);
        }
        InformationSelectAlert.ShowAlert(title, message);
    }

    public void ClearCatList()
    {
        var tasks = ViewModel.CategoriesCollection.Select(cat => Task.Run(() => cat.SetIsSelected(false))).ToArray();
        Task.WhenAll(tasks); // This line ensures tasks are started but not awaited
    }

    private void OnSizeChanged(object sender, EventArgs e)
    {
        Orientation = DeviceDisplay.MainDisplayInfo.Orientation;
        if (PreviousOrientation != Orientation)
        {
            //fix the grid not always using the entire screen in portrait mode on first rotation mdail 3-19-25
            if (Orientation == DisplayOrientation.Portrait)
            {
                ViewModel.GridMargin = new Thickness(0, 0, 0, 0);
            }
            else if (Orientation == DisplayOrientation.Landscape)
            {
                ViewModel.GridMargin = new Thickness(0, 0, -1, 0);
            }
        }
        PreviousOrientation = Orientation;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Task.Run(async () => await ExecuteExitLogic());
    }

    private async Task ExecuteExitLogic()
    {
        await ViewModel.SaveSelectedCats();
        ClearCatList();
    }

    private static async Task OnBackButtonClicked() => await Shell.Current.GoToAsync("//MainPage");

    protected override bool OnBackButtonPressed()
    {
        MainThread.InvokeOnMainThreadAsync(async () =>
        {
            try
            {
                await Shell.Current.GoToAsync("//MainPage");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Navigation error: {ex.Message}");
            }
        });
        return true;
    }
}
