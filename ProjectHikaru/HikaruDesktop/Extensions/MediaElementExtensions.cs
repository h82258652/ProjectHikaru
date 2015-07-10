using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HikaruDesktop.Extensions
{
    public static class MediaElementExtensions
    {
        public static async Task WaitForMediaEndedAsync(this MediaElement mediaElement)
        {
            TaskCompletionSource<RoutedEventArgs> tcs = new TaskCompletionSource<RoutedEventArgs>();

            RoutedEventHandler handler = null;

            handler = (sender, e) =>
            {
                mediaElement.MediaEnded -= handler;
                tcs.SetResult(e);
            };

            mediaElement.MediaEnded += handler;

            await tcs.Task;
        }
    }
}