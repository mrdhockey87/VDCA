using Microsoft.Maui.Graphics;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VDCA.Utils;

namespace VDCA.Models
{
    [Table("review")]
    public partial class ReviewQuiz : INotifyPropertyChanged
    {
        //private readonly string LOGTAG = "ReviewQuiz";
        
        private int IdLocal = 0;
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id
        {
            get
            {
                return IdLocal;
            }
            set
            {
                if (IdLocal != value)
                {
                    IdLocal = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }
        private string DateTakenLocal = "";
        [Column("date")]
        public string DateTaken
        {
            get
            {
                return DateTakenLocal;
            }
            set
            {
                if (DateTakenLocal != value)
                {
                    DateTakenLocal = value;
                    OnPropertyChanged(nameof(DateTaken));
                }
            }
        }
        private string TimeOfDayLocal = "";
        [Column("time")]
        public string TimeOfDay
        {
            get
            {
                return TimeOfDayLocal;
            }
            set
            {
                if (TimeOfDayLocal != value)
                {
                    TimeOfDayLocal = value;
                    OnPropertyChanged(nameof(TimeOfDay));
                }
            }
        }
        private int CorrectLocal = 0;
        [Column("correct")]
        public int Correct
        {
            get
            {
                return CorrectLocal;
            }
            set
            {
                if (CorrectLocal != value)
                {
                    CorrectLocal = value;
                    OnPropertyChanged(nameof(Correct));
                }
            }
        }
        private int TotalLocal = 0;
        [Column("total")]
        public int TotalQuestions

        {
            get
            {
                return TotalLocal;
            }
            set
            {
                if (TotalLocal != value)
                {
                    TotalLocal = value;
                    OnPropertyChanged(nameof(TotalQuestions));
                }
            }
        }
        private int DateTimeLocal = 0;
        [Column("datetime")]
        public int DateTimeTaken
        {
            get
            {
                return DateTimeLocal;
            }
            set
            {
                if (DateTimeLocal != value)
                {
                    DateTimeLocal = value;
                    OnPropertyChanged(nameof(DateTimeTaken));
                }
            }
        }
        private string DateAndTimeLocal;
        [Ignore]
        public string DateAndTime
        {
            get
            {
                DateAndTimeLocal = DateTaken + " " + TimeOfDay;
                return DateAndTimeLocal;
            }
            set
            {
                if (DateAndTimeLocal != value)
                {
                    DateAndTimeLocal = DateTaken + " " + TimeOfDay;
                    OnPropertyChanged(nameof(DateAndTime));
                }
            }
        }
        private string TotalTimeLocal = "";
        [Column("totaltime")]
        public string TotalTime
        {
            get
            {
                return TotalTimeLocal;
            }
            set
            {
                if (TotalTimeLocal != value)
                {
                    TotalTimeLocal = value;
                    OnPropertyChanged(nameof(TotalTime));
                }
            }
        }
        private string TotalTimeLabelLocal;
        [Ignore]
        public string TotalTimeLabel
        {
            get
            {
                TotalTimeLabelLocal = "Total time: " +  TotalTime;
                return TotalTimeLabelLocal;
            }
            set
            {
                if (TotalTimeLabelLocal != value)
                {
                    TotalTimeLabelLocal = "Total time: " + TotalTime;
                    OnPropertyChanged(nameof(TotalTimeLabel));
                }
            }
        }
        private string CorrectOfTotalLocal;
        [Ignore]
        public string CorrectOfTotal
        {
            get
            {
                CorrectOfTotalLocal = Correct + " of " + TotalQuestions + " question";
                return CorrectOfTotalLocal;
            }
            set
            {
                if (CorrectOfTotalLocal != value)
                {
                    CorrectOfTotalLocal = Correct + " of " + TotalQuestions + " question";
                    OnPropertyChanged(nameof(CorrectOfTotal));
                }
            }
        }
        private int GetPercentLocal { get; set; }
        [Ignore]
        public int GetPercent
        {
            get
            {
                GetPercentLocal = Util.CalculateScorePercentage(Correct, TotalQuestions);
                return GetPercentLocal;
            }
            set
            {
                if (GetPercentLocal != value)
                {
                    GetPercentLocal = Util.CalculateScorePercentage(Correct, TotalQuestions);
                    OnPropertyChanged(nameof(GetPercent));
                }
            }
        }
        private string StringPercentLocal { get; set; }
        [Ignore]
        public string StringPercent
        {
            get
            {
                StringPercentLocal = Util.CalculateScorePercentage(Correct, TotalQuestions) + "%";
                return StringPercentLocal;
            }
            set
            {
                if (StringPercentLocal != value)
                {
                    StringPercentLocal = Util.CalculateScorePercentage(Correct, TotalQuestions) + "%";
                    OnPropertyChanged(nameof(StringPercent));
                }
            }
        }
        private Color ResultColorLocal { get; set; }
        [Ignore]
        public Color ResultColor
        {
            get
            {
                ResultColorLocal = Util.GetPercentageColor(GetPercent);
                return ResultColorLocal;
            }
            set
            {
                if (ResultColorLocal != value)
                {
                    ResultColorLocal = Util.GetPercentageColor(GetPercent);
                    OnPropertyChanged(nameof(ResultColor));
                }
            }
        }
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
