using Microsoft.Maui.Storage;
using System;

namespace VDCA.CustomControl
{
    public static class OnboardingPreferenceService
    {
        public const string DontShowSupportKeyPrefix = "DontShowSupport_";        

        public static bool GetDontShowSupport(SupportedPages page)
        {
            return Preferences.Get(DontShowSupportKeyPrefix + page, false);
        }
        public static void SetDontShowSupport(SupportedPages page, bool value)
        {
            Preferences.Set(DontShowSupportKeyPrefix + page, value);
        }
        public static bool GetSupportRequired(SupportedPages page)
        {
            return Preferences.Get(Constants.SupportRequiredKeyPrefix + page, false);
        }
        public static void SetSupportRequired(SupportedPages page, bool value)
        {
            Preferences.Set(Constants.SupportRequiredKeyPrefix + page, value);
        }
        public static void ResetDontShowSupport()
        {
            //reset all the pages to DontShowSupport false call for the App.xaml.cs mdail 11-1-24
            foreach (var page in Enum.GetNames(typeof(SupportedPages)))
            {
                Preferences.Set(DontShowSupportKeyPrefix + page, false);
            }
        }
    }
}
