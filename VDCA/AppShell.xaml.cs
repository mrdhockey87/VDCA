using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System;
using System.Linq;
using System.Threading.Tasks;
using VDCA.Data;
using VDCA.SendEmails;
using VDCA.Views;
namespace VDCA;

public partial class AppShell : Shell
{
    private MainPage mainPage;
    private bool isNavigating = false; // Flag to track navigation

    public AppShell()
    {
        InitializeComponent();
        AppShell.RegisterRoutes();
        BindingContext = this;
        this.Navigated += OnNavigated;
    }

    private static void RegisterRoutes()
    {
        Routing.RegisterRoute("Flashcard", typeof(FlashcardView));
        Routing.RegisterRoute("Practice", typeof(PracticecardView));
        Routing.RegisterRoute("Quiz", typeof(QuizView));
        Routing.RegisterRoute("SelectFlash", typeof(SelectFlashView));
        Routing.RegisterRoute("SelectPractice", typeof(SelectPracticeView));
        Routing.RegisterRoute("SelectQuiz", typeof(SelectQuizView));
        Routing.RegisterRoute("ReviewQuizzes", typeof(ReviewQuizzesView));
    }

    private void OnNavigated(object sender, ShellNavigatedEventArgs e)
    {
        // Change the FlyoutBehavior depending on the current route
        string route = e.Current?.Location.ToString();
        if (route == @"//MainPage")
        {
            // Show the hamburger menu on MainPage
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            mainPage = Shell.Current.CurrentPage as MainPage;
        }
        else
        {
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
        }
    }
    private async Task OnMenuItemClicked(string route)
    {
        if (isNavigating) return; // Prevent multiple clicks
        isNavigating = true;
        Shell.Current.FlyoutIsPresented = false;
        await mainPage?.ShowProgressBar();
        await Shell.Current.GoToAsync(route);
        mainPage?.HideProgressBar();
        isNavigating = false;
    }

    private async void OnFlashcardsClicked(object sender, EventArgs e)
    {
        await OnMenuItemClicked("SelectFlash");
    }

    private async void OnPracticeClicked(object sender, EventArgs e)
    {
        await OnMenuItemClicked("SelectPractice");
    }

    private async void OnSelectQuizzesClicked(object sender, EventArgs e)
    {
        await OnMenuItemClicked("SelectQuiz");
    }

    private async void OnReviewQuizzesClicked(object sender, EventArgs e)
    {
        ReviewDatabase rd = new();
        await rd.GetReviewQuizzes();
        await OnMenuItemClicked("ReviewQuizzes");
    }

    private async void OnFlaggedOnlyClicked(object sender, EventArgs e)
    {
        if (isNavigating) return; // Prevent multiple clicks
        isNavigating = true;

        var menuItem = sender as MenuItem;
        menuItem.IsEnabled = false;
        Shell.Current.FlyoutIsPresented = false;
        await mainPage?.ShowProgressBar();
        QuestionsDatabase db = new();
        await db.GetFlaggedQuestions();
        int count = Constants.Questions.Count;
        if (count > 0)
        {
            await Shell.Current.GoToAsync("Flashcard");
        }
        else
        {
            var alertPage = new AlertPage();
            await AppShell.ShowCustomModalAsync(alertPage);
            await alertPage.ShowAlertAsync("No Flagged Questions!", "No flagged questions were found!");
        }
        mainPage?.HideProgressBar();
        menuItem.IsEnabled = true;

        isNavigating = false;
    }

    private async void OnHiddenOnlyClicked(object sender, EventArgs e)
    {
        if (isNavigating) return; // Prevent multiple clicks
        isNavigating = true;

        var menuItem = sender as MenuItem;
        menuItem.IsEnabled = false;
        Shell.Current.FlyoutIsPresented = false;
        await mainPage?.ShowProgressBar();
        QuestionsDatabase db = new();
        await db.GetHiddenQuestions();
        int count = Constants.Questions.Count;
        if (count > 0)
        {
            await Shell.Current.GoToAsync("Flashcard");
        }
        else
        {
            var alertPage = new AlertPage();
            await AppShell.ShowCustomModalAsync(alertPage);
            await alertPage.ShowAlertAsync("No Hidden Questions!", "No hidden questions were found!");
        }
        mainPage?.HideProgressBar();
        menuItem.IsEnabled = true;

        isNavigating = false;
    }

