using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SBCore.Configurator.Services;
using SBShared.Types;
using SBShared.Types.Data;
using Shared.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace SBCore.Configurator.EditWindows
{
    internal partial class CashPreviewWindow : Window
    {
        private readonly ILogger _logger;
        private readonly IAPSCashReader _cashReader;
        private readonly IHostApplicationLifetime _applicationLifetime;

        private readonly ObservableCollection<CashStatus> cashStatus = new();

        public CmiuDeviceData CmiuDeviceData { get; set; }

        private const string sDefaultBnaCountPath = "sydata/bnacount.dat";
        private const string sDefaultBnaOpDataPath = "sydata/bnaopdat.dat";
        private const string sDefaultBnvCountPath = "sydata/bnvcount.dat";
        private const string sDefaultBnvOpDataPath = "sydata/bnvopdat.dat";

        public CashPreviewWindow(ILogger<CashPreviewWindow> logger, IAPSCashReader cashReader, IHostApplicationLifetime applicationLifetime)
        {
            InitializeComponent();

            _logger = logger;
            _cashReader = cashReader;
            _applicationLifetime = applicationLifetime;
            _applicationLifetime.ApplicationStopping.Register(() => Close());
            _logger.LogDebug("Создание экземпляра {Name}", GetType().Name);
        }

        private void WindowClose(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            WaitLabel.Dispatcher.Invoke(() => WaitLabel.Visibility = Visibility.Visible);
            await ProcessCashDataAsync(_applicationLifetime.ApplicationStopping);
            WaitLabel.Dispatcher.Invoke(() => WaitLabel.Visibility = Visibility.Hidden);
            cashStatusGrid.Dispatcher.Invoke(() => cashStatusGrid.Visibility = Visibility.Visible);
        }

        private async Task ProcessCashDataAsync(CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (CmiuDeviceData == null)
                    throw new Exception("Parameter CmiuDeviceData is null");

                await Task.Delay(TimeSpan.FromSeconds(0.5), cancellationToken);

                IEnumerable<CashierOpData<EntervoDeviceKind>> apsCashData;
                if (CmiuDeviceData.Protocol.ToUpperInvariant() == "SSH")
                {
                    apsCashData = await _cashReader.LoadDataAsync(CmiuDeviceData.DeviceIP, CmiuDeviceData.Port ?? 0, CashierOpDataProtocol.SFTP, CmiuDeviceData.DeviceAuth,
                        $"/usr/{sDefaultBnaCountPath}", $"/usr/{sDefaultBnaOpDataPath}",
                        $"/usr/{sDefaultBnvCountPath}", $"/usr/{sDefaultBnvOpDataPath}", cancellationToken);
                }
                else
                {
                    apsCashData = await _cashReader.LoadDataAsync(CmiuDeviceData.DeviceIP, 0, CashierOpDataProtocol.FTP, CmiuDeviceData.DeviceAuth,
                        $"/usr/{sDefaultBnaCountPath}", $"/usr/{sDefaultBnaOpDataPath}",
                        $"/usr/{sDefaultBnvCountPath}", $"/usr/{sDefaultBnvOpDataPath}", cancellationToken);
                }

                if (null == apsCashData)
                    return;

                foreach (var data in apsCashData)
                    _logger.LogTrace("CashCounters: {device} ({ip}): {data}", CmiuDeviceData.CmiuDeviceNumber, CmiuDeviceData.DeviceIP, data);

                foreach (var deviceKind in EntervoDeviceKind.List)
                {
                    foreach (var note in Notes.List)
                    {
                        var data = apsCashData.FirstOrDefault(item => item.Rating == (note.Value * 100U) && item.DeviceKind == deviceKind);
                        uint MoneyValue;
                        uint Quantity;
                        if (data == null)
                        {
                            MoneyValue = note.Value * 100U;
                            Quantity = 0U;
                        }
                        else
                        {
                            MoneyValue = data.Rating;
                            Quantity = data.Count;
                        }

                        var cs = new CashStatus
                        {
                            MoneyValue = MoneyValue,
                            Quantity = Quantity,
                            DeviceKind = deviceKind,
                        };
                        cashStatus.Add(cs);
                    }
                }

                CollectionViewSource itemCollectionViewSource;
                itemCollectionViewSource = (CollectionViewSource)FindResource("ItemCollectionViewSource");
                itemCollectionViewSource.Source = cashStatus;
            }
            catch (Exception ex)
            {
                _logger.LogError("{ex}", ex);
            }
        }
    }
}
