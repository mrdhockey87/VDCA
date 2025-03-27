using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace VDCA.Converters
{
    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int lengthoftext = (int)value;
            return (int)lengthoftext != 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
