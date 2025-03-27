using Microsoft.Maui.Graphics.Converters;
using System.Globalization;

using Microsoft.Maui.Controls;
using System;
using Microsoft.Maui.Graphics;

namespace VDCA.Converters
{
    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = value.ToString();
            ColorTypeConverter converter = new();
            return (Color)converter.ConvertFromString(color);
            /*
             * 
                if (value is Color color)
                {
                    return color;
                }
                else if (value is string colorString)
                {
                    ColorTypeConverter converter = new();
                    return (Color)converter.ConvertFromString(colorString);
                }
                return Colors.Transparent; // Default color if conversion fails
             */
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
