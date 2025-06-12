using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Input;
using VDCA.Models;
using VDCA.Utils;
using VDCA.ViewModels;

namespace VDCA.Views;

public partial class CardView : ContentPage
{
    private static readonly string LOGTAG = "CardView";
    public CardViewModel ViewModel;
    public CarouselView CarouselCardView;
    public Label RunningCount;
    public ProgressBar RunningProgressBar;
    public Grid PageGrid;
    public Grid MainGrid;
    public HelpContentView HelpContentView;
    public InformationAlert InformationAlert;
    public QuestionAlert QuestionAlert;
    public ProgressBarOverlay ProgressBarQuizOverlay;
    public ReviewQuestion ReviewQuizQuestion;
    private readonly string Flash = "FlashcardView";
    private readonly string Practice = "PracticecardView";
    private readonly string Quiz = "QuizView";
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
    public CardView()
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
        _ = OnUpdateProgressCommandExecuted();
    }
    public void SetViewModel(CardViewModel viewModel)
    {
        ViewModel = viewModel;
        BindingContext = viewModel;
    }
    public void SetCardView(CarouselView carouselView, Label runningCount, ProgressBar runningProgressBar, Grid painGrid, Grid mainGrid) //, RefreshView refreshView)
    {
        CarouselCardView = carouselView;
        CarouselCardView.Scrolled += OnScrolled;
        RunningCount = runningCount;
        RunningProgressBar = runningProgressBar;
        PageGrid = painGrid;
        MainGrid = mainGrid;
        //CardRefreshView = refreshView;
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            CarouselCardView.IsScrollAnimated = false;
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
        Console.WriteLine("OnScrolled position = " + CarouselCardView.Position.ToString());
        if (DeviceInfo.Current.Platform == DevicePlatform.Android || DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            int position = ViewModel.CardQuestions.IndexOf(ViewModel.CurrentQuestion);
            double scrollThreshold = 0.3; // Set your desired threshold (e.g., 80%)
            double currentOffset = e.HorizontalOffset;
            double viewWidth = CarouselCardView.Width;
            if (position < 0)
            {
                if (currentOffset > viewWidth * scrollThreshold)
                {
                    // Scroll to the next item
                    position++;
                    CarouselCardView.CurrentItem = ViewModel.CardQuestions[position];
                    Console.WriteLine("Scrolled Right");
                }
                else if (currentOffset < viewWidth * (1 - scrollThreshold))
                {
                    // Scroll to the previous item
                    position--;
                    CarouselCardView.CurrentItem = ViewModel.CardQuestions[position];
                    Console.WriteLine("Scrolled Left");
                }
            }
        }
        Console.WriteLine("OnScrolled position = " + CarouselCardView.Position.ToString());
    }
    public async void OnPositionChanged(object sender, PositionChangedEventArgs e)
    {
        string CardName = ViewModel.cv.GetType().Name;
        if (CardName == Flash)
        {
            //Reset the previous card to unflipped so the cards are always answer up mdail 3-19-25
            ResetFlipp(e.PreviousPosition);
        }
        else if ((CardName == Practice || CardName == Quiz) && (DeviceInfo.Platform != DevicePlatform.WinUI))
        {
            // Find the ScrollView inside the current & previous items then scroll to the top mdail 2-27-25
            await ResetScrollToTop(e.PreviousPosition);
        }
        int position = e.CurrentPosition;
        ExplanationVisible = ViewModel.CardQuestions[position].Explanation.Length > 0;
        CitationVisible = ViewModel.CardQuestions[position].Citation.Length > 0;
        await ViewModel.ReloadCurrentItem();
    }
    private async Task ResetScrollToTop(int PreviousPosition)
    {
        var currentItemView = CarouselCardView.VisibleViews[0];
        if (currentItemView != null)
        {
            ScrollView scrollView = currentItemView.FindByName<ScrollView>("TheCardScrollView");
            await scrollView?.ScrollToAsync(0, 0, false);
        }
        if (PreviousPosition != -1 && CarouselCardView.ItemsSource != null)
        {
            if (PreviousPosition >= 0 && PreviousPosition < ViewModel.CardQuestions.Count)
            {
                var previousItem = ViewModel.CardQuestions[PreviousPosition];
                var previousView = CarouselCardView.VisibleViews.FirstOrDefault(v => v.BindingContext == previousItem);
                if (previousView != null)
                {
                    ScrollView previousScrollView = previousView.FindByName<ScrollView>("TheCardScrollView");
                    previousScrollView?.ScrollToAsync(0, 0, false);
                }
            }
        }
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
            Console.WriteLine(LOGTAG + "ExecuteRefresh");
            this.InvalidateMeasure();
            MainGrid.RowDefinitions.Clear();
            MainGrid.ColumnDefinitions.Clear();
            MainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            Grid.SetRow(CarouselCardView, 0);
            Grid.SetColumn(CarouselCardView, 0);
            //RefreshViewIsRefreshing = false;
            Console.WriteLine(LOGTAG + "ExecuteRefresh Done");
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
        if (CarouselCardView != null)
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
    //Move to the card at the given position with the animation mdail 5-14-24
    public async Task MoveToCard(int position)
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            if (position < 0 || position >= ViewModel.CardQuestions.Count)
                return;
            if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
            {
                await WindowsScollAnimation(position);
                await Task.Delay(300);
            }
            else
            {
                CarouselCardView.ScrollTo(position, position: ScrollToPosition.Center, animate: true);
                Console.WriteLine("Scrolled to " + CarouselCardView.CurrentItem);
            }
        });
    }
    public async Task WindowsScollAnimation(int position)
    {
        int previousPosition = ViewModel.CardQuestions.IndexOf(ViewModel.CurrentQuestion);
        await MainThread.InvokeOnMainThreadAsync(async () =>        {
            // Determine the direction of the animation
            double translationDirection = previousPosition < position ? -CarouselCardView.Width : CarouselCardView.Width;
            // Animate the translation
            await CarouselCardView.TranslateTo(translationDirection, 0, 250, Easing.SinOut);
            // Update the current question
            ViewModel.CurrentQuestion = ViewModel.CardQuestions[position];
            // Reset the translation to its original position
            CarouselCardView.TranslationX = 0;
            // Optionally, animate the translation back to the original position
            await CarouselCardView.TranslateTo(0, 0, 250, Easing.SinOut);
            // Wait for a short duration
            await Task.Delay(400);
        });
    }
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
                await VDCAMessages.ShowToast(CarouselCardView, Constants.LastCard);
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
                await VDCAMessages.ShowToast(CarouselCardView, Constants.FirstCard);
            }
        });
    }
    private async Task OnRandomCommandExecuted()
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            int currentPosition = ViewModel.CurrentIndex;
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
            // Shrink the CarouselFlashcardCardView to 0.5 for WinUI & MacCatalyst and 0.8 on others mdail 6-13-24
            await CarouselCardView.ScaleTo(DeviceInfo.Current.Platform == DevicePlatform.WinUI ||
                  DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst ? 0.5 : 0.8, 200, Easing.Linear);
            // Update the current question to the new position
            if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
            { 
                CarouselCardView.IsScrollAnimated = false;
                ViewModel.CurrentQuestion = ViewModel.CardQuestions[position];
                CarouselCardView.IsScrollAnimated = false;
                await Task.Delay(400);
            }
            else
            {
                CarouselCardView.IsScrollAnimated = false;
                ViewModel.CurrentQuestion = ViewModel.CardQuestions[position];
                CarouselCardView.IsScrollAnimated = true;
            }
            // Grow the CarouselFlashcardCardView back to its original size
            await CarouselCardView.ScaleTo(1, 200, Easing.Linear);
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
    //Fix the CarouselView losing the position on screen rotation mdail 3-6-25
    private void OnSizeChanged(object sender, EventArgs e)
    {
        Orientation = DeviceDisplay.MainDisplayInfo.Orientation;
        if (PreviousOrientation != Orientation)
            MainThread.InvokeOnMainThreadAsync(async () =>
            {
                int position = CarouselCardView.Position;
                if (DeviceInfo.Current.Platform == DevicePlatform.Android)
                {
                    // Force the CarouselView to recreate its views
                    var itemsSource = CarouselCardView.ItemsSource;
                    CarouselCardView.ItemsSource = null;
                    CarouselCardView.ItemsSource = itemsSource;
                }
                await ResetScrollToTop(position);
                //This tiny delay ensure the CarouselView will scroll to the position requested mdail 3-6-25
                await Task.Delay(1);
                CarouselCardView.ScrollTo(position, position: ScrollToPosition.Center, animate: false);
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
    }
}