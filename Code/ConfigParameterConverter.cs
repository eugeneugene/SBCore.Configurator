using SBShared.Types;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SBCore.Configurator.Code
{
    internal class ConfigParameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ConfigParameter configParameter)
            {
                if (targetType == typeof(string))
                    return configParameter.Name;
                return configParameter;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ConfigParameter configParameter)
                return configParameter;
            return Binding.DoNothing;
        }
    }
}
