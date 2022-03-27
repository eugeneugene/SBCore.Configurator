using Microsoft.Extensions.DependencyInjection;
using SBCore.Configurator.TextResources;
using System;
using System.Windows;
using System.Windows.Input;

namespace SBCore.Configurator.EditWindows
{
    internal partial class PicturePreviewWindow
    {
        private void ImageIsLoadedCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is PicturePreviewWindow)
            {
                e.CanExecute = ImageIsLoaded && bitmapImage != null;
                e.Handled = true;
            }
        }

        private void ImageUpdateCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is PicturePreviewWindow)
            {
                e.CanExecute = !ImageIsBusy;
                e.Handled = true;
            }
        }

        private void CloseCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void CopyCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                Clipboard.SetImage(bitmapImage);
                Message(Russian.ImageCopied);
            }
            catch (Exception ex)
            {
                Error(ex.Message, ex.ToString());
            }
        }

        private void FullScreenPreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var fullScreenPreviewWindow = _serviceProvider.GetRequiredService<FullScreenPreviewWindow>();
            fullScreenPreviewWindow.BitmapImage = bitmapImage;
            fullScreenPreviewWindow.ShowDialog();
        }

        private async void ImageUpdateExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            await ShowPictureAsync(_applicationLifetime.ApplicationStopping);
        }
    }
}
