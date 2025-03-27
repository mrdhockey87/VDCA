using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using VDCA.Models;

namespace VDCA.Views;

public partial class ReviewQuestion : ContentView
{
    
    private TaskCompletionSource<bool> _tcs;
    public Task<bool> WaitForUserResponseAsync()
    {
        return _tcs.Task;
    }
    public ReviewQuestion()
	{
		InitializeComponent();
        _tcs = new TaskCompletionSource<bool>();
       // maybe not it is bound to the view model BindingContext = this;
        if(Constants.LastQuiz is not null)
        {
            _ = SetLastQuiz(Constants.LastQuiz);
        }
    }
    public async Task SetLastQuiz(ReviewQuiz ThisQuiz)
    {
        if (ThisQuiz is not null)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                rqvm.Review_Correct = ThisQuiz.Correct;
                rqvm.Review_Date = ThisQuiz.DateTaken;
                rqvm.Review_Time = ThisQuiz.TimeOfDay;
                rqvm.Review_TotalTime = ThisQuiz.TotalTime;
                rqvm.Review_Total = ThisQuiz.TotalQuestions;
                rqvm.Review_DateTime = ThisQuiz.DateTimeTaken;
            });
        }
    }
    private async Task Reset()
    {
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            _tcs = new TaskCompletionSource<bool>();
        });
    }
    private async void OnExit_Clicked(object sender, EventArgs args)
    {
        Constants.REVIEW = false;
        try
        {
            _tcs.TrySetResult(false);
            await Reset();
            this.IsVisible = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(this.GetType().Name + " - removed pop-up _ReviewQuizPopup error: " + ex.Message);
        }
    }
    private async void ReviewQuizPopup_Clicked(object sender, EventArgs e)
    {
        Constants.REVIEW = true;
        try
        {
            _tcs.TrySetResult(true);
            await Reset();
            this.IsVisible = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(this.GetType().Name + " - removed pop-up _ReviewQuizPopup error: " + ex.Message);
        }
    }

}