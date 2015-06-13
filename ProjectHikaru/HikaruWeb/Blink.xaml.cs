using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HikaruWeb
{
    public sealed partial class Blink : UserControl
    {
        private double _elapsedSeconds = 0.0d;
        private DispatcherTimer _timer = new DispatcherTimer();

        public Blink()
        {
            this.InitializeComponent();
            this._timer.Interval = TimeSpan.FromSeconds(0.1d);
            this._timer.Tick += this.Timer_Tick;
            this._timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            this._elapsedSeconds += 0.1d;
            if (this._elapsedSeconds > 7.0d)
            {
                this._elapsedSeconds = 0.0d;
            }
            this._elapsedSeconds = Math.Round(this._elapsedSeconds, 1);// 修正 javascript 浮点数精确度。

            if (this._elapsedSeconds == 3.0d)
            {
                this.imgBlink.Visibility = Visibility.Visible;
            }
            else if (this._elapsedSeconds == 3.1d)
            {
                this.imgBlink.Visibility = Visibility.Collapsed;
            }
            else if (this._elapsedSeconds == 3.2d)
            {
                this.imgBlink.Visibility = Visibility.Visible;
            }
            else if (this._elapsedSeconds == 3.3d)
            {
                this.imgBlink.Visibility = Visibility.Collapsed;
            }
            else if (this._elapsedSeconds == 4.8d)
            {
                this.imgBlink.Visibility = Visibility.Visible;
            }
            else if (this._elapsedSeconds == 4.9d)
            {
                this.imgBlink.Visibility = Visibility.Collapsed;
            }
            else if (this._elapsedSeconds == 5.0d)
            {
                this.imgSmile.Visibility = Visibility.Collapsed;
            }
            else if (this._elapsedSeconds == 7.0d)
            {
                this.imgSmile.Visibility = Visibility.Visible;
            }
        }
    }
}