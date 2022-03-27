using Microsoft.Extensions.Logging;
using SBCore.Configurator.TextResources;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SBCore.Configurator.EditWindows
{
    internal partial class CmiuImagePreviewWindow : Window
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        private volatile bool ImageIsBusy = false;
        private volatile bool ImageIsLoaded = false;

        public Image Image
        {
            get => _image;
            set
            {
                _image = value;
                ShowPicture();
            }
        }

        private Image _image;
        private BitmapImage bitmapImage;

        public CmiuImagePreviewWindow(ILogger<CmiuImagePreviewWindow> logger, IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _logger = logger;
            _serviceProvider = serviceProvider;

            _logger.LogDebug("Создание экземпляра {Name}", GetType().Name);
        }

        private void ShowPicture()
        {
            try
            {
                if (ImageIsBusy)
                {
                    Debug.WriteLine("Image is busy");
                    return;
                }

                if (_image == null)
                {
                    FormImage = null;
                    return;
                }

                ImageIsBusy = true;
                ImageIsLoaded = false;

                using var ms = new MemoryStream { Position = 0 };
                _image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                FormImage.Dispatcher.Invoke(() =>
                {
                    var bi = new BitmapImage();
                    bi.BeginInit();
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.StreamSource = ms;
                    bi.EndInit();
                    FormImage.Source = bi;
                    bitmapImage = bi;
                });

                ImageIsLoaded = true;
            }
            catch (Exception ex)
            {
                Message(Russian.ExceptionA + ex.Message);
            }
            finally
            {
                ImageIsBusy = false;
            }
        }

        private void Message(string msg)
        {
            _logger.LogInformation(msg);
            StatusBlock.Dispatcher.Invoke(() =>
            {
                StatusBlock.Text = msg;
            });
        }
    }
}
