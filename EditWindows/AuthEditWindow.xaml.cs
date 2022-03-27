using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SBCore.Configurator.Models;
using System.Windows;
using System.Windows.Controls;

namespace SBCore.Configurator.EditWindows
{
    internal partial class AuthEditWindow : Window
    {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _applicationLifetime;

        public AuthModel Model { get; }

        public AuthEditWindow(ILogger<AuthEditWindow> logger, IHostApplicationLifetime applicationLifetime)
        {
            InitializeComponent();

            _logger = logger;
            _applicationLifetime = applicationLifetime;
            _applicationLifetime.ApplicationStopping.Register(() => Close());
            _logger.LogDebug("Создание экземпляра {Name}", GetType().Name);

            Model = new();
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;
        }
    }
}
