using FluentFTP;
using Microsoft.Extensions.Logging;
using Renci.SshNet;
using SBShared.Types;
using Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SBCore.Configurator.Services
{
    public interface IAPSCashReader
    {
        Task<IEnumerable<CashierOpData<EntervoDeviceKind>>> LoadDataAsync(string DataSource, int DataPort, CashierOpDataProtocol Protocol, string AuthInfo,
            string BnaCountUrl, string BnaOpDataUrl, string BnvCountUrl, string BnvOpDataUrl, CancellationToken cancellationToken);
    }

    public class APSCashReader : IAPSCashReader
    {
        private readonly ILogger _logger;

        public APSCashReader(ILogger<APSCashReader> logger)
        {
            _logger = logger;
            _logger.LogDebug("Создание экземпляра {Name}", GetType().Name);
        }

        public async Task<IEnumerable<CashierOpData<EntervoDeviceKind>>> LoadDataAsync(string DataSource, int DataPort, CashierOpDataProtocol Protocol, string AuthInfo,
            string BnaCountUrl, string BnaOpDataUrl, string BnvCountUrl, string BnvOpDataUrl, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            byte[] BnaCountData = null;
            byte[] BnaOpData = null;
            byte[] BnvCountData = null;
            byte[] BnvOpData = null;

            var CashierData = new List<CashierOpData<EntervoDeviceKind>> { };

            if (Protocol == CashierOpDataProtocol.FILE || Protocol == CashierOpDataProtocol.NOTSET)
            {
                try
                {
                    if (Protocol == CashierOpDataProtocol.NOTSET)
                        _logger.LogWarning("Не задан Protocol. Подразумевается File");

                    _logger.LogTrace("BnaCountUrl: {url}", BnaCountUrl);
                    _logger.LogTrace("BnaOpDataUrl: {url}", BnaOpDataUrl);
                    _logger.LogTrace("BnvCountUrl: {url}", BnvCountUrl);
                    _logger.LogTrace("BnvOpDataUrl: {url}", BnvOpDataUrl);

                    using (var stream = File.Open(BnaCountUrl, FileMode.Open))
                    {
                        BnaCountData = new byte[stream.Length];
                        await stream.ReadAsync(BnaCountData.AsMemory(0, (int)stream.Length), cancellationToken);
                    }

                    using (var stream = File.Open(BnaOpDataUrl, FileMode.Open))
                    {
                        BnaOpData = new byte[stream.Length];
                        await stream.ReadAsync(BnaOpData.AsMemory(0, (int)stream.Length), cancellationToken);
                    }

                    using (var stream = File.Open(BnvCountUrl, FileMode.Open))
                    {
                        BnvCountData = new byte[stream.Length];
                        await stream.ReadAsync(BnvCountData.AsMemory(0, (int)stream.Length), cancellationToken);
                    }

                    using (var stream = File.Open(BnvOpDataUrl, FileMode.Open))
                    {
                        BnvOpData = new byte[stream.Length];
                        await stream.ReadAsync(BnvCountData.AsMemory(0, (int)stream.Length), cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("{ex}", ex);
                }
            }
            else if (Protocol == CashierOpDataProtocol.FTP)
            {
                try
                {
                    if (string.IsNullOrEmpty(DataSource))
                    {
                        _logger.LogError("Не задан DataSource");
                        return null;
                    }

                    if (string.IsNullOrEmpty(AuthInfo))
                    {
                        _logger.LogError("Не задан AuthInfo");
                        return null;
                    }

                    var bnaCountUrl = new Uri($"ftp://{DataSource}/{BnaCountUrl}");
                    var bnaOpDataUrl = new Uri($"ftp://{DataSource}/{BnaOpDataUrl}");
                    var bnvCountUrl = new Uri($"ftp://{DataSource}/{BnvCountUrl}");
                    var bnvOpDataUrl = new Uri($"ftp://{DataSource}/{BnvOpDataUrl}");

                    _logger.LogTrace("BnaCountUrl: {url}", bnaCountUrl);
                    _logger.LogTrace("BnaOpDataUrl: {url}", bnaOpDataUrl);
                    _logger.LogTrace("BnvCountUrl: {url}", bnvCountUrl);
                    _logger.LogTrace("BnvOpDataUrl: {url}", bnvOpDataUrl);

                    if (LoginCrypt.TryDecryptLoginPwd(AuthInfo, out string Login, out string Password))
                    {
                        var _BnaCountData = await DownloadData(bnaCountUrl, Login, Password, cancellationToken);
                        BnaCountData = _BnaCountData?.ToArray();

                        var _BnaOpData = await DownloadData(bnaOpDataUrl, Login, Password, cancellationToken);
                        BnaOpData = _BnaOpData?.ToArray();

                        var _BnvCountData = await DownloadData(bnvCountUrl, Login, Password, cancellationToken);
                        BnvCountData = _BnvCountData?.ToArray();

                        var _BnvOpData = await DownloadData(bnvOpDataUrl, Login, Password, cancellationToken);
                        BnvOpData = _BnvOpData?.ToArray();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("{ex}", ex);
                }
            }
            else if (Protocol == CashierOpDataProtocol.SFTP)
            {
                try
                {
                    if (string.IsNullOrEmpty(DataSource))
                    {
                        _logger.LogError("Не задан DataSource");
                        return null;
                    }

                    if (string.IsNullOrEmpty(AuthInfo))
                    {
                        _logger.LogError("Не задан AuthInfo");
                        return null;
                    }

                    _logger.LogTrace("BnaCountUrl: Host {host} Url {url}", DataSource, BnaCountUrl);
                    _logger.LogTrace("BnaOpDataUrl: Host {host} Url {url}", DataSource, BnaOpDataUrl);
                    _logger.LogTrace("BnvCountUrl: Host {host} Url {url}", DataSource, BnvCountUrl);
                    _logger.LogTrace("BnvOpDataUrl: Host {host} Url {url}", DataSource, BnvOpDataUrl);

                    if (LoginCrypt.TryDecryptLoginPwd(AuthInfo, out string Login, out string Password))
                    {
                        var connectionInfo = new ConnectionInfo(DataSource, DataPort, Login, new PasswordAuthenticationMethod(Login, Password));
                        using var client = new SftpClient(connectionInfo);
                        client.Connect();

                        var registration = cancellationToken.Register(() =>
                        {
                            _logger.LogWarning("Операция прервана");
                            client.Dispose();
                        }, useSynchronizationContext: false);
                        try
                        {
                            BnaCountData = client.ReadAllBytes(BnaCountUrl);
                            BnaOpData = client.ReadAllBytes(BnaOpDataUrl);
                            BnvCountData = client.ReadAllBytes(BnvCountUrl);
                            BnvOpData = client.ReadAllBytes(BnvOpDataUrl);
                        }
                        finally
                        {
                            registration.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("{ex}", ex);
                }
            }
            else
            {
                _logger.LogError("Поддерживаются только протоколы FTP, SFTP и File");
                return null;
            }

            if (BnaCountData == null)
            {
                _logger.LogError("BnaCountData is null");
                return null;
            }

            if (BnaOpData == null)
            {
                _logger.LogError("BnaOpData is null");
                return null;
            }

            if (BnvCountData == null)
            {
                _logger.LogError("BnvCountData is null");
                return null;
            }

            if (BnvOpData == null)
            {
                _logger.LogError("BnvOpData is null");
                return null;
            }

            _logger.LogTrace("BnaCountData {length} bytes read", BnaCountData.Length);
            _logger.LogTrace("BnaOpData {length} bytes read", BnaOpData.Length);
            _logger.LogTrace("BnvCountData {length} bytes read", BnvCountData.Length);
            _logger.LogTrace("BnvOpData {length} bytes read", BnvOpData.Length);

            try
            {
                for (int i = 0; i < 8; i++)
                {
                    var d = new CashierOpData<EntervoDeviceKind>(
                        rating: (uint)GetLittleEndianIntegerFromByteArray(BnaOpData, i * 71 + 123),
                        position: (uint)GetLittleEndianIntegerFromByteArray(BnaOpData, i * 71 + 139),
                        currency: Encoding.Default.GetString(BnaOpData, i * 71 + 143, 3),
                        count: (uint)GetLittleEndianIntegerFromByteArray(BnaCountData, i * 4 + 12),
                        deviceKind: EntervoDeviceKind.BNA);
                    if (d.Rating != 0)
                        CashierData.Add(d);
                }

                for (int i = 0; i < 8; i++)
                {
                    var d = new CashierOpData<EntervoDeviceKind>(
                        rating: (uint)GetLittleEndianIntegerFromByteArray(BnvOpData, i * 71 + 123),
                        position: (uint)GetLittleEndianIntegerFromByteArray(BnvOpData, i * 71 + 139),
                        currency: Encoding.Default.GetString(BnvOpData, i * 71 + 143, 3),
                        count: (uint)GetLittleEndianIntegerFromByteArray(BnvCountData, i * 4 + 12),
                        deviceKind: EntervoDeviceKind.BNV);
                    if (d.Rating != 0)
                        CashierData.Add(d);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("{ex}", ex);
            }

            return CashierData;
        }

        public static int GetBigEndianIntegerFromByteArray(byte[] data, int startIndex)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            return (data[startIndex] << 24)
                 | (data[startIndex + 1] << 16)
                 | (data[startIndex + 2] << 8)
                 | data[startIndex + 3];
        }

        public static int GetLittleEndianIntegerFromByteArray(byte[] data, int startIndex)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            return (data[startIndex + 3] << 24)
                 | (data[startIndex + 2] << 16)
                 | (data[startIndex + 1] << 8)
                 | data[startIndex];
        }

        public static short GetBigEndianShortFromByteArray(byte[] data, int startIndex)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            return (short)((ushort)(data[startIndex] << 8) | data[startIndex + 1]);
        }

        public static short GetLittleEndianShortFromByteArray(byte[] data, int startIndex)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            return (short)((ushort)(data[startIndex + 1] << 8) | data[startIndex]);
        }

        private async Task<IEnumerable<byte>> DownloadData(Uri Uri, string Login, string Password, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                _logger.LogTrace("Host: {host}", Uri.Host);
                _logger.LogTrace("Path: {path}", Uri.AbsolutePath);
                using var ftp = new FtpClient(Uri.Host, Login, Password);
                ftp.Connect();
                using var stream = new MemoryStream();
                if (await ftp.DownloadAsync(stream, Uri.AbsolutePath, token: cancellationToken))
                    return await ReadByteArrayAsync(stream, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("{ex}", ex);
            }

            return default;
        }

        private static async Task<IEnumerable<byte>> ReadByteArrayAsync(Stream input, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            byte[] buffer = new byte[16 * 1024];
            using MemoryStream ms = new();
            int read;
            while ((read = await input.ReadAsync(buffer.AsMemory(0, buffer.Length), cancellationToken)) > 0)
                ms.Write(buffer, 0, read);
            return ms.ToArray();
        }
    }
}
