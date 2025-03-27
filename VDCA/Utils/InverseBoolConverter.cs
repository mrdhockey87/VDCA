using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;

namespace VDCA.Utils
{
    public class InverseBoolConverter : IValueConverter, IMarkupExtension
    {
        //public bool converted;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //converted = !(bool)value;
            return !(bool)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //converted = !(bool)value;
            return !(bool)value;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this; // converted;
        }
    }
}