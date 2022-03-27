using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrimS.Telnet;
using Renci.SshNet;
using SBCore.Configurator.TextResources;
using SBShared.Types.Data;
using Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WpfNotification;

namespace SBCore.Configurator.EditWindows
{
    internal partial class CmiuDeviceEditWindow
    {
        private void PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is DataGrid grid && e.Command == DataGrid.DeleteCommand)
            {
                if (grid.SelectedItems.Count > 0)
                {
                    var selectedItems = grid.SelectedItems.Cast<CmiuDeviceData>().ToArray();
                    for (int i = 0; i < selectedItems.Length; i++)
                    {
                        var selectedItem = selectedItems[i];
                        Debug.WriteLine("Item is being removed: {0}", selectedItem);
                        var removeItem = observableCollection.FirstOrDefault(item => item.Id == selectedItem.Id);
                        bool removed = false;
                        if (removeItem != null)
                            removed = observableCollection.Remove(removeItem);
                        if (removed)
                            Debug.WriteLine("Successfully removed");
                        else
                            Debug.WriteLine("Not removed");
                    }
                }

                e.Handled = true;
            }
        }

        private void AddRowCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is CmiuDeviceEditWindow)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
        }

        private void RemoveRowCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is CmiuDeviceEditWindow window)
            {
                var grid = window.DeviceMapGrid;
                if (grid.CurrentItem is CmiuDeviceData)
                    e.CanExecute = ClickedItem != null;
                e.Handled = true;
            }
        }

        private void PingCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is CmiuDeviceEditWindow window)
            {
                var grid = window.DeviceMapGrid;
                if (grid.CurrentItem is CmiuDeviceData item)
                {
                    if (!string.IsNullOrWhiteSpace(item.DeviceIP))
                        e.CanExecute = ClickedItem != null;
                }

                e.Handled = true;
            }
        }

        private void VersionCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is CmiuDeviceEditWindow window)
            {
                var grid = window.DeviceMapGrid;
                if (grid.CurrentItem is CmiuDeviceData item)
                {
                    if (item.CmiuDeviceType == Shared.Types.CMIU.CmiuDeviceType.Server && !string.IsNullOrWhiteSpace(item.DeviceIP) && item.Port != null && item.Port > 0)
                        e.CanExecute = ClickedItem != null;
                }

                e.Handled = true;
            }
        }

        private void CashCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is CmiuDeviceEditWindow window)
            {
                var grid = window.DeviceMapGrid;
                if (grid.CurrentItem is CmiuDeviceData item)
                {
                    if (item.CmiuDeviceType == Shared.Types.CMIU.CmiuDeviceType.Cashier && (item.Protocol.ToUpperInvariant() == "SSH" || item.Protocol.ToUpperInvariant() == "TELNET") &&
                        !string.IsNullOrWhiteSpace(item.Prompt) && !string.IsNullOrWhiteSpace(item.DeviceIP))
                        e.CanExecute = ClickedItem != null;
                }

                e.Handled = true;
            }
        }

        private void RebootCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is CmiuDeviceEditWindow window)
            {
                var grid = window.DeviceMapGrid;
                if (grid.CurrentItem is CmiuDeviceData item)
                {
                    if ((item.CmiuDeviceType == Shared.Types.CMIU.CmiuDeviceType.Cashier || item.CmiuDeviceType == Shared.Types.CMIU.CmiuDeviceType.Entry || item.CmiuDeviceType == Shared.Types.CMIU.CmiuDeviceType.Exit) &&
                        (item.Protocol.ToUpperInvariant() == "SSH" || item.Protocol.ToUpperInvariant() == "TELNET") &&
                        !string.IsNullOrWhiteSpace(item.Prompt) && !string.IsNullOrWhiteSpace(item.DeviceIP))
                        e.CanExecute = ClickedItem != null;
                }

                e.Handled = true;
            }
        }

        private void AuthHelperCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is CmiuDeviceEditWindow window)
            {
                var grid = window.DeviceMapGrid;
                if (grid.CurrentItem is CmiuDeviceData)
                    e.CanExecute = ClickedItem != null;
                e.Handled = true;
            }
        }

        private void CloseCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void AddRowExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            observableCollection.Add(new CmiuDeviceData());

            if (sender is CmiuDeviceEditWindow window)
            {
                var grid = window.DeviceMapGrid;
                grid.UnselectAllCells();
                grid.CommitEdit();
                var last = observableCollection.Last();
                grid.CurrentCell = new DataGridCellInfo(last, grid.Columns[0]);
                grid.SelectedCells.Add(grid.CurrentCell);
                grid.SelectedItem = last;
                grid.Focus();
            }

            e.Handled = true;
        }

        private void RemoveRowExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is CmiuDeviceEditWindow window)
            {
                var selectedItems = new List<CmiuDeviceData> { };
                var grid = window.DeviceMapGrid;
                grid.CancelEdit();
                if (grid.SelectedItems.Count > 0)
                    selectedItems.AddRange(grid.SelectedItems.Cast<CmiuDeviceData>());
                else
                {
                    if (grid.CurrentItem is CmiuDeviceData cmiuDevice)
                        selectedItems.Add(cmiuDevice);
                }

                foreach (var selectedItem in selectedItems)
                {
                    Debug.WriteLine("Item is being removed: {0}", selectedItem);

                    var removeItem = observableCollection.FirstOrDefault(item => item.Id == selectedItem.Id);
                    bool removed = false;
                    if (removeItem != null)
                        removed = observableCollection.Remove(removeItem);
                    if (removed)
                        Debug.WriteLine("Successfully removed");
                    else
                        Debug.WriteLine("Not removed");
                }

                e.Handled = true;
            }
        }

        private async void PingExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                string CurrentCellDeviceIP = null;
                if (sender is CmiuDeviceEditWindow window)
                {
                    var grid = window.DeviceMapGrid;
                    if (grid.CurrentItem is CmiuDeviceData cmiuDevice)
                    {
                        if (!string.IsNullOrWhiteSpace(cmiuDevice.DeviceIP))
                            CurrentCellDeviceIP = cmiuDevice.DeviceIP;
                    }
                }

                if (CurrentCellDeviceIP == null)
                {
                    Dispatcher.Invoke(() => ShowError(Russian.Ping, Russian.NoIP));
                    return;
                }

                using Ping pingSender = new();
                PingOptions options = new()
                {
                    DontFragment = true
                };
                byte[] buffer = new byte[32];
                int timeout = 120;
                PingReply reply = await pingSender.SendPingAsync(CurrentCellDeviceIP, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                    Dispatcher.Invoke(() => ShowNotification(Russian.Ping,
                        string.Format(CultureInfo.CurrentCulture, Russian.PingSuccess2A2, CurrentCellDeviceIP, reply.RoundtripTime),
                        string.Format(CultureInfo.CurrentCulture, Russian.PingSuccess1A2, CurrentCellDeviceIP, reply.RoundtripTime)));
                else
                    Dispatcher.Invoke(() => ShowError(Russian.Ping,
                        string.Format(CultureInfo.CurrentCulture, Russian.PingFail2A2, CurrentCellDeviceIP, reply.Status),
                        string.Format(CultureInfo.CurrentCulture, Russian.PingFail1A2, CurrentCellDeviceIP, reply.Status)));
            }
            catch (Exception ex)
            {
                StringBuilder sb = new();
                sb.AppendLine(Russian.ExceptionA + ex.Message);
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    sb.AppendLine(Russian.InnerExceptionA + ex.Message);
                }

                Dispatcher.Invoke(() => ShowError(Russian.PingException, Russian.ExceptionA + ex.Message, ex.Message, sb.ToString()));
            }
        }

        private async void VersionExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                string ServerIP = null;
                string ServerPort = null;
                string ServerProtocol = null;

                if (sender is CmiuDeviceEditWindow window)
                {
                    var grid = window.DeviceMapGrid;
                    if (grid.CurrentItem is CmiuDeviceData cmiuDevice)
                    {
                        if (!string.IsNullOrWhiteSpace(cmiuDevice.DeviceIP))
                            ServerIP = cmiuDevice.DeviceIP;
                        if (cmiuDevice.Port != null && cmiuDevice.Port > 0)
                            ServerPort = cmiuDevice.Port?.ToString(CultureInfo.CurrentCulture) ?? "null";
                        if (!string.IsNullOrWhiteSpace(cmiuDevice.Protocol))
                            ServerProtocol = cmiuDevice.Protocol;
                    }
                }

                if (ServerIP == null)
                {
                    Dispatcher.Invoke(() => ShowError(Russian.Version, Russian.NoIP));
                    return;
                }

                if (ServerPort == null)
                {
                    Dispatcher.Invoke(() => ShowError(Russian.Version, Russian.NoPort));
                    return;
                }

                if (ServerProtocol == null)
                {
                    Dispatcher.Invoke(() => ShowError(Russian.Version, Russian.NoProtoHttp));
                    return;
                }

                if (ServerProtocol.ToUpperInvariant() != "HTTP" && ServerProtocol.ToUpperInvariant() != "HTTPS")
                {
                    Dispatcher.Invoke(() => ShowError(Russian.Version, Russian.WrongProtoHttp));
                    return;
                }

                var client = _clientFactory.CreateClient("Configurator");
                var VersionUri = new Uri($"{ServerProtocol}://{ServerIP}:{ServerPort}/api/v1/version");
                using var request = new HttpRequestMessage(HttpMethod.Get, VersionUri);
                var response = await client.SendAsync(request, _applicationLifetime.ApplicationStopping);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync(_applicationLifetime.ApplicationStopping);
                    Dispatcher.Invoke(() => ShowNotification(Russian.Version, responseContent));
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync(_applicationLifetime.ApplicationStopping);
                    Dispatcher.Invoke(() => ShowError(Russian.Version,
                        $"Response: {response.StatusCode} " + (string.IsNullOrWhiteSpace(responseContent) ? "(No content)" : "Content: " + responseContent),
                        $"Response: {response.StatusCode}" + Environment.NewLine + (string.IsNullOrWhiteSpace(responseContent) ? "No content" : responseContent)));
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new();
                sb.AppendLine(Russian.ExceptionA + ex.Message);
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    sb.AppendLine(Russian.InnerExceptionA + ex.Message);
                }

                Dispatcher.Invoke(() => ShowError(Russian.VersionException, Russian.ExceptionA + ex.Message, ex.Message, sb.ToString()));
            }
        }

        private void CashExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            CmiuDeviceData cmiuDevice = null;

            if (sender is CmiuDeviceEditWindow window)
            {
                var grid = window.DeviceMapGrid;
                if (grid.CurrentItem is CmiuDeviceData i)
                    cmiuDevice = i;
            }

            var cashPreviewWindow = _serviceProvider.GetRequiredService<CashPreviewWindow>();
            cashPreviewWindow.Owner = this;
            cashPreviewWindow.CmiuDeviceData = cmiuDevice;
            cashPreviewWindow.ShowDialog();
        }

        private async void RebootExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            CmiuDeviceData cmiuDevice = null;

            if (sender is CmiuDeviceEditWindow window)
            {
                var grid = window.DeviceMapGrid;
                if (grid.CurrentItem is CmiuDeviceData i)
                    cmiuDevice = i;

                if (cmiuDevice != null)
                {
                    var protocol = cmiuDevice.Protocol;
                    switch (protocol.ToUpperInvariant())
                    {
                        case "TELNET":
                            await RebootTelnetAsync(cmiuDevice, _applicationLifetime.ApplicationStopping);
                            break;
                        case "SSH":
                            await RebootSshAsync(cmiuDevice, _applicationLifetime.ApplicationStopping);
                            break;
                        default:
                            ShowError(Russian.WrongProto, Russian.WrongProtoA + cmiuDevice.Protocol);
                            break;
                    }
                }
            }
        }

        private async Task RebootTelnetAsync(CmiuDeviceData cmiuDevice, CancellationToken cancellationToken)
        {
            if (cmiuDevice == null)
                throw new ArgumentNullException(nameof(cmiuDevice));

            cancellationToken.ThrowIfCancellationRequested();

            int Timeout = _configuration.GetValue("SBCore.Configurator:TelnetTimeout", -1);
            double TimeoutMs;
            if (Timeout > 0)
                TimeoutMs = TimeSpan.FromSeconds(Timeout).TotalMilliseconds;
            else
                TimeoutMs = 100.0;

            if (string.IsNullOrEmpty(cmiuDevice.DeviceAuth) ||
                string.IsNullOrEmpty(cmiuDevice.DeviceIP) ||
                cmiuDevice.Port == null ||
                string.IsNullOrEmpty(cmiuDevice.Prompt) ||
                string.IsNullOrEmpty(cmiuDevice.Reboot))
            {
                Dispatcher.Invoke(() => ShowError(Russian.Reboot, Russian.WrongRebootData));
                return;
            }

            LoginCrypt.TryDecryptLoginPwd(cmiuDevice.DeviceAuth, out string Login, out string Password);
            if (string.IsNullOrEmpty(Login))
            {
                Dispatcher.Invoke(() => ShowError(Russian.Reboot, Russian.NoLogin));
                return;
            }

            using (var client = new Client(cmiuDevice.DeviceIP, cmiuDevice.Port ?? 0, cancellationToken))
            {
                try
                {
                    var res1 = await client.TryLoginAsync(Login, Password, Convert.ToInt32(TimeoutMs), cmiuDevice.Prompt, "\n");

                    if (!res1)
                        throw new OperationCanceledException("Login failed");

                    Debug.WriteLine("Telnet write reboot command: " + cmiuDevice.Reboot);
                    await client.WriteLine(cmiuDevice.Reboot);
                    string res2 = await client.ReadAsync(TimeSpan.FromMilliseconds(TimeoutMs));
                    string ress = res2.TrimEnd();
                    Debug.WriteLine("Telnet read response: {0}", ress);
                }
                catch (Exception ex)
                {
                    StringBuilder sb = new();
                    sb.AppendLine(Russian.ExceptionA + ex.Message);
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                        sb.AppendLine(Russian.InnerExceptionA + ex.Message);
                    }

                    Dispatcher.Invoke(() => ShowError(Russian.Reboot, Russian.ExceptionA + ex.Message, ex.Message, sb.ToString()));
                    return;
                }
            }

            Dispatcher.Invoke(() => ShowNotification(Russian.Reboot, Russian.RebootSent));
        }

        private async Task RebootSshAsync(CmiuDeviceData cmiuDevice, CancellationToken cancellationToken)
        {
            try
            {
                if (cmiuDevice == null)
                    throw new ArgumentNullException(nameof(cmiuDevice));

                cancellationToken.ThrowIfCancellationRequested();

                int Timeout = _configuration.GetValue("SBCore.Configurator:SshTimeout", -1);

                if (string.IsNullOrEmpty(cmiuDevice.DeviceAuth) ||
                    string.IsNullOrEmpty(cmiuDevice.DeviceIP) ||
                    cmiuDevice.Port == null ||
                    string.IsNullOrEmpty(cmiuDevice.Prompt) ||
                    string.IsNullOrEmpty(cmiuDevice.Reboot))
                {
                    Dispatcher.Invoke(() => ShowError(Russian.Reboot, Russian.WrongRebootData));
                    return;
                }

                LoginCrypt.TryDecryptLoginPwd(cmiuDevice.DeviceAuth, out string Login, out string Password);
                if (string.IsNullOrEmpty(Login))
                {
                    Dispatcher.Invoke(() => ShowError(Russian.Reboot, Russian.NoLogin));
                    return;
                }

                var connectionInfo = new ConnectionInfo(cmiuDevice.DeviceIP, cmiuDevice.Port ?? 0, Login,
                                            new PasswordAuthenticationMethod(Login, Password)
                                            //,new PrivateKeyAuthenticationMethod("rsa.key")
                                            );
                if (Timeout > 0)
                    connectionInfo.Timeout = TimeSpan.FromSeconds(Timeout);

                using var client = new SshClient(connectionInfo);
                client.Connect();
                if (!client.IsConnected)
                {
                    Dispatcher.Invoke(() => ShowError(Russian.Reboot, Russian.SSHConnectionFailed));
                    return;
                }

                var command = client.CreateCommand(cmiuDevice.Reboot);
                var rebootTask = Task.Run(() => command.Execute(), cancellationToken);
                if (await Task.WhenAny(rebootTask, Task.Delay(TimeSpan.FromSeconds(Timeout), cancellationToken)) != rebootTask)
                {
                    Dispatcher.Invoke(() => ShowError(Russian.Reboot, Russian.SSHRebootTimeout));
                    return;
                }

                if (!rebootTask.IsCompletedSuccessfully)
                {
                    Dispatcher.Invoke(() => ShowError(Russian.Reboot, Russian.SSHRebootTaskFailed));
                    return;
                }

                if (command.ExitStatus != 0)
                {
                    Dispatcher.Invoke(() => ShowError(Russian.Reboot, string.Format(CultureInfo.CurrentCulture, Russian.SSHRebootFailedA2, command.Error, command.Result)));
                    return;
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new();
                sb.AppendLine(Russian.ExceptionA + ex.Message);
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    sb.AppendLine(Russian.InnerExceptionA + ex.Message);
                }

                Dispatcher.Invoke(() => ShowError(Russian.Reboot, Russian.ExceptionA + ex.Message, ex.Message, sb.ToString()));
                return;
            }

            Dispatcher.Invoke(() => ShowNotification(Russian.Reboot, Russian.RebootSent));
        }

        private void AuthHelperExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is CmiuDeviceEditWindow window)
            {
                var grid = window.DeviceMapGrid;
                var item = grid.CurrentItem as CmiuDeviceData;

                if (item is not null)
                {
                    grid.CommitEdit(DataGridEditingUnit.Row, true);

                    var authWindow = _serviceProvider.GetRequiredService<AuthEditWindow>();
                    authWindow.Owner = this;
                    authWindow.Model.Auth = item.DeviceAuth;
                    authWindow.Model.GenerateSalt = true;
                    authWindow.ShowDialog();

                    if (authWindow.Model.Auth is not null)
                    {
                        item.DeviceAuth = authWindow.Model.Auth;
                        CollectionViewSource.GetDefaultView(grid.ItemsSource).Refresh();
                    }
                }
            }

            e.Handled = true;
        }

        private void ShowError(string Title, string Message, string ToolTip = null)
        {
            ShowNotification(NotificationType.Error, Title, Message, ToolTip);
        }

        private void ShowError(string Title, string Message1, string Message2, string ToolTip = null)
        {
            ShowNotification(NotificationType.Error, Title, Message1, Message2, ToolTip);
        }

        private void ShowNotification(string Title, string Message, string ToolTip = null)
        {
            ShowNotification(NotificationType.Notification, Title, Message, ToolTip);
        }

        private void ShowNotification(NotificationType Notification, string Title, string Message, string ToolTip)
        {
            StatusBlock.Text = Message;
            StatusBlock.ToolTip = ToolTip ?? string.Empty;
            var popup = new PopupWindow(Title, Message, Notification)
            {
                Owner = this
            };
            popup.Show();
        }

        private void ShowNotification(NotificationType Notification, string Title, string Message1, string Message2, string ToolTip)
        {
            StatusBlock.Text = Message1;
            StatusBlock.ToolTip = ToolTip ?? string.Empty;
            var popup = new PopupWindow(Title, Message2, Notification)
            {
                Owner = this
            };
            popup.Show();
        }
    }
}
