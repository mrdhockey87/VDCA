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
    // Method to show the help and return a task that completes when the alert is hidden
    public Task ShowAlertAsync()
    {
        IsVisible = true;
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