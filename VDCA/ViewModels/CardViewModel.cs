using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VDCA.Models;
using VDCA.Views;

namespace VDCA.ViewModels
{
    public partial class CardViewModel : BindableObject
    {
        //private static readonly string LOGTAG = "CardViewModel";
        private Questions CurrentQuestionLocal;
        private int CurrentIndexLocal;
        public Func<Task> ReloadCurrentItemAsync { get; set; }
        public CardViewModel()
        {
            CardQuestions = [];
        }
        public virtual void SetView(CardView _cv)
        {
            cv = _cv;
        }
        private ObservableCollection<Questions> CardQuestionsLocal { get; set; }
        public Questions CurrentQuestion
        {
            get => CurrentQuestionLocal;
            set
            {
                if (value != null)
                {
                    if (CurrentQuestionLocal != value)
                    {

                        CurrentQuestionLocal = value;
                        _ = MainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            await cv.OnUpdateProgressCommandExecuted();
                        });
                        OnPropertyChanged();
                    }
                }
            }
        }
        public int CurrentIndex
        {
            get => CurrentIndexLocal;
            set
            {
                if (CurrentIndexLocal != value)
                {

                    CurrentIndexLocal = value;
                    OnPropertyChanged();
                }
            }
        }
        public CardView cv;
        public ObservableCollection<Questions> CardQuestions
        {
            get => CardQuestionsLocal;
            set
            {
                if (value != null)
                {
                    if (CardQuestionsLocal != value)
                    {

                        CardQuestionsLocal = value;
                        OnPropertyChanged();
                    }
                }
            }
        }
        //Added try catch around the method code due to out of index error I got during debugging after leaving the page mdail 10-6-23
        public async Task ReloadCurrentItem()
        {
            try
            {
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    int position = cv.CarouselCardView.Position;
                    if (cv is QuizView qv)
                    {
                        CardQuestions[position].BorderPadding = 0;
                        CardQuestions[position].Question = CardQuestions[position].Question;
                        CardQuestions[position].ShuffledAnswer1 = CardQuestions[position].ShuffledAnswer1;
                        CardQuestions[position].ShuffledAnswer2 = CardQuestions[position].ShuffledAnswer2;
                        if (CardQuestions[position].ShuffledAnswer3.Length > 0)
                        {
                            CardQuestions[position].ShuffledAnswer3 = CardQuestions[position].ShuffledAnswer3;
                        }
                        else
                        {
                            CardQuestions[position].ShuffledAnswer3 = "";
                        }
                        if (CardQuestions[position].ShuffledAnswer4.Length > 0)
                        {
                            CardQuestions[position].ShuffledAnswer4 = CardQuestions[position].ShuffledAnswer4;
                        }
                        else
                        {
                            CardQuestions[position].ShuffledAnswer4 = "";
                        }
                        if (qv.ReviewQuiz || Constants.REVIEW)
                        {
                            CardQuestions[position].Locked = true;
                            //If the AnswerSelected is 0, then this was loaded from the review button on the main page so set the AnswerSelected
                            //using the review value to find where the answer selected is now, else use the AnswerSelected as is.
                            //the other possibility it being 0 is it was skipped which is handled by the inner else statement mdail 2-22-24
                            if (CardQuestions[position].AnswerSelected == 0)
                            {
                                // Find the answer with the matching AnswerNumber
                                var newAnswerSelected = CardQuestions[position].Answers.Find(a =>
                                                                    a.AnswerNumber == CardQuestions[position].Review);
                                // Set the AnswerSelected variable based on the matching AnswerNumber - Set to 5 if no matching answer found
                                int selectedAnswer = newAnswerSelected != default ? newAnswerSelected.AnswerNumber : 5;
                                if (selectedAnswer != 5)
                                {
                                    if (CardQuestions[position].ShuffledAnswer1Number == selectedAnswer)
                                    {
                                        CardQuestions[position].AnswerSelected = 1;
                                    }
                                    else if (CardQuestions[position].ShuffledAnswer2Number == selectedAnswer)
                                    {
                                        CardQuestions[position].AnswerSelected = 2;
                                    }
                                    else if (CardQuestions[position].ShuffledAnswer3Number == selectedAnswer)
                                    {
                                        CardQuestions[position].AnswerSelected = 3;
                                    }
                                    else if (CardQuestions[position].ShuffledAnswer4Number == selectedAnswer)
                                    {
                                        CardQuestions[position].AnswerSelected = 4;
                                    }
                                }
                                else
                                {
                                    CardQuestions[position].Review = 5;
                                    CardQuestions[position].AnswerSelected = 5;
                                }
                            }
                            else
                            {
                                CardQuestions[position].Review = 0;
                                CardQuestions[position].AnswerSelected = CardQuestions[position].AnswerSelected;
                            }
                            CardQuestions[position].Locked = CardQuestions[position].Locked;
                        }
                    }
                    else
                    {
                        //Questions current = (Questions)CurrentQuestion;
                        CardQuestions[position].Locked = CardQuestions[position].Locked;
                        CardQuestions[position].BorderPadding = 0;
                        CardQuestions[position].Question = CardQuestions[position].Question;
                        CardQuestions[position].ShuffledAnswer1 = CardQuestions[position].ShuffledAnswer1;
                        CardQuestions[position].ShuffledAnswer2 = CardQuestions[position].ShuffledAnswer2;
                        if (CardQuestions[position].ShuffledAnswer3.Length > 0)
                        {
                            CardQuestions[position].ShuffledAnswer3 = CardQuestions[position].ShuffledAnswer3;
                        }
                        else
                        {
                            CardQuestions[position].ShuffledAnswer3 = "";
                        }
                        if (CardQuestions[position].ShuffledAnswer4.Length > 0)
                        {
                            CardQuestions[position].ShuffledAnswer4 = CardQuestions[position].ShuffledAnswer4;
                        }
                        else
                        {
                            CardQuestions[position].ShuffledAnswer4 = "";
                        }
                    }
                    await cv.ExecuteRefresh();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Copy database failed ex: " + ex.Message);
            }
        }
    }
}
