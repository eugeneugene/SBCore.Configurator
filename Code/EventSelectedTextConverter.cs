using System;
using System.Globalization;
using System.Windows.Data;

namespace SBCore.Configurator.Code
{
    internal class EventSelectedTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is uint uvalue)
            {
                if (StaticResources.EventMap.TryGetValue(uvalue, out string svalue))
                    return $"{uvalue} {svalue}";
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                var i = str.IndexOf(' ');
                if (i > 0)
                    str = str.Substring(0, i);
                if (uint.TryParse(str, NumberStyles.Integer, CultureInfo.InvariantCulture, out uint val2))
                    return val2;
            }

            return null;
        }
    }
}
