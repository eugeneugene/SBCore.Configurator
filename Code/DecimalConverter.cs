using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace SBCore.Configurator.Code
{
    internal class DecimalConverter : IValueConverter
    {
        // Object => String
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                Debug.WriteLine($"Convert Value: {value ?? "null"} Type: {value?.GetType().ToString() ?? "null"} TargetType: {targetType}");
                if (targetType == typeof(string))
                {
                    var dec = System.Convert.ToDecimal(value);
                    return dec.ToString(culture);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return string.Empty;
        }

        // Object => Decimal
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                Debug.WriteLine($"ConvertBack Value: {value ?? "null"} Type: {value?.GetType().ToString() ?? "null"} TargetType: {targetType}");
                if (targetType == typeof(decimal?))
                {
                    if (decimal.TryParse(value?.ToString(), NumberStyles.Any, culture, out var dec))
                        return dec;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return null;
        }
    }
}
