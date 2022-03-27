using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SBCore.Configurator.EditWindows
{
    /// <summary>
    /// Interaction logic for FullScreenPreviewWindow.xaml
    /// </summary>
    internal partial class FullScreenPreviewWindow : Window
    {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _applicationLifetime;

        public BitmapImage BitmapImage { get; set; } = null;

        public FullScreenPreviewWindow(ILogger<FullScreenPreviewWindow> logger, IHostApplicationLifetime applicationLifetime)
        {
            InitializeComponent();

            _logger = logger;
            _applicationLifetime = applicationLifetime;
            _applicationLifetime.ApplicationStopping.Register(() => Close());
            _logger.LogDebug("Создание экземпляра {Name}", GetType().Name);
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            Image.Source = BitmapImage;
            Image.Visibility = Visibility.Visible;
        }

        private void WindowMouseClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            Close();
        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (e.Key == Key.Escape && e.KeyStates == KeyStates.Down && e.SystemKey == Key.None)
                Close();
        }
    }
}
