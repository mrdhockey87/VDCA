using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System;
using System.Linq;
using System.Threading.Tasks;
using VDCA.Converters;
using VDCA.CustomControl;

namespace VDCA.Views;

public partial class PracticecardView : CardView
{
    private static readonly string LOGTAG = "PracticecardView";

    public PracticecardView()
    {
        WidthConverter wc = new();
        InitializeComponent();
        Title = Constants.PracticeTitle;
        SetViewModel(pcvm);
        SetCardView(CarouselPracticecardView, running_count, count_progress, pageGrid, mainGrid);
        pcvm.SetView(this);
        SetContextViews(informationPracticeAlert, questionPracticeAlert);
        pcvm.CurrentQuestion = pcvm.CardQuestions.FirstOrDefault();
        Orientation = DeviceDisplay.Current.MainDisplayInfo.Orientation;
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior
        {
            IsVisible = true,
            IconOverride = "chevron_left_white.png",
            Command = new Command(async () => await OnBackButtonClicked())
        });
        _ = pcvm.ReloadCurrentItem();
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
        if (!pcvm.CardQuestions[CarouselCardView.Position].Locked)
        {
            TappedEventArgs tapEventArgs = (TappedEventArgs)args;
            await AnswerPressed((string)tapEventArgs.Parameter);
        }
    }
    public async Task AnswerPressed(string SelectedButton)
    {
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            int position = CarouselCardView.Position;
            if (!pcvm.CardQuestions[position].Locked)
            {
                switch (SelectedButton)
                {
                    case "AnswerA":
                        pcvm.CardQuestions[position].ASelected = true;
                        break;
                    case "AnswerB":
                        pcvm.CardQuestions[position].BSelected = true;
                        break;
                    case "AnswerC":
                        pcvm.CardQuestions[position].CSelected = true;
                        break;
                    case "AnswerD":
                        pcvm.CardQuestions[position].DSelected = true;
                        break;
                    default:
                        break;
                }
                pcvm.CardQuestions[position].Locked = true;
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