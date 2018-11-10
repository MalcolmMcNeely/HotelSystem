using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace HotelSystem.Infrastructure.WPF.Converters
{
    public class BooleanToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public bool Collapse { get; set; }

        public bool Reverse { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bValue = value != null && (bool)value;

            if (bValue != Reverse)
            {
                return Visibility.Visible;
            }

            return Collapse ? Visibility.Collapsed : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Reverse;
            }

            var visibility = (Visibility)value;

            if (visibility == Visibility.Visible)
            {
                return !Reverse;
            }

            return Reverse;
        }
    }
}
