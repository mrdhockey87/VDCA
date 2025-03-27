using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Threading.Tasks;

namespace VDCA.CustomControl;

public partial class NavigationOnboardingOverlayControl : ContentView
{
    private int currentPage = 0;
    private TaskCompletionSource<bool> tcs;

    // Define the bindable property for totalPages
    public static readonly BindableProperty TotalPagesProperty =
        BindableProperty.Create(
            nameof(TotalPages),
            typeof(int),
            typeof(NavigationOnboardingOverlayControl),
            3, // Default value is 3
            propertyChanged: OnTotalPagesChanged);

    public int TotalPages
    {
        get => (int)GetValue(TotalPagesProperty);
        set => SetValue(TotalPagesProperty, value);
    }

    // Define the custom events
    public event EventHandler NextButtonClicked;
    public event EventHandler PreviousButtonClicked;
    public AbsoluteLayout NavigationOverlayAbsoluteLayout;
    //public OnboardingOverlayControl OnboardingOverlayControl_;
    public NavigationOnboardingOverlayControl()
    {
        InitializeComponent();
        NavigationOverlayAbsoluteLayout = navigationOverlayAbsoluteLayout;
        //OnboardingOverlayControl_ = onboardingOverlayControl;
    }

    private static void OnTotalPagesChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (NavigationOnboardingOverlayControl)bindable;
        control.GenerateDots();
        control.UpdateNavigationControls();
    }

    private void GenerateDots()
    {
        DotsIndicator.Children.Clear();
        if (TotalPages > 1)
        {
            for (int i = 0; i < TotalPages; i++)
            {
                DotsIndicator.Children.Add(new Label
                {
                    Text = "●",
                    FontSize = 24,
                    TextColor = i == currentPage ? (Color)Application.Current.Resources["PrimaryBackgroundColor"] : (Color)Application.Current.Resources["PrimaryListDescriptionText"]
                });
            }
            DotsIndicator.IsVisible = true;
        }
        else
        {
            DotsIndicator.IsVisible = false;
        }
    }

    private void OnNextButtonClicked(object sender, EventArgs e)
    {
        currentPage++;
        if (currentPage >= TotalPages)
        {
            // Hide the overlay on the last page
            this.IsVisible = false;
        }
        else
        {
            UpdateNavigationControls();
        }

        // Raise the custom event
        NextButtonClicked?.Invoke(this, EventArgs.Empty);
    }

    private void OnPreviousButtonClicked(object sender, EventArgs e)
    {
        currentPage--;
        UpdateNavigationControls();

        // Raise the custom event
        PreviousButtonClicked?.Invoke(this, EventArgs.Empty);
    }

    private void UpdateNavigationControls()
    {
        GenerateDots();

        // Change the button image source based on the current page
        NextButton.Source = currentPage == TotalPages - 1 || TotalPages == 1 ? "gotit_navy.png" : "next_navy.png";

        // Show or hide the Previous button
        PreviousButton.IsVisible = currentPage > 0 && TotalPages > 1;
    }

    public Task ShowOverlayAsync()
    {
        this.IsVisible = true;
        tcs = new TaskCompletionSource<bool>();
        return tcs.Task;
    }

    public void ShowOverlay()
    {
        this.IsVisible = true;
    }

    public void HideOverlay()
    {
        this.IsVisible = false;
        if (tcs != null && !tcs.Task.IsCompleted)
        {
            tcs.SetResult(true);
        }
    }
}
