using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System;
using System.Linq;
using System.Threading.Tasks;
using VDCA.Converters;
using VDCA.CustomControl;

namespace VDCA.Views;

public partial class WindowsPracticecardView : WindowsCardView
{
    private static readonly string LOGTAG = "PracticecardView";

    public WindowsPracticecardView()
    {
        WidthConverter wc = new();
        InitializeComponent();
        Title = Constants.PracticeTitle;
        SetViewModel(winpcvm);
        SetCardView(CarouselPracticecardView, running_count, count_progress, pageGrid, mainGrid);
        winpcvm.SetView(this);
        SetContextViews(helpContentPracticeOverlay, informationPracticeAlert, questionPracticeAlert);
        winpcvm.CurrentQuestion = winpcvm.CardQuestions.FirstOrDefault();
        Orientation = DeviceDisplay.Current.MainDisplayInfo.Orientation;
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior
        {
            IsVisible = true,
            IconOverride = "chevron_left_white.png",
            Command = new Command(async () => await OnBackButtonClicked())
        });
        _ = winpcvm.ReloadCurrentItem();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (OnboardingSupportHelp.SupportReguiredThisPage(SupportedPages.PracticecardView))
        {
            //this is where to put the code to show the onboarding support overlays for the main page mdail 10-29-24
        }
    }
    private async void OnTap_Practice(object sender, TappedEventArgs args)
    {
        // Only run the code if the question has not been answered
        if (!winpcvm.CardQuestions[ViewModel.CardQuestions.IndexOf(ViewModel.CurrentQuestion)].Locked)
        {
            TappedEventArgs tapEventArgs = (TappedEventArgs)args;
            await AnswerPressed((string)tapEventArgs.Parameter);
        }
    }
    public async Task AnswerPressed(string SelectedButton)
    {
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            int position = ViewModel.CardQuestions.IndexOf(ViewModel.CurrentQuestion);
            if (!winpcvm.CardQuestions[position].Locked)
            {
                switch (SelectedButton)
                {
                    case "AnswerA":
                        winpcvm.CardQuestions[position].ASelected = true;
                        break;
                    case "AnswerB":
                        winpcvm.CardQuestions[position].BSelected = true;
                        break;
                    case "AnswerC":
                        winpcvm.CardQuestions[position].CSelected = true;
                        break;
                    case "AnswerD":
                        winpcvm.CardQuestions[position].DSelected = true;
                        break;
                    default:
                        break;
                }
                winpcvm.CardQuestions[position].Locked = true;
            }
        });
    }
    private async Task OnBackButtonClicked()
    {
        // Await the user's response
        bool answer = await ShowQuestionAlertAsync(Constants.ExitPracticeTitle, Constants.ExitPracticeMessage);
        if (answer)
        {
            await Shell.Current.GoToAsync(".."); ;
        }
    }
    protected override bool OnBackButtonPressed()
    {
        MainThread.InvokeOnMainThreadAsync(async () =>
        {
            try
            {
                // Await the user's response
                bool answer = await ShowQuestionAlertAsync(Constants.ExitPracticeTitle, Constants.ExitPracticeMessage);
                if (answer)
                {
                    await Shell.Current.GoToAsync(".."); ;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(LOGTAG + $"Navigation error: {ex.Message}");
            }
        });
        return true;
    }
}