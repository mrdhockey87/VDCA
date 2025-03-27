using System.Collections.Generic;
using System.Collections.ObjectModel;

using VDCA.Views;

namespace VDCA.Ask
{
    public static class AskConstants
    {
        public static int ASK_COUNT = -1;
        public static List<AskText> atList = new()
        {
            new AskText { StandardFont = 16, MessageFont = 14, GlyphSize = 16 }
        };
        public static ObservableCollection<AskText> askText = new();
        public static MainPage mpForAsk;
        public static string yesButtonTextAsk = "Love it!";
        public static string yesButtonTextYes = "Rate Now!";
        public static string yesButtonTextNo = "Send Feedback";
        public static string noButtonTextAsk = "Needs work";
        public static string noButtonTextYes = "Not Now";
        public static string noButtonTextNo = "Not Now";
        public static string askTitleAsk = "Enjoying VA Accredited Exam Study Guide?";
        public static string askTitleYes = "Great! Would you like to leave us a review!";
        public static string askTitleNo = "Would like to send us feedback on what we could do better?";
        public static string askMessageessageAsk = "We’d love to hear your thoughts on VA Accredited Exam Study Guide!";
        public static string askMessageessageYes = "Your ratings and reviews can make a big difference to our disable veteran-owned small business!";
        public static string askMessageessageNo = "We appreciate your feedback to help fix bugs, errors, and improvements.";
        //sets Constants show ask to either show or hide the ask view  also setting ask count to 0 to set the ask view text mdail 8-18-21
        public static void SetShowHideAsk(bool show)
        {
            if (Constants.DONTSHOW)
            {
                Constants.SHOW_ASK = false;
                ASK_COUNT = -1;
            }
            else
            {
                Constants.SHOW_ASK = show;
                if (Constants.SHOW_ASK)
                {
                    ASK_COUNT = 0;
                }
                else
                {
                    ASK_COUNT = -1;
                }
            }
        }
    }
}