    private async void OnClearFlaggedClicked(object sender, EventArgs e)
    {
        if (isNavigating) return; // Prevent multiple clicks
        isNavigating = true;
        var menuItem = sender as MenuItem;
        menuItem.IsEnabled = false;
        Shell.Current.FlyoutIsPresented = false;
        await mainPage?.ShowProgressBar();
        QuestionsDatabase db = new();
        db.ClearFlaggedQuestions();
        var alertPage = new AlertPage();
        await AppShell.ShowCustomModalAsync(alertPage);
        await alertPage.ShowAlertAsync("Flagged Questions Cleared!", "The flagged questions have been cleared!");
        mainPage?.HideProgressBar();
        menuItem.IsEnabled = true;
        isNavigating = false;
    }

    private async void OnClearHiddenClicked(object sender, EventArgs e)
    {
        if (isNavigating) return; // Prevent multiple clicks
        isNavigating = true;
        var menuItem = sender as MenuItem;
        menuItem.IsEnabled = false;
        Shell.Current.FlyoutIsPresented = false;
        await mainPage?.ShowProgressBar();
        QuestionsDatabase db = new();
        db.ClearHiddenQuestions();
        var alertPage = new AlertPage();
        await AppShell.ShowCustomModalAsync(alertPage);
        await alertPage.ShowAlertAsync("Hidden Questions Cleared!", "The hidden questions have been cleared!");
        mainPage?.HideProgressBar();
        menuItem.IsEnabled = true;
        isNavigating = false;
    }

    private async void OnClearReviewClicked(object sender, EventArgs e)
    {
        if (isNavigating) return; // Prevent multiple clicks
        isNavigating = true;
        var menuItem = sender as MenuItem;
        menuItem.IsEnabled = false;
        Shell.Current.FlyoutIsPresented = false;
        await mainPage?.ShowProgressBar();
        ReviewDatabase rdb = new();
        await rdb.ClearAllReviewQuestions();
        await rdb.ClearAllReview();
        await rdb.GetReviewQuizzes();
        var alertPage = new AlertPage();
        await AppShell.ShowCustomModalAsync(alertPage);
        await alertPage.ShowAlertAsync("Review Has Been Cleared!", "The past quizzes have been cleared!");
        mainPage?.HideProgressBar();
        menuItem.IsEnabled = true;
        isNavigating = false;
    }

    private async void OnSendFeedbackClicked(object sender, EventArgs e)
    {
        if (isNavigating) return; // Prevent multiple clicks
        isNavigating = true;
        var menuItem = sender as MenuItem;
        menuItem.IsEnabled = false;
        Shell.Current.FlyoutIsPresented = false;
        await mainPage?.ShowProgressBar();
        SendFeedbackEmails sfe = new();
        await sfe.SendAppFeedback();
        mainPage?.HideProgressBar();
        menuItem.IsEnabled = true;
        isNavigating = false;
    }

    private async void OnRateAppClicked(object sender, EventArgs e)
    {
        if (isNavigating) return; // Prevent multiple clicks
        isNavigating = true;
        var menuItem = sender as MenuItem;
        menuItem.IsEnabled = false;
        Shell.Current.FlyoutIsPresented = false;
        await mainPage?.ShowProgressBar();
        // Implement your logic to rate the app here
        // For example, navigate to a rating page or open the app store

        var alertPage = new AlertPage();
        await AppShell.ShowCustomModalAsync(alertPage);
        await alertPage.ShowAlertAsync("Rated App!", "Thank you for rating our app!");
        mainPage?.HideProgressBar();
        menuItem.IsEnabled = true;
        isNavigating = false;
    }

    private static async Task ShowCustomModalAsync(Page page)
    {
        // Check if there are any pages in the navigation stack
        var navigationStack = Shell.Current.Navigation.NavigationStack;
        if (navigationStack.Count > 0)
        {
            // Push the modal page without animation
            await navigationStack[navigationStack.Count - 1].Navigation.PushModalAsync(page, false);
        }
        else
        {
            // If the navigation stack is empty, push the modal page directly
            await Shell.Current.Navigation.PushModalAsync(page, false);
        }
    }
}
