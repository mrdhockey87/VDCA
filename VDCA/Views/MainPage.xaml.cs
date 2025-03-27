using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using VDCA.Ask;
using VDCA.CustomControl;
using VDCA.Data;
using VDCA.Utils;

namespace VDCA.Views;

public partial class MainPage  : ContentPage, INotifyPropertyChanged
{
    private readonly string LOGTAG = "MainPage";
    public Label versionNo;
    private MainContentLandscape LandscapeView;
    private MainContentPortrait PortraitView;
    public IAskView askView;
    public ContentView askViewContainer;
    public RowDefinition askRow;
    private readonly string versionText = "";
    private double _Width = 0;
    private double _Height = 0;
    private DisplayOrientation CurrentOrientation = DisplayOrientation.Unknown;
    public ICommand FlashCommand => new Command(FlashClicked);
    public ICommand PracticeCommand => new Command(PracticeClicked);
    public ICommand QuizCommand => new Command(QuizClicked);
    public ICommand ReviewCommand => new Command(ReviewClicked);

    public MainPage()
    {
        InitializeComponent();
        Title= "VA Accredited Exam Study Guide";
        versionText = Constants.COPYRIGHT + Constants.NEWLINE + "Build: " + Constants.APP_BUILD
                      + " Version: " + Constants.APP_VERSION + " Database Version " + Constants.DB_VERSION_NUMBER;
        BindingContext = this;
        CheckForAsk();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
        if (mainDisplayInfo.Width > mainDisplayInfo.Height && !(DeviceInfo.Platform == DevicePlatform.WinUI) && !(DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst))
        {
            CurrentOrientation = DisplayOrientation.Landscape;
            // Landscape
            LandscapeView ??= new MainContentLandscape();
        }
        else
        {
            CurrentOrientation = DisplayOrientation.Portrait;
            // Portrait
            PortraitView ??= new MainContentPortrait();
        }
        // Load the proper ContentView based on the orientation
        if (CurrentOrientation == DisplayOrientation.Landscape)
        {
            // Wait for the ContentView to load
            LandscapeView.Loaded += (sender, e) =>
            {
                //await OnboardingOverlay();
            };
        }
        else
        {
            PortraitView.Loaded += (sender, e) =>
            {
               // await OnboardingOverlay();
            };
        }
    }

    private async Task OnboardingOverlay()
    {
        if (OnboardingSupportHelp.SupportReguiredThisPage(SupportedPages.MainPage))
        {
            VisualElement targetControl;
            VisualElement page;
            if (CurrentOrientation == DisplayOrientation.Portrait)
            {
                targetControl = PortraitView.Quiz;
                page = PortraitView;
            }
            else
            {
                targetControl = LandscapeView.Quiz;
                page = LandscapeView;
            }
            NavigationOnboardingOverlayControl navigationOnboardingOverlayControl;
            if(CurrentOrientation == DisplayOrientation.Portrait)
            {
                navigationOnboardingOverlayControl = PortraitView.NavigationOnboardingOverlayControlPortrait;
            }
            else
            {
                navigationOnboardingOverlayControl = LandscapeView.NavigationOnboardingOverlayControlLandscape;
            }

            OnboardingOverlayControl onboardingOverlayControl2 = new()
            {
                InformationText = SupportDataVersion5.SupportPagesDataList[1].ControlExplainationText,
                VisibleArrows = SupportDataVersion5.SupportPagesDataList[1].ArrowDirection,
                IsVisible = true,
                Background = Colors.Aqua
            };
            // Get the adjusted bounds for the OnboardingOverlayControl
            var adjustedBounds = OnboardingSupportHelp.GetAdjustedBounds(targetControl, page, onboardingOverlayControl2);
            // Determine the arrow direction based on the adjusted bounds
            var arrowDirection = OnboardingSupportHelp.GetArrowDirection(targetControl.Bounds, adjustedBounds);
            if (arrowDirection != ArrowDirection.Up)
            {
                SupportDataVersion5.SupportPagesDataList[1].ArrowDirection = [arrowDirection, ArrowDirection.Down];
            }
            else if (arrowDirection != ArrowDirection.Down)
            {
                SupportDataVersion5.SupportPagesDataList[1].ArrowDirection = [arrowDirection, ArrowDirection.Up];
            }
            else
            {
                SupportDataVersion5.SupportPagesDataList[1].ArrowDirection = [arrowDirection];
            }
            OnboardingOverlayControl onboardingOverlayControl1 = new()
            {
                InformationText = SupportDataVersion5.SupportPagesDataList[0].ControlExplainationText,
                VisibleArrows = SupportDataVersion5.SupportPagesDataList[0].ArrowDirection,
                IsVisible = true
            };
            onboardingOverlayControl2.Background = Colors.Coral;
            navigationOnboardingOverlayControl.NavigationOverlayAbsoluteLayout.SetLayoutBounds(onboardingOverlayControl1, new Rect(10, 10, 300, 300));
            navigationOnboardingOverlayControl.NavigationOverlayAbsoluteLayout.SetLayoutFlags(onboardingOverlayControl1, AbsoluteLayoutFlags.All);
            navigationOnboardingOverlayControl.NavigationOverlayAbsoluteLayout.Children.Add(onboardingOverlayControl1);
            navigationOnboardingOverlayControl.NavigationOverlayAbsoluteLayout.SetLayoutBounds(onboardingOverlayControl2, new Rect(10, 300, 300, 300)); // adjustedBounds);
            navigationOnboardingOverlayControl.NavigationOverlayAbsoluteLayout.SetLayoutFlags(onboardingOverlayControl2, AbsoluteLayoutFlags.All);
            navigationOnboardingOverlayControl.NavigationOverlayAbsoluteLayout.Children.Add(onboardingOverlayControl2);
            navigationOnboardingOverlayControl.TotalPages = 2;
            navigationOnboardingOverlayControl.NextButtonClicked += (sender, e) =>
            {
                navigationOnboardingOverlayControl.HideOverlay();
            };
            navigationOnboardingOverlayControl.PreviousButtonClicked += (sender, e) =>
            {
            };
            await navigationOnboardingOverlayControl.ShowOverlayAsync();
        }
    }

