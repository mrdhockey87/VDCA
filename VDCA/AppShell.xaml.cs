using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using VDCA.Data;
using VDCA.SendEmails;
using VDCA.Services;
using VDCA.Views;
#if MACCATALYST
using VDCA.Platforms.MacCatalyst;
#endif
namespace VDCA;

public partial class AppShell : Shell
{
    private bool isNavigating = false; // Flag to track navigation
    //MenuBar commands for Win & Mac platforms mdail 6-10-25
    public ICommand MenuBarHome { get; private set; }
    public ICommand MenuBarExit { get; private set; }
    public ICommand MenuBarFlashcards { get; private set; }
    public ICommand MenuBarPractice { get; private set; }
    public ICommand MenuBarQuiz { get; private set; }
    public ICommand MenuBarReview { get; private set; } 
    public ICommand MenuBarFlagged { get; private set; }
    public ICommand MenuBarHidden { get; private set; }
    public ICommand MenuBarClearFlagged { get; private set; }
    public ICommand MenuBarClearHidden { get; private set; }
    public ICommand MenuBarClearReview { get; private set; }
    public ICommand MenuBarSendFeedback { get; private set; }
    public ICommand MenuBarRateApp { get; private set; }
    public ICommand MenuBarAbout { get; private set; }
    public ICommand MenuBarLicenses { get; private set; }
    public ICommand MenuBarHelp { get; private set; }

    public AppShell()
    {
        InitializeCommands();
        InitializeComponent();
        
        // Configure platform-specific menus after component initialization
        ConfigurePlatformSpecificMenus();
        
        RegisterRoutes();
        BindingContext = this;
        // Force flyout behavior after MenuBarItems are added
        FlyoutBehavior = FlyoutBehavior.Flyout;
        this.Navigated += OnNavigated;
        this.Loaded += OnLoaded;
    }

    private void ConfigurePlatformSpecificMenus()
    {
        // Clear existing menu items
        MenuBarItems.Clear();

        if (DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst)
        {
            // Mac-specific menu structure
            CreateMacMenus();
        }
        else
        {
            // Windows and other platforms - keep the original structure
            CreateWindowsMenus();
        }
    }

    private void CreateMacMenus()
    {
        // For Mac: No "Home" menu, "Questions" becomes "Options" with "Home Page" first, 
        // "Application" menu with Help items, no separate "Help" menu

        // Options menu (formerly Questions menu)
        var optionsMenu = new MenuBarItem { Text = "Options" };
        optionsMenu.Add(new MenuFlyoutItem { Text = "Home Page", Command = MenuBarHome });
        optionsMenu.Add(new MenuFlyoutItem { Text = "Flashcard", Command = MenuBarFlashcards });
        optionsMenu.Add(new MenuFlyoutItem { Text = "Practice", Command = MenuBarPractice });
        optionsMenu.Add(new MenuFlyoutItem { Text = "Quiz", Command = MenuBarQuiz });
        optionsMenu.Add(new MenuFlyoutItem { Text = "Review", Command = MenuBarReview });
        optionsMenu.Add(new MenuFlyoutItem { Text = "Flagged Only", Command = MenuBarFlagged });
        optionsMenu.Add(new MenuFlyoutItem { Text = "Hidden Only", Command = MenuBarHidden });
        MenuBarItems.Add(optionsMenu);

        // Maintenance menu (unchanged)
        var maintenanceMenu = new MenuBarItem { Text = "Maintenance" };
        maintenanceMenu.Add(new MenuFlyoutItem { Text = "Clear Flagged", Command = MenuBarClearFlagged });
        maintenanceMenu.Add(new MenuFlyoutItem { Text = "Clear Hidden", Command = MenuBarClearHidden });
        maintenanceMenu.Add(new MenuFlyoutItem { Text = "Clear Review", Command = MenuBarClearReview });
        MenuBarItems.Add(maintenanceMenu);

        // Contact menu (unchanged)
        var contactMenu = new MenuBarItem { Text = "Contact" };
        contactMenu.Add(new MenuFlyoutItem { Text = "SendFeedback", Command = MenuBarSendFeedback });
        contactMenu.Add(new MenuFlyoutItem { Text = "Rate App", Command = MenuBarRateApp });
        MenuBarItems.Add(contactMenu);

        // Application menu (contains former Help items + Exit)
        var applicationMenu = new MenuBarItem { Text = "Application" };
        applicationMenu.Add(new MenuFlyoutItem { Text = "About", Command = MenuBarAbout });
        applicationMenu.Add(new MenuFlyoutItem { Text = "3rd License Info", Command = MenuBarLicenses });
        applicationMenu.Add(new MenuFlyoutItem { Text = "Help", Command = MenuBarHelp });
        MenuBarItems.Add(applicationMenu);
    }

