using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDCA.Models;

namespace VDCA.CustomControl
{
    public static class SupportDataVersion5
    {
        public static List<SupportPagesData> SupportPagesDataList = new List<SupportPagesData>
        {
            new() {
                PageName = SupportedPages.MainPage,
                ControlExplainationText = "Click this for the Menu with more options.",
                ControlName =  "FlyoutIcon",
                ArrowDirection = [ArrowDirection.UpLeft],
                VersionNumber = 5
            },
            new() {
                PageName = SupportedPages.MainPage,
                ControlExplainationText = "Select the way you want to study or review past quizzes.",
                ControlName = "quiz_button",
                ArrowDirection = [ArrowDirection.Up,ArrowDirection.Down],
                VersionNumber = 5
            },
            new() {
                PageName = SupportedPages.FlashcardView,
                ControlExplainationText = "This is the quiz page",
                ArrowDirection = [ArrowDirection.UpLeft],
                VersionNumber = 5
            },
            new() {
                PageName = SupportedPages.PracticecardView,
                ControlExplainationText = "This is the review page",
                ArrowDirection = [ArrowDirection.UpLeft],
                VersionNumber = 5
            },
            new() {
                PageName = SupportedPages.QuizView,
                ControlExplainationText = "This is the quiz page",
                ArrowDirection = [ArrowDirection.UpLeft],
                VersionNumber = 5
            }
        };
    }
}
