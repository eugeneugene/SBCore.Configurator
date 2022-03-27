using SBShared.Types.Data;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SBCore.Configurator.Code
{
    internal class CameraCellEditingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CameraData<CmiuDeviceData> cameraData)
                return $"Cameratype: {cameraData.Cameratype}, CameraUri: {cameraData.CameraUri} ({cameraData.Id})";
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
