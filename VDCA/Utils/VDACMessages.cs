using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System.Threading.Tasks;
using VDCA.Models;
using VDCA.Popups;
using VDCA.SendEmails;

namespace VDCA.Utils
{
    public static class VDCAMessages
    {

        public static async Task ShowToast(VisualElement parent, string message)
        {
            // Check if the current platform is Windows
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                // For Windows, show the custom toast
                await ShowCustomToastAsync(parent, message);
            }
            else
            {
                // For other platforms, use the standard toast method
                await GenerateToast(message);
            }
        }
        public static Layout FindParentLayout(VisualElement element)
        {
            var parent = element.Parent;
            while (parent != null)
            {
                if (parent is Layout layout)
                {
                    return layout;
                }
                parent = parent.Parent;
            }
            return null; // No parent Layout found
        }
        public static async Task GenerateToast(string message)
        {
            double fontSize = 14;
            var toast = CommunityToolkit.Maui.Alerts.Toast.Make(message, ToastDuration.Long, fontSize);
            await toast.Show();
        }
        public static async Task ShowCustomToastAsync(VisualElement parent, string message, int duration = 1000)
        {
            var parentLayout = FindParentLayout(parent);
            var toastView = new CustomToastView(message);
            // Assuming `parent` is the layout container where the toast should be shown
            if (parentLayout is Layout layout)
            {
                layout.Children.Add((Microsoft.Maui.IView)toastView);
                await Task.Delay(duration); // Wait for the duration of the toast
                layout.Children.Remove((Microsoft.Maui.IView)toastView); // Remove the toast view
            }
        }

        public static async Task SendFeedBack(Questions question)
        {
           // int _index = pcv.CardsCarousel.Position;
            string cardQuestion = "ID: " + question.Id + " Question: " + question.Question;
            string cardAnswer = "Answer: " + question.Answer1;
            SendFeedbackEmails sfe = new();
            await sfe.SendQuestionFeedbackCV(cardQuestion, cardAnswer);
        }
    }
}
