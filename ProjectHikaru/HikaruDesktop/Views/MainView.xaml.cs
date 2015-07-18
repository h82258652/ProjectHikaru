using HikaruDesktop.Datas;
using System;
using System.Windows;
using System.Windows.Input;

namespace HikaruDesktop.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            this.InitializeComponent();
            this.LoadLocation();
            this.LoadMode();
        }

        private void LoadMode()
        {
            if (AppConfig.Mode == 0)
            {
                hikaruMurmur.Visibility = Visibility.Visible;
                hikaruClock.Visibility = Visibility.Collapsed;
            }
            else
            {
                hikaruClock.Visibility = Visibility.Visible;
                hikaruMurmur.Visibility = Visibility.Collapsed;
            }
        }

        private void LoadLocation()
        {
            double left = AppConfig.Left;
            double top = AppConfig.Top;

            if (left < 0 || top < 0)
            {
                this.Left = SystemParameters.WorkArea.Width - this.Width;
                this.Top = SystemParameters.WorkArea.Height - this.Height;
            }
            else
            {
                this.Left = left;
                this.Top = top;
            }
        }

        private void MnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveLocation()
        {
            AppConfig.Left = this.Left;
            AppConfig.Top = this.Top;
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            this.SaveLocation();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void MnuSwitchMode_Click(object sender, RoutedEventArgs e)
        {
            if (AppConfig.Mode == 0)
            {
                AppConfig.Mode = 1;
            }
            else
            {
                AppConfig.Mode = 0;
            }
            this.LoadMode();
        }
    }
}