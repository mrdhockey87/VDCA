
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace VDCA.Views;

public partial class ReviewQuizzesView : ContentPage
{
    public Button LastReviewQuestions;
    public Grid ReviewGrid;
    public ProgressBarOverlay ProgressBarReiewOverlay;
    public InformationAlert InformationAlert;

    public ReviewQuizzesView()
    {
        InitializeComponent();
        InformationAlert = informationReviewAlert;
        ProgressBarReiewOverlay = progressBarReviewOverlay;
        SetupPage();
        rqvm.SetView(this);
        SetButtonVisibility();
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior
        {
            IsVisible = true,
            IconOverride = "chevron_left_white.png",
            Command = new Command(async () => await OnBackButtonClicked())
        });
    }
    private void SetupPage()
    {
        LastReviewQuestions = last_quiz_qusetion_review;
        ReviewGrid = last_quiz_background;
        last_quiz_background.BackgroundColor = rqvm.LastReviewQuiz.ResultColor;
        last_quiz_percent.Text = rqvm.LastReviewQuiz.StringPercent;
        last_quiz_date.Text = rqvm.LastReviewQuiz.DateTaken;
        last_quiz_time.Text = rqvm.LastReviewQuiz.TimeOfDay;
        last_quiz_totaltime.Text = rqvm.LastReviewQuiz.TotalTime;
        last_quiz_number_correct.Text = rqvm.LastReviewQuiz.CorrectOfTotal;
    }

    public void ShowInformationAlert(string title, string message)
    {
        InformationAlert.ShowAlert(title, message);
    }

    private void SetButtonVisibility()
    {
        //if ReviewQuestions is null or if the count is equals 0 set the LastReviewQuestions button to be not visible, also  mdail 8-2-23
        if (rqvm.ReviewQuestions is not null)
        {
            if (rqvm.ReviewQuestions.Count == 0)
            {
                LastReviewQuestions.IsVisible = false;
            }
        }
        else
        {
            LastReviewQuestions.IsVisible = false;
        }
    }

    async void OnReviewQuestion_Clicked(object sender, EventArgs e)
    {
        await rqvm.ReviewQuestionClicked();
    }
    private static async Task OnBackButtonClicked()
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
    protected override bool OnBackButtonPressed()
    {
        Shell.Current.GoToAsync("//MainPage");
        return true;
    }
}
