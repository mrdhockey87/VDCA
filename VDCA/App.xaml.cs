using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using VDCA.Ask;
using VDCA.CustomControl;
using VDCA.Data;
using VDCA.Models;
using VDCA.Sizes;

namespace VDCA;

public partial class App : Application
{
    private static readonly string LOGTAG = "App";
    public static int AppDatabaseVersion = 0;
    public App()
    {
        try
        {
            InitializeComponent();
            InitializeAppAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Console.WriteLine(LOGTAG + " error: " + ex.Message);
        }
        //InitializeComponent();
        //Make the observable collection that contains one object of ask text to control the ask text font size and text mdail 8-25-23
        //AskConstants.askText = new(AskConstants.atList);
        //MainPage = new AppShell();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {

        //var window = new Window(new AppShell());
        return new Window { Page = new AppShell() };
    }
    private async Task InitializeAppAsync()
    {
        try
        {
            //Check to see if the app has been updated and if so set the support required flag mdail 11-1-24
            //Need to set the proper constants var for each page if new support pages are required mdail 11-1-24
          /*  OnboardingSupportHelp.GetVersions();
            if(OnboardingSupportHelp.CurrentVersion > OnboardingSupportHelp.PreviousVersion)
            {
                Constants.SetOnboardingSupportRequired();
                OnboardingPreferenceService.ResetDontShowSupport();
                OnboardingSupportHelp.SetPreviousVersion(OnboardingSupportHelp.CurrentVersion);
            }*/
            // Make the observable collection that contains one object of ask text to control the ask text font size and text mdail 8-25-23
            AskConstants.askText = [.. AskConstants.atList];
            LoadDeviceSpecificSizes();
            await GetStartingDataAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(LOGTAG + " error: " + ex.Message);
        }
        //MainPage = new AppShell();
    }

    //Load VM_Categories at startup, if the database doesn't exist copy it into place mdail 4-1-23
    private static async Task GetStartingDataAsync()
    {
        DatabaseManatance dm = new();
        if (DatabaseManatance.CheckDBExists())
        {
            DbVersion dbVersion = await DbVersion.CreateAsync();
            Constants.DB_VERSION_NUMBER = await dbVersion.CheckVersionNo();
            AppDatabaseVersion = Constants.DB_VERSION_NUMBER;
            Constants.AppVersionNumberInfo.ID= 0;
            Constants.AppVersionNumberInfo.Version_no = App.AppDatabaseVersion;
            Constants.AppVersionNumberInfo.VersionString = Constants.COPYRIGHT + Constants.NEWLINE + "Build: " + Constants.APP_BUILD
                      + " Version: " + Constants.APP_VERSION + " Database Version " + Constants.AppVersionNumberInfo.Version_no;
            QuestionsDatabase db = new();
            db.LoadCountData();
            db.LoadCountDataFlashOnly();
        }
        else
        {
            // The CopyDBIfNotExists calls LoadCountDataAsync then LoadCountDataFlashOnlyAsync after copying the database into place mdail 9-23-23
            await dm.CopyDBIfNotExists();
        }
    }
    //Check to see if this is a small tablet or any other device and load the correct sizes file mdail 7-7-23
    private void LoadDeviceSpecificSizes()
    {
        try
        {
            //the AppSizses.xaml file fixes the sizes for deifferent devices, however tablet 8 inches or smaller need to use differnt sizes
            //than the fill sets so we need to checnk for that and load a different sizes file for them mdail 7-7-23
            var isTablet = DeviceInfo.Idiom == DeviceIdiom.Tablet;
            if (isTablet)
            {
                var screenWidthPixels = DeviceDisplay.MainDisplayInfo.Width;
                var screenHeightPixels = DeviceDisplay.MainDisplayInfo.Height;
                var screenDensity = DeviceDisplay.MainDisplayInfo.Density;
                // Correct the calculation for screen width and height in inches
                var screenWidthInches = screenWidthPixels / screenDensity / 160; 
                // Assuming screenDensity is in DPI and 160 DPI is the base density.
                var screenHeightInches = screenHeightPixels / screenDensity / 160;
                // Calculate the diagonal screen size in inches correctly
                var screenSizeInches = Math.Sqrt(Math.Pow(screenWidthInches, 2) + Math.Pow(screenHeightInches, 2));
                if (screenSizeInches <= 8) // Devices with less than 10 inches are considered small tablets
                {
                    AddResourceDictionary(new SmallTabletSizes());
                }
                else
                {
                    AddResourceDictionary(new AppSizes());
                }
            }
            else
            {
                AddResourceDictionary(new AppSizes());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(LOGTAG + " error: " + ex.Message);
        }
    }
    private void AddResourceDictionary(ResourceDictionary resourceDictionary)
    {
        try
        {
            Resources.MergedDictionaries.Add(resourceDictionary);
        }
        catch (Exception ex)
        {
            Console.WriteLine(LOGTAG + " error: " + ex.Message);
        }
    }
}
