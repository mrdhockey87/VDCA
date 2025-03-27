using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VDCA.Views;

public partial class AlertPage : ContentPage
{
    private string alertTitle;
    public string AlertTitle
    {
        get => alertTitle;
        set
        {
            alertTitle = value;
            OnPropertyChanged();
        }
    }
    private string alertMessage;
    public string AlertMessage
    {
        get => alertMessage;
        set
        {
            alertMessage = value;
            OnPropertyChanged();
        }
    }
    private TaskCompletionSource<bool> tcs;
    public ICommand DummyAlertCommand => new Command(() => { /* Does nothing */ });
    public AlertPage()
    {
        InitializeComponent();
        BindingContext = this;
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
    private async void ExitButton_Clicked(object sender, EventArgs e)
    {
        try
        {
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
}
