using Microsoft.Maui.Storage;
using System;
using System.Globalization;

namespace VDCA.Ask
{
    class AppPreferences
    {
        //private readonly string LOGTAG = "AppPreferences";
        private readonly string SavedDate = "SavedDate";
        private readonly string VerNo = "VerNo";
        private readonly string RunCount = "RunCount";
        private readonly string DontShow = "DontShow";
        private readonly string RejectedCount = "RejectedCount";
        private readonly string RejectsAllowed = "RejectsAllowed";
        private readonly string DontAsk = "DontAsk";
        private readonly string CrashCount = "CrashCount";
        private readonly string HasShown = "HasShown";
        private int rejects = 3;
        private int crash_count = 1;
        private int has_shown = 0;

        public AppPreferences() { }

        public int GetHas_shown()
        {
            has_shown = Preferences.Get(HasShown, 0);
            return has_shown;
        }
        //set has shown to one so we know it has shown before mdail 3-13-19
        public void SetHas_shown()
        {
            has_shown = 1;
            Preferences.Set(HasShown, has_shown);
        }
        //reset has show when after new version mdail 3-13-19
        private void ResetHas_Shown()
        {
            Preferences.Set(HasShown, 0);
        }
        //Used to set the crash count, called form the Exception Handler class mdail 3-7-19
        public void SetCrash_count()
        {
            int cc = Preferences.Get(CrashCount, 0);
            crash_count = cc + 1;
            Preferences.Set(CrashCount, crash_count);
        }
        //Used to see if there were any crashes for this version mdail 3-7-19
        public int GetCrash_count()
        {
            crash_count = Preferences.Get(CrashCount, 0);
            return crash_count;
        }
        //Used to reset the crash count mdail 3-7-19
        private void ResetCrash_count()
        {
            Preferences.Set(CrashCount, 0);
        }
        //get the temp off pref mdail 2-27-19
        public int GetTempOff()
        {
            if (Constants.TEMP_OFF)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        //reset the temp off pref - this is run once when the app first starts and sets temp off to 0 mdail 2-27-19
        public void ResetTempOff()
        {
            Constants.TEMP_OFF = true;
        }
        //set don't ask again this run mdail 2-27-19
        public void SetTempOff()
        {
            if (Constants.TEMP_OFF)
            {
                Constants.TEMP_OFF = false;
            }
        }
        //gets the count before showing the don't show button mdail 2-27-19
        public int GetShowDont()
        {
            return Preferences.Get(DontShow, 0);
        }
        //set the count before showing the don't show button to one higher than it was mdail 2-27-19
        public void SetShowDont()
        {
            int show = Preferences.Get(DontShow, 0);
            show++;
            Preferences.Set(DontShow, show);
        }
        //reset the count before showing the don't show button to 0 mdail 2-27-19
        public void ResetShowDont()
        {
            int show = 0;
            Preferences.Set(DontShow, show);
        }
        //sets the number of allowed rejects mdail 2-27-19
        public void SetRejectNumber(int _reject) { rejects = _reject; }
        //save the new version to use for version checking mdail 2-22-19
        public void SaveNewVersion(int NewVer)
        {
            Preferences.Set(VerNo, NewVer);
            ResetForNewVersion();
        }
        //get & return the saved version number mdail 2-22-19
        public int GetVersionSaved()
        {
            return Preferences.Get(VerNo, 0);
        }
        //get the saved date and see if it is more than 3 weeks age and return true if it is and false if not mdail 2-22-19
        public int CheckDateEnough()
        {
            int days = 0;
            try
            {
                string savedDate = Preferences.Get(SavedDate, "");
                if (!String.IsNullOrEmpty(savedDate))
                {
                    DateTime dateIn = DateTime.ParseExact(savedDate, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    DateTime now = DateTime.Now;
                    days = (now.Date - dateIn.Date).Days;
                }
            }
            catch (Exception)
            {
                //if we catch and exception, save now as the date 0 days will be returned mdail 8-11-21
                SaveToday();
            }
            return days;
        }
        //save today's date as a double of millisecond to the preferences file mdail 8-11-21
        private void SaveToday()
        {
            String now = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
            Preferences.Set(SavedDate, now);
        }
        //get the run count mdail 2-25-19
        public int GetRunCounts()
        {
            int runs = Preferences.Get(RunCount, 0);
            //set the number of runs mdail 2-25-19
            SetRunCount(runs);
            return runs;
        }

        //set the number of runs mdail 2-25-19
        public void SetRunCountDebug(int temp_count)
        {
            Preferences.Set(RunCount, temp_count);
        }

        //set the number of runs mdail 2-25-19
        public void SetRunCount(int runs)
        {
            runs++;
            Preferences.Set(RunCount, runs);
            Constants.COUNT_DONE = false;
        }
        //Clear the run count mdail 2-25-19
        private void ClearRuns()
        {
            Preferences.Set(RunCount, 0);
        }
        //sets the rejected count mdail 2-25-19
        public int GetRejected()
        {
            return Preferences.Get(RejectedCount, 0);
        }
        //sets the rejected count mdail 2-25-19
        public int GetRejects()
        {
            return Preferences.Get(RejectsAllowed, 3);
        }
        //save the number of rejects to allow before setting don't ask - Changed the name by adding Allowed to it mdail 2-27-19 Changed 3-6-19
        public void SaveRejectsAllowed()
        {
            Preferences.Set(RejectsAllowed, rejects);
        }
        //set the rejected count, if rejected count is greater then 3 set don't mdail 2-25-19
        public void SetRejected()
        {
            int rejected = Preferences.Get(RejectedCount, 0);
            rejected++;
            Preferences.Set(RejectedCount, rejected);
            //set don't ask if rejected as many times as the allowed reject count already mdail 2-25-19
            if (rejected >= rejects)
            {
                SetDontAsk();
            }
        }
        //to reset the rejected count to 0 because they responded to a question mdail 3-13-19
        public void ResetRejected()
        {
            Preferences.Set(RejectedCount, 0);
        }
        //sets don't ask anymore mdail 2-25-19
        public int GetDontAsk()
        {
            return Preferences.Get(DontAsk, 0);
        }
        //set don't ask anymore - if don't ask = 1 that means the user selected don't ask mdail 2-25-19
        public void SetDontAsk()
        {
            Preferences.Set(DontAsk, 1);
        }
        //Clear the run count
        private void ClearDontAsk()
        {
            Preferences.Set(DontAsk, 0);
        }
        //reset all the preferences for a new version so the user gets asked when time or run count met mdail 2-25-19
        private void ResetForNewVersion()
        {
            ResetHas_Shown();
            SaveToday();
            ClearRuns();
            ClearDontAsk();
            ResetCrash_count();
        }
    }
}
