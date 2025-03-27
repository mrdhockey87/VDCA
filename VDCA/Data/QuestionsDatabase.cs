using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using VDCA.Extension;
using VDCA.Models;
using VDCA.Utils;

namespace VDCA.Data
{
    public class QuestionsDatabase
    {
        private SQLiteConnection db;

        private void Open()
        {
            try
            {
                db = new SQLiteConnection(Constants.DBPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.GetType().Name + " QuestionsDatabase Error Open: " + ex.Message);
            }
        }
        private void Close()
        {
            try
            {
                db.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.GetType().Name + " QuestionsDatabase Error Close: " + ex.Message);
            }        
        }
        public async Task LoadCountDataAsync()
        {
            await GetCategoriesAsync();
        }
        public async Task LoadCountDataFlashOnlyAsync()
        {
            await GetCategoriesFlashOnlyAsync();
        }
        public void LoadCountData()
        {
             GetCategories();
        }
        public void LoadCountDataFlashOnly()
        {
            GetCategoriesFlashOnly();
        }
        public int QuestionCount()
        {
            try
            {
                Open();
                return db.ExecuteScalar<int>(Sql.SelectTotalQuestionCount());
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.GetType().Name + " Error in GetCategories error: " + ex.Message);
                return 0;
            }
            finally
            {
                Close();
            }
        }

        //Get the data as list of objects for the Models and convert them to ObservableCollection so they can be bound later mdail 3-31-23
        public void GetCategories()
        {
            try
            {
                List<Categories> categoriesLoad = [];
                Open();
                categoriesLoad = db.Query<Categories>(Sql.CountByCategory(), "");
                Constants.Categories = new ObservableCollection<Categories>(categoriesLoad);
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.GetType().Name + " Error in GetCategories error: " + ex.Message);
            }
            finally
            {
                Close();
            }
        }
        //Get the data as list of objects for the Models and convert them to ObservableCollection so they can be bound later mdail 3-31-23
        public void GetCategoriesFlashOnly()
        {
            try
            {
                List<Categories> categoriesLoad = [];
                Open();
                categoriesLoad = db.Query<Categories>(Sql.CountByCategoryFlashOnly(), "");
                Constants.CategoriesFlash = new ObservableCollection<Categories>(categoriesLoad);
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.GetType().Name + " Error in GetCategories error: " + ex.Message);
            }
            finally
            {
                Close();
            }
        }
        //Get the data async as list of objects for the Models and convert them to ObservableCollection so they can be bound later mdail 3-31-23
        public async Task GetCategoriesAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    List<Categories> categoriesLoad = [];
                    Open();
                    categoriesLoad = db.Query<Categories>(Sql.CountByCategory(), "");
                    Constants.Categories = new ObservableCollection<Categories>(categoriesLoad);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(this.GetType().Name + " Error in GetCategories error: " + ex.Message);
                }
                finally
                {
                    Close();
                }
            });
        }
        //Get the data async as list of objects for the Models and convert them to ObservableCollection so they can be bound later mdail 3-31-23
        public async Task GetCategoriesFlashOnlyAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    List<Categories> categoriesLoad = [];
                    Open();
                    categoriesLoad = db.Query<Categories>(Sql.CountByCategoryFlashOnly(), "");
                    Constants.CategoriesFlash = new ObservableCollection<Categories>(categoriesLoad);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(this.GetType().Name + " Error in GetCategories error: " + ex.Message);
                }
                finally
                {
                    Close();
                }
            });
        }
        public async Task GetSelectCategoryFlashcardQuestions(string sqlPart)
        {
            await Task.Run(async () =>
            {
                try
                {
                    Open();
                    List<Questions> questionsLoad = [];
                    List<Questions> questionsPipeFixed = [];
                    List<Questions> questionsFlashOut = [];
                    questionsLoad = db.Query<Questions>(Sql.GetCategoryQuestions(sqlPart));
                    //Shuffles the answers within each questions and sorts t/f and above etc questions mdail 9-20-23
                    questionsPipeFixed.AddRange(await QuestionsDatabase.FixPipeAsync(questionsLoad));
                    questionsPipeFixed.Shuffle();
                    questionsFlashOut.AddRange(QuestionsDatabase.FixFlashAnswers(questionsPipeFixed));
                    Constants.Questions = new ObservableCollection<Questions>(questionsFlashOut);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(this.GetType().Name + " Error in GetSelectSubjectsCards error: " + ex.Message);
                }
                finally
                {
                    Close();
                }
            });
        }
        public async Task GetSelectCategoryPracticeQuestions(String sqlPart)
        {
            await Task.Run(async () =>
            {
                try
                {
                    Open();
                    List<Questions> questionsLoad = [];
                    List<Questions> questionsShuffled = [];
                    List<Questions> questionsShuffledOut = [];
                    List<Questions> questionsFlashOut = [];
                    questionsLoad = db.Query<Questions>(Sql.GetCategoryQuestions(sqlPart));
                    //Shuffles the answers within each questions and sorts t/f and above etc questions mdail 9-20-23
                    questionsShuffled.AddRange(Utils.ShuffleAnswers.SortAnswers(await QuestionsDatabase.FixPipeAsync(questionsLoad)));
                    //Shuffle the entire array of questions 9-200-23 mdail
                    questionsShuffled.Shuffle();
                    questionsShuffledOut.AddRange(SetShuffledAnswersNumbers(questionsShuffled));
                    questionsFlashOut.AddRange(questionsShuffledOut);
                    Constants.Questions = new ObservableCollection<Questions>(questionsFlashOut);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(this.GetType().Name + " Error in GetSelectSubjectsCards error: " + ex.Message);
                }
                finally
                {
                    Close();
                }
            });
        }
        private static List<Questions> FixFlashAnswers(List<Questions> questions)
        {
            var tasks = questions.Select(question =>
            {
                return Task.Run(() =>
                {
                    if (question != null)
                    {
                        // Assuming FixFlashAnswer is a method that fixes a single question
                        FixFlashAnswer(question);
                    }
                });
            });
            Task.WhenAll(tasks).Wait();
            return questions;
        }

        private static void FixFlashAnswer(Questions question)
        {
            const string ALLANSWERS = "All of these";
            const string SEEEXPLANATION = "; See Explanation";
            const string NONEOFTHESE = "None of these";
            if (question.Answer1 == ALLANSWERS || question.Answer1 == ALLANSWERS + SEEEXPLANATION)
            {
                string answer2 = question.Answer2 == NONEOFTHESE ? "" : question.Answer2;
                string answer3 = question.Answer3 == NONEOFTHESE ? "" : question.Answer3;
                string answer4 = question.Answer4 == NONEOFTHESE ? "" : question.Answer4;
                question.Answer1 = answer2 + (string.IsNullOrEmpty(answer3) ? "" : Environment.NewLine + answer3) +
                                  (string.IsNullOrEmpty(answer4) ? "" : Environment.NewLine + answer4);
            }
        }
        /*
        private static List<Questions> FixFlashAnswers(List<Questions> questions)
        {
            const string ALLANSWERS = "All of these";
            const string SEEEXPLANATION = "; See Explanation";
            const string NONEOFTHESE = "None of these";
            foreach (Questions question in questions)
            {
                if (question.Answer1 == ALLANSWERS || question.Answer1 == ALLANSWERS + SEEEXPLANATION)
                {
                    string answer2 = question.Answer2 == NONEOFTHESE ? "" : question.Answer2;
                    string answer3 = question.Answer3 == NONEOFTHESE ? "" : question.Answer3;
                    string answer4 = question.Answer4 == NONEOFTHESE ? "" : question.Answer4;
                    question.Answer1 = answer2 + (string.IsNullOrEmpty(answer3) ? "" : Environment.NewLine + answer3) +
                                      (string.IsNullOrEmpty(answer4) ? "" : Environment.NewLine + answer4);
                }
            }
            return questions;
        }*/
        public async Task GetSelectQuiz1Questions()
        {
            await Task.Run(async () =>
            {
                try
                {
                    if(Constants.Questions.Count > 0)
                    {
                        Constants.Questions.Clear();
                    }
                    List<Questions> questionsLoad = [];
                    List<Questions> questionsPipeFixed = [];
                    List<Questions> questionsShuffled = [];
                    List<Questions> questionsShuffledOut = [];
                    Open();
                    questionsLoad = db.Query<Questions>(Sql.GetQuiz1Questions());
                    questionsPipeFixed.AddRange(await QuestionsDatabase.FixPipeAsync(questionsLoad));
                    questionsShuffled.AddRange(Utils.ShuffleAnswers.SortAnswers(questionsPipeFixed));
                    questionsShuffled.Shuffle();
                    questionsShuffledOut.AddRange(SetShuffledAnswersNumbers(questionsShuffled));
                    Constants.Questions = new ObservableCollection<Questions>(questionsShuffledOut);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(this.GetType().Name + " Error in GetSelectSubjectsCards error: " + ex.Message);
                }
                finally
                {
                    Close();
                }
            });
        }
        //to make quiz 
        public async Task GetSelectQuiz2Questions()
        {
            await Task.Run(async () =>
            {
                try
                {
                    if (Constants.Questions.Count > 0)
                    {
                        Constants.Questions.Clear();
                    }
                    Random rnd = new();
                    int quiz = rnd.Next(1, 4);
                    List<Questions> questionsLoad = [];
                    List<Questions> questionsPipeFixed = [];
                    List<Questions> questionsShuffled = [];
                    List<Questions> questionsShuffledOut = [];
                    Open();
                    questionsLoad = db.Query<Questions>(Sql.GetQuiz2Questions(quiz));
                    questionsLoad.AddRange(db.Query<Questions>(Sql.GetQuiz2Questions(quiz + 1)));
                    questionsPipeFixed.AddRange(await QuestionsDatabase.FixPipeAsync(questionsLoad));
                    questionsShuffled.AddRange(Utils.ShuffleAnswers.SortAnswers(questionsPipeFixed));
                    questionsShuffled.Shuffle();
                    questionsShuffledOut.AddRange(SetShuffledAnswersNumbers(questionsShuffled));
                    Constants.Questions = new ObservableCollection<Questions>(questionsShuffledOut);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(this.GetType().Name + " Error in GetSelectSubjectsCards error: " + ex.Message);
                }
                finally
                {
                    Close();
                }
            });
        }
        public async Task GetSelectQuiz3Questions()
        {
            await Task.Run(async () =>
            {
                try
                {
                    if (Constants.Questions.Count > 0)
                    {
                        Constants.Questions.Clear();
                    }
                    List<Questions> questionsLoad = [];
                    List<Questions> questionsPipeFixed = [];
                    List<Questions> questionsShuffled = [];
                    List<Questions> questionsShuffledOut = [];
                    Open();
                    questionsLoad = db.Query<Questions>(Sql.GetQuiz3Questions());
                    questionsPipeFixed.AddRange(await QuestionsDatabase.FixPipeAsync(questionsLoad));
                    questionsShuffled.AddRange(Utils.ShuffleAnswers.SortAnswers(questionsPipeFixed));
                    questionsShuffled.Shuffle();
                    questionsShuffledOut.AddRange(SetShuffledAnswersNumbers(questionsShuffled));
                    Constants.Questions = new ObservableCollection<Questions>(questionsShuffledOut);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(this.GetType().Name + " Error in GetSelectSubjectsCards error: " + ex.Message);
                }
                finally
                {
                    Close();
                }
            });
        }
        public async Task GetFlaggedQuestions()
        {
            await Task.Run(async () =>
            {
                try
                {
                    List<Questions> questionsLoad = [];
                    List<Questions> questionsPipeFixed = [];
                    List<Questions> questionsFlashOut = [];
                    Open();
                    questionsLoad = db.Query<Questions>(Sql.GetFlaggedQuestions());
                    questionsPipeFixed.AddRange(await QuestionsDatabase.FixPipeAsync(questionsLoad));
                    questionsPipeFixed.Shuffle();
                    questionsFlashOut.AddRange(QuestionsDatabase.FixFlashAnswers(questionsPipeFixed));
                    Constants.Questions = new ObservableCollection<Questions>(questionsFlashOut);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(this.GetType().Name + " Error in GetSelectSubjectsCards error: " + ex.Message);
                }
                finally
                {
                    Close();
                }
            });
        }
        public async Task GetHiddenQuestions()
        {
            await Task.Run(async () =>
            {
                try
                {
                    List<Questions> questionsLoad = [];
                    List<Questions> questionsPipeFixed = [];
                    List<Questions> questionsFlashOut = [];
                    Open();
                    questionsLoad = db.Query<Questions>(Sql.GetHiddenQuestions());
                    questionsPipeFixed.AddRange(await QuestionsDatabase.FixPipeAsync(questionsLoad));
                    questionsPipeFixed.Shuffle();
                    questionsFlashOut.AddRange(QuestionsDatabase.FixFlashAnswers(questionsPipeFixed));
                    Constants.Questions = new ObservableCollection<Questions>(questionsFlashOut);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(this.GetType().Name + " Error in GetSelectSubjectsCards error: " + ex.Message);
                }
                finally
                {
                    Close();
                }
            });
        }
        static public List<Questions> SetShuffledAnswersNumbers(List<Questions> questionsShuffled)
        {
            try
            {
                Parallel.ForEach(questionsShuffled, question =>
                {
                    int count = question.Answers.Count();
                    question.Question = Util.FirstCharToUpper(question.Question);
                    question.ShuffledAnswer1 = Util.FirstCharToUpper(question.Answers[0].Answer) ?? "";
                    question.ShuffledAnswer1Number = question.Answers[0].AnswerNumber;
#if DEBUG
                    if (question.ShuffledAnswer1Number == 1)
                    {
                        question.ShuffledAnswer1 = "* " + question.ShuffledAnswer1;
                    }
#endif
                    question.ShuffledAnswer2 = Util.FirstCharToUpper(question.Answers[1].Answer) ?? "";
                    question.ShuffledAnswer2Number = question.Answers[1].AnswerNumber;

#if DEBUG
                    if (question.ShuffledAnswer2Number == 1)
                    {
                        question.ShuffledAnswer2 = "* " + question.ShuffledAnswer2;
                    }
#endif
                    if (count > 2)
                    {
                        question.ShuffledAnswer3 = Util.FirstCharToUpper(question.Answers[2].Answer) ?? "";
                        question.ShuffledAnswer3Number = question.Answers[2].AnswerNumber;
#if DEBUG
                        if (question.ShuffledAnswer3Number == 1)
                        {
                            question.ShuffledAnswer3 = "* " + question.ShuffledAnswer3;
                        }
#endif
                    }
                    if (count > 3)
                    {
                        question.ShuffledAnswer4 = Util.FirstCharToUpper(question.Answers[3].Answer) ?? "";
                        question.ShuffledAnswer4Number = question.Answers[3].AnswerNumber;
#if DEBUG
                        if (question.ShuffledAnswer4Number == 1)
                        {
                            question.ShuffledAnswer4 = "* " + question.ShuffledAnswer4;
                        }
#endif
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("QuestionsDatabase Error in SetShuffledAnswers error: " + ex.Message);
            }
            return questionsShuffled;
        }
        public void ClearFlaggedQuestions()
        {
            try
            {
                Open();
                _ = db.Execute(Sql.ClearFlagged(), [""]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.GetType().Name + " Error in ClearFlaggedQuestions error: " + ex.Message);
            }
            finally
            {
                Close();
            }
        }
        public void ClearHiddenQuestions()
        {
            try
            {
                Open();
                _ = db.Execute(Sql.ClearHidden(), [""]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.GetType().Name + " Error in ClearHiddenQuestions error: " + ex.Message);
            }
            finally
            {
                Close();
                try
                {
                    _ = LoadCountDataAsync().ConfigureAwait(false);
                    _ = LoadCountDataFlashOnlyAsync().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(this.GetType().Name + " Error in ClearHiddenQuestions error: " + ex.Message);
                }
            }
        }
        private static async Task<List<Questions>> FixPipeAsync(List<Questions> questions)
        {
            List<Questions> fixedQuestions = [];

            await Parallel.ForEachAsync(questions, async (question, cancellationToken) =>
            {
                question.Question = await Task.Run(() => FixPipeInString(question.Question));
                question.Answer1 = await Task.Run(() => FixPipeInString(question.Answer1));
                question.Answer2 = await Task.Run(() => FixPipeInString(question.Answer2));
                question.Answer3 = await Task.Run(() => FixPipeInString(question.Answer3));
                question.Answer4 = await Task.Run(() => FixPipeInString(question.Answer4));

                fixedQuestions.Add(question);
            });
            return fixedQuestions;
        }
        private static string FixPipeInString(string str)
        {
            return str.Contains('|') ? str.Replace("|", Constants.NEWLINE) : str;
        }

        public void UpdateHideStatus(int _id, int _hide)
        {
            try
            {
                Open();
                _ = db.Execute(Sql.SetHideStatus(_id, _hide), [""]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.GetType().Name + " Error UpdateHideStatus error: " + ex.Message);
            }
            finally
            {
                Close();
            }
        }

        public void UpdateFlagStatus(int _id, int _flag)
        {
            try
            {
                Open();
                _ = db.Execute(Sql.SetFlagStatus(_id, _flag), [""]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.GetType().Name + " Error UpdateFlagStatus error: " + ex.Message);
            }
            finally
            {
                Close();
            }
        }
    }
 }
