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

    /// <summary>
    /// Method to show the help and return a task that completes when the alert is hidden
    /// </summary>
    /// <param name="hideExitRow">Optional parameter to hide the exit row (default: false)</param>
    /// <returns>A task that completes when the alert is hidden</returns>
    public Task ShowAlertAsync(bool hideExitRow = false)
    {
        // Set visibility of the content
        IsVisible = true;
        // Adjust the visibility of the exit row if needed
        if (hideExitRow)
        {
            // Assuming the exit button is in a grid at row 7
            // Find the grid that contains the exit button and adjust its visibility
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