    private void CreateWindowsMenus()
    {
        // Windows menu structure (original)
        
        // Home menu
        var homeMenu = new MenuBarItem { Text = "Home" };
        homeMenu.Add(new MenuFlyoutItem { Text = "Home Page", Command = MenuBarHome });
        homeMenu.Add(new MenuFlyoutItem { Text = "Exit", Command = MenuBarExit });
        MenuBarItems.Add(homeMenu);

        // Questions menu
        var questionsMenu = new MenuBarItem { Text = "Questions" };
        questionsMenu.Add(new MenuFlyoutItem { Text = "Flashcard", Command = MenuBarFlashcards });
        questionsMenu.Add(new MenuFlyoutItem { Text = "Practice", Command = MenuBarPractice });
        questionsMenu.Add(new MenuFlyoutItem { Text = "Quiz", Command = MenuBarQuiz });
        questionsMenu.Add(new MenuFlyoutItem { Text = "Review", Command = MenuBarReview });
        questionsMenu.Add(new MenuFlyoutItem { Text = "Flagged Only", Command = MenuBarFlagged });
        questionsMenu.Add(new MenuFlyoutItem { Text = "Hidden Only", Command = MenuBarHidden });
        MenuBarItems.Add(questionsMenu);

        // Maintenance menu
        var maintenanceMenu = new MenuBarItem { Text = "Maintenance" };
        maintenanceMenu.Add(new MenuFlyoutItem { Text = "Clear Flagged", Command = MenuBarClearFlagged });
        maintenanceMenu.Add(new MenuFlyoutItem { Text = "Clear Hidden", Command = MenuBarClearHidden });
        maintenanceMenu.Add(new MenuFlyoutItem { Text = "Clear Review", Command = MenuBarClearReview });
        MenuBarItems.Add(maintenanceMenu);

        // Contact menu
        var contactMenu = new MenuBarItem { Text = "Contact" };
        contactMenu.Add(new MenuFlyoutItem { Text = "SendFeedback", Command = MenuBarSendFeedback });
        contactMenu.Add(new MenuFlyoutItem { Text = "Rate App", Command = MenuBarRateApp });
        MenuBarItems.Add(contactMenu);

        // Help menu
        var helpMenu = new MenuBarItem { Text = "Help" };
        helpMenu.Add(new MenuFlyoutItem { Text = "About", Command = MenuBarAbout });
        helpMenu.Add(new MenuFlyoutItem { Text = "3rd License Info", Command = MenuBarLicenses });
        helpMenu.Add(new MenuFlyoutItem { Text = "Help", Command = MenuBarHelp });
        MenuBarItems.Add(helpMenu);
    }

    private async void OnLoaded(object sender, EventArgs e)
    {
        if (DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst)
        {
            // Multiple refresh attempts with different delays
            await Task.Delay(50);
            RefreshMenuBar();
            await Task.Delay(150);
            RefreshMenuBar();
            await Task.Delay(300);
            RefreshMenuBar();
        }
    }
    
