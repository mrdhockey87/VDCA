using SQLite;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VDCA.Extension;
using VDCA.Models;

namespace VDCA.Data
{
    public class ReviewDatabase : IDisposable
    {
        private SQLiteConnection db;
        private SQLiteAsyncConnection dba;
        private bool _disposed = false;
        public ReviewDatabase() { }
        private void Open()
        {
            try
            {
                var options = new SQLiteConnectionString(Constants.DBPath, true, key: Constants.COPYRIGHT);
                db = new SQLiteConnection(options);
                //db = new SQLiteConnection(Constants.DBPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.GetType().Name + " QuestionsDatabase Error Open: " + ex.Message);
            }
        }
        private void Close()
        {
            db.Close();
            db.Dispose();
        }

        public async Task SetQuestionReviewStatusBatchAsync(List<(int QuestionId, int ReviewStatus)> updates)
        {
            try
            {
                // Ensure database is initialized
                //await db.CreateTableAsync<Questions>().ConfigureAwait(false);
                var options = new SQLiteConnectionString(Constants.DBPath, true, key: Constants.COPYRIGHT);
                dba = new SQLiteAsyncConnection(options);
                // Begin a transaction for all updates
                await dba.RunInTransactionAsync(tran =>
                {
                    foreach (var (QuestionId, ReviewStatus) in updates)
                    {
                        var command = tran.CreateCommand(Sql.SetReviewQuestions(QuestionId, ReviewStatus), [""]);
                        // Execute the statement with new values
                        command.ExecuteNonQuery();
                    }
                }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.GetType().Name + " Error in SetQuestionReviewStatusBatchAsync error: " + ex.Message);
            }
            finally
            {
                dba?.CloseAsync().Wait();
            }
        }
        /*public void SetQuestionReviewStatus(int _id, int _review)
        {
            try
            {
                Open();
                _ = db.ExecuteAsync(Sql.SetReviewQuestions(_id, _review), [""]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.GetType().Name + " Error SetQuestionReviewStatus error: " + ex.Message);
            }
            finally
            {
                Close();
            }
        }*/
        //Converted to use the regular long sql string and moved it to the Sql class mdail 9-21-23
        public async Task<string> AddReviewRecord(ReviewQuiz rq)
        {
            int rowID = -1;
            await Task.Run(async () =>
            {
                try
                {
                    Open();
                    rowID = db.Execute(Sql.EnterReviewRecords(rq), [""]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(this.GetType().Name + " Error in AddReviewRecord error: " + ex.Message);
                }
                finally
                {
                    Close();
                }
            });
            return rowID.ToString();
        }
        public void ClearReviewTable()
        {
            //ClearReviewTable()
            try
            {
                Open();
                _ = db.Execute(Sql.ClearReviewTable(), [""]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.GetType().Name + " Error SetQuestionReviewStatus error: " + ex.Message);
            }
            finally
            {
                Close();
            }
        }
        public async Task ClearReviewQuestions()
        {
            await Task.Run(() =>
            {
                try
                {
                    Open();
                    _ = db.Execute(Sql.ClearReviewQuestions(), [""]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(this.GetType().Name + " Error in ClearReviewQuestions error: " + ex.Message);
                }
                finally
                {
                    Close();
                }
            });
        }

        public async Task ClearAllReviewQuestions()
        {
            await Task.Run(() =>
            {
                try
                {
                    Open();
                    _ = db.Execute(Sql.ClearReviewQuestions(), [""]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(this.GetType().Name + " Error in ClearReviewQuestions error: " + ex.Message);
                }
                finally
                {
                    Close();
                }
            });
        }
        public async Task ClearAllReview()
        {
            await Task.Run(() =>
            {
                try
                {
                    Open();
                    _ = db.Execute(Sql.ClearReviewTable(), [""]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(this.GetType().Name + " Error in ClearReviewQuestions error: " + ex.Message);
                }
                finally
                {
                    Close();
                }
            });
        }
        public async Task GetReviewQuizzes()
        {
            await Task.Run(async () =>
            {
                try
                {
                    Open();
                    List<ReviewQuiz> reviewLoaded = [];
                    List<Questions> questionsPipeFixed = [];
                    List<Questions> questionsShuffled = [];
                    List<Questions> questionsShuffledOut = [];
                    reviewLoaded = db.Query<ReviewQuiz>(Sql.GetReviewQuizzes());
                    if (reviewLoaded.Count > 0)
                    {
                        ReviewDatabase.SetLastQuiz(reviewLoaded[0]);
                        reviewLoaded.RemoveAt(0);
                        Constants.ReviewQuizzes = [.. reviewLoaded];
                        List<Questions> reviewCardsLoaded = [];
                        reviewCardsLoaded = db.Query<Questions>(Sql.GetReviewQuestions());
                        questionsShuffled.AddRange(Utils.ShuffleAnswers.SortAnswers(ReviewDatabase.FixPipe(reviewCardsLoaded)));
                        questionsShuffled.Shuffle();
                        questionsShuffledOut.AddRange(QuestionsDatabase.SetShuffledAnswersNumbers(questionsShuffled));
                        Constants.ReviewQuestions = [.. questionsShuffledOut];

                    }
                    else
                    {
                        Constants.ReviewQuizzes = [];
                        Constants.ReviewQuestions = [];
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(this.GetType().Name + " Error getting reviews error: " + ex.Message);
                }
                finally
                {
                    Close();
                }
            });
        }
        private static void SetLastQuiz(ReviewQuiz reviewQuiz)
        {
            Constants.LastQuiz = new()
            {
                Id = reviewQuiz.Id,
                DateTaken = reviewQuiz.DateTaken,
                TimeOfDay = reviewQuiz.TimeOfDay,
                Correct = reviewQuiz.Correct,
                TotalQuestions = reviewQuiz.TotalQuestions,
                DateTimeTaken = reviewQuiz.DateTimeTaken,
                TotalTime = reviewQuiz.TotalTime
            };
        }

        private static List<Questions> FixPipe(List<Questions> Questions)
        {
            const string pipe = "|";
            foreach (Questions questions in Questions)
            {
                if (questions.Answer1.Contains(pipe))
                {
                    string fixedAnswer1 = questions.Answer1.Replace(pipe, Constants.NEWLINE);
                    questions.Answer1 = fixedAnswer1;
                }
                if (questions.Answer2.Contains(pipe))
                {
                    string fixedAnswer2 = questions.Answer2.Replace(pipe, Constants.NEWLINE);
                    questions.Answer2 = fixedAnswer2;
                }
                if (questions.Answer3.Contains(pipe))
                {
                    string fixedAnswer3 = questions.Answer3.Replace(pipe, Constants.NEWLINE);
                    questions.Answer3 = fixedAnswer3;
                }
                if (questions.Answer4.Contains(pipe))
                {
                    string fixedAnswer4 = questions.Answer4.Replace(pipe, Constants.NEWLINE);
                    questions.Answer4 = fixedAnswer4;
                }
                if (questions.Question.Contains(pipe))
                {
                    string fixedQuestion = questions.Question.Replace(pipe, Constants.NEWLINE);
                    questions.Question = fixedQuestion;
                }
            }
            return Questions;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (db != null)
                    {
                        db.Close();
                        db.Dispose();
                    }
                    if (dba != null)
                        dba?.CloseAsync().Wait();
                }

                _disposed = true;
            }
        }
    }
}
