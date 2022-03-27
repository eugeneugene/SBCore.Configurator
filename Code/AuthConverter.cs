using Shared;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace SBCore.Configurator.Code
{
    // ToolTip
    internal class AuthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine($"AuthConverter: Value: {value}");
            if (value is string auth)
            {
                if (LoginCrypt.TryDecryptLoginPwd(auth, out string login, out string password))
                    return $"{login} / {password}";
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
