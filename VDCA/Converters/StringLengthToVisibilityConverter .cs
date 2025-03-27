using System.Globalization;
using Microsoft.Maui.Controls;
using System;

namespace VDCA.Converters
{
    public class StringLengthToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Check if the string length is 0 (inverted condition)
            bool visible = !string.IsNullOrEmpty((string)value);
            return visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
