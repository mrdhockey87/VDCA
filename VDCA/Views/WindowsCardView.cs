using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System;
using System.Collections.ObjectModel;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Input;
using VDCA.Models;
using VDCA.Utils;
using VDCA.ViewModels;

namespace VDCA.Views;

public partial class WindowsCardView : ContentPage
{
    private static readonly string LOGTAG = "WindowsCardView";
    public WindowsCardViewModel ViewModel;
    public CollectionView CollectionCardView;
    public Label RunningCount;
    public ProgressBar RunningProgressBar;
    public Grid PageGrid;
    public Grid MainGrid;
    public HelpContentView HelpContentView;
    public InformationAlert InformationAlert;
    public QuestionAlert QuestionAlert;
    public ProgressBarOverlay ProgressBarQuizOverlay;
    public ReviewQuestion ReviewQuizQuestion;
    private readonly string Flash = "WindowsFlashcardView";
    private readonly string Practice = "WindowsPracticecardView";
    private readonly string Quiz = "WindowsQuizView";
    public DisplayOrientation Orientation = DisplayOrientation.Unknown;
    public DisplayOrientation PreviousOrientation = DisplayOrientation.Unknown;
    public ICommand UpdateProgressCommand { get; set; }
    public ICommand NextCommand { get; set; }
    public ICommand PreviousCommand { get; set; }
    public ICommand RandomCommand { get; set; }
    public ICommand ExplanationCommand { get; set; }
    public ICommand CitationCommand { get; set; }
    public ICommand FeedbackCommand { get; set; }
    public ICommand HiddedCommand { get; set; }
    public ICommand FlaggedCommand { get; set; }
    public ICommand HelpCommand { get; set; }
    public ICommand OnSelectionChangedCommand { get; set; }
    public ICommand DummyQuestionCommand { get; set; }
    private bool ExplanationVisibleLocal { get; set; }
    public bool ExplanationVisible 
    {
        get 
        {
            return ExplanationVisibleLocal;
        }
        set
        {
                ExplanationVisibleLocal = value;
                OnPropertyChanged(nameof(ExplanationVisible));
        }
    }
    private bool CitationVisibleLocal { get; set; }
    public bool CitationVisible
    {
        get
        {
            return CitationVisibleLocal;
        }
        set
        {
            CitationVisibleLocal = value;
            OnPropertyChanged(nameof(CitationVisible));
        }
    }
    public WindowsCardView()
    {
        BindingContext = ViewModel;
        Orientation = DeviceDisplay.MainDisplayInfo.Orientation;
        PreviousOrientation = Orientation;
        this.SizeChanged += OnSizeChanged;
        UpdateProgressCommand = new Command(async () => await OnUpdateProgressCommandExecuted());
        NextCommand = new Command(async () => await OnNextCommandExecuted());
        PreviousCommand = new Command(async () => await OnPreviousCommandExecuted());
        RandomCommand = new Command(async () => await OnRandomCommandExecuted());
        ExplanationCommand = new Command(async () => await OnExplanationCommandExecuted());
        CitationCommand = new Command(async () => await OnCitationCommandExecuted());
        FeedbackCommand = new Command(async () => await OnFeedbackCommandExecuted());
        HiddedCommand = new Command(async () => await OnHiddedCommandExecuted());
        FlaggedCommand = new Command(async () => await OnFlaggedCommandExecuted());
        HelpCommand = new Command(async () => await OnHelpCommandExecuted());
        DummyQuestionCommand = new Command(DummyQuestionCommand_Executed);
        _ = OnUpdateProgressCommandExecuted();
    }
    public void DummyQuestionCommand_Executed()
    {
        // This command is used to handle the selection change event in the CollectionView
        // It is intentionally left empty as it is not used in this context
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        System.Diagnostics.Debug.WriteLine($"FlashcardView dimensions: {Width}x{Height}");
    }
    public void SetViewModel(WindowsCardViewModel viewModel)
    {
        ViewModel = viewModel;
        BindingContext = viewModel;
    }
    public void SetCardView(CollectionView carouselView, Label runningCount, ProgressBar runningProgressBar, Grid painGrid, Grid mainGrid) //, RefreshView refreshView)
    {
        CollectionCardView = carouselView;
        RunningCount = runningCount;
        RunningProgressBar = runningProgressBar;
        PageGrid = painGrid;
        MainGrid = mainGrid;
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            //CarouselCardView.IsScrollAnimated = false;
        }
    }
    public void SetContextViews(HelpContentView helpContentView = null, InformationAlert informationAlert = null, QuestionAlert questionAlert = null, 
        ProgressBarOverlay progressBarQuizOverlay = null, ReviewQuestion reviewQuizQuestion = null)
    {

        if (helpContentView != null)
        {
            HelpContentView = helpContentView;
        }
        if (informationAlert != null)
        {
            InformationAlert = informationAlert;
        }

        if (questionAlert != null)
        {
            QuestionAlert = questionAlert;
        }

        if (progressBarQuizOverlay != null)
        {
            ProgressBarQuizOverlay = progressBarQuizOverlay;
        }

        if (reviewQuizQuestion != null)
        {
            ReviewQuizQuestion = reviewQuizQuestion;
        }
    }
    private void OnScrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        if (sender is CollectionView collectionView)
        {
            // Get the first visible item index
            int centerIndex = e.CenterItemIndex;

            // If valid index and different from current selected item
            if (centerIndex >= 0 && centerIndex < ViewModel.CardQuestions.Count &&
                                                  ViewModel.CurrentQuestion != ViewModel.CardQuestions[centerIndex])
            {
                // Update the selected item
                collectionView.SelectedItem = ViewModel.CardQuestions[centerIndex];
                // Update the view model's current question
                ViewModel.CurrentQuestion = ViewModel.CardQuestions[centerIndex];
            }
        }
    }
    public async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int position = ViewModel.CardQuestions.IndexOf(ViewModel.CurrentQuestion);
        string CardName = ViewModel.cv.GetType().Name;
        if (CardName == Flash)
        {
            //Reset the cards before an after to unflipped so the cards are always answer up mdail 3-19-25
            ResetFlipp(position - 1);
            ResetFlipp(position + 1);
        }
        await ViewModel.ReloadCurrentItem();
    }
    private void ResetFlipp(int PreviousPosition) 
    {
        if (PreviousPosition >= 0 && PreviousPosition < ViewModel.CardQuestions.Count)
        {
            //reset the previous card to unflipped so the cards are always answer up mdail 9-23-24
            ViewModel.CardQuestions[PreviousPosition].Flipped = false;
        }
    }
    public async Task ExecuteRefresh()
    {
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            this.InvalidateMeasure();
            MainGrid.RowDefinitions.Clear();
            MainGrid.ColumnDefinitions.Clear();
            MainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            Grid.SetRow(CollectionCardView, 0);
            Grid.SetColumn(CollectionCardView, 0);
        });
    }
    public async Task OnHelpCommandExecuted()
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            if (HelpContentView != null)
            {
                await HelpContentView.ShowAlertAsync(true);
            }
        });
    }
    public async Task OnUpdateProgressCommandExecuted()
    {
        if (CollectionCardView != null)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                int position = ViewModel.CardQuestions.IndexOf(ViewModel.CurrentQuestion);
                position++;
                int count = ViewModel.CardQuestions.Count;
                RunningCount.Text = position.ToString() + " of " + count.ToString();
                double progress = (double)position / (double)count;
                RunningProgressBar.Progress = progress;
            });
        }
    }
    //Move to the card at the given position with the animation
    public async Task MoveToCard(int position)
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            if (position < 0 || position >= ViewModel.CardQuestions.Count)
                return;

            int currentPosition = ViewModel.CardQuestions.IndexOf(ViewModel.CurrentQuestion);
            System.Diagnostics.Debug.WriteLine($"Moving from position {currentPosition} to {position}");

            if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
            {
                // For all navigation, use the same sliding animation regardless of index
                bool isForward = currentPosition < position;
                await SimpleWindowsScrollAnimation(currentPosition, position, isForward);
            }
            else
            {
                // For other platforms, use standard scrolling
                CollectionCardView.ScrollTo(position, position: ScrollToPosition.Center, animate: true);
            }

            // Update progress indicator
            await OnUpdateProgressCommandExecuted();
        });
    }

    // Improved sliding animation method for Windows
    private async Task SimpleWindowsScrollAnimation(int fromPosition, int toPosition, bool isForward)
    {
        await MainThread.InvokeOnMainThreadAsync(async () => {
            try
            {
                // Direction based on whether we're going forward or backward
                double translationDirection = isForward ? -CollectionCardView.Width : CollectionCardView.Width;
                // First translate the view off screen in the appropriate direction
                await CollectionCardView.TranslateTo(translationDirection, 0, 250, Easing.SinOut);
                // While off screen, scroll to the new position without animation
                CollectionCardView.ScrollTo(toPosition, position: ScrollToPosition.Center, animate: false);
                // Update the selection
                CollectionCardView.SelectedItem = ViewModel.CardQuestions[toPosition];
                ViewModel.CurrentQuestion = ViewModel.CardQuestions[toPosition];
                // Reset the translation immediately (puts it off screen in opposite direction)
                CollectionCardView.TranslationX = -translationDirection;
                // Small delay to ensure layout is updated
                await Task.Delay(10);
                // Animate back to center
                await CollectionCardView.TranslateTo(0, 0, 250, Easing.SinOut);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in SimpleWindowsScrollAnimation: {ex.Message}");
                // Fallback: direct scroll without animation
                CollectionCardView.ScrollTo(toPosition, position: ScrollToPosition.Center, animate: false);
                CollectionCardView.SelectedItem = ViewModel.CardQuestions[toPosition];
                ViewModel.CurrentQuestion = ViewModel.CardQuestions[toPosition];
            }
        });
    }
    // Grow back
    private async Task OnNextCommandExecuted()
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            int index = ViewModel.CardQuestions.IndexOf(ViewModel.CurrentQuestion);
            index++;
            if (index < ViewModel.CardQuestions.Count)
            {
                await MoveToCard(index);
            }
            else
            {
                await VDCAMessages.ShowToast(CollectionCardView, Constants.LastCard);
            }
        });
    }
    private async Task OnPreviousCommandExecuted()
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            int index = ViewModel.CardQuestions.IndexOf(ViewModel.CurrentQuestion);
            index--;
            if (index >= 0)
            {
                await MoveToCard(index);
            }
            else
            {
                await VDCAMessages.ShowToast(CollectionCardView, Constants.FirstCard);
            }
        });
    }
    private async Task OnRandomCommandExecuted()
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            int currentPosition = ViewModel.CardQuestions.IndexOf(ViewModel.CurrentQuestion);
            Random random_number = new();
            int newPosition;
            do
            {
                newPosition = random_number.Next(ViewModel.CardQuestions.Count);
            } while (newPosition == currentPosition);
            await MoveToCardRandom(newPosition);
        });
    }
    //Move to the card at the given position with the animation mdail 5-14-24
    public async Task MoveToCardRandom(int position)
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            int currentPosition = ViewModel.CardQuestions.IndexOf(ViewModel.CurrentQuestion);
            // Shrink effect
            await CollectionCardView.ScaleTo(DeviceInfo.Current.Platform == DevicePlatform.WinUI ||
                  DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst ? 0.5 : 0.8, 200, Easing.Linear);
            // Direct scroll without animation
            CollectionCardView.ScrollTo(position, position: ScrollToPosition.Center, animate: false);
            // Update selection
            CollectionCardView.SelectedItem = ViewModel.CardQuestions[position];
            ViewModel.CurrentQuestion = ViewModel.CardQuestions[position];
            // Small delay to ensure layout is updated
            await Task.Delay(200);
            // Grow back
            await CollectionCardView.ScaleTo(1, 200, Easing.Linear);
        });
    }
    public async Task OnExplanationCommandExecuted()
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            if (ViewModel.CurrentQuestion.Explanation.Length > 0)
            {
                await ShowInformationAlert(Constants.Explanation, Util.FixPipeExplanation(ViewModel.CurrentQuestion.Explanation));
            }
        });
    }
    public async Task OnCitationCommandExecuted()
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            if (ViewModel.CurrentQuestion.Citation.Length > 0)
            {
                await ShowInformationAlert(Constants.Citation, ViewModel.CurrentQuestion.Citation);
            }
        });
    }
    public async Task ShowInformationAlert(string title, string message)
    {
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            InformationAlert?.ShowAlert(title, message);
        });
    }
    public async Task<bool> ShowQuestionAlertAsync(string title, string message)
    {
        QuestionAlert.AlertTitle = title;
        QuestionAlert.AlertMessage = message;
        QuestionAlert.IsVisible = true;
        // Wait for the user's response
        bool result = await QuestionAlert.WaitForUserResponseAsync();
        // Hide the QuestionAlert
        QuestionAlert.IsVisible = false;
        return result;
    }
    private async Task OnFeedbackCommandExecuted()
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            await VDCAMessages.SendFeedBack(ViewModel.CurrentQuestion);
        });
    }
    private async Task OnHiddedCommandExecuted()
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            await UpdateQuestionFileds.HideItem(ViewModel.CurrentQuestion);
        });
    }
    private async Task OnFlaggedCommandExecuted()
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            await UpdateQuestionFileds.FlagItem(ViewModel.CurrentQuestion);
        });
    }
    private void OnSizeChanged(object sender, EventArgs e)
    {
        Orientation = DeviceDisplay.MainDisplayInfo.Orientation;
        if (PreviousOrientation != Orientation && ViewModel?.CardQuestions?.Count > 0)
        {
            MainThread.InvokeOnMainThreadAsync(async () =>
            {
                try
                {
                    // Save current position before making any changes
                    int position = ViewModel.CardQuestions.IndexOf(ViewModel.CurrentQuestion);
                    if (position < 0) position = 0; // Safety check
                    // Ensure we're on the correct item
                    if (position < ViewModel.CardQuestions.Count)
                    {
                        // First set the selected item
                        CollectionCardView.SelectedItem = ViewModel.CardQuestions[position];
                        // Then scroll to it
                        CollectionCardView.ScrollTo(position, position: ScrollToPosition.Center, animate: false);
                        await Task.Delay(100);
                        // Ensure visual state is correct
                        ExplanationVisible = ViewModel.CardQuestions[position].Explanation.Length > 0;
                        CitationVisible = ViewModel.CardQuestions[position].Citation.Length > 0;
                        // Update the progress indicator for all platforms
                        await OnUpdateProgressCommandExecuted();
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error in orientation change: {ex.Message}");
                }
            });

            PreviousOrientation = Orientation;
        }
    }
    //Fix the CarouselView losing the position on screen rotation mdail 3-6-25
    /* private void OnSizeChanged(object sender, EventArgs e)
     {
         Orientation = DeviceDisplay.MainDisplayInfo.Orientation;
         if (PreviousOrientation != Orientation)
             MainThread.InvokeOnMainThreadAsync(async () =>
             {
                 int position = ViewModel.CardQuestions.IndexOf(ViewModel.CurrentQuestion);
                 if (DeviceInfo.Current.Platform == DevicePlatform.Android)
                 {
                     // Force the CarouselView to recreate its views
                     var itemsSource = CollectionCardView.ItemsSource;
                     CollectionCardView.ItemsSource = null;
                     CollectionCardView.ItemsSource = itemsSource;
                 }
                 await ResetScrollToTop(position);
                 //This tiny delay ensure the CarouselView will scroll to the position requested mdail 3-6-25
                 await Task.Delay(1);
                 CollectionCardView.ScrollTo(position, position: ScrollToPosition.Center, animate: false);
                 //Running OnUpdateProgressCommandExecuted caused an index out of range on iOS
                 // and also the code below does as well mdail 3-14-25
                 if (DeviceInfo.Current.Platform == DevicePlatform.Android)
                 {
                     position++;
                     int count = ViewModel.CardQuestions.Count;
                     RunningCount.Text = position.ToString() + " of " + count.ToString();
                     double progress = (double)position / (double)count;
                     RunningProgressBar.Progress = progress;
                 }
             });
         PreviousOrientation = Orientation;
     }*/
}