    /// <summary>
    /// Refreshes the menu bar on macOS by calling SetNeedsRebuild on the IUIMenuBuilder
    /// </summary>
    public void RefreshMenuBar()
    {
        if (DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst)
        {
#if MACCATALYST
            try
            {
                var menuBarService = MenuBarServiceProvider.GetInstance();
                if (menuBarService != null)
                {
                    menuBarService.SetNeedsRebuild();
                    System.Diagnostics.Debug.WriteLine("AppShell RefreshMenuBar - SetNeedsRebuild called");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("AppShell RefreshMenuBar - MenuBarService not available yet");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AppShell RefreshMenuBar error: {ex.Message}");
            }
#endif
        }
    }
    /// <summary>
    /// Gets access to the macOS IUIMenuBuilder through the MacMenuBarService
    /// </summary>
    /// <returns>The MacMenuBarService instance or null if not available</returns>
    public object GetMenuBarService()
    {
#if MACCATALYST
        return MenuBarServiceProvider.GetInstance();
#else
        return null;
#endif
    }

    public void InitializeCommands()
    {   // Set up the menu bar commands for Windows and Mac platforms
        MenuBarHome = new Command(MenuBarHome_Clicked);
        MenuBarExit = new Command(MenuBarExit_Clicked);
        MenuBarFlashcards = new Command(MenuBarFlashcards_Clicked);
        MenuBarPractice = new Command(MenuBarPractice_Clicked);
        MenuBarQuiz = new Command(MenuBarQuiz_Clicked);
        MenuBarReview = new Command(MenuBarReview_Clicked);
        MenuBarFlagged = new Command(MenuBarFlagged_Clicked);
        MenuBarHidden = new Command(MenuBarHidden_Clicked);
        MenuBarClearFlagged = new Command(MenuBarClearFlagged_Clicked);
        MenuBarClearHidden = new Command(MenuBarClearHidden_Clicked);
        MenuBarClearReview = new Command(MenuBarClearReview_Clicked);
        MenuBarSendFeedback = new Command(MenuBarSendFeedback_Clicked);
        MenuBarRateApp = new Command(MenuBarRateApp_Clicked);
        MenuBarAbout = new Command(MenuBarAbout_Clicked);
        MenuBarLicenses = new Command(MenuBarLicenses_Clicked);
        MenuBarHelp = new Command(MenuBarHelp_Clicked);
    }

    private void RegisterRoutes()
    {
        // Default views for other platforms
        Routing.RegisterRoute("Flashcard", typeof(FlashcardView));
        Routing.RegisterRoute("Practice", typeof(PracticecardView));
        Routing.RegisterRoute("Quiz", typeof(QuizView));
        Routing.RegisterRoute("SelectFlash", typeof(SelectFlashView));
        Routing.RegisterRoute("SelectPractice", typeof(SelectPracticeView));
        Routing.RegisterRoute("SelectQuiz", typeof(SelectQuizView));
        Routing.RegisterRoute("ReviewQuizzes", typeof(ReviewQuizzesView));
        Routing.RegisterRoute("LicenseInfo", typeof(LicenseInfoView));
        Routing.RegisterRoute("LicenseView", typeof(LicenseView));
        Routing.RegisterRoute("About", typeof(About));
    }
    protected override void OnNavigating(ShellNavigatingEventArgs args)
    {
        base.OnNavigating(args);
    }
    private void OnNavigated(object sender, ShellNavigatedEventArgs e)
    {
        if ((DeviceInfo.Platform == DevicePlatform.WinUI) || (DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst))
        {
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
        }
        else
        {
            // Show the hamburger menu
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
        }
    }
    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        if (propertyName == nameof(FlyoutIsPresented))
        {
            if (this.FlyoutIsPresented)
            {
                // Reset the isNavigating flag when the flyout menu is opened
                isNavigating = false;
            }
        }
    }
    public async Task OnMenuItemClicked(string route)
    {
        if (isNavigating) return; // Prevent multiple clicks
        isNavigating = true;
        Shell.Current.FlyoutIsPresented = false;
        await ProgressService.Instance.ShowProgressAsync();
        await Shell.Current.GoToAsync(route);
        ProgressService.Instance.HideProgress();
        isNavigating = false;
    }
    public async Task OnHomeClicked()
    {
        await OnMenuItemClicked("///MainPage");
    }
    public async Task OnFlashcardsClicked()
    {
        await OnMenuItemClicked("SelectFlash");
    }
    public async Task OnPracticeClicked()
    {
        await OnMenuItemClicked("SelectPractice");
    }
    public async Task OnSelectQuizzesClicked()
    {
        await OnMenuItemClicked("SelectQuiz");
    }
    public async Task OnReviewQuizzesClicked()
    {
        ReviewDatabase rd = new();
        await rd.GetReviewQuizzes();
        await OnMenuItemClicked("ReviewQuizzes");
    }
    public async Task OnFlaggedOnlyClicked()
    {
        if (isNavigating) return; // Prevent multiple clicks
        isNavigating = true;
        Shell.Current.FlyoutIsPresented = false;
        await ProgressService.Instance.ShowProgressAsync();
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
        ProgressService.Instance.HideProgress();
        isNavigating = false;
    }
    public async Task OnHiddenOnlyClicked()
    {
        if (isNavigating) return; // Prevent multiple clicks
        isNavigating = true;
        Shell.Current.FlyoutIsPresented = false;
        await ProgressService.Instance.ShowProgressAsync();
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
        ProgressService.Instance.HideProgress();
        isNavigating = false;
    }
    public async Task OnClearFlaggedClicked()
    {
        if (isNavigating) return; // Prevent multiple clicks
        isNavigating = true;
        Shell.Current.FlyoutIsPresented = false;
        await ProgressService.Instance.ShowProgressAsync();
        QuestionsDatabase db = new();
        db.ClearFlaggedQuestions();
        var alertPage = new AlertPage();
        await AppShell.ShowCustomModalAsync(alertPage);
        await alertPage.ShowAlertAsync("Flagged Questions Cleared!", "The flagged questions have been cleared!");
        ProgressService.Instance.HideProgress();
        isNavigating = false;
    }
    public async Task OnClearHiddenClicked()
    {
        if (isNavigating) return; // Prevent multiple clicks
        isNavigating = true;
        Shell.Current.FlyoutIsPresented = false;
        await ProgressService.Instance.ShowProgressAsync();
        QuestionsDatabase db = new();
        db.ClearHiddenQuestions();
        var alertPage = new AlertPage();
        await AppShell.ShowCustomModalAsync(alertPage);
        await alertPage.ShowAlertAsync("Hidden Questions Cleared!", "The hidden questions have been cleared!");
        ProgressService.Instance.HideProgress();
        isNavigating = false;
    }
    public async Task OnClearReviewClicked()
    {
        if (isNavigating) return; // Prevent multiple clicks
        isNavigating = true;
        Shell.Current.FlyoutIsPresented = false;
        await ProgressService.Instance.ShowProgressAsync();
        ReviewDatabase rdb = new();
        await rdb.ClearAllReviewQuestions();
        await rdb.ClearAllReview();
        await rdb.GetReviewQuizzes();
        rdb.Dispose();
        var alertPage = new AlertPage();
        await AppShell.ShowCustomModalAsync(alertPage);
        await alertPage.ShowAlertAsync("Review Has Been Cleared!", "The past quizzes have been cleared!");
        ProgressService.Instance.HideProgress();
        isNavigating = false;
    }
    public async Task OnSendFeedbackClicked()
    {
        if (isNavigating) return; // Prevent multiple clicks
        isNavigating = true;
        Shell.Current.FlyoutIsPresented = false;
        await ProgressService.Instance.ShowProgressAsync();
        SendFeedbackEmails sfe = new();
        await sfe.SendAppFeedback();
        ProgressService.Instance.HideProgress();
        isNavigating = false;
    }
    public async Task OnLincesesInfoClicked()
    {
        await OnMenuItemClicked("LicenseInfo");
    }

