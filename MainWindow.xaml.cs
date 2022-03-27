using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Win32;
using SBCore.Configurator;
using SBCore.Configurator.TextResources;
using SBShared;
using Shared;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace SBCoreConfigurator
{
    internal partial class MainWindow : Window
    {
        private readonly OpenFileDialog _openFileDialog = new();
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _applicationLifetime;

        private readonly MRU _mru = new(5);
        private readonly ObservableCollection<string> _mruList = new();

        public MainWindow(IServiceProvider serviceProvider, IHostApplicationLifetime applicationLifetime)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _applicationLifetime = applicationLifetime ?? throw new Exception(nameof(applicationLifetime));
            _applicationLifetime.ApplicationStopping.Register(() => Close());

            _openFileDialog.FileName = "SBCore.db";
            _openFileDialog.DefaultExt = ".db";
            _openFileDialog.Filter = "SQLite database files (.db)|*.db";
            _openFileDialog.CheckFileExists = false;
            _openFileDialog.CheckPathExists = true;
            _openFileDialog.Multiselect = false;
            _openFileDialog.Title = Russian.OpenFileDialogTitle;

            CollectionViewSource itemCollectionViewSource;
            itemCollectionViewSource = (CollectionViewSource)FindResource("MRUCollection");
            itemCollectionViewSource.Source = _mruList;
        }

        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void DbNameBrowseClick(object sender, RoutedEventArgs e)
        {
            bool? result = _openFileDialog.ShowDialog();
            if (result == true)
                dbName.Text = _openFileDialog.FileName;
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveConfig();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            dbOpen.IsEnabled = false;
            dbName.Text = Main.Default.dbName ?? string.Empty;
            _mru.LoadList(new string[]
            {
                Main.Default.dbNameMRU1 ?? string.Empty,
                Main.Default.dbNameMRU2 ?? string.Empty,
                Main.Default.dbNameMRU3 ?? string.Empty,
                Main.Default.dbNameMRU4 ?? string.Empty,
                Main.Default.dbNameMRU5 ?? string.Empty,
            });

            foreach (var item in _mru.MRUList)
            {
                if (!string.IsNullOrWhiteSpace(item))
                    _mruList.Add(item);
            }

            StatusBlock.Text = VersionHelper.VersionString;
        }

        private void SaveConfig()
        {
            Main.Default.dbName = dbName.Text;
            var a = _mru.MRUList.ToArray();
            Main.Default.dbNameMRU1 = a[0];
            Main.Default.dbNameMRU2 = a[1];
            Main.Default.dbNameMRU3 = a[2];
            Main.Default.dbNameMRU4 = a[3];
            Main.Default.dbNameMRU5 = a[4];
            Main.Default.Save();
        }

        private void DbOpenClick(object sender, RoutedEventArgs e)
        {
            var text = dbName.Text;
            _mru.ProcessMRUEntry(dbName.Text);

            _mruList.Clear();
            foreach (var item in _mru.MRUList)
            {
                if (!string.IsNullOrWhiteSpace(item))
                    _mruList.Add(item);
            }

            dbName.Text = text;
            var _dbAnalyze = _serviceProvider.GetRequiredService<DbAnalyze>();
            _dbAnalyze.DbName = text;
            _dbAnalyze.OpenMode = comboOpenMode.Text;
            _dbAnalyze.ShowDialog();
        }

        private void DbNameTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            dbOpen.IsEnabled = !string.IsNullOrWhiteSpace(dbName.Text);
        }
    }
}
