using SBShared.Types.Data;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SBCore.Configurator.Code
{
    internal class CameraCellConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CameraData<CmiuDeviceData> cameraData)
                return cameraData.Id;
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
