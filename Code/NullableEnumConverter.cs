using System;
using System.Globalization;
using System.Windows.Data;

namespace SBCore.Configurator.Code
{
    internal class NullableEnumConverter : IValueConverter
    {
        public static string NullComboStringValue => "(Null)";

        static NullableEnumConverter()
        {
            Instance = new NullableEnumConverter();
        }

        public static NullableEnumConverter Instance { get; private set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ?? NullComboStringValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type enumType = parameter.GetType();
            if (value is null || Equals(value, NullComboStringValue))
                return null;

            object rawEnum = Enum.Parse(enumType, value.ToString() ?? string.Empty);
            return System.Convert.ChangeType(rawEnum, enumType);
        }
    }
}
