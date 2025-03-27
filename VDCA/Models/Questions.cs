using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VDCA.Models
{
    [Table("VAQuestions")]
    public partial class Questions : INotifyPropertyChanged
    {
        public Questions() { }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Access_ID { get; set; }
        [Ignore]
        private string Local_Question { get; set; }
        public string Question
        {
            set
            {
                Local_Question = value;
                OnPropertyChanged(nameof(Question));
            }
            get
            {
                {
                    return Local_Question ?? "";
                }
            }
        }
        [Ignore]
        private string Local_Answer1 { get; set; } = "";
        public string Answer1
        {
            set
            {
                Local_Answer1 = value;
                OnPropertyChanged(nameof(Answer1));
            }
            get
            {
                {
                    return Local_Answer1 ?? "";
                }
            }
        }

        private Thickness _MarginTop;
        public Thickness MarginTop
        {
            get => _MarginTop;
            set
            {
                _MarginTop = value;
                OnPropertyChanged(nameof(MarginTop));
            }
        }
        [Ignore]
        private string Local_Answer2 { get; set; } = "";
        public string Answer2
        {
            set
            {
                Local_Answer2 = value;
                OnPropertyChanged(nameof(Answer2));
            }
            get
            {
                return Local_Answer2 ?? "";
            }
        }
        [Ignore]
        private string Local_Answer3 { get; set; } = "";
        public string Answer3 {
            set
            {
                Local_Answer3 = value;
                OnPropertyChanged(nameof(Answer3));
            }
            get
            {
                return Local_Answer3 ?? "";
            }
        }
        [Ignore]
        private string Local_Answer4 { get; set; } = "";
        public string Answer4 {
            set
            {
                Local_Answer4 = value;
                OnPropertyChanged(nameof(Answer4));
            }
            get
            {
                return Local_Answer4 ?? "";
            }
        }
        public string Symbol { get; set; }
        private string Local_Citation { get; set; }
        public string Citation
        {
            set
            {
                Local_Citation = value;
                OnPropertyChanged(nameof(Citation));
            }
            get
            {
                {
                    return Local_Citation ?? "";
                }
            }
        }
        public string Category { get; set; }
        private string Local_Explanation { get; set; }
        public string Explanation
        {
            set
            {
                Local_Explanation = value;
                OnPropertyChanged(nameof(Explanation));
            }
            get
            {
                {
                    return Local_Explanation ?? "";
                }
            }
        }
        //hold the shuffled sorted answer and their answer number mdail 6-9-23
        public List<(string Answer, int AnswerNumber)> Answers = [];
        [Ignore]
        private string Local_ShuffledAnswer1 { set; get; }
        [Ignore]
        public string ShuffledAnswer1
        {
            set
            {
                Local_ShuffledAnswer1 = value;
                OnPropertyChanged(nameof(ShuffledAnswer1));
            }
            get
            {
                {
                    return Local_ShuffledAnswer1 ?? "";
                }
            }
        }
        [Ignore]
        private string Local_ShuffledAnswer2 { set; get; }
        [Ignore]
        public string ShuffledAnswer2
        {
            set
            {
                Local_ShuffledAnswer2 = value;
                OnPropertyChanged(nameof(ShuffledAnswer2));
            }
            get
            {
                return Local_ShuffledAnswer2 ?? "";
            }
        }
        [Ignore]
        private string Local_ShuffledAnswer3 { set; get; }
        [Ignore]
        public string ShuffledAnswer3
        {
            set
            {
                Local_ShuffledAnswer3 = value;
                IsCVisible = !string.IsNullOrEmpty(ShuffledAnswer3);
                OnPropertyChanged(nameof(ShuffledAnswer3));
            }
            get
            {
                return Local_ShuffledAnswer3 ?? "";
            }
        }
        [Ignore]
        private string Local_ShuffledAnswer4 { set; get; }
        [Ignore]
        public string ShuffledAnswer4
        {
            set
            {
                Local_ShuffledAnswer4 = value;
                IsDVisible = !string.IsNullOrEmpty(ShuffledAnswer4);
                OnPropertyChanged(nameof(ShuffledAnswer4));
            }
            get
            {
                return Local_ShuffledAnswer4 ?? "";
            }
        }

        private bool Local_IsCVisible { set; get; }
        [Ignore]
        public bool IsCVisible
        {
            set
            {
                Local_IsCVisible = value;
                OnPropertyChanged(nameof(IsCVisible));
            }
            get
            {
                return !string.IsNullOrEmpty(ShuffledAnswer3);
            }
        }
        [Ignore]
        private bool Local_IsDVisible { set; get; }
        [Ignore]
        public bool IsDVisible
        {
            set
            {
                Local_IsDVisible = value;
                OnPropertyChanged(nameof(IsDVisible));
            }
            get
            {
                return !string.IsNullOrEmpty(ShuffledAnswer4);
            }
        }
        [Ignore]
        private int Local_ShuffledAnswer1Number { set; get; }
        [Ignore]
        public int ShuffledAnswer1Number
        {
            set
            {
                if (value != Local_ShuffledAnswer1Number)
                {
                    Local_ShuffledAnswer1Number = value;
                    OnPropertyChanged(nameof(ShuffledAnswer1Number));
                }
            }
            get
            {
                {
                    return Local_ShuffledAnswer1Number;
                }
            }
        }
        [Ignore]
        private int Local_ShuffledAnswer2Number { set; get; }
        [Ignore]
        public int ShuffledAnswer2Number
        {
            set
            {
                if (value != Local_ShuffledAnswer2Number)
                {
                    Local_ShuffledAnswer2Number = value;
                    OnPropertyChanged(nameof(ShuffledAnswer2Number));
                }
            }
            get
            {
                return Local_ShuffledAnswer2Number;
            }
        }
        [Ignore]
        private int Local_ShuffledAnswer3Number { set; get; }
        [Ignore]
        public int ShuffledAnswer3Number
        {
            set
            {
                if (value != Local_ShuffledAnswer3Number)
                {
                    Local_ShuffledAnswer3Number = value;
                    OnPropertyChanged(nameof(ShuffledAnswer3Number));
                }
            }
            get
            {
                return Local_ShuffledAnswer3Number;
            }
        }
        [Ignore]
        private int Local_ShuffledAnswer4Number { set; get; }
        [Ignore]
        public int ShuffledAnswer4Number
        {
            set
            {
                if (value != Local_ShuffledAnswer4Number)
                {
                    Local_ShuffledAnswer4Number = value;
                    OnPropertyChanged(nameof(ShuffledAnswer4Number));
                }
            }
            get
            {
                return Local_ShuffledAnswer4Number;
            }
        }
        [Ignore]
        public int NumberOfAnswer { get; set; }
        private bool AnsweredLocal { get; set; } = false;
        [Ignore]
        public bool Answered
        {
            get
            {
                return AnsweredLocal;
            }
            set
            {
                if (AnsweredLocal != value)
                {
                    AnsweredLocal = value;
                    OnPropertyChanged(nameof(Answered));
                }
            }
        }
        private int Local_WasPicked { get; set; }
        public int WasPicked
        {
            get
            {
                return Local_WasPicked;
            }
            set
            {
                if (Local_WasPicked != value)
                {
                    Local_WasPicked = value;
                    OnPropertyChanged(nameof(WasPicked));
                }
            }
        }
        public int Review { get; set; }
        private int AnswerSelectedLocal { get; set; } = 0;
        public int AnswerSelected
        {
            get
            {
                return AnswerSelectedLocal;
            }
            set
            {
                AnswerSelectedLocal = value;
                if (AnswerSelectedLocal == 0)
                {
                    //SetCarouselViewsDefault();
                }
                OnPropertyChanged(nameof(AnswerSelected));
            }
        }
        private int Local_Hidden { set; get; } = 0;
        public int Hidden
        {
            get
            {
                return Local_Hidden;
            }
            set
            {
                if (Local_Hidden != value)
                {
                    Local_Hidden = value;
                    OnPropertyChanged(nameof(Hidden));
                }
            }
        }
        private int Local_Flagged { set; get; } = 0;
        public int Flagged
        {
            get
            {
                return Local_Flagged;
            }
            set
            {
                if (Local_Flagged != value)
                {
                    Local_Flagged = value;
                    OnPropertyChanged(nameof(Flagged));
                }
            }
        }
        private bool FlippedLocal { set; get; } = false;
        [Ignore]
        public bool Flipped
        {
            get
            {
                return FlippedLocal;
            }
            set
            {
                if (FlippedLocal != value)
                {
                    FlippedLocal = value;
                    OnPropertyChanged(nameof(Flipped));
                }
            }
        }
        private bool CorrectLocal { set; get; } = false;
        [Ignore]
        public bool Correct
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
        private bool IncorrectLocal { get; set; } = false;
        [Ignore]
        public bool Incorrect
        {
            get
            {
                return IncorrectLocal;
            }
            set
            {
                if (IncorrectLocal != value)
                {
                    IncorrectLocal = value;
                    OnPropertyChanged(nameof(Incorrect));
                }
            }
        }
        private int CardindexLocal { get; set; } = 0;
        [Ignore]
        public int CardIndex
        {
            get => Constants.Current_Index;
            set
            {
                if (CardindexLocal != value)
                {
                    Constants.Current_Index = value;
                    CardindexLocal = Constants.Current_Index;
                    OnPropertyChanged(nameof(CardIndex));
                }
            }
        }

        private int CardtotalLocal = Constants.Questions.Count;
        [Ignore]
        public int CardTotal
        {
            get
            {
                return CardtotalLocal;
            }

            set
            {
                if (Constants.Questions.Count != value)
                {
                    CardtotalLocal = Constants.Questions.Count;
                }
            }
        }

        private string LocalFlipTextColor { set; get; } = "#000080";
        [Ignore]
        public string FlipTextColor
        {
            get => LocalFlipTextColor;
            set
            {
                if (LocalFlipTextColor != value)
                {
                    LocalFlipTextColor = value;
                    OnPropertyChanged(nameof(FlipTextColor));
                }
            }
        }
        private string LocalFlipBackColor { set; get; } = "#ffffff";
        [Ignore]
        public string FlipBackColor
        {
            get => LocalFlipBackColor;
            set
            {
                if (LocalFlipBackColor != value)
                {
                    LocalFlipBackColor = value;
                    OnPropertyChanged(nameof(FlipBackColor));
                }
            }
        }
        private bool LockedLocal { set; get; } = false;
        [Ignore]
        public bool Locked
        {
            get
            {
                return LockedLocal;
            }
            set
            {
                if (LockedLocal != value || Constants.REVIEW)
                {
                    LockedLocal = value;
                    if (LockedLocal)
                    {
                        CheckAnswer();
                    }
                    else
                    {
                        SetCarouselViewsDefault();
                        CheckAnswer();
                    }
                    OnPropertyChanged(nameof(Locked));
                }
            }
        }        
        private string Local_LockedBackgroundColor { set; get; } = Constants.PrimaryTextColor;
        public string LockedBackgroundColor
        {
            get
            {
                return Local_LockedBackgroundColor;
            }
            set
            {
                if (Local_LockedBackgroundColor != value)
                {
                    Local_LockedBackgroundColor = value;
                    OnPropertyChanged(nameof(LockedBackgroundColor));
                }
            }
        }
        private string Local_LabelText { set; get; } = Constants.PracticeSelectMessage;
        public string LabelText
        {
            get
            {
                return Local_LabelText;
            }
            set
            {
                if (Local_LabelText != value)
                {
                    Local_LabelText = value;
                    OnPropertyChanged(nameof(LabelText));
                }
            }
        }
        private int LabelFontSizeLocal { set; get; } = Constants.InstructionLabelNormalFontSize;
        public int LabelFontSize
        {
            get
            {
                return LabelFontSizeLocal;
            }
            set
            {
                if (LabelFontSizeLocal != value)
                {
                    LabelFontSizeLocal = value;
                    OnPropertyChanged(nameof(LabelFontSize));
                }
            }
        }
        private string LabelTextColorLocal { set; get; } = Constants.PrimaryTextColor;
        public string LabelTextColor
        {
            get
            {
                return LabelTextColorLocal;
            }
            set
            {
                if (LabelTextColorLocal != value)
                {
                    LabelTextColorLocal = value;
                    OnPropertyChanged(nameof(LabelTextColor));
                }
            }
        }
        private bool LocalASelected { set; get; } = false;
        [Ignore]
        public bool ASelected
        {
            get
            {
                return LocalASelected;
            }
            set
            {
                if (LocalASelected != value)
                {
                    LocalASelected = value;
                    if (LocalASelected)
                    {
                        BSelected = false;
                        CSelected = false;
                        DSelected = false;
                        AnswerSelected = 1;
                        SetCarouselViewsA();
                    }
                    OnPropertyChanged(nameof(ASelected));
                }
            }
        }
        private bool LocalBSelected { set; get; } = false;
        [Ignore]
        public bool BSelected
        {
            get
            {
                return LocalBSelected;
            }
            set
            {
                if (LocalBSelected != value)
                {
                    LocalBSelected = value;
                    if (LocalBSelected)
                    {
                        ASelected = false;
                        CSelected = false;
                        DSelected = false;
                        AnswerSelected = 2;
                        SetCarouselViewsB();
                    }
                    OnPropertyChanged(nameof(BSelected));
                }
            }
        }
        private bool LocalCSelected { set; get; } = false;
        [Ignore]
        public bool CSelected
        {
            get
            {
                return LocalCSelected;
            }
            set
            {
                if (LocalCSelected != value)
                {
                    LocalCSelected = value;
                    if (LocalCSelected)
                    {
                        ASelected = false;
                        BSelected = false;
                        DSelected = false;
                        AnswerSelected = 3;
                        SetCarouselViewsC();
                    }
                    OnPropertyChanged(nameof(CSelected));
                }
            }
        }
        private bool LocalDSelected { set; get; } = false;
        [Ignore]
        public bool DSelected
        {
            get
            {
                return LocalDSelected;
            }
            set
            {
                if (LocalDSelected != value)
                {
                    LocalDSelected = value;
                    if (LocalDSelected)
                    {
                        ASelected = false;
                        BSelected = false;
                        CSelected = false;
                        AnswerSelected = 4;
                        SetCarouselViewsD();
                    }
                    OnPropertyChanged(nameof(DSelected));
                }
            }
        }
        private string LocalAImage { set; get; } = Constants.ANormalImage;
        [Ignore]
        public string AImage
        {
            get
            {
                return LocalAImage;
            }
            set
            {
                if (LocalAImage != value)
                {
                    LocalAImage = value;
                    OnPropertyChanged(nameof(AImage));
                }
            }
        }
        private string LocalBImage { set; get; } = Constants.BNormalImage;
        [Ignore]
        public string BImage
        {
            get
            {
                return LocalBImage;
            }
            set
            {
                if (LocalBImage != value)
                {
                    LocalBImage = value;
                    OnPropertyChanged(nameof(BImage));
                }
            }
        }
        private string LocalCImage { set; get; } = Constants.CNormalImage;
        [Ignore]
        public string CImage
        {
            get
            {
                return LocalCImage;
            }
            set
            {
                if (LocalCImage != value)
                {
                    LocalCImage = value;
                    OnPropertyChanged(nameof(CImage));
                }
            }
        }
        private string LocalDImage { set; get; } = Constants.DNormalImage;
        [Ignore]
        public string DImage
        {
            get
            {
                return LocalDImage;
            }
            set
            {
                if (LocalDImage != value)
                {
                    LocalDImage = value;
                    OnPropertyChanged(nameof(DImage));
                }
            }
        }
        private string LocalATextColor { set; get; } = Constants.PrimaryTextColor;
        [Ignore]
        public string ATextColor
        {
            get
            {
                return LocalATextColor;
            }
            set
            {
                if (LocalATextColor != value)
                {
                    LocalATextColor = value;
                    OnPropertyChanged(nameof(ATextColor));
                }
            }
        }
        private string LocalBTextColor { set; get; } = Constants.PrimaryTextColor;
        [Ignore]
        public string BTextColor
        {
            get
            {
                return LocalBTextColor;
            }
            set
            {
                if (LocalBTextColor != value)
                {
                    LocalBTextColor = value;
                    OnPropertyChanged(nameof(BTextColor));
                }
            }
        }
        private string LocalCTextColor { set; get; } = Constants.PrimaryTextColor;
        [Ignore]
        public string CTextColor
        {
            get
            {
                return LocalCTextColor;
            }
            set
            {
                if (LocalCTextColor != value)
                {
                    LocalCTextColor = value;
                    OnPropertyChanged(nameof(CTextColor));
                }
            }
        }
        private string LocalDTextColor { set; get; } = Constants.PrimaryTextColor;
        [Ignore]
        public string DTextColor
        {
            get
            {
                return LocalDTextColor;
            }
            set
            {
                if (LocalDTextColor != value)
                {
                    LocalDTextColor = value;
                    OnPropertyChanged(nameof(DTextColor));
                }
            }
        }
        private string LocalABackColor { set; get; } = Constants.PrimaryBackgroundColor;
        [Ignore]
        public string ABackColor
        {
            get
            {
                return LocalABackColor;
            }
            set
            {
                if (LocalABackColor != value)
                {
                    LocalABackColor = value;
                    OnPropertyChanged(nameof(ABackColor));
                }
            }
        }
        private string LocalBBackColor { set; get; } = Constants.PrimaryBackgroundColor;
        [Ignore]
        public string BBackColor
        {
            get
            {
                return LocalBBackColor;
            }
            set
            {
                if (LocalBBackColor != value)
                {
                    LocalBBackColor = value;
                    OnPropertyChanged(nameof(BBackColor));
                }
            }
        }
        private string LocalCBackColor { set; get; } = Constants.PrimaryBackgroundColor;
        [Ignore]
        public string CBackColor
        {
            get
            {
                return LocalCBackColor;
            }
            set
            {
                if (LocalCBackColor != value)
                {
                    LocalCBackColor = value;
                    OnPropertyChanged(nameof(CBackColor));
                }
            }
        }
        private string LocalDBackColor { set; get; } = Constants.PrimaryBackgroundColor;
        [Ignore]
        public string DBackColor
        {
            get
            {
                return LocalDBackColor;
            }
            set
            {
                if (LocalDBackColor != value)
                {
                    LocalDBackColor = value;
                    OnPropertyChanged(nameof(DBackColor));
                }
            }
        }
        private int BorderPaddingLocal { set; get; } = 8;
        [Ignore]
        public int BorderPadding
        {
            get
            {
                return BorderPaddingLocal;
            }
            set
            {
                if (BorderPaddingLocal != value)
                {
                    BorderPaddingLocal = value;
                    OnPropertyChanged(nameof(BorderPadding));
                }
            }
        }
        private void SetCarouselViewsDefault()
        {
            if (!Locked)
            {
                ANormalColors();
                BNormalColors();
                CNormalColors();
                DNormalColors();
            }
            else
            {
                CheckAnswer();
            }
        }
        private void SetCarouselViewsA()
        {
            if (!Locked)
            {
                ASelectedColors();
                BNormalColors();
                CNormalColors();
                DNormalColors();
            }
            else
            {
                CheckAnswer();
            }
        }
        private void SetCarouselViewsB()
        {
            if (!Locked)
            {
                ANormalColors();
                BSelectedColors();
                CNormalColors();
                DNormalColors();
            }
            else
            {
                CheckAnswer();
            }
        }
        private void SetCarouselViewsC()
        {
            if (!Locked)
            {
                ANormalColors();
                BNormalColors();
                CSelectedColors();
                DNormalColors();
            }
            else
            {
                CheckAnswer();
            }
        }
        private void SetCarouselViewsD()
        {
            if (!Locked)
            {
                ANormalColors();
                BNormalColors();
                CNormalColors();
                DSelectedColors();
            }
            else
            {
                CheckAnswer();
            }
        }
        private void CheckAnswer()
        {
            switch (AnswerSelected)
            {
                case 1:
                    if (ShuffledAnswer1Number == 1)
                    {
                        APassColors();
                        BNormalColors();
                        CNormalColors();
                        DNormalColors();
                        InstructionLabelPass();
                    }
                    else
                    {
                        AFailColors();
                        InstructionLabelFail();
                        if (ShuffledAnswer2Number == 1)
                        {
                            BPassColors();
                        }
                        else
                        {
                            BNormalColors();
                        }
                        if (ShuffledAnswer3Number == 1)
                        {
                            CPassColors();
                        }
                        else
                        {
                            CNormalColors();
                        }
                        if (ShuffledAnswer4Number == 1)
                        {
                            DPassColors();
                        }
                        else
                        {
                            DNormalColors();
                        }
                    }
                    break;
                case 2:
                    if (ShuffledAnswer2Number == 1)
                    {
                        ANormalColors();
                        BPassColors();
                        CNormalColors();
                        DNormalColors();
                        InstructionLabelPass();
                    }
                    else
                    {
                        BFailColors();
                        InstructionLabelFail();
                        if (ShuffledAnswer1Number == 1)
                        {
                            APassColors();
                        }
                        else
                        {
                            ANormalColors();
                        }
                        if (ShuffledAnswer3Number == 1)
                        {
                            CPassColors();
                        }
                        else
                        {
                            CNormalColors();
                        }
                        if (ShuffledAnswer4Number == 1)
                        {
                            DPassColors();
                        }
                        else
                        {
                            DNormalColors();
                        }
                    }
                    break;
                case 3:
                    if (ShuffledAnswer3Number == 1)
                    {
                        ANormalColors();
                        BNormalColors();
                        CPassColors();
                        DNormalColors();
                        InstructionLabelPass();
                    }
                    else
                    {
                        CFailColors();
                        InstructionLabelFail();
                        if (ShuffledAnswer1Number == 1)
                        {
                            APassColors();
                        }
                        else
                        {
                            ANormalColors();
                        }
                        if (ShuffledAnswer2Number == 1)
                        {
                            BPassColors();
                        }
                        else
                        {
                            BNormalColors();
                        }
                        if (ShuffledAnswer4Number == 1)
                        {
                            DPassColors();
                        }
                        else
                        {
                            DNormalColors();
                        }
                    }
                    break;
                case 4:
                    if (ShuffledAnswer4Number == 1)
                    {
                        ANormalColors();
                        BNormalColors();
                        CNormalColors();
                        DPassColors();
                        InstructionLabelPass();
                    }
                    else
                    {
                        DFailColors();
                        InstructionLabelFail();
                        if (ShuffledAnswer1Number == 1)
                        {
                            APassColors();
                        }
                        else
                        {
                            ANormalColors();
                        }
                        if (ShuffledAnswer2Number == 1)
                        {
                            BPassColors();
                        }
                        else
                        {
                            BNormalColors();
                        }
                        if (ShuffledAnswer3Number == 1)
                        {
                            CPassColors();
                        }
                        else
                        {
                            CNormalColors();
                        }
                    }
                    break;
                case 5:
                    if (ShuffledAnswer1Number == 1)
                    {
                        APassColors();
                    }
                    else
                    {
                        ANormalColors();
                    }
                    if (ShuffledAnswer2Number == 1)
                    {
                        BPassColors();
                    }
                    else
                    {
                        BNormalColors();
                    }
                    if (ShuffledAnswer3Number == 1)
                    {
                        CPassColors();
                    }
                    else
                    {
                        CNormalColors();
                    }
                    if (ShuffledAnswer4Number == 1)
                    {
                        DPassColors();
                    }
                    else
                    {
                        DNormalColors();
                    }
                    InstructionLabelSkippedFail();
                    break;
                default:
                    ANormalColors();
                    BNormalColors();
                    CNormalColors();
                    DNormalColors();
                    InstructionLabelNormal();
                    break;
            }
        }
        private void InstructionLabelNormal()
        {
            if (Constants.FROM_QUIZ)
            {
                LabelText = Constants.QuizSelectMessage;
            }
            else
            {
                LabelText = Constants.PracticeSelectMessage;
            }
            LabelFontSize = Constants.InstructionLabelNormalFontSize;
            LabelTextColor = Constants.PrimaryTextColor;
        }
        private void InstructionLabelFail()
        {
            LabelText = Constants.IncorrectMessage;
            LabelFontSize = Constants.InstructionLabelAnsweredFontSize;
            LabelTextColor = Constants.FailColor;
        }
        private void InstructionLabelPass()
        {
            LabelText = Constants.CorrectMessage;
            LabelFontSize = Constants.InstructionLabelAnsweredFontSize;
            LabelTextColor = Constants.PassColor;
        }
        private void InstructionLabelSkippedFail()
        {
            LabelText = Constants.SkippedMessage;
            LabelFontSize = Constants.InstructionLabelNormalFontSize;
            LabelTextColor = Constants.FailColor;
        }
        private void ANormalColors()
        {
            AImage = Constants.ANormalImage;
            ATextColor = Constants.PrimaryTextColor;
            ABackColor = Constants.PrimaryBackgroundColor;
        }
        private void ASelectedColors()
        {
            AImage = Constants.ASelectedImage;
            ATextColor = Constants.PrimaryBackgroundColor;
            ABackColor = Constants.PrimaryTextColor;
        }
        private void AFailColors()
        {
            AImage = Constants.ASelectedImage;
            ATextColor = Constants.PrimaryBackgroundColor;
            ABackColor = Constants.FailColor;
        }
        private void APassColors()
        {
            AImage = Constants.ASelectedImage;
            ATextColor = Constants.PrimaryBackgroundColor;
            ABackColor = Constants.PassColor;
        }
        private void BNormalColors()
        {
            BImage = Constants.BNormalImage;
            BTextColor = Constants.PrimaryTextColor;
            BBackColor = Constants.PrimaryBackgroundColor;
        }
        private void BSelectedColors()
        {
            BImage = Constants.BSelectedImage;
            BTextColor = Constants.PrimaryBackgroundColor;
            BBackColor = Constants.PrimaryTextColor;
        }
        private void BFailColors()
        {
            BImage = Constants.BSelectedImage;
            BTextColor = Constants.PrimaryBackgroundColor;
            BBackColor = Constants.FailColor;
        }
        private void BPassColors()
        {
            BImage = Constants.BSelectedImage;
            BTextColor = Constants.PrimaryBackgroundColor;
            BBackColor = Constants.PassColor;
        }
        private void CNormalColors()
        {
            CImage = Constants.CNormalImage;
            CTextColor = Constants.PrimaryTextColor;
            CBackColor = Constants.PrimaryBackgroundColor;
        }
        private void CSelectedColors()
        {
            CImage = Constants.CSelectedImage;
            CTextColor = Constants.PrimaryBackgroundColor;
            CBackColor = Constants.PrimaryTextColor;
        }
        private void CFailColors()
        {
            CImage = Constants.CSelectedImage;
            CTextColor = Constants.PrimaryBackgroundColor;
            CBackColor = Constants.FailColor;
        }
        private void CPassColors()
        {
            CImage = Constants.CSelectedImage;
            CTextColor = Constants.PrimaryBackgroundColor;
            CBackColor = Constants.PassColor;
        }
        private void DNormalColors()
        {
            DImage = Constants.DNormalImage;
            DTextColor = Constants.PrimaryTextColor;
            DBackColor = Constants.PrimaryBackgroundColor;
        }
        private void DSelectedColors()
        {
            DImage = Constants.DSelectedImage;
            DTextColor = Constants.PrimaryBackgroundColor;
            DBackColor = Constants.PrimaryTextColor;
        }
        private void DFailColors()
        {
            DImage = Constants.DSelectedImage;
            DTextColor = Constants.PrimaryBackgroundColor;
            DBackColor = Constants.FailColor;
        }
        private void DPassColors()
        {
            DImage = Constants.DSelectedImage;
            DTextColor = Constants.PrimaryBackgroundColor;
            DBackColor = Constants.PassColor;
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
