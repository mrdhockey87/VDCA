using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System;
using System.Linq;
using System.Threading.Tasks;
using VDCA.Converters;
using VDCA.CustomControl;

namespace VDCA.Views;

public partial class QuizView : CardView
{
    //private static readonly string LOGTAG = "QuizView";
    public Label ElapsedTimeLabel;
    public Button ScoreQuiz;
    public bool ReviewQuiz = false;

    public QuizView()
    {
        ReviewQuiz = Constants.IsREVIEW;
        _ = new WidthConverter();
        InitializeComponent();
        SetViewModel(qvm);
        SetCardView(CarouselQuizView, running_count, count_progress, pageGrid, mainGrid);
        qvm.SetView(this);
        SetContextViews(helpContentQuizOverlay, informationQuizAlert, questionQuizAlert, progressBarQuizOverlay, reviewQuizQuestion);
        Orientation = DeviceDisplay.Current.MainDisplayInfo.Orientation;
        // Set the title based on the state of ReviewQuiz
        Title = ReviewQuiz ? Constants.ReviewQuizTitle : Constants.QuizTitle;
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior
        {
            IsVisible = true,
            IconOverride = "chevron_left_white.png",
            Command = new Command(async () => await OnBackButtonClicked())
        });
        ScoreQuiz = Button_Score_Quiz;
        qvm.CurrentQuestion = qvm.CardQuestions.FirstOrDefault();
        //Required to display the selected answer information for the first question mdaia 6-7-24
        _ = qvm.ReloadCurrentItem();
        ElapsedTimeLabel = elapsedTimeLabel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (OnboardingSupportHelp.SupportReguiredThisPage(SupportedPages.QuizView))
        {
            //this is where to put the code to show the onboarding support overlays for the main page mdail 10-29-24
        }
    }
    private void CarouselViewLoaded(object sender, EventArgs e)
    {
        MainThread.InvokeOnMainThreadAsync(async () =>
        {
            if (ReviewQuiz || Constants.REVIEW)
            {
                if (qvm.CardQuestions.Count > 0)
                {
                    await qvm.ReloadCurrentItem();
                }
            }
        });
    }
    public async Task<bool> ShowReviewQuestionAsync()
    {
        if (Constants.LastQuiz is not null)
        {
            await ReviewQuizQuestion.SetLastQuiz(Constants.LastQuiz);
            ReviewQuizQuestion.IsVisible = true;
            bool result = await ReviewQuizQuestion.WaitForUserResponseAsync();
            return result;
        }
        return false;
    }
    private async void OnTap_Quiz(object sender, TappedEventArgs args)
    {
        int position = CarouselCardView.Position;
        if (!qvm.CardQuestions[position].Locked)
        {
            TappedEventArgs tapEventArgs = (TappedEventArgs)args;
            string answer = (string)tapEventArgs.Parameter;
            await qvm.AnswerPressed(answer);
        }
    }
    private async void Score_Quiz(object sender, EventArgs e)
    {
        if (ReviewQuiz || Constants.REVIEW)
        {
            ScoreQuiz.IsEnabled = false;
            return;
        }
        await qvm.ScoreQuizPressed();
    }
    protected override bool OnBackButtonPressed()
    {
        MainThread.InvokeOnMainThreadAsync(async () =>
        {
            if (!Constants.REVIEW)
            {
                // Await the user's response
                bool answer = await ShowQuestionAlertAsync(Constants.ExitQuizTitle, Constants.ExitQuizMessage);
                if (answer)
                {
                    Constants.FROM_QUIZ = false;
                    await Shell.Current.GoToAsync(".."); ;
                }
            }
            else
            {
                Constants.FROM_QUIZ = false;
                {
                    await Shell.Current.GoToAsync("..");
                }
            }
        });
        return true;
    }
    public async Task OnBackButtonClicked()
    {
        if (!Constants.REVIEW)
        {
            bool answer = await ShowQuestionAlertAsync(Constants.ExitQuizTitle, Constants.ExitQuizMessage);
            if (answer)
            {
                Constants.FROM_QUIZ = false;
                await Shell.Current.GoToAsync(".."); ;
            }
        }
        else
        {
            Constants.FROM_QUIZ = false;
            await Shell.Current.GoToAsync("..");

        }
    }
}