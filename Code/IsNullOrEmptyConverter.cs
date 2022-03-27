using Shared;
using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace SBCore.Configurator.Code
{
    internal class IsNullOrEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable enumerable)
                return enumerable.IsNullOrEmpty();
            return value == null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("IsNullConverter can only be used OneWay.");
        }
    }
}
