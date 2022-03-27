using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using SBCore.Configurator;
using SBCore.Configurator.Data;
using SBCore.Configurator.EditWindows;
using SBCore.Configurator.Services;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

namespace SBCoreConfigurator
{
    public partial class App : Application
    {
        private readonly IHost host;

        public App()
        {
            var args = Environment.GetCommandLineArgs();
            var hostBuilder = Host.CreateDefaultBuilder(args);
            hostBuilder.ConfigureAppConfiguration((context, builder) => { });
            hostBuilder.ConfigureServices((context, services) =>
            {
                ConfigureServices(services);
            });
            host = hostBuilder.Build();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<Connection>();
            services.AddTransient<DbAnalyze>();
            services.AddTransient<AdminEditWindow>();
            services.AddTransient<ParkingEditWindow>();
            services.AddTransient<AisLogEditWindow>();
            services.AddTransient<AisSessionDeliveryStatusEditWindow>();
            services.AddTransient<AisReplacementEditWindow>();
            services.AddTransient<CmiuDeviceEditWindow>();
            services.AddTransient<IAPSCashReader, APSCashReader>();
            services.AddTransient<CashPreviewWindow>();
            services.AddTransient<ConfigEditWindow>();
            services.AddTransient<CmiuCameraEditWindow>();
            services.AddTransient<AuthEditWindow>();
            services.AddTransient<PicturePreviewWindow>();
            services.AddTransient<FullScreenPreviewWindow>();
            services.AddTransient<VideoPreviewWindow>();
            services.AddTransient<LprIssEventEditWindow>();
            services.AddTransient<CmiuCashStatusEditWindow>();
            services.AddTransient<EventCameraEditWindow>();
            services.AddTransient<CmiuEventEditWindow>();
            services.AddTransient<CmiuImageEditWindow>();
            services.AddTransient<CmiuIssImageEditWindow>();
            services.AddTransient<CmiuImagePreviewWindow>();
            services.AddTransient<CmiuLevelsEditWindow>();
            services.AddTransient<DeviceCmiuCameraEditWindow>();
            services.AddTransient<CmiuLprEditWindow>();
            services.AddTransient<CmiuMovementEditWindow>();
            services.AddTransient<CmiuParkingLevelEditWindow>();
            services.AddTransient<CmiuPaymentEditWindow>();
            services.AddTransient<CmiuPlacesEditWindow>();
            services.AddTransient<CmiuPlacesUrlEditWindow>();
            services.AddTransient<ExemptionJobsEditWindow>();
            services.AddTransient<ExemptionLogEditWindow>();
            services.AddTransient<ExemptionParkingEditWindow>();
            services.AddTransient<ExemptionProvidersEditWindow>();
            services.AddHttpClient("Configurator", (provider, client) => client.Timeout = TimeSpan.FromSeconds(5)).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, x509Certificate, x509Chain, sslPolicyErrors) =>
                {
                    return true;
                },
            });
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CurrentUICulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CurrentUICulture;

            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            await host.StartAsync();

            var mainWindow = host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await host.StopAsync(TimeSpan.FromSeconds(5));
            host.Dispose();

            LogManager.Flush((ex) =>
            {
                if (ex != null)
                    Debug.WriteLine("Exception: {0}", ex.Message);
            }, TimeSpan.FromSeconds(0.5));
            LogManager.Shutdown();

            base.OnExit(e);
        }

        private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Debug.WriteLine("Unhandled exception: {0}", e.Exception);
#if DEBUG
            MessageBox.Show(e.Exception.ToString(), "Unhandled exception");
#else
            MessageBox.Show(e.Exception.Message, "Unhandled exception");
#endif
            if (e.Exception is COMException comException && comException.ErrorCode == -2147221040)
                e.Handled = true;
        }
    }
}
