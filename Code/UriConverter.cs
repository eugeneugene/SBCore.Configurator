using System;
using System.Globalization;
using System.Windows.Data;

namespace SBCore.Configurator.Code
{
    internal class UriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(string))
                return value.ToString();
            if (targetType == typeof(Uri))
            {
                if (value is Uri uri)
                    return uri;
                if (Uri.TryCreate(value.ToString(), UriKind.RelativeOrAbsolute, out var result))
                    return result;
            }
            if (targetType == typeof(object))
                return value;
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(string))
                return value.ToString();
            if (targetType == typeof(Uri))
            {
                if (value is Uri uri)
                    return uri;
                if (Uri.TryCreate(value.ToString(), UriKind.RelativeOrAbsolute, out var result))
                    return result;
            }
            if (targetType == typeof(object))
                return value;
            return Binding.DoNothing;
        }
    }
}
