using System;
using System.Globalization;
using System.Windows.Data;

namespace Clock
{
    public class DateTimeToAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var baseValue = System.Convert.ToInt32(value);
            var steps = System.Convert.ToDouble(parameter);
            return baseValue / steps * 360.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
