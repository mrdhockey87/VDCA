using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using VDCA.CustomControl;
using VDCA.Models;

namespace VDCA
{
    public static class Constants
    {
        //private static readonly string LOGTAG = "Constants";
        public static readonly string APP_VERSION = AppInfo.Current.VersionString;
        public static readonly string APP_BUILD = AppInfo.Current.BuildString;
        //database constants
        public static int DATABASE_VERSION = 9;
        //OnboardingSupport constants - OnboardingSupportRequired needs to be set to true in the Constants.cs
        //file if onboarding support is required
        private static readonly bool OnboardingSupportRequiredMainPage = true;
        private static readonly bool OnboardingSupportRequiredFlashcardView = true;
        private static readonly bool OnboardingSupportRequiredPracticeView = true;
        private static readonly bool OnboardingSupportRequiredQuizView = true;
        public const string SupportRequiredKeyPrefix = "SupportRequiredKey_";
        //When Onboarding Support is required, set the support required for each page above
        public static void SetOnboardingSupportRequired()
        {
            OnboardingPreferenceService.SetSupportRequired(SupportedPages.MainPage, Constants.OnboardingSupportRequiredMainPage);
            OnboardingPreferenceService.SetSupportRequired(SupportedPages.FlashcardView, Constants.OnboardingSupportRequiredFlashcardView);
            OnboardingPreferenceService.SetSupportRequired(SupportedPages.PracticecardView, Constants.OnboardingSupportRequiredPracticeView);
            OnboardingPreferenceService.SetSupportRequired(SupportedPages.QuizView, Constants.OnboardingSupportRequiredQuizView);
        }
        //need to set these at start up then I can check them in the OnAppearing of each page mdail 11-1-24
        public static bool ShowOnboardingSupportMain = false;
        public static bool ShowOnboardingSupportFlash = false;
        public static bool ShowOnboardingSupportPractice = false;
        public static bool ShowOnboardingSupportQuiz = false;
        public static string VersionLabelStringPreDatabase = Constants.COPYRIGHT + " " + Constants.NEWLINE + "Build: " + Constants.APP_BUILD +
                                                             " Version: " + Constants.APP_VERSION + "Database Version: ";
        [System.ComponentModel.DefaultValue(0)]
        public static int DB_VERSION_NUMBER { get; set; }
        public static string PassColor = "#22b573";
        public static string FailColor = "#ff0000";
        public static string PrimaryBackgroundColor = "#FFFFFF";
        public static string ButtonDeselectedColor = "#000080";
        public static string ButtonSelectedColor = "#d4d4d4";
        public static string CardBackColor = "#FFFFFF";
        public static string CardBackColorFlipped = "#d4d4d4";
        public static string CardBackColorDark = "#000080";
        public static string CardBackColorFlippedDark = "#000046";
        public static string PrimaryTextColor = "#000080";
        public static string ANormalButtonImage = "white_a.png";
        public static string ASelectedButtonImage = "navy_a.png";
        public static string BNormalButtonImage = "white_b.png";
        public static string BSelectedButtonImage = "navy_b.png";
        public static string CNormalButtonImage = "white_c.png";
        public static string CSelectedButtonImage = "navy_c.png";
        public static string DNormalButtonImage = "white_d.png";
        public static string DSelectedButtonImage = "navy_d.png";
        public static string ANormalImage = "navy_a.png";
        public static string ASelectedImage = "white_a.png";
        public static string BNormalImage = "navy_b.png";
        public static string BSelectedImage = "white_b.png";
        public static string CNormalImage = "navy_c.png";
        public static string CSelectedImage = "white_c.png";
        public static string DNormalImage = "navy_d.png";
        public static string DSelectedImage = "white_d.png";
        public static string LockNormalImage = "white_checkmark.png";
        public static string LockPassFailImage = "white_checkmark.png";
        public static string LockSelectedImage = "navy_checkmark.png";

        public const string DB_FILE_NAME = "vdca.db";
        public const string NAME_DATABASE = "vdca";
        public const string EXT_DATABASE = "db";
        public const string QUESTIONS_TABLE = "VAQuestions";
        public static string Explanation = "Explanation";
        public static string Citation = "Citation";
        public static string Confirm = "Please Confirm Selection!";
        public static string ScoreQuizTitle = "Finish Quiz?";
        public static string ScoreQuizMessage = "Do you want to finish the quiz and have it scored?";
        public static string QuizTitle = "Quiz";
        public static string ExitQuizTitle = "Exit Quiz?";
        public static string PracticeTitle = "Practice";
        public static string ExitPracticeTitle = "Exit Practice?";        
        public static string FlashcardsTitle = "Flashcards";
        public static string ReviewTitle = "Review Questions";
        public static string ReviewQuizTitle = "Review Quiz Questions";
        public static string ExitFlashcardsTitle = "Exit Flashcards?";
        public static string ExitQuizMessage = "Do you really want to exit this Quiz without scoring it?";
        public static string ExitPracticeMessage = "Do you really want to exit the Practice?";
        public static string ExitFlashcardsMessage = "Do you really want to exit the Flashcards?";
        public static string PracticeSelectMessage = "Select the correct answer.";
        public static string QuizSelectMessage = "Select the correct answer.";
        public static string CorrectMessage = "Correct";
        public static string IncorrectMessage = "Incorrect";
        public static string SkippedMessage = "Incorrect The Question was Skipped and Not Answered!";
        public static string FirstCard = "Your already at the first card!";
        public static string LastCard = "Your already at the last card!";

