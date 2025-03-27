
using Microsoft.Maui.Controls;
using System;

namespace VDCA.Converters
{
    public class HalfSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double size)
            {
                return size / 2;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
