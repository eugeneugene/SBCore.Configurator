using CameraShared.CameraHelpers;
using CameraShared.Types;
using CameraShared.Types.Data;
using LibVLCSharp.Shared;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Targets;
using NLog.Targets.Wrappers;
using ONVIF.Media;
using SBCore.Configurator.TextResources;
using Shared;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using Brushes = System.Windows.Media.Brushes;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace SBCore.Configurator.EditWindows
{
    enum VideoPreviewViewMode { None, Video, Log, Wait }
    enum VlcVerbosity { Default = -1, Info = 0, Error = 1, Warning = 2, Debug = 3 }

    internal partial class VideoPreviewWindow : Window, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHostApplicationLifetime _applicationLifetime;

        private IOnvifCameraProfiles OnvifCameraProfiles;

        private readonly LibVLC _libVLC;
        private readonly MediaPlayer _mediaPlayer;
        private bool _disposed;
        private VideoPreviewViewMode viewMode = VideoPreviewViewMode.None;

        private readonly AutoResetEvent Stopped = new(false);

        public CameraItem CameraItem { get; set; }
        private string CameraLogin;
        private string CameraPassword;

        private readonly AsyncTargetWrapper asyncTarget;
        private readonly MethodCallTarget wrappedTarget;

        public VideoPreviewWindow(ILogger<VideoPreviewWindow> logger, IHostEnvironment environment, IHttpClientFactory clientFactory, IHostApplicationLifetime applicationLifetime)
        {
            InitializeComponent();

            _logger = logger;
            _clientFactory = clientFactory;
            _applicationLifetime = applicationLifetime;

            wrappedTarget = new MethodCallTarget("LogControl", (logEvent, parms) => AddLogMessage(logEvent, parms));
            asyncTarget = new AsyncTargetWrapper("AsyncTarget", wrappedTarget)
            {
                //  ForceLockingQueue = true,
                OverflowAction = AsyncTargetWrapperOverflowAction.Grow,
            };

            LogManager.Configuration.AddTarget(asyncTarget);
            var rule = new NLog.Config.LoggingRule("*", NLog.LogLevel.Trace, asyncTarget);  // LogLevel.Debug - Не выводить в LogControl сообщения от VLC (виснет на Flush)
            LogManager.Configuration.LoggingRules.Add(rule);
            LogManager.ReconfigExistingLoggers();

            var VlcVerbose = environment.IsDevelopment() ? VlcVerbosity.Debug : VlcVerbosity.Default;

            _libVLC = new LibVLC($"verbose={VlcVerbose}");
            _mediaPlayer = new MediaPlayer(_libVLC);

            _logger.LogDebug("Создание экземпляра {Name}", GetType().Name);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                OnvifCameraProfiles?.MediaClient.Abort();
                _mediaPlayer?.Dispose();
                _libVLC?.Dispose();
                Stopped?.Dispose();

                wrappedTarget?.Dispose();
                asyncTarget?.Dispose();
            }

            _disposed = true;
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

        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            // we need the VideoView to be fully loaded before setting a MediaPlayer on it.
            VideoView.Loaded += (sender, e) => VideoView.MediaPlayer = _mediaPlayer;
            _libVLC.Log += (sender, e) => _logger.LogTrace("{message}", e.Message);
            _mediaPlayer.EncounteredError += (object sender, EventArgs e) => _logger.LogError("Media Player encountered an error");
            _mediaPlayer.Stopped += (object sender, EventArgs e) => { Stopped.Set(); };

            await ShowVideo();
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _libVLC.Log -= (sender, e) => _logger.LogTrace("{message}", e.Message);
        }

        private async Task ShowVideo()
        {
            try
            {
                MakeView(VideoPreviewViewMode.Wait);
                Message(Russian.CameraPrepare);
                if (CameraItem is not null)
                {
                    CameraBase Camera = null;
                    float? Quality = null;
                    VideoResolution Resolution = null;
                    Profile profile = null;

                    LoginCrypt.TryDecryptLoginPwd(CameraItem.Auth, out CameraLogin, out CameraPassword);
                    var prepare = await PrepareCameraAsync(_applicationLifetime.ApplicationStopping);
                    if (!prepare)
                    {
                        Error(Russian.CameraNotReady);
                        return;
                    }

                    if (CameraItem.Cameratype == CmiuCameraType.ONVIF)
                    {
                        profile = OnvifCameraProfiles.Profiles.FirstOrDefault(item => item.VideoEncoderConfiguration.Encoding == VideoEncoding.H264);
                        if (profile is null)
                        {
                            Error(Russian.ProfileJPEGNotFound);
                            return;
                        }

                        Camera = new OnvifCamera(
                            httpClient: _clientFactory.CreateClient(),
                            mediaClient: OnvifCameraProfiles.MediaClient,
                            profile: profile,
                            cameraUri: CameraItem.CameraUri,
                            user: CameraLogin,
                            password: CameraPassword,
                            compression: CameraItem.Compression ?? 0,
                            resolution: CameraItem.Resolution,
                            delay: CameraItem.Delay,
                            timeout: null);
                        Quality = profile.VideoEncoderConfiguration.Quality;
                        Resolution = profile.VideoEncoderConfiguration.Resolution;
                    }

                    if (Camera is null)
                    {
                        Error(Russian.InvalidCameraType);
                        return;
                    }

                    StreamSetup streamSetup = new()
                    {
                        Stream = StreamType.RTPUnicast,
                        Transport = new Transport { Protocol = TransportProtocol.RTSP }
                    };

                    var mediaUri = await OnvifCameraProfiles.MediaClient.GetStreamUriAsync(streamSetup, profile!.token);
                    var uri = new Uri(mediaUri.Uri);

                    Message(Russian.StartVideoA + uri);
                    var media = new LibVLCSharp.Shared.Media(_libVLC, uri.ToString(), FromType.FromLocation);
                    var mc = new MediaConfiguration
                    {
                        EnableHardwareDecoding = true
                    };
                    media.AddOption(mc);
                    media.AddOption($"rtsp-user={CameraLogin}");
                    media.AddOption($"rtsp-pwd={CameraPassword}");

                    MakeView(VideoPreviewViewMode.Video);

                    VideoView.Dispatcher.Invoke(() => VideoView.MediaPlayer?.Play(media));
                }
            }
            catch (Exception ex)
            {
                Error(ex.Message, ex.ToString());
            }
        }

        private async Task<bool> PrepareCameraAsync(CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (CameraItem is null)
                    return false;

                if (CameraLogin is null || CameraPassword is null)
                    return false;

                if (CameraItem.Cameratype != CmiuCameraType.ONVIF)
                    return true;

                if (OnvifCameraProfiles is not null)
                    return true;

                Message(Russian.ProfileLoad);
                OnvifCameraProfiles = await OnvifHelper.GetOnvifCameraProfilesAsync(CameraItem.CameraUri, CameraLogin, CameraPassword, cancellationToken);
                if (OnvifCameraProfiles is not null)
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

        private void MakeView(VideoPreviewViewMode mode)
        {
            switch (mode)
            {
                case VideoPreviewViewMode.None:
                    Log.Dispatcher.Invoke(() => { Log.Visibility = Visibility.Hidden; });
                    VideoView.Dispatcher.Invoke(() => { VideoView.Visibility = Visibility.Hidden; });
                    WaitLabel.Dispatcher.Invoke(() => { WaitLabel.Visibility = Visibility.Hidden; });
                    viewMode = VideoPreviewViewMode.None;
                    break;
                case VideoPreviewViewMode.Video:
                    Log.Dispatcher.Invoke(() => { Log.Visibility = Visibility.Hidden; });
                    WaitLabel.Dispatcher.Invoke(() => { WaitLabel.Visibility = Visibility.Hidden; });
                    VideoView.Dispatcher.Invoke(() => { VideoView.Visibility = Visibility.Visible; });
                    viewMode = VideoPreviewViewMode.Video;
                    break;
                case VideoPreviewViewMode.Log:
                    VideoView.Dispatcher.Invoke(() => { VideoView.Visibility = Visibility.Hidden; });
                    WaitLabel.Dispatcher.Invoke(() => { WaitLabel.Visibility = Visibility.Hidden; });
                    Log.Dispatcher.Invoke(() => { Log.Visibility = Visibility.Visible; });
                    viewMode = VideoPreviewViewMode.Log;
                    break;
                case VideoPreviewViewMode.Wait:
                    Log.Dispatcher.Invoke(() => { Log.Visibility = Visibility.Hidden; });
                    WaitLabel.Dispatcher.Invoke(() => { WaitLabel.Visibility = Visibility.Visible; });
                    VideoView.Dispatcher.Invoke(() => { VideoView.Visibility = Visibility.Visible; });
                    viewMode = VideoPreviewViewMode.Video;
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
                case VideoPreviewViewMode.None:
                case VideoPreviewViewMode.Video:
                    MakeView(VideoPreviewViewMode.Log);
                    break;
                case VideoPreviewViewMode.Log:
                    MakeView(VideoPreviewViewMode.Video);
                    break;
            }
        }

        private void CloseCommandExecuted(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}