        public static int InstructionLabelNormalFontSize = 20;
        public static int InstructionLabelAnsweredFontSize = 24;
        public static int VisibleWidth = 30;
        public static int InvisibleWidth = 0;


        public static string DEVICE_PUBLIC_FOLDER = Microsoft.Maui.Storage.FileSystem.AppDataDirectory;
        public static string DBPath = Path.Combine(FileSystem.Current.AppDataDirectory, Constants.DB_FILE_NAME);
        
        public static ObservableCollection<Questions> Questions = [];
        public static ObservableCollection<Questions> ReviewQuestions = [];
        public static ObservableCollection<ReviewQuiz> ReviewQuizzes = [];
        public static ReviewQuiz LastQuiz = null;
        public static bool REVIEW = false;
        public static bool IsREVIEW = false;
        public static bool FROM_QUIZ = false;
        public static bool ShowSupportPages = false;
        public static int Current_Index = 0;
        public static string SavedFlashCats = Path.Combine(Constants.DEVICE_PUBLIC_FOLDER, "SavedFlashCats.json");
        public static string SavedPracticeCats = Path.Combine(Constants.DEVICE_PUBLIC_FOLDER, "SavedPracticeCats.json");

        [System.ComponentModel.DefaultValue(0)]
        public static int FlaggedTotal { get; set; }
        [System.ComponentModel.DefaultValue(0)]
        public static int HiddenTotal { get; set; }

        public static ObservableCollection<Categories> Categories = [];
        public static ObservableCollection<Categories> CategoriesFlash = [];
        //used in the ask view mdail
        public static bool COUNT_DONE = false;
        //If the use has selected don't show again this gets set to true mdail 8-13-21
        public static bool DONTSHOW = false;
        public static bool TEMP_OFF = true;
        public static bool SHOW_ASK = false;
        public static bool DONT_ASK = false;
        //for easily adding single or double new lines to the ends of strings mdail
        public static string NEWLINE = Environment.NewLine;
        public static string TWONEWLINE = Environment.NewLine + Environment.NewLine;
        public static string COPYRIGHT = "© 2025 ALL RIGHTS RESERVED, VDCA, Inc. VDCA Veteran's Disability Claims Assistance";
        public static string iosAppId = ""; //iOS: This is found on your iTunes connect page for your app
        public static string macosAppId = ""; //iOS: This is found on your iTunes connect page for your app
        public static string windowsAppId = ""; //UWP: This is the Store ID: You can find the link to your app's Store listing on the App identity page,
                                                //in the App management section of each app in your dashboard.

        public static DBVersionNo AppVersionNumberInfo { get; set; } = new();
        public static string androidAppId = AppInfo.PackageName;
        public static Dictionary<string, object> NavigationParameters = new()
        {
            { "Animated", false }
        };
        //Send the user to the rating page for the app mdail 8-18-21
        public static async Task SendToRatingPage()
        {
            await Task.Run(() =>
            {
                string appId;
                if (DeviceInfo.Platform == Microsoft.Maui.Devices.DevicePlatform.Android)
                {
                    appId = Constants.androidAppId;
                    //CrossStoreReview.Current.OpenStoreListing(appId);
                }
                else if (DeviceInfo.Current.Platform == Microsoft.Maui.Devices.DevicePlatform.iOS)
                {
                    appId = Constants.iosAppId;
                    // CrossStoreReview.Current.OpenStoreListing(appId);
                }
                else if (DeviceInfo.Current.Platform == Microsoft.Maui.Devices.DevicePlatform.macOS)
                {
                    appId = Constants.macosAppId;
                    //CrossStoreReview.Current.OpenStoreListing(appId);
                }
                else if (DeviceInfo.Current.Platform == Microsoft.Maui.Devices.DevicePlatform.WinUI)
                {
                    appId = Constants.windowsAppId;
                    //Need to figure out how to send the user to the Microsoft store here mdail 3-23-=23
                }
            });
        }
    }
}
