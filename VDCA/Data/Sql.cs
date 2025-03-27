using System;
using VDCA.Models;

namespace VDCA.Data
{
    public static class Sql
    {
        public static string SelectTotalQuestionCount()
        {
            return "SELECT count(*) from VAQuestions Where Category NOT IN('Dates','Definitions');";
        }
        public static string CountByCategory()
        {
            return "SELECT VAQuestions.Category, Count(*) AS count FROM VAQuestions WHERE VAQuestions.Hidden = 0 AND Category NOT IN('Dates','Definitions') GROUP BY VAQuestions.Category ORDER BY VAQuestions.Category;";
        }
        public static string CountByCategoryFlashOnly()
        {
            return "SELECT VAQuestions.Category, Count(*) AS count FROM VAQuestions WHERE VAQuestions.Hidden = 0 GROUP BY VAQuestions.Category ORDER BY VAQuestions.Category;";
        }
        //returns category records for selected cat mdail 7-12-18
        public static string GetCategoryQuestions(String sqlPart)
        {
            return "Select * from VAQuestions WHERE Hidden = 0" + sqlPart + " ORDER BY Category;";
        }
        //returns every 3rd question with the number starting at the random number from 1 to 3 to get a random 1/3 of the questions mdail 6-20-23
        public static string GetQuiz1Questions()
        {
            Random rnd = new();
            int quiz = rnd.Next(1, 4);
            return string.Format("SELECT * FROM (SELECT *, row_number() over() rn FROM VAQuestions) Questions WHERE Category NOT IN('Dates','Definitions') AND (Questions.rn - {0}) % 3 = 0;", quiz);
        }
        //returns every 3rd question with the number starting at the random number sent to it which is do twice to get a random 2/3rd of the questions mdail 6-20-23
        public static string GetQuiz2Questions(int quiz)
        {
            return string.Format("SELECT * FROM (SELECT *, row_number() over() rn FROM VAQuestions) Questions WHERE Category NOT IN('Dates','Definitions') AND (Questions.rn - {0}) % 3 = 0;", quiz);
        }
        //returns all of the questions in the database mdail 6-20-23
        public static string GetQuiz3Questions()
        {
            return "Select * from VAQuestions Where Category NOT IN('Dates','Definitions');";
        }

        //Set flagged question mdail 7-12-18
        public static string SetFlagStatus(int _id, int _flag)
        {
            return "Update VAQuestions set Flagged = " + _flag + " where id = " + _id + ";";
        }
        //Set flagged question mdail 7-12-18
        public static string SetHideStatus(int _id, int _hide)
        {
            return "Update VAQuestions set Hidden = " + _hide + " where id = " + _id + ";";
        }
        public static string GetFlaggedQuestions()
        {
            return "Select * from VAQuestions where Flagged = 1;";
        }
        //returns the questions that are hidden mdail 7-23-18
        public static string GetHiddenQuestions()
        {
            return "Select * from VAQuestions where Hidden = 1;";
        }
        //Clear flagged question mdail 7-12-18
        public static string ClearFlagged()
        {
            return "Update VAQuestions set Flagged = 0;";
        }
        //Clear flagged question mdail 7-12-18
        public static string ClearHidden()
        {
            return "Update VAQuestions set Hidden = 0;";
        }
        //Clear flagged question mdail 7-12-18
        public static string ClearReviewQuestions()
        {
            return "Update VAQuestions set Review = 0;";
        }

        //the sql string to enter a review record into the database mdail 8-30-18
        public static string InsertNewReview(string date, int correct, int total)
        {
            return "INSERT INTO review (date, correct, total, elapsed_time) VALUES ('" + date + "', " + correct + ", " + total + ");";
        }
        //the sql string to enter a review record into the database mdail 8-30-18
        public static string ClearReviewTable()
        {
            return "DELETE FROM review;";
        }
        //the sql string to enter a review record into the database mdail 8-30-18
        public static string GetReviewQuizzes()
        {
            return "select * from review order by id Desc;";
        }
        //Set flagged question mdail 7-12-18
        public static string SetReviewQuestions(int _id, int _review)
        {
            return "Update VAQuestions set Review = " + _review + " where id = " + _id + ";";
        }
        //returns the questions that are review mdail 7-23-18
        public static string GetReviewQuestions()
        {
            return "Select * from VAQuestions where Review > 0;";
        }
        //returns the string to isert a neew review record mdail 7-23-18
        public static string EnterReviewRecords(ReviewQuiz rq)
        {
            return "insert into review (date, time, correct, total, datetime, totaltime) values ('" + rq.DateTaken + "', '" + rq.TimeOfDay
                                       + "', " + rq.Correct + ", " + rq.TotalQuestions + ", " + rq.DateTimeTaken + ", '" + rq.TotalTime + "');";
        }
    }
}
