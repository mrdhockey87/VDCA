using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using VDCA.Models;
using VDCA.Utils;
using VDCA.Views;

namespace VDCA.ViewModels;

public partial class ReviewQuizzesViewModel : ContentPage
{
    private ObservableCollection<ReviewQuiz> _Quizzes = [];
    private ObservableCollection<Questions> _ReviewQuestions = [];    
    private ReviewQuiz LocalLastReviewQuiz { get; set; }
    public ReviewQuiz LastReviewQuiz
    {
        get
        {

            return LocalLastReviewQuiz;
        }
        set
        {
            if (LocalLastReviewQuiz != value)
            {
                LocalLastReviewQuiz = value;
                OnPropertyChanged(nameof(LastReviewQuiz));
            }
        }
    }
    public ReviewQuizzesView rqv;
    public ICommand ReviewQuestionsCmd;
    public ReviewQuizzesViewModel()
	{
        Constants.LastQuiz ??= new()
            {
                Id = 0,
                Correct = 0,
                TotalQuestions = 0,
                DateTimeTaken = 0,
                DateTaken = "00/00/0000",
                TimeOfDay = "00:00:00 AM",
                GetPercent = Util.CalculateScorePercentage(0, 0),
                ResultColor = Util.GetPercentageColor(0)
            };
        LastReviewQuiz = Constants.LastQuiz;
        Quizzes = Constants.ReviewQuizzes;
        ReviewQuestions = Constants.ReviewQuestions;
    }
    public void SetView(ReviewQuizzesView _rqv)
    {
        rqv = _rqv;
    }

    public ObservableCollection<Questions> ReviewQuestions
    {
        get { return _ReviewQuestions; }
        set
        {
            if (_ReviewQuestions != value)
            {
                _ReviewQuestions = value;
                OnPropertyChanged(nameof(ReviewQuestions));
            }
        }
    }

    public ObservableCollection<ReviewQuiz> Quizzes
    {
        get { return _Quizzes; }
        set
        {
            if (_Quizzes != value)
            {
                _Quizzes = value;
                OnPropertyChanged(nameof(Quizzes));
            }
        }
    }

    public async Task ReviewQuestionClicked()
    {
        if (ReviewQuestions.Count > 0)
        {
            Constants.REVIEW = true;
            await ShowProgressBarUtil.ShowProgressBar(rqv.ProgressBarReiewOverlay);
            Constants.IsREVIEW = true;
            await Shell.Current.GoToAsync("Quiz");
            await ShowProgressBarUtil.HideProgressBar(rqv.ProgressBarReiewOverlay);
        }
        else
        {
            rqv.ShowInformationAlert("No Wrong Questions!", "There are no questions to review!");
        }
    }
}