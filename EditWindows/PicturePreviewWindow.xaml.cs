using CameraShared.CameraHelpers;
using CameraShared.Types;
using CameraShared.Types.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Targets;
using NLog.Targets.Wrappers;
using ONVIF.Media;
using SBCore.Configurator.TextResources;
using SBShared.Types.Data;
using Shared;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using Brushes = System.Windows.Media.Brushes;

namespace SBCore.Configurator.EditWindows
{
    enum PicturePreviewViewMode { None, Image, Log, Wait }

    internal partial class PicturePreviewWindow : Window
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHostApplicationLifetime _applicationLifetime;

        private IOnvifCameraProfiles OnvifCameraProfiles;
        private BitmapImage bitmapImage = null;

        public CameraItem CmiuCameraData { get; set; }
        private string CameraLogin;
        private string CameraPassword;
        private PicturePreviewViewMode viewMode = PicturePreviewViewMode.None;

        private readonly AsyncTargetWrapper asyncTarget;
        private readonly MethodCallTarget wrappedTarget;

        private volatile bool ImageIsBusy = false;
        private volatile bool ImageIsLoaded = false;

        public PicturePreviewWindow(ILogger<PicturePreviewWindow> logger, IServiceProvider serviceProvider, IHttpClientFactory clientFactory, IHostApplicationLifetime applicationLifetime)
        {
            InitializeComponent();

            _logger = logger;
            _serviceProvider = serviceProvider;
            _clientFactory = clientFactory;
            _applicationLifetime = applicationLifetime;

            wrappedTarget = new MethodCallTarget("LogControl", (logEvent, parms) => AddLogMessage(logEvent, parms));
            asyncTarget = new AsyncTargetWrapper("AsyncTarget", wrappedTarget)
            {
                ForceLockingQueue = true,
                OverflowAction = AsyncTargetWrapperOverflowAction.Grow,
            };

            LogManager.Configuration.AddTarget(asyncTarget);
            var rule = new NLog.Config.LoggingRule("*", NLog.LogLevel.Trace, asyncTarget);
            LogManager.Configuration.LoggingRules.Add(rule);
            LogManager.ReconfigExistingLoggers();

            _logger.LogDebug("Создание экземпляра {Name}", GetType().Name);
        }

        private void AddLogMessage(LogEventInfo logEventInfo, params object[] _)
        {
            string message = Russian.ImageProcess;
            var message1 = logEventInfo.FormattedMessage.Replace(Environment.NewLine, "; ");
            if (logEventInfo.Level == NLog.LogLevel.Error || logEventInfo.Level == NLog.LogLevel.Fatal)
                Error(message, message1);
            else
                Message(message, message1);
        }

        private async void PicturePreviewWindowLoaded(object sender, RoutedEventArgs e)
        {
            await ShowPictureAsync(_applicationLifetime.ApplicationStopping);
        }

        private async Task ShowPictureAsync(CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (ImageIsBusy)
                    return;

                ImageIsBusy = true;
                ImageIsLoaded = false;
                MakeView(PicturePreviewViewMode.Wait);
                Message(Russian.CameraPrepare);
                if (CmiuCameraData != null)
                {
                    CameraBase Camera = null;
                    float? Quality = null;
                    VideoResolution Resolution = null;
                    LoginCrypt.TryDecryptLoginPwd(CmiuCameraData.Auth, out CameraLogin, out CameraPassword);
                    var prepare = await PrepareCameraAsync(cancellationToken);
                    if (!prepare)
                    {
                        Error(Russian.CameraNotReady);
                        return;
                    }

                    if (CmiuCameraData.Cameratype == CmiuCameraType.ONVIF)
                    {
                        var profile = OnvifCameraProfiles.Profiles.FirstOrDefault(item => item.VideoEncoderConfiguration.Encoding == VideoEncoding.JPEG);
                        if (profile == null)
                        {
                            Error(Russian.ProfileJPEGNotFound);
                            return;
                        }

                        Camera = new OnvifCamera(
                            httpClient: _clientFactory.CreateClient(),
                            mediaClient: OnvifCameraProfiles.MediaClient,
                            profile: profile,
                            cameraUri: CmiuCameraData.CameraUri,
                            user: CameraLogin,
                            password: CameraPassword,
                            compression: CmiuCameraData.Compression ?? 0,
                            resolution: CmiuCameraData.Resolution,
                            delay: CmiuCameraData.Delay,
                            timeout: null);
                        Quality = profile.VideoEncoderConfiguration.Quality;
                        Resolution = profile.VideoEncoderConfiguration.Resolution;
                    }

                    if (CmiuCameraData.Cameratype == CmiuCameraType.Axis)
                        Camera = new AxisCamera(
                            httpClient: _clientFactory.CreateClient(),
                            cameraUri: CmiuCameraData.CameraUri,
                            user: CameraLogin,
                            password: CameraPassword,
                            compression: CmiuCameraData.Compression ?? 0,
                            resolution: CmiuCameraData.Resolution,
                            delay: CmiuCameraData.Delay,
                            timeout: null);
                    if (Camera == null)
                    {
                        Error(Russian.InvalidCameraType);
                        return;
                    }

                    using var image = await Camera.GetImageAsync(cancellationToken);
                    if (image == null)
                    {
                        Error(Russian.NoImageFromCamera);
                        return;
                    }

                    using var ms = new MemoryStream { Position = 0 };
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    Image.Dispatcher.Invoke(() =>
                    {
                        var bi = new BitmapImage();
                        bi.BeginInit();
                        bi.CacheOption = BitmapCacheOption.OnLoad;
                        bi.StreamSource = ms;
                        bi.EndInit();
                        Image.Source = bi;
                        bitmapImage = bi;
                    });

                    if (CmiuCameraData.Cameratype == CmiuCameraType.ONVIF)
                        Message(string.Format(CultureInfo.CurrentCulture,
                            Russian.ImageReceivedOnvifA5,
                            Quality ?? 0.0f,
                            Resolution?.Width ?? 0,
                            Resolution?.Height ?? 0,
                            image.Width,
                            image.Height));
                    if (CmiuCameraData.Cameratype == CmiuCameraType.Axis)
                        Message(Russian.ImageReceivedAxis);

                    MakeView(PicturePreviewViewMode.Image);

                    ImageIsLoaded = true;
                }
            }
            catch (Exception ex)
            {
                Error(ex.Message, ex.ToString());
            }
            finally
            {
                ImageIsBusy = false;
            }
        }

        private async Task<bool> PrepareCameraAsync(CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (CmiuCameraData == null)
                    return false;

                if (CameraLogin == null || CameraPassword == null)
                    return false;

                if (CmiuCameraData.Cameratype != CmiuCameraType.ONVIF)
                    return true;

                if (OnvifCameraProfiles != null)
                    return true;

                Message(Russian.ProfileLoad);
                OnvifCameraProfiles = await OnvifHelper.GetOnvifCameraProfilesAsync(CmiuCameraData.CameraUri, CameraLogin, CameraPassword, cancellationToken);
                if (OnvifCameraProfiles != null)
                {
                    Message(Russian.ProfileLoadSuccess);
                    return true;
                }
                else
                    Error(Russian.ProfileLoadError);
            }
            catch (Exception ex)
            {
                Error(ex.Message, ex.ToString());
            }

            return false;
        }

        private void MakeView(PicturePreviewViewMode mode)
        {
            switch (mode)
            {
                case PicturePreviewViewMode.None:
                    Log.Dispatcher.Invoke(() => { Log.Visibility = Visibility.Hidden; });
                    Image.Dispatcher.Invoke(() => { Image.Visibility = Visibility.Hidden; });
                    WaitLabel.Dispatcher.Invoke(() => { WaitLabel.Visibility = Visibility.Hidden; });
                    viewMode = PicturePreviewViewMode.None;
                    break;
                case PicturePreviewViewMode.Image:
                    Log.Dispatcher.Invoke(() => { Log.Visibility = Visibility.Hidden; });
                    WaitLabel.Dispatcher.Invoke(() => { WaitLabel.Visibility = Visibility.Hidden; });
                    Image.Dispatcher.Invoke(() => { Image.Visibility = Visibility.Visible; });
                    viewMode = PicturePreviewViewMode.Image;
                    break;
                case PicturePreviewViewMode.Log:
                    Image.Dispatcher.Invoke(() => { Image.Visibility = Visibility.Hidden; });
                    WaitLabel.Dispatcher.Invoke(() => { WaitLabel.Visibility = Visibility.Hidden; });
                    Log.Dispatcher.Invoke(() => { Log.Visibility = Visibility.Visible; });
                    viewMode = PicturePreviewViewMode.Log;
                    break;
                case PicturePreviewViewMode.Wait:
                    Log.Dispatcher.Invoke(() => { Log.Visibility = Visibility.Hidden; });
                    WaitLabel.Dispatcher.Invoke(() => { WaitLabel.Visibility = Visibility.Visible; });
                    Image.Dispatcher.Invoke(() => { Image.Visibility = Visibility.Visible; });
                    viewMode = PicturePreviewViewMode.Image;
                    break;
            }
        }

        private void Message(string msg1, string msg2 = null)
        {
            StatusBlock.Dispatcher.Invoke(() =>
            {
                StatusBlock.Text = msg1;
            });
            string msg = msg2 ?? msg1;
            Log.Dispatcher.Invoke(() =>
            {
                Inline inline = new Run(msg);
                Paragraph paragraph = new(inline)
                {
                    TextAlignment = TextAlignment.Left,
                    Foreground = Brushes.Black,
                };
                Log.Document.Blocks.Add(paragraph);
            });
        }

        private void Error(string msg1, string msg2 = null)
        {
            StatusBlock.Dispatcher.Invoke(() =>
            {
                StatusBlock.Text = msg1;
            });
            string msg = msg2 ?? msg1;
            Log.Dispatcher.Invoke(() =>
            {
                Inline inline = new Run(msg);
                Paragraph paragraph = new(inline)
                {
                    TextAlignment = TextAlignment.Left,
                    Foreground = Brushes.Red,
                };
                Log.Document.Blocks.Add(paragraph);
            });
        }

        private void StatusBarMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            switch (viewMode)
            {
                case PicturePreviewViewMode.None:
                case PicturePreviewViewMode.Image:
                    MakeView(PicturePreviewViewMode.Log);
                    break;
                case PicturePreviewViewMode.Log:
                    MakeView(PicturePreviewViewMode.Image);
                    break;
            }
        }
    }
}
