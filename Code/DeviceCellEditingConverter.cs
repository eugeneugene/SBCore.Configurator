using SBShared.Types.Data;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SBCore.Configurator.Code
{
    internal class DeviceCellEditingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CmiuDeviceData deviceData)
                return $"CmiuDeviceNumber: {deviceData.CmiuDeviceNumber}, CmiuDeviceType: {deviceData.CmiuDeviceType}, DeviceIP: {deviceData.DeviceIP} ({deviceData.Id})";
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