    public async Task OnRateAppClicked()
    {
        if (isNavigating) return; // Prevent multiple clicks
        isNavigating = true;
        Shell.Current.FlyoutIsPresented = false;
        await ProgressService.Instance.ShowProgressAsync();
        // Implement your logic to rate the app here
        // For example, navigate to a rating page or open the app store

        var alertPage = new AlertPage();
        await AppShell.ShowCustomModalAsync(alertPage);
        await alertPage.ShowAlertAsync("Rated App!", "Thank you for rating our app!");
        ProgressService.Instance.HideProgress();
        isNavigating = false;
    }
    public async Task OnShowHelpClicked()
    {
        if (isNavigating) return; // Prevent multiple clicks
        isNavigating = true;
        await ProgressService.Instance.ShowProgressAsync();
        var helpPage = new HelpPage();
        await AppShell.ShowCustomModalAsync(helpPage);
        await helpPage.ShowAlertAsync();
        ProgressService.Instance.HideProgress();
        isNavigating = false;
    }
    public async Task OnAboutClicked()
    {
        await ProgressService.Instance.ShowProgressAsync();
        await OnMenuItemClicked("About");
        ProgressService.Instance.HideProgress();
    }
    private static async Task ShowCustomModalAsync(Page page)
    {
        // Check if there are any pages in the navigation stack
        var navigationStack = Shell.Current.Navigation.NavigationStack;
        if (navigationStack.Count > 1)
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
    //MenuBar Click Handlers for Win & Mac platforms mdail 6-10-25
    public async void MenuBarHome_Clicked()
    {
        await OnMenuItemClicked("///MainPage");
    }

    public void MenuBarExit_Clicked()
    {
        //await OnMenuItemClicked("///MainPage");
        if(DeviceInfo.Current.Platform == DevicePlatform.WinUI || DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst)
        {
            // Close the application on Windows or Mac
            Application.Current.Quit();
        }
        else
        {
            // Close the application on all other platforms
            System.Environment.Exit(0);
        }
    }
    private static async Task GoMainPage()
    {
        await Shell.Current.GoToAsync(new ShellNavigationState("///MainPage"), false);
    }
    // Add this method to handle smooth page transitions
    private static async Task NavigateSmoothly(string route)
    {
        // Show progress indicator with opaque background to completely mask the transition
        await ProgressService.Instance.ShowProgressAsync();
        try
        {
            // Check if we're already on the MainPage
            if (!(Shell.Current.CurrentPage?.GetType().Name == "MainPage"))
            {
                // Make overlay background completely opaque
                var overlay = ProgressService.Instance.GetOverlay();
                overlay.BackgroundColor = Colors.Black;
                overlay.Opacity = 1.0;
                // Only navigate to MainPage if we're not already there
                await Shell.Current.GoToAsync("///MainPage", false);
            }
            // Small delay to ensure Progress bar is animated & if needed the first navigation completes
            await Task.Delay(15);
            // Then navigate to the target page
            await Shell.Current.GoToAsync(route);
        }
        finally
        {
            // Hide progress indicator
            ProgressService.Instance.HideProgress();
        }
    }
    public async void MenuBarFlashcards_Clicked()
    {
        //await AppShell.GoMainPage();
        //await OnMenuItemClicked("SelectFlash");
        await NavigateSmoothly("SelectFlash");
    }

    public async void MenuBarPractice_Clicked()
    {
        //await AppShell.GoMainPage();
        //await OnMenuItemClicked("SelectPractice");
        await NavigateSmoothly("SelectPractice");
    }
    public async void MenuBarQuiz_Clicked()
    {
        await NavigateSmoothly("SelectQuiz");
    }
    public async void MenuBarReview_Clicked()
    {
        await NavigateSmoothly("ReviewQuizzes");
    }
    public async void MenuBarFlagged_Clicked()
    {
        _ = AppShell.GoMainPage();
        await OnFlaggedOnlyClicked();
    }
    public async void MenuBarHidden_Clicked()
    {
        _ = AppShell.GoMainPage();
        await OnHiddenOnlyClicked();
    }
    public async void MenuBarClearFlagged_Clicked()
    {
        _ = AppShell.GoMainPage();
        await OnClearFlaggedClicked();
    }
    public async void MenuBarClearHidden_Clicked()
    {
        _ = AppShell.GoMainPage();
        await OnClearHiddenClicked();
    }
    public async void MenuBarClearReview_Clicked()
    {
        _ = AppShell.GoMainPage();
        await OnClearReviewClicked();
    }
    
    public async void MenuBarSendFeedback_Clicked()
    {
        _ = AppShell.GoMainPage();
        await OnSendFeedbackClicked();
    }
    public async void MenuBarRateApp_Clicked()
    {
        _ = AppShell.GoMainPage();
        await OnRateAppClicked();
    }
    public async void MenuBarAbout_Clicked()
    {
        _ = AppShell.GoMainPage();
        await OnAboutClicked();
    }
    public async void MenuBarLicenses_Clicked()
    {
        await OnLincesesInfoClicked();
    }
    public async void MenuBarHelp_Clicked()
    {
        // Get the current page
        var currentPage = Shell.Current.CurrentPage;
        // Check if the current page is a CardView (base for FlashcardView, PracticecardView, QuizView)
        if (currentPage is VDCA.Views.CardView cardView)
        {
            // Fire the HelpCommand if available and can execute
            if (cardView.HelpCommand != null && cardView.HelpCommand.CanExecute(null))
            {
                cardView.HelpCommand.Execute(null);
            }
        }
        else
        {
            await AppShell.GoMainPage();
            await OnShowHelpClicked();
        }    
    }
}
