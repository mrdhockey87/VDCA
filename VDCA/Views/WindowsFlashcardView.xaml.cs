using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System;
using System.Linq;
using System.Threading.Tasks;
using VDCA.CustomControl;

namespace VDCA.Views;

public partial class WindowsFlashcardView : WindowsCardView
{
    private static readonly string LOGTAG = "FlashcardView";
    public Border TheCard;
    public ImageButton _random;
    public WindowsFlashcardView() 
    {
        InitializeComponent();
        SetViewModel(winfcvm);
        SetCardView(CarouselFlashcardCardView, running_count, count_progress, pageGrid, mainGrid);
        winfcvm.SetView(this);
        SetContextViews(helpContentFlashcardOverlay, informationFlashcardAlert, questionFlashcardAlert);
        winfcvm.CurrentQuestion = winfcvm.CardQuestions.FirstOrDefault();
        Orientation = DeviceDisplay.Current.MainDisplayInfo.Orientation;
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior
        {
            IsVisible = true,
            IconOverride = "chevron_left_white.png",
            Command = new Command(async () => await OnBackButtonClicked())
        });
        _ = winfcvm.ReloadCurrentItem();
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
                    int position = ViewModel.CardQuestions.IndexOf(ViewModel.CurrentQuestion);
                    // Fade out the front side
                    await CollectionCardView.FadeTo(0, 150, Easing.Linear);
                    // Change the side of the card that's visible
                    winfcvm.CardQuestions[position].Flipped = !winfcvm.CardQuestions[position].Flipped;
                    // Fade in the back side
                    await CollectionCardView.FadeTo(1, 150, Easing.Linear);
                });
            }
            else
            {
                // Original animation for other platforms
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    var scaleDownTask = CollectionCardView.ScaleTo(0.8, 150, Easing.Linear);
                    var rotateHalfwayTask = CollectionCardView.RotateYTo(-90, 150, Easing.Linear);
                    await Task.WhenAll(scaleDownTask, rotateHalfwayTask);
                    int position = ViewModel.CardQuestions.IndexOf(ViewModel.CurrentQuestion);
                    winfcvm.CardQuestions[position].Flipped = !winfcvm.CardQuestions[position].Flipped;
                    CollectionCardView.RotationY = 90;
                    var scaleUpTask = CollectionCardView.ScaleTo(1, 150, Easing.Linear);
                    var rotateFinishTask = CollectionCardView.RotateYTo(0, 150, Easing.Linear);
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