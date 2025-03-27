using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VDCA.Data;
using VDCA.Models;

namespace VDCA.Utils
{
    public static class UpdateQuestionFileds
    {
        public static async Task HideItem(Questions question)
        {
            int hide = question.Hidden;
            question.Hidden = hide == 0 ? 1 : 0;
            Constants.HiddenTotal = hide == 0 ? ++Constants.HiddenTotal : --Constants.HiddenTotal;
            if (Constants.HiddenTotal < 0)
            {
                Constants.HiddenTotal = 0;
            }
            await Task.Run(() =>
            {
                QuestionsDatabase db = new();
                try
                {
                    db.UpdateHideStatus(question.Id, question.Hidden);
                    _ = db.LoadCountDataAsync().ConfigureAwait(false);
                    _ = db.LoadCountDataFlashOnlyAsync().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating hide status error: " + ex.Message);
                }
            });
        }
        //set the flag in the cards array and then in the database mdail 8-10-21
        public static async Task FlagItem(Questions question)
        {
            int flag = question.Flagged;
            question.Flagged = flag == 0 ? 1 : 0;
            Constants.FlaggedTotal = flag == 0 ? Constants.FlaggedTotal += 1 : Constants.FlaggedTotal -= 1;
            if (Constants.FlaggedTotal < 0)
            {
                Constants.FlaggedTotal = 0;
            }
            await Task.Run(() =>
            {
                QuestionsDatabase db = new();
                try
                {
                    db.UpdateFlagStatus(question.Id, question.Flagged);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating flag status error: " + ex.Message);
                }
            });
        }
    }
}
