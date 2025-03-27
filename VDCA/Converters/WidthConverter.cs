using System.Globalization;
using Microsoft.Maui.Controls;
using System;
using Microsoft.Maui.Devices;

namespace VDCA.Converters
{
    public class WidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //var width = (double)value;
            double screen_width = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            int width = (int)screen_width;
            // return screen_width;// * 0.25;

            return (double)width;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}
