using Microsoft.Extensions.DependencyInjection;
using SBCore.Configurator.TextResources;
using System;
using System.Windows;
using System.Windows.Input;

namespace SBCore.Configurator.EditWindows
{
    internal partial class CmiuImagePreviewWindow
    {
        private void ImageIsLoadedCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is CmiuImagePreviewWindow)
            {
                e.CanExecute = ImageIsLoaded && bitmapImage != null;
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
                Message(ex.Message);
            }
        }

        private void FullScreenPreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var fullScreenPreviewWindow = _serviceProvider.GetRequiredService<FullScreenPreviewWindow>();
            fullScreenPreviewWindow.BitmapImage = bitmapImage;
            fullScreenPreviewWindow.ShowDialog();
        }
    }
}
