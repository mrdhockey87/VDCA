using System.Collections.ObjectModel;
using System.Globalization;
using VDCA.CustomControl;
using Microsoft.Maui.Controls;
using System;

namespace VDCA.Converters
{
    public class VisibleArrowsConverter : IValueConverter
    {
        public ArrowDirection TargetArrow { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ObservableCollection<ArrowDirection> visibleArrows)
            {
                return visibleArrows.Contains(TargetArrow);
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
