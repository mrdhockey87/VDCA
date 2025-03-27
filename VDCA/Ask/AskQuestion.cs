using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDCA.Ask
{
    class AskQuestion
    {
        //private readonly string LOGTAG = "AskQuestion";
        //presets to determine if we should ask of not
        private int runs = 21;
        private int tooMany = 1000;
        private int days = 21;
        private int rejects = 3;
        private int showDont = 3;

        private readonly bool dontAsk = false;
        private readonly bool yesAsk = true;
        private readonly int dontAskTrue = 1;
        private int hasShown = 0;

        public AskQuestion() { }
        //Allow for the setting of the number of runs and too many runs and the number of days mdail 2-25-19
        public void SetRuns(int _runs)
        {
            runs = _runs;
        }
        public void SetTooMany(int _tooMant)
        {
            tooMany = _tooMant;
        }
        public void SetDays(int _days)
        {
            days = _days;
        }

        public void SetShowDont(int _showDont) { showDont = _showDont; }

        public bool TimeAskQuestion()
        {
            AppPreferences ap = new();
            int temp = ap.GetTempOff();
            if (temp == 1)
            {
                return dontAsk;
            }
            rejects = ap.GetRejects();
            hasShown = ap.GetHas_shown();
            //if the version of the app has crashed don't ask for feedback or rating mdail 3-7-19
            int crashes = ap.GetCrash_count();
            if (crashes >= 1)
            {
                SetAskVisible(dontAsk);
                return dontAsk;
            }
            //if the user has hit don't ask again 3 times don't ask even if it is a new version mdail 3-7-19
            int rejected = ap.GetRejected();
            if (rejected >= rejects)
            {
                SetAskVisible(dontAsk);
                return dontAsk;
            } //if the saved version is lower than the current version, save the number version number, hide the ask view and return it not time to ask mdail 2-27-19
            bool new_level = NewLevel();
            if (new_level)
            {
                SetAskVisible(dontAsk);
                return dontAsk;
            }
            //if the don't ask anymore pref is set to 1 (dontAskTrue == 1) then hide the ask view 3-6-19
            int dont_ask = ap.GetDontAsk();
            if (dont_ask == dontAskTrue)
            {
                //SetAskVisible(dontAsk);
                //return dontAsk;
            }
            //ap.SetRunCountDebug(0) // <- For debugging so the count reset to 0 mdail 7-17-19
            //ap.SetRunCountDebug(21); // <- For debugging so the count can be set without running app 21 times mdail 7-17-19
            //set the run count to track the number of runs on this version mdail 2-27-19
            int run = ap.GetRunCounts();
            int show = ap.GetShowDont();
            ap.SetShowDont();
            //Get the number of days since the saved date.
            int _days = ap.CheckDateEnough();
            //add one to runs because run count is set to 1 on the first run so when the number of runs count is set it needs one added to it to make it more than the number of runs
            //the same is true of the having shown it too many time mdail 3-6-19
            if ((run >= runs && run <= tooMany) || (_days >= days))
            {
                //if this is the first time the ask view is shown, then reset the don't show button count so it is not shown until the number of runs is the same as the number to allow
                //showing the don't ask anymore button - whether to show the don't ask button is handled in the setAskVisible method mdail 3-6-19
                if ((run == runs || (days == _days)) && hasShown == 0)
                {
                    ap.ResetShowDont();
                    show = 0;
                    ap.SetHas_shown();
                }
                if (show < showDont)
                {
                    SetDontAskVisible(false);
                }
                else
                {
                    SetDontAskVisible(true);
                }
                SetAskVisible(yesAsk);
                return yesAsk;
            }
            else if (run > tooMany)
            {
                SetAskVisible(dontAsk);
                return dontAsk;
            }
            //if we get here that means its not time to show the ask so hide it mdail 3-6-19
            SetAskVisible(dontAsk);
            return dontAsk;
        }
        //Sets the Ask view and ask close image button to gone mdail 2-27-19
        private void SetAskVisible(bool show)
        {
            Constants.SHOW_ASK = show;
        }
        //Sets the Ask view and ask close image button to gone mdail 2-27-19
        private void SetDontAskVisible(bool dont)
        {
            Constants.DONT_ASK = dont;
        }
        //get the version string and split it at the periods, get the save version number and see if it is lower than the int value of the
        //first part of the string version, if it is return true else false mdail 2-25-19 -- Updated to C# 8-12-21
        private bool NewLevel()
        {
            string strVn = Constants.APP_VERSION;
            string[] aryVN = strVn.Split('.');
            AppPreferences ap = new();
            int oldVN = ap.GetVersionSaved();
            int newVN = 0;
            try
            {
                newVN = Int32.Parse(aryVN[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Unable to parse " + ex.Message);
            }
            if (oldVN < newVN)
            {
                ap.SaveNewVersion(newVN);
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
