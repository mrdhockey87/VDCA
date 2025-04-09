namespace VDCA.Views;

public partial class HelpPage : ContentPage
{
    private TaskCompletionSource<bool> tcs;
    private HelpContentView helpContentView;
    public HelpPage()
    {
        InitializeComponent();
        helpContentView = new HelpContentView
        {
            IsVisible = true     
        };
        // Subscribe to the ExitRequested event
        helpContentView.ExitEventClicked += OnHelpContentViewExitRequested;
        Content = helpContentView;
    }
    private async void OnHelpContentViewExitRequested(object sender, EventArgs e)
    {

        try
        {
            helpContentView.IsVisible = false;
            IsVisible = false;
            tcs?.SetResult(true);
            await Shell.Current.Navigation.PopModalAsync(); // Close the modal
            Shell.Current.FlyoutIsPresented = false; // Close the flyout menu
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{GetType().Name} - removed error: {ex.Message}");
        }
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Custom animation to slide in from the left
        this.TranslationX = -this.Width;
        await this.TranslateTo(0, 0, 250, Easing.SinIn);
    }
    protected override async void OnDisappearing()
    {
        base.OnDisappearing();
        // Custom animation to slide out to the left
        await this.TranslateTo(-this.Width, 0, 250, Easing.SinIn);
    }
    // Method to show the alert and return a task that completes when the alert is hidden
    public Task ShowAlertAsync()
    {
        IsVisible = true;
        tcs = new TaskCompletionSource<bool>();
        return tcs.Task;
    }
}