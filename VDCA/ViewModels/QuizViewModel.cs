using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using VDCA.Data;
using VDCA.Models;
using VDCA.Utils;
using VDCA.Views;

namespace VDCA.ViewModels;

public partial class QuizViewModel : CardViewModel
{
    public QuizView qv;
    public int answered = 0;
    public int skipped = 0;
    public int total_questions = 0;
    public int correct = 0;
    private bool isTimerRunning = false;
    private DateTime startTime;
    private TimeSpan elapsedTime;
    private Timer timer;
    public String TotalElapsedTime;
    public string ElapsedTime => elapsedTime.ToString(@"hh\:mm\:ss");
    private bool isPaused = false;
    private TimeSpan pausedElapsedTime = TimeSpan.Zero;
    private DateTime pauseStartTime;
    private int LastSelectedAnswer;
    private bool scoring_answer = false;
    public ICommand StartTimerCommand { get; }
    public ICommand StopTimerCommand { get; }
    new public event EventHandler<PropertyChangedEventArgs> PropertyChanged;
    public QuizViewModel()
    {
        Constants.FROM_QUIZ = true;
        ReloadCurrentItemAsync = ReloadCurrentItem;
        StartTimerCommand = new Command(StartTimer);
        StopTimerCommand = new Command(StopTimer);
    }
    public override void SetView(CardView cv)
    {
        if (cv is QuizView _qv)
        {
            qv = _qv;
            if (qv.ReviewQuiz || Constants.REVIEW)
            {
                CardQuestions = [.. Constants.ReviewQuestions];
            }
            else
            {
                CardQuestions = [.. Constants.Questions];
            }
            total_questions = CardQuestions.Count;
            skipped = total_questions;
            StartTimer();
            this.cv = qv;
        }
        else
        {
            base.SetView(cv); // Call the base implementation if needed
        }
    }
    private void StartTimer()
    {
        if (!isTimerRunning)
        {
            if (isPaused)
            {
                // Adjust start time based on paused time
                startTime += DateTime.Now - pauseStartTime;
                isPaused = false;
            }
            else
            {
                startTime = DateTime.Now;
            }
            isTimerRunning = true;
            timer = new Timer(UpdateElapsedTime, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }
    }
    private async void UpdateElapsedTime(object state)
    {
        await Task.Run(async () =>
        {
            if (isTimerRunning && !isPaused)
            {
                elapsedTime = DateTime.Now - startTime - pausedElapsedTime;
                if (qv != null && qv.ElapsedTimeLabel != null)
                {
                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        qv.ElapsedTimeLabel.Text = ElapsedTime;
                    });
                }
                Dispatcher.Dispatch(() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ElapsedTime))));
            }
        });
    }
    private void PauseTimer()
    {
        if (isTimerRunning && !isPaused)
        {
            isPaused = true;
            pauseStartTime = DateTime.Now;
        }
    }
    private void ResumeTimer()
    {
        if (isTimerRunning && isPaused)
        {
            pausedElapsedTime += DateTime.Now - pauseStartTime;
            isPaused = false;
        }
    }
    private void StopTimer()
    {
        if (isTimerRunning)
        {
            isTimerRunning = false;
            TotalElapsedTime = ElapsedTime;
            timer?.Dispose();
        }
    }
    public async Task AnswerPressed(string SelectedButton)
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            //scoring_answer is set to false at the end of SetEnabled method to keep from having 2 clicks
            //running at the same time and messing up the scoring mdail 10-31-23
            int position = qv.CarouselCardView.Position;
            if (!scoring_answer)
            {
                scoring_answer = true;
                LastSelectedAnswer = CardQuestions[position].AnswersSelected;
                switch (SelectedButton)
                {
                    case "AnswerA":
                        CardQuestions[position].ASelected = true;
                        LetterAPressed();
                        break;
                    case "AnswerB":
                        CardQuestions[position].BSelected = true;
                        LetterBPressed();
                        break;
                    case "AnswerC":
                        CardQuestions[position].CSelected = true;
                        LetterCPressed();
                        break;
                    case "AnswerD":
                        CardQuestions[position].DSelected = true;
                        LetterDPressed();
                        break;
                    default:
                        break;
                    case "Lock":
                        break;
                }
                await ReloadCurrentItem();
                SetEnabled();
            }
        });
    }
    private void LetterAPressed()
    {
        if (!CardQuestions[qv.CarouselCardView.Position].Answered)
        {
            SetAnswered(1);
        }
        else
        {
            CheckWasCorrect(1);
            SetReAnswered(1);
        }
    }
    private void LetterBPressed()
    {
        if (!CardQuestions[qv.CarouselCardView.Position].Answered)
        {
            SetAnswered(2);
        }
        else
        {
            CheckWasCorrect(2);
            SetReAnswered(2);
        }
    }
    private void LetterCPressed()
    {
        if (!CardQuestions[qv.CarouselCardView.Position].Answered)
        {
            SetAnswered(3);
        }
        else
        {
            CheckWasCorrect(3);
            SetReAnswered(3);
        }
    }
    private void LetterDPressed()
    {
        if (!CardQuestions[qv.CarouselCardView.Position].Answered)
        {
            SetAnswered(4);
        }
        else
        {
            CheckWasCorrect(4);
            SetReAnswered(4);
        }
    }
    private void SetAnswered(int answerNumber)
    {
        int position = qv.CarouselCardView.Position;
        CardQuestions[position].Answered = true;
        answered++;
        skipped--;
        CardQuestions[position].AnswersSelected = answerNumber;
        int idxCount = CardQuestions[position].Answers.Count;
        int chkIdx = answerNumber - 1;
        if (idxCount > chkIdx && chkIdx >= 0)
        {
            if (CardQuestions[position].Answers[chkIdx].AnswerNumber == 1)
            {
                correct++;
            }
        }
    }
    private void SetReAnswered(int answerNumber)
    {
        if (LastSelectedAnswer != answerNumber)
        {
            int position = qv.CarouselCardView.Position;
            CardQuestions[position].AnswersSelected = answerNumber;
            int idxCount = CardQuestions[position].Answers.Count;
            int chkIdx = answerNumber - 1;
            if (idxCount > chkIdx && chkIdx >= 0)
            {
                if (CardQuestions[position].Answers[chkIdx].AnswerNumber == 1)
                {
                    correct++;
                }
            }
        }
    }
    private void CheckWasCorrect(int answerNumber)
    {
        if (LastSelectedAnswer != answerNumber)
        {
            int position = qv.CarouselCardView.Position;
            int idxCount = CardQuestions[position].Answers.Count;
            int chkIdx = LastSelectedAnswer - 1;
            if (idxCount > chkIdx && chkIdx >= 0)
            {
                int WasCorrect = CardQuestions[position].Answers[chkIdx].AnswerNumber;
                if (WasCorrect == 1)
                {
                    correct--;
                }
            }
        }
    }
    private void SetEnabled()
    {
        scoring_answer = false;
    }
    public async Task ScoreQuizPressed()
    {
        PauseTimer();
        // Await the user's response
        bool answer = await qv.ShowQuestionAlertAsync(Constants.ScoreQuizTitle, Constants.ScoreQuizMessage);
        if (answer)
        {
            StopTimer();
            qv.ScoreQuiz.IsEnabled = false;
            await ShowProgressBarUtil.ShowProgressBar(qv.ProgressBarQuizOverlay);
            ReviewDatabase db = new();
            await db.ClearReviewQuestions();
            db.Dispose();
            await ProcessQuestionsInBackgroundAsync();
            DateTime dt = DateTime.Now;
            const string date = "dd/MM/yyyy";
            const string time = "hh:mm:ss tt";
            Constants.LastQuiz = new()
            {
                DateTaken = DateTime.Now.ToString(date),
                TimeOfDay = DateTime.Now.ToString(time),
                Correct = correct,
                TotalQuestions = total_questions,
                DateTimeTaken = (int)(dt - new DateTime(1970, 1, 1)).TotalMilliseconds,
                TotalTime = TotalElapsedTime
            };
            _ = await db.AddReviewRecord(Constants.LastQuiz);
            await ProcessQuestionsInBackgroundAsync();
            if (Constants.LastQuiz != null)
            {
                await ShowProgressBarUtil.HideProgressBar(qv.ProgressBarQuizOverlay);
                bool result = await qv.ShowReviewQuestionAsync();
                if (result is bool boolResult)
                {
                    if (boolResult)
                    {
                        CardQuestions[qv.CarouselCardView.Position].Locked = true;
                        await ReloadCurrentItem();
                        if (boolResult)
                        {
                            qv.Title = Constants.ReviewQuizTitle;
                        }
                    }
                    else
                    {
                        // Navigate back to the MainPage and clear the navigation stack
                        await Shell.Current.GoToAsync("..");
                    }
                }
            }
            else
            {
                await ShowProgressBarUtil.HideProgressBar(qv.ProgressBarQuizOverlay);
                await qv.ShowInformationAlert("Error!", "An error occurred while scoring and saving this quiz!");
                await Shell.Current.GoToAsync("Information", Constants.NavigationParameters);
            }
        }
        else
        {
            ResumeTimer();
        }
    }
    public async Task ProcessQuestionsInBackgroundAsync()
    {
        await Task.Run(async () =>
        {
            // Create a list to batch the updates
            var questionsToUpdate = new List<(int QuestionId, int ReviewStatus)>();

            // Process all the questions first without DB access
            foreach (Questions _question in CardQuestions)
            {
                int answerSelected = -1;
                int index = _question.AnswersSelected - 1;
                if (index >= 0 && index < _question.Answers.Count)
                {
                    answerSelected = _question.Answers[index].AnswerNumber;
                }
                if (_question.Answered && answerSelected != 1)
                {
                    _question.Review = answerSelected;
                    questionsToUpdate.Add((_question.Id, _question.Review));
                }
                if (!_question.Answered)
                {
                    _question.AnswersSelected = 5;
                    _question.Review = _question.AnswersSelected;
                    questionsToUpdate.Add((_question.Id, _question.Review));
                }
            }

            // Now perform a single batch database operation
            using ReviewDatabase db = new();
            await db.SetQuestionReviewStatusBatchAsync(questionsToUpdate);
            db.Dispose();
        });
    }
    /*
    public async Task ProcessQuestionsInBackgroundAsync()
    {
        await Task.Run(() =>
        {
            ReviewDatabase db = new();
            foreach (Questions _question in CardQuestions)
            {
                int answerSelected = -1;
                int index = _question.AnswersSelected - 1;
                if (index >= 0 && index < _question.Answers.Count)
                {
                    answerSelected = _question.Answers[index].AnswerNumber;
                }
                if (_question.Answered && answerSelected != 1)
                {
                    _question.Review = answerSelected;
                    db.SetQuestionReviewStatus(_question.Id, _question.Review);
                }
                if (!_question.Answered)
                {
                    _question.AnswersSelected = 5;
                    _question.Review = _question.AnswersSelected;
                    db.SetQuestionReviewStatus(_question.Id, _question.Review);
                }
            }
        });
    }*/
}