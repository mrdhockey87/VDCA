using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VDCA.Views;

public partial class InformationAlert : ContentView
{
    private string Alert_Title;
    public string AlertTitle
    {
        get => Alert_Title;
        set
        {
            Alert_Title = value;
            OnPropertyChanged();
        }
    }

    private string Alert_Message;
    public string AlertMessage
    {
        get => Alert_Message;
        set
        {
            Alert_Message = InformationAlert.CapitalizeFirstLetter(value);
            OnPropertyChanged();
        }
    }

    private TaskCompletionSource<bool> tcs; 
    public static ICommand DummyInformationCommand { set; get; }
    public InformationAlert()
    {
        InitializeComponent();
        BindingContext = this;
        InformationAlert.DummyInformationCommand = new Command(InformationAlert.DummyInformationCommand_Executed);
    }

    public static void DummyInformationCommand_Executed()
    {
        // This command is used to handle the selection change event in the CollectionView
        // It is intentionally left empty as it is not used in this context
    }

    // Method to show the alert and return a task that completes when the alert is hidden
    public Task ShowAlertAsync(string title, string message)
    {
        AlertTitle = title;
        AlertMessage = message;
        IsVisible = true;
        tcs = new TaskCompletionSource<bool>();
        return tcs.Task;
    }

    // Method to show the alert without waiting
    public void ShowAlert(string title, string message)
    {
        AlertTitle = title;
        AlertMessage = message;
        IsVisible = true;
    }

    private void ExitButton_Clicked(object sender, EventArgs e)
    {
        this.IsVisible = false;
    }
    // Helper method to capitalize the first letter of a string
    private static string CapitalizeFirstLetter(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return char.ToUpper(input[0]) + input[1..];
    }
}
