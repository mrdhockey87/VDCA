using Microsoft.Maui.Controls;
using System.Windows.Input;
using VDCA.Utils;

namespace VDCA.ViewModels;

public partial class ReviewQuestionModel : BindableObject
{
    public static ICommand DummyReviewCommand => new Command(() => { /* Does nothing */ });
    public ReviewQuestionModel()
    {
        if (Constants.LastQuiz is not null)
        {
            Review_Correct = Constants.LastQuiz.Correct;
            Review_Date = Constants.LastQuiz.DateTaken;
            Review_Time = Constants.LastQuiz.TimeOfDay;
            Review_Total = Constants.LastQuiz.TotalQuestions;
            Review_TotalTime = Constants.LastQuiz.TotalTime;
            Review_DateTime = Constants.LastQuiz.DateTimeTaken;
        }
    }
    private int Review_Correct_Local { set; get; } = 0;
    public int Review_Correct
    {
        get => Review_Correct_Local;
        set
        {
            if (value != Review_Correct_Local)
            {
                Review_Correct_Local = value;
                OnPropertyChanged(nameof(Review_Correct));
            }
        }
    }
    private string Review_Date_Local { set; get; } = "";
    public string Review_Date
    {
        get => Review_Date_Local;
        set
        {
            if (value != Review_Date_Local)
            {
                Review_Date_Local = value;
                OnPropertyChanged(nameof(Review_Date));
            }
        }
    }
    private string Review_Time_Local { set; get; } = "00:00 AM";
    public string Review_Time
    {
        get => Review_Time_Local;
        set
        {
            if (value != Review_Time_Local)
            {
                Review_Time_Local = value;
                OnPropertyChanged(nameof(Review_Time));
            }
        }
    }
    private int Review_Total_Local { set; get; } = 0;
    public int Review_Total
    {
        get => Review_Total_Local;
        set
        {
            if (value != Review_Total_Local)
            {
                Review_Total_Local = value;
                int percent = Util.CalculateScorePercentage(Review_Correct, Review_Total);
                Review_Percent = percent;
                OnPropertyChanged(nameof(Review_Percent));
                Review_CorrectOfTotal = $"{Review_Correct} of {Review_Total} question";
                OnPropertyChanged(nameof(Review_CorrectOfTotal));
                Review_String_Percent = $"{Util.CalculateScorePercentage(Review_Correct, Review_Total)}%";
                OnPropertyChanged(nameof(Review_String_Percent));
                string color = Util.GetPercentageColor(Review_Percent).ToHex();
                Review_Result_Color = color;
                OnPropertyChanged(nameof(Review_Result_Color));
                OnPropertyChanged(nameof(Review_Total));
            }
        }
    }

    private string Review_TotalTime_Local { set; get; } = "00:00";
    public string Review_TotalTime
    {
        get => Review_TotalTime_Local;
        set
        {
            if (value != Review_TotalTime_Local)
            {
                Review_TotalTime_Local = value;
                OnPropertyChanged(nameof(Review_TotalTime));
            }
        }
    }
    private int Review_DateTime_Local { set; get; } = 0;
    public int Review_DateTime
    {
        get => Review_DateTime_Local;
        set
        {
            Review_DateTime_Local = value;
            OnPropertyChanged(nameof(Review_Total));
            OnPropertyChanged(nameof(Review_Percent));
            if (Review_CorrectOfTotal is not null)
            {
                OnPropertyChanged(nameof(Review_CorrectOfTotal));
            }
            if (Review_Result_Color is not null) 
            { 
                OnPropertyChanged(nameof(Review_Result_Color));
            }
            if (Review_TotalTime is not null)
            {
                OnPropertyChanged(nameof(Review_TotalTime));
            }
            OnPropertyChanged(nameof(Review_DateTime));
        }
    }
    private int Review_Percent_Local { set; get; } = 0;
    public int Review_Percent //=> Util.CalculateScorePercentage(Review_Correct, Review_Total);
    {
        get
        {
            //Review_Percent_Local = Util.CalculateScorePercentage(Review_Correct, Review_Total);
            return Review_Percent_Local;
        } 
        set
        {
            //Review_Percent_Local = Util.CalculateScorePercentage(Review_Correct, Review_Total);
            Review_Percent_Local = value;
            OnPropertyChanged(nameof(Review_Percent));
        }
    }
    private string Review_CorrectOfTotal_Local { set; get; } = "0 of 0 question";
    public string Review_CorrectOfTotal
    {
        get
        {
            //Review_CorrectOfTotal_Local = $"{Review_Correct} of {Review_Total} question";
            return Review_CorrectOfTotal_Local;
        }
        set
        {
            //Review_CorrectOfTotal_Local = $"{Review_Correct} of {Review_Total} question";
            Review_CorrectOfTotal_Local = value;
            OnPropertyChanged(nameof(Review_CorrectOfTotal));

        }
    }   
    private string Review_String_Percent_Local { set; get; } = "0%";
    public string Review_String_Percent
    {
        //} => $"{Util.CalculateScorePercentage(Review_Correct, Review_Total)}%";
        get
        {
            //Review_String_Percent_Local = $"{Util.CalculateScorePercentage(Review_Correct, Review_Total)}%";
            return Review_String_Percent_Local;
        }
        set
        {
            //Review_String_Percent_Local = $"{Util.CalculateScorePercentage(Review_Correct, Review_Total)}%";
            Review_String_Percent_Local = value;
            OnPropertyChanged(nameof(Review_String_Percent));

        }
    }
    private string Review_Result_Color_Local { set; get; } = Constants.PrimaryTextColor;
    public string Review_Result_Color
    {
        get 
        {
            //Review_Result_Color_Local = Util.GetPercentageColor(Review_Percent).ToHex();
            return Review_Result_Color_Local;
        }
        set
        {
            //Review_Result_Color_Local = Util.GetPercentageColor(Review_Percent).ToHex();
            Review_Result_Color_Local = value;
            OnPropertyChanged(nameof(Review_Result_Color));

        }
    }
}
