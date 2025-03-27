using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using System;
using System.Runtime.InteropServices;

namespace VDCA.Utils
{
    public static class CalculateTextDimensions
    {


        public static double CalculateGridWidth()
        {
            double width = new();
            try
            {
                    double screenWidth = DeviceDisplay.Current.MainDisplayInfo.Width;
                    double screenDensity = DeviceDisplay.Current.MainDisplayInfo.Density;
                    const double fractionOfScreenWidth = 0.8; //fraction of the screen width
                    width = screenWidth * fractionOfScreenWidth / screenDensity;
            }
            catch (Exception ex)
            {
                Console.WriteLine("CalculateTextDimensions QuestionsDatabase Error Open: " + ex.Message);
            }
            return width;
        }
        public static double CalculateWidth(string text, Label label, double carouselViewViewItemWidth)//, double _FontSize)
        {
            Size sizeRequest = new();
            double width = new();
            double availableWidth = new();
            try
            {
                if (carouselViewViewItemWidth <= 0)
                {
                    double screenWidth = DeviceDisplay.Current.MainDisplayInfo.Width;
                    double screenDensity = DeviceDisplay.Current.MainDisplayInfo.Density;
                    const double fractionOfScreenWidth = 0.8; //fraction of the screen width
                    width = screenWidth * fractionOfScreenWidth / screenDensity;
                }
                else
                {
                    const double framePadding = 8;
                    const double frameWidth = 1;
                    const double imageWidth = 35;
                    //const double _FontSize = 14;
                    const double used_width = imageWidth + (2 * framePadding) + (2 * frameWidth);
                    availableWidth = used_width - carouselViewViewItemWidth;
                }
                if (availableWidth > 0)
                {
                    sizeRequest = label.Measure(availableWidth, double.PositiveInfinity);
                }
                else
                {
                    //the method label.Measure takes input of(double widthConstraint, double heightConstraint,
                    //Optional Microsoft.Maui.Controls.MeasureFlags (Enum 1 = IncludeMargins, 0 = none) mdail 9-5-23
                    sizeRequest = label.Measure(width, double.PositiveInfinity);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("CalculateTextDimensions QuestionsDatabase Error Open: " + ex.Message);
            }
            return sizeRequest.Width;
        }

        // Helper method to calculate text height based on font size and content
        public static double CalculateHeight(string text, double _FontSize, [Optional] double calculateWidth)
        {
            Size sizeRequest = new();
            try
            {
                var label = new Label
                {
                    Text = text,
                    FontSize = _FontSize,
                    LineBreakMode = LineBreakMode.WordWrap
                };
                //the method label.Measure takes input of(double widthConstraint, double heightConstraint, Optional Microsoft.Maui.Controls.MeasureFlags (Enum 1 = IncludeMargins, 0 = none) mdail 9-5-23
                if (calculateWidth != 0)
                {
                    sizeRequest = label.Measure(calculateWidth, double.PositiveInfinity);
                }
                else
                {
                    calculateWidth = Utils.CalculateTextDimensions.CalculateHeight(text, 14);
                    sizeRequest = label.Measure(calculateWidth, double.PositiveInfinity);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("CalculateTextDimensions QuestionsDatabase Error Open: " + ex.Message);
            }
            return sizeRequest.Height;
        }
    }
}
