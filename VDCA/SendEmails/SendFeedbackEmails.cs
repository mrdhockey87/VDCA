using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDCA.Views;

namespace VDCA.SendEmails
{
    public class SendFeedbackEmails
    {
        //private readonly string LOGTAG = "SendFeedbackEmails";
        public SendFeedbackEmails() { }

        private readonly string[] recipients = ["markd@VDCA.us", "markg@vdca.us"];
        private readonly string device = DeviceInfo.Model;
        private readonly string manufacturer = DeviceInfo.Manufacturer;
        private readonly string deviceName = DeviceInfo.Name;
        private readonly string platform = DeviceInfo.Platform.ToString();
        private readonly string idiom = DeviceInfo.Idiom.ToString();
        private readonly string appName = AppInfo.Name;
        private readonly string app_version = AppVersion.AppVersionNo;
        private readonly string build = AppVersion.AppBuildNo;

        public async Task SendQuestionFeedback(string question, string answer)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = "Feedback regarding Question: " + question,
                    Body = "Feedback regarding Question: " + question + Constants.NEWLINE + "Answer: " + answer + Constants.NEWLINE +
                           appName + "  version: " + app_version + " Build: " + build + Constants.NEWLINE + platform +
                           " : " + manufacturer + " - " + device + " - " + deviceName + " - " + idiom + Constants.NEWLINE + Constants.NEWLINE + 
                           Constants.NEWLINE + "Feedback follows: " + Constants.NEWLINE,
                    To = [.. recipients],
               };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                Console.WriteLine("Error sending feedback error: " + fbsEx.Message);
                await SendFeedbackEmails.DisplayNotSupported();
                
            }
            catch (Exception ex)
            {
                await SendFeedbackEmails.DisplayEmailError(ex.Message);
            }
        }
        public async Task SendAppFeedback()
        {
            try
            {
                EmailMessage message = new()
                {
                    Subject = "Feedback regarding App: " + appName + " OS = " + platform,
                    Body = "Feedback: " + appName + "  version: " + app_version + " Build: " + build + Constants.NEWLINE + platform +
                           " : " + manufacturer + " - " + device + " - " + deviceName + " - " + idiom + Constants.NEWLINE + Constants.NEWLINE +
                           Constants.NEWLINE + "Feedback follows: " + Constants.NEWLINE,
                    To = [.. recipients],
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                Console.WriteLine("Error sending feedback error: " + fbsEx.Message);
                await SendFeedbackEmails.DisplayNotSupported();

            }
            catch (Exception ex)
            {
                await SendFeedbackEmails.DisplayEmailError(ex.Message);
            }
        }

        private static async Task DisplayNotSupported()
        {
            string InformationTitle = "Email Feature Not Supported!";
            string InformationMessage = "The app cannot send emails because the phone reports" +
                                      " that the feature is not supported!";
            var alertPage = new AlertPage();
            await Shell.Current.Navigation.PushModalAsync(alertPage);
            await alertPage.ShowAlertAsync(InformationTitle, InformationMessage);
        }

        private static async Task DisplayEmailError(string error)
        {
            string InformationTitle = "Email Error Occurred!";
            string InformationMessage = "AN error occurred trying to send the email, please try again later." +
                Constants.NEWLINE + "If the problem persists please call our customer support team with the error message!" +
                Constants.NEWLINE + "Error message is: " + error;
            var alertPage = new AlertPage();
            await Shell.Current.Navigation.PushModalAsync(alertPage);
            await alertPage.ShowAlertAsync(InformationTitle, InformationMessage);
        }

        public async Task SendQuestionFeedbackCV(string question, string answer)
        {
            try
            {
                EmailMessage message = new()
                {
                    Subject = "Feedback regarding Question: " + question,
                    Body = "Feedback regarding Question: " + question + Constants.NEWLINE + "Answer: " + answer + Constants.NEWLINE +
                           appName + "  version: " + app_version + " Build: " + build + Constants.NEWLINE + platform.ToString() +
                           " : " + manufacturer + " - " + device + " - " + deviceName + " - " + idiom + Constants.NEWLINE + Constants.NEWLINE +
                           Constants.NEWLINE + "Feedback follows: " + Constants.NEWLINE,
                    To = [.. recipients],
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                Console.WriteLine("Error sending feedback error: " + fbsEx.Message);
                await SendFeedbackEmails.DisplayNotSupportedVC();

            }
            catch (Exception ex)
            {
                await SendFeedbackEmails.DisplayEmailErrorCV(ex.Message);
            }
        }

        private static async Task DisplayNotSupportedVC()
        {
            string InformationTitle = "Email Feature Not Supported!";
            string InformationMessage = "The app cannot send emails because the phone reports" +
                                      " that the feature is not supported!";
            var alertPage = new AlertPage();
            await Shell.Current.Navigation.PushModalAsync(alertPage);
            await alertPage.ShowAlertAsync(InformationTitle, InformationMessage);
        }
        private static async Task DisplayEmailErrorCV(string error)
        {
            string InformationTitle = "Email Error Occurred!";
            string InformationMessage = "AN error occurred trying to send the email, please try again later."
                    + Constants.NEWLINE + "If the problem persists please call our customer support team with the error message!"
                    + Constants.NEWLINE + "Error message is: " + error;
            var alertPage = new AlertPage();
            await Shell.Current.Navigation.PushModalAsync(alertPage);
            await alertPage.ShowAlertAsync(InformationTitle, InformationMessage);
        }
    }
}
