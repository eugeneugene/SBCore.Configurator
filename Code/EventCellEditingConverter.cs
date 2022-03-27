using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace SBCore.Configurator.Code
{
    internal class EventCellEditingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is KeyValuePair<uint, string> data)
                return $"{data.Key} {data.Value}";
            if (value is uint uivalue)
            {
                if (StaticResources.EventMap.TryGetValue(uivalue, out string descr))
                    return $"{value} {descr}";
                else
                    return $"{value}";
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
