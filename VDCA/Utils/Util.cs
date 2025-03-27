using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using System;

namespace VDCA.Utils
{
    public static class Util
    {
        //private static readonly string LOGTAG = "Util";

        public static bool IsPortrait(Page p) { return p.Width < p.Height; }
        //if either value is 0 return 0 else calculate the percentage rounded to an int mdail 8-6-21
        public static int CalculateScorePercentage(int correct, int total)
        {
            if (total == 0 || correct == 0)
            {
                return 0;
            }
            else
            {
                float fraction = (float)correct / (float)total;
                float percentage = fraction * 100;
                int percent = (int)Math.Round(percentage, 0);
                return percent;
            }
        }
        //get the background color for the review quizzes page based on the percentage score mdail 8-6-21
        public static Color GetPercentageColor(int percent)
        {
            if (percent > 79)
            {
                // Retrieve the Primary color value which is in the page's resource dictionary
                var hasValue = App.Current.Resources.TryGetValue("PrimaryPass", out object passcolor);
                if (hasValue)
                {
                    return (Color)passcolor;
                }
                else
                {
                    return Color.FromArgb("0xFF22B573");
                }
            }
            else if (percent <= 79 && percent > 69)
            {
                // Retrieve the Primary color value which is in the page's resource dictionary
                var hasValue = App.Current.Resources.TryGetValue("PrimaryLowPass", out object lowpasscolor);
                if (hasValue)
                {
                    return (Color)lowpasscolor;
                }
                else
                {
                    return Color.FromArgb("0xFFFFC800");
                }
            }
            else if (percent <= 69)
            {
                var hasValue = App.Current.Resources.TryGetValue("PrimaryFail", out object failcolor);
                if (hasValue)
                {
                    return (Color)failcolor;
                }
                else
                {
                    return Color.FromArgb("0xFFFF0000");
                }
            }
            else
            {
                var hasValue = App.Current.Resources.TryGetValue("PrimaryText", out object textcolor);
                if (hasValue)
                {
                    return (Color)textcolor;
                }
                else
                {
                    return Color.FromArgb("0xFF000080");
                }
            }
        }
        public static string FirstCharToUpper(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            return $"{char.ToUpper(input[0])}{input[1..]}";
        }
        public static double GetScreenWidth()
        {
            double width = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            return width;
        }
        public static double GetScreenHeight()
        {
            double height = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
            return height;
        }
        public static string FixPipeExplanation(string explanation) {
            const string pipe = "|";
            if (explanation.Contains(pipe))
            {
                explanation = explanation.Replace(pipe, Constants.NEWLINE);
            }
            return explanation;
        }
    }
}
