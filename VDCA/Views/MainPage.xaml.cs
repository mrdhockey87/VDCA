using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using VDCA.Ask;
using VDCA.CustomControl;
using VDCA.Data;
using VDCA.Models;
using VDCA.Utils;

namespace VDCA.Views;

public partial class MainPage  : ContentPage, INotifyPropertyChanged
{
    private readonly string LOGTAG = "MainPage";
    public Label versionNo;
    public MainContentLandscape LandscapeView;
    public MainContentPortrait PortraitView;
    public ProgressBarOverlay ProgressBarOverlayMain;
    public IAskView askView;
    public ContentView askViewContainer;
    public RowDefinition askRow;
    private readonly string versionText = "";
    private double _Width = 0;
    private double _Height = 0;
    public HelpContentView HelpContent;
    private DisplayOrientation CurrentOrientation = DisplayOrientation.Unknown;
    public ICommand FlashCommand => new Command(FlashClicked);
    public ICommand PracticeCommand => new Command(PracticeClicked);
    public ICommand QuizCommand => new Command(QuizClicked);
    public ICommand ReviewCommand => new Command(ReviewClicked);

    private DBVersionNo _versionInfo;
    public DBVersionNo VersionInfo
    {
        get => _versionInfo;
        set
        {
            if (_versionInfo != value)
            {
                _versionInfo = value;
                OnPropertyChanged(nameof(VersionInfo));
            }
        }
    }
    public MainPage()
    {
        InitializeComponent();
        VersionInfo = Constants.AppVersionNumberInfo;
        Title = "VA Accredited Exam Study Guide";
        BindingContext = this;
        HelpContent = helpContent;
        ProgressBarOverlayMain = progressBarOverlayMain;
        CheckForAsk();
    }
    private async void OnHelpClicked(object sender, EventArgs e)
    {
        await HelpContent.ShowAlertAsync();
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
            MainContentContainer.Content = LandscapeView;
        }
        else
        {
            CurrentOrientation = DisplayOrientation.Portrait;
            // Portrait
            PortraitView ??= new MainContentPortrait();
            MainContentContainer.Content = PortraitView;
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
                HideProgressBar();
                CurrentOrientation = DisplayOrientation.Landscape;
                LandscapeView ??= new MainContentLandscape();
                MainContentContainer.Content = LandscapeView;
                LandscapeView.MP = this;
                LandscapeView.MLP = LandscapeView;
                askView ??= new AskViewLandscape();
                askRow = LandscapeView.AskRow;
                LandscapeView.AskViewContainer.Content ??= askView as ContentView;
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
                HideProgressBar();
                CurrentOrientation = DisplayOrientation.Portrait;
                PortraitView ??= new MainContentPortrait();
                MainContentContainer.Content = PortraitView;
                PortraitView.MP = this;
                PortraitView.MPP = PortraitView;
                askView ??= new AskViewPortrait();
                askRow = PortraitView.AskRow;
                PortraitView.AskViewContainer.Content ??= askView as ContentView;
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
            rd.Dispose();
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
            await ShowProgressBarUtil.ShowProgressBar(ProgressBarOverlayMain);
        });
        await Task.Delay(5);
    }

    public void HideProgressBar()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await ShowProgressBarUtil.HideProgressBar(ProgressBarOverlayMain);
        });
    }
}