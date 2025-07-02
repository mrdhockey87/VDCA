using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System;
using System.Linq;
using System.Threading.Tasks;
using VDCA.CustomControl;

namespace VDCA.Views;

public partial class FlashcardView : CardView
{
    private static readonly string LOGTAG = "FlashcardView";
    public Border TheCard;
    public ImageButton _random;
    public FlashcardView() 
    {
        InitializeComponent();
        SetViewModel(fcvm);
        SetCardView(CarouselFlashcardCardView, running_count, count_progress, pageGrid, mainGrid);
        fcvm.SetView(this);
        SetContextViews(helpContentFlashcardOverlay, informationFlashcardAlert, questionFlashcardAlert);
        fcvm.CurrentQuestion = fcvm.CardQuestions.FirstOrDefault();
        Orientation = DeviceDisplay.Current.MainDisplayInfo.Orientation;
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior
        {
            IsVisible = true,
            IconOverride = "chevron_left_white.png",
            Command = new Command(async () => await OnBackButtonClicked())
        });
        _ = fcvm.ReloadCurrentItem();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (OnboardingSupportHelp.SupportReguiredThisPage(SupportedPages.FlashcardView))
        {
            //this is where to put the code to show the onboarding support overlays for the main page mdail 10-29-24
        }
    }
    //toggles the filliped boolean of the current card and flips the entire carousel view to make the flip animation mdail 8-2-21 
    private async void Flip_Clicked(object sender, EventArgs e)
    {
        await FlipCardAnimation();
    }
    private async Task FlipCardAnimation()
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
            {
                // Original animation for other platforms
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    int position = CarouselCardView.SelectedIndex;
                    // Fade out the front side
                    await CarouselCardView.FadeTo(0, 150, Easing.Linear);
                    // Change the side of the card that's visible
                    fcvm.CardQuestions[position].Flipped = !fcvm.CardQuestions[position].Flipped;
                    // Fade in the back side
                    await CarouselCardView.FadeTo(1, 150, Easing.Linear);
                });
            }
            else
            {
                // Original animation for other platforms
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    var scaleDownTask = CarouselCardView.ScaleTo(0.8, 150, Easing.Linear);
                    var rotateHalfwayTask = CarouselCardView.RotateYTo(-90, 150, Easing.Linear);
                    await Task.WhenAll(scaleDownTask, rotateHalfwayTask);
                    int position = CarouselCardView.SelectedIndex;
                    fcvm.CardQuestions[position].Flipped = !fcvm.CardQuestions[position].Flipped;
                    CarouselCardView.RotationY = 90;
                    var scaleUpTask = CarouselCardView.ScaleTo(1, 150, Easing.Linear);
                    var rotateFinishTask = CarouselCardView.RotateYTo(0, 150, Easing.Linear);
                    await Task.WhenAll(scaleUpTask, rotateFinishTask);
                });
            }
        });
    }

    private async Task OnBackButtonClicked()
    {
        // Await the user's response
        bool answer = await ShowQuestionAlertAsync(Constants.ExitFlashcardsTitle, Constants.ExitFlashcardsMessage);
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
                bool answer = await ShowQuestionAlertAsync(Constants.ExitFlashcardsTitle, Constants.ExitFlashcardsMessage);
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