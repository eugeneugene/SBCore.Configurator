using SBShared.Types.Data;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SBCore.Configurator.Code
{
    internal class DeviceCmiuCameraCellEditingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DeviceCmiuCameraData<CmiuDeviceData> deviceCmiuCameraData)
                return $"CmiuCameraId: {deviceCmiuCameraData.CmiuCameraId}, IssArchiveCameraId: {deviceCmiuCameraData.IssArchiveCameraId} ({deviceCmiuCameraData.Id})";
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
