using System.Windows.Input;

namespace VDCA.Views;

public partial class MainHelp : ContentView
{
    private TaskCompletionSource<bool> tcs;
    public ICommand DummyQuestionCommand => new Command(() => { /* Does nothing */ });
    public MainHelp()
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
        IsVisible = false;
    }
}