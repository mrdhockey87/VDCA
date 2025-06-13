using System.Windows.Input;

namespace VDCA.Views;

public partial class HelpContentView : ContentView
{
    private TaskCompletionSource<bool> tcs;
    public ICommand DummyQuestionCommand => new Command(() => { /* Does nothing */ });
    public event EventHandler ExitEventClicked;

    public HelpContentView()
    {
        InitializeComponent();
    }

    public Task ShowAlertAsync(bool HideMenuRow = false)
    {
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI || DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst)
        {
            HideMenuRow = true;
        }
        // Set visibility of the content
        IsVisible = true;
        // Adjust the visibility of the exit row if needed - above set it to true for WinUI and MacCatalyst as it is not used there
        if (HideMenuRow)
        {
            var contentGrid = this.FindByName<RowDefinition>("MenuLine");
            var menuIcon = this.FindByName<Image>("MenuIcon");
            var menuLabel = this.FindByName<Label>("MenuLabel");
            if (contentGrid != null)
            {
                contentGrid.Height = 0; // Hide the row
                menuIcon.IsVisible = false; // Hide the icon
                menuLabel.IsVisible = false; // Hide the label
            }
        }

        tcs = new TaskCompletionSource<bool>();
        return tcs.Task;
    }

    private void OnExit(object sender, EventArgs e)
    {
        // Trigger the ExitRequested event first
        ExitEventClicked?.Invoke(this, EventArgs.Empty);

        // If no subscribers (standalone use) or the subscriber didn't handle the visibility
        // then handle it here
        if (ExitEventClicked == null || IsVisible)
        {
            IsVisible = false;
            tcs?.SetResult(true);
        }
    }
}
