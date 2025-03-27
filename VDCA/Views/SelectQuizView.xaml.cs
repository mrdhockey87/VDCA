using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using VDCA.Data;
using VDCA.Utils;

namespace VDCA.Views;

public partial class SelectQuizView : ContentPage
{
    //private readonly string LOGTAG = "SelectQuizView";
    public ProgressBarOverlay ProgressBarSelectQuizOverlay;
    public SelectQuizView()
	{
        Constants.REVIEW = false;
        InitializeComponent();
        ProgressBarSelectQuizOverlay = progressSeleectQuizBarOverlay;
        Title = "Select Quiz Length";
        QuestionsDatabase qd = new();
        int Total = qd.QuestionCount();
        int Short = Total / 3;
        int Medium = Short * 2;
        btn_short.Text = "Short Quiz" + Constants.NEWLINE + "Approximately " + Short + " Questions" + Constants.NEWLINE + "Approximately 1 hour";
        btn_medium.Text = "Medium Quiz" + Constants.NEWLINE + "Approximately " + Medium + " Questions" + Constants.NEWLINE + "Approximately 2 hours";
        btn_total.Text = "Long Quiz" + Constants.NEWLINE + "Approximately " + Total + " Questions" + Constants.NEWLINE + "Approximately 3 hour";
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior
        {
            IsVisible = true,
            IconOverride = "chevron_left_white.png",
            Command = new Command(async () => await OnBackButtonClicked())
        });
    }
    private async void QuizClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        int classId = int.Parse((string)button.ClassId);
        await ShowProgressBarUtil.ShowProgressBar(ProgressBarSelectQuizOverlay);
        QuestionsDatabase qdb = new();
        Constants.REVIEW = false;
        switch (classId)
        {
            case 1:
                await qdb.GetSelectQuiz1Questions();
                Constants.IsREVIEW = false;
                await Shell.Current.GoToAsync("Quiz");
                break;
            case 2:
                await qdb.GetSelectQuiz2Questions();
                Constants.IsREVIEW = false;
                await Shell.Current.GoToAsync("Quiz");
                break;
            case 3:
                await qdb.GetSelectQuiz3Questions();
                Constants.IsREVIEW = false;
                await Shell.Current.GoToAsync("Quiz");
                break;
            default:
                break;
        }
        await ShowProgressBarUtil.HideProgressBar(ProgressBarSelectQuizOverlay);
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