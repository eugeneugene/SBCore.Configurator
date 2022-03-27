using Shared;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace SBCore.Configurator.Code
{
    internal class AuthDecorationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine($"AuthDecorationConverter: Value: {value}");
            if (value is string auth)
            {
                if (!LoginCrypt.IsValidAuth(auth))
                {
                    TextDecorationCollection redStrikthroughTextDecoration = TextDecorations.Strikethrough.CloneCurrentValue();
                    redStrikthroughTextDecoration[0].Pen = new Pen { Brush = Brushes.Red, Thickness = 3 };
                    return redStrikthroughTextDecoration;
                }
            }

            return new TextDecorationCollection();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
