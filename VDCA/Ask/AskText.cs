using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VDCA.Ask
{
    public class AskText : INotifyPropertyChanged
    {
        //private readonly string LOGTAG = "AskText";
        public AskText() { }
        //Message for the title of the ask view mdail 9-14-21
        private string titleText = "Enjoying VA Accredited Exam Study Guide?";
        public string TitleText
        {
            get => titleText;
            set
            {
                titleText = value;
                OnPropertyChanged(nameof(TitleText));
            }
        }
        //Message for the message of the ask view mdail 9-14-21
        private string messageText = "We’d love to hear your thoughts on VA Accredited Exam Study Guide!";
        public string MessageText
        {
            get => messageText;
            set
            {
                messageText = value;
                OnPropertyChanged(nameof(MessageText));
            }
        }
        //Message for the yes button of the ask view mdail 9-14-21
        private string yesButtonText = "Love it!";
        public string YesButtonText
        {
            get => yesButtonText;
            set
            {
                yesButtonText = value;
                OnPropertyChanged(nameof(YesButtonText));
            }
        }
        //Message for the no button of the ask view mdail 9-14-21
        private string noButtonText = "Needs work";
        public string NoButtonText
        {
            get => noButtonText;
            set
            {
                noButtonText = value;
                OnPropertyChanged(nameof(NoButtonText));
            }
        }

        public bool DontAsk
        {
            get => Constants.DONT_ASK;
            set
            {
                Constants.DONT_ASK = value;
                OnPropertyChanged(nameof(DontAsk));
            }
        }
        //Added for changing the font size for smaller screens mdail 10-26-21
        private int TheGlyphSize { set; get; } = 10;
        public int GlyphSize
        {
            get => TheGlyphSize;
            set
            {
                if (TheGlyphSize != value)
                {
                    TheGlyphSize = value;
                    OnPropertyChanged(nameof(GlyphSize));
                }
            }
        }
        //Added for changing the font size for smaller screens mdail 10-26-21
        private int TheStandardFont { set; get; } = 16;
        public int StandardFont
        {
            get => TheStandardFont;
            set
            {
                if (TheStandardFont != value)
                {
                    TheStandardFont = value;
                    OnPropertyChanged(nameof(StandardFont));
                }
            }
        }

        private int TheMessageFont { set; get; } = 14;
        public int MessageFont
        {
            get => TheMessageFont;
            set
            {
                if (TheMessageFont != value)
                {
                    TheMessageFont = value;
                    OnPropertyChanged(nameof(MessageFont));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void ResetAskText()
        {
            TitleText = "Enjoying VA Accredited Exam Study Guide?";
            MessageText = "We’d love to hear your thoughts on VA Accredited Exam Study Guide!";
            YesButtonText = "Love it!";
            NoButtonText = "Needs work";
        }

        public void SetAskTextYes()
        {
            TitleText = "Great! Would you like to leave us a review!";
            MessageText = "Your ratings and reviews can make a big difference to our disable veteran-owned small business!";
            YesButtonText = "Rate Now!";
            NoButtonText = "Not Now";
        }

        public void SetAskTextNo()
        {
            TitleText = "Would like to send us feedback on what we could do better?";
            MessageText = "We appreciate your feedback to help fix bugs, errors, and improvements.";
            YesButtonText = "Send Us Feedback?";
            NoButtonText = "Not Now";
        }
        private void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
