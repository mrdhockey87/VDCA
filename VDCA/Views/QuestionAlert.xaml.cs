using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using VDCA.Utils;

namespace VDCA.Views;

public partial class QuestionAlert : ContentView
{
    private TaskCompletionSource<bool> _tcs;
    public ICommand DummyQuestionCommand => new Command(() => { /* Does nothing */ });

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
            Alert_Message = value;
            OnPropertyChanged();
        }
    }
    public QuestionAlert()
    {
        InitializeComponent();
        _tcs = new TaskCompletionSource<bool>();
    }


    public Task<bool> WaitForUserResponseAsync()
    {
        return _tcs.Task;
    }


    private void YesButton_Clicked(object sender, EventArgs e)
    {
        _tcs.TrySetResult(true);
        Reset();
    }


    private void Reset()
    {
        _tcs = new TaskCompletionSource<bool>();
    }
    private void NoButton_Clicked(object sender, EventArgs e)
    {
        _tcs.TrySetResult(false);
        Reset();
    }
}
