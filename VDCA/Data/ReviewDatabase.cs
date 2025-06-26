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
                        questionsPipeFixed.AddRange(await ReviewDatabase.FixPipeAsync(reviewCardsLoaded));
                        questionsShuffled.AddRange(Utils.ShuffleAnswers.SortAnswers(questionsPipeFixed));
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
        private static async Task<List<Questions>> FixPipeAsync(List<Questions> questions)
        {
            List<Questions> fixedQuestions = [];
            object lockObj = new();
            await Parallel.ForEachAsync(questions, async (question, cancellationToken) =>
            {
                question.Question = await Task.Run(() => FixPipeInString(question.Question));
                question.Answer1 = await Task.Run(() => FixPipeInString(question.Answer1));
                question.Answer2 = await Task.Run(() => FixPipeInString(question.Answer2));
                question.Answer3 = await Task.Run(() => FixPipeInString(question.Answer3));
                question.Answer4 = await Task.Run(() => FixPipeInString(question.Answer4));
                lock (lockObj)
                {
                    fixedQuestions.Add(question);
                }
            });
            return fixedQuestions;
        }
        private static string FixPipeInString(string str)
        {
            return str.Contains('|') ? str.Replace("|", Constants.NEWLINE) : str;
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