    private void CheckForAsk()
    {
        AskQuestion aq = new();
        AskConstants.SetShowHideAsk(aq.TimeAskQuestion());
        if (Constants.SHOW_ASK)
        {
            AskConstants.ASK_COUNT = 0;
            AskConstants.askText[0].ResetAskText();
            AskConstants.mpForAsk = this;
        } 
        else
        {
            AskConstants.ASK_COUNT = -1;
        }
    }
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        if (_Width != width || _Height != height)
        {
            _Width = width;
            _Height = height;
            var isTablet = DeviceInfo.Idiom == DeviceIdiom.Tablet;
            if (_Width > _Height && !(DeviceInfo.Platform == DevicePlatform.WinUI) && !( DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst))
            {
                CurrentOrientation = DisplayOrientation.Landscape;
                LandscapeView ??= new MainContentLandscape();
                Content = LandscapeView;
                LandscapeView.MP = this;
                LandscapeView.MLP = LandscapeView;
                askView ??= new AskViewLandscape();
                askRow = LandscapeView.AskRow;
                LandscapeView.AskViewContainer.Content ??= askView as ContentView;
                LandscapeView.VersionNo.Text = versionText;
                if (isTablet)
                {
                    LandscapeView.Logo.Source = "vdca_solid_color.png";
                    if (_Height > 500) 
                    {
                        if (Constants.SHOW_ASK)
                        {
                            LandscapeView.Logo.HeightRequest = 400;
                            LandscapeView.Logo.WidthRequest = 200;
                        }
                        else
                        {
                            LandscapeView.Logo.HeightRequest = 600;
                            LandscapeView.Logo.WidthRequest = 300;
                        }
                    }
                    else
                    {
                       if (Constants.SHOW_ASK)
                        {
                            LandscapeView.Logo.HeightRequest = 350;
                            LandscapeView.Logo.WidthRequest = 175;
                        }
                        else
                        {
                            LandscapeView.Logo.HeightRequest = 400;
                            LandscapeView.Logo.WidthRequest = 200;
                        }
                    }
                }
            }
            else
            {
                CurrentOrientation = DisplayOrientation.Portrait;
                PortraitView ??= new MainContentPortrait();
                Content = PortraitView;
                PortraitView.MP = this;
                PortraitView.MPP = PortraitView;
                askView ??= new AskViewPortrait();
                askRow = PortraitView.AskRow;
                PortraitView.AskViewContainer.Content ??= askView as ContentView;
                PortraitView.VersionNo.Text = versionText;
            }
            askView.SetIsVisible(Constants.SHOW_ASK);
            if(!Constants.SHOW_ASK)
            {
                askRow.Height = 0;
            }
            else
            {
                askView.SetupAskView();
            }
        }
    }
    async void FlashClicked()
    {
        await ShowProgressBar();
        try
        {
            await Shell.Current.GoToAsync("SelectFlash");
        }
        catch(Exception ex)
        {
            Console.WriteLine(LOGTAG + " error: " + ex.Message);
        }
        finally
        {
            HideProgressBar();
        }
    }
    async void PracticeClicked()
    {
        await ShowProgressBar();
        try
        {
            await Shell.Current.GoToAsync("SelectPractice");
        }
        catch (Exception ex)
        {
            Console.WriteLine(LOGTAG + " error: " + ex.Message);
        }
        finally
        {
            HideProgressBar();
        }
    }
    async void QuizClicked()
    {
        await ShowProgressBar();
        try
        {
            await Shell.Current.GoToAsync("SelectQuiz");
        }
        catch (Exception ex)
        {
            Console.WriteLine(LOGTAG + " error: " + ex.Message);
        }
        finally
        {
            HideProgressBar();
        }
    }
    async void ReviewClicked()
    {
        await ShowProgressBar();
        try
        {
            ReviewDatabase rd = new();
            await rd.GetReviewQuizzes();
            await Shell.Current.GoToAsync("ReviewQuizzes");
        }
        catch (Exception ex)
        {
            Console.WriteLine(LOGTAG + " error: " + ex.Message);
        }
        finally
        {
            HideProgressBar();
        }
    }
    // hide the ask view and set it's height to 0 mdail 9-9-21
    public void CloseAskViews()
    {
        Constants.SHOW_ASK = false;
        askView.SetIsVisible(false);
        if(askRow is not null)
        {
            askRow.Height = 0;
        }
        Constants.SHOW_ASK = false;
    }
    public async Task ShowProgressBar()
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            if (Content is MainContentLandscape landscapeView)
            {
                await ShowProgressBarUtil.ShowProgressBar(landscapeView.ProgressBarLandscapeOverlay);
            }
            else if (Content is MainContentPortrait portraitView)
            {
                await ShowProgressBarUtil.ShowProgressBar(portraitView.ProgressBarPortraitOverlay);
            }
        });
        await Task.Delay(5);
    }

    public void HideProgressBar()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            if (Content is MainContentLandscape landscapeView)
            {
                await ShowProgressBarUtil.HideProgressBar(landscapeView.ProgressBarLandscapeOverlay);
            }
            else if (Content is MainContentPortrait portraitView)
            {
                await ShowProgressBarUtil.HideProgressBar(portraitView.ProgressBarPortraitOverlay);
            }
        });
    }
}