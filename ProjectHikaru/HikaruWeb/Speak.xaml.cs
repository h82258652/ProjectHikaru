using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HikaruWeb
{
    public sealed partial class Speak : UserControl
    {
        private double _elapsedSeconds = 0.0d;
        private DispatcherTimer _timer = new DispatcherTimer();
        private int _times = 0;

        public Speak()
        {
            this.InitializeComponent();
            this._timer.Interval = TimeSpan.FromSeconds(0.1d);
            this._timer.Tick += this.Timer_Tick;
            this._timer.Start();
        }

        internal void SetVisibility(Visibility visibility)
        {
            this.Visibility = visibility;
            if (CSharpXamlForHtml5.DomManagement.IsControlInVisualTree(this))
            {
                var domThis = CSharpXamlForHtml5.DomManagement.GetDomElementFromControl(this);
                domThis.style.marginRight = "-9px";
            }
        }

        internal void SpeakAnimateBegin()
        {
            this.ResetAnimate();
            _elapsedSeconds = 0.0d;
            _times = 0;
            _timer.Start();
        }

        internal void SpeakAnimateStop()
        {
            this.ResetAnimate();
            _timer.Stop();
            _elapsedSeconds = 0.0d;
            _times = 0;
        }

        private void ResetAnimate()
        {
            // 重置动画到初始状态。
            this.imgSpeak1.Visibility = Visibility.Visible;
            this.imgSing2.Visibility = Visibility.Collapsed;
            this.imgSing1.Visibility = Visibility.Collapsed;
            this.imgSpeak2.Visibility = Visibility.Collapsed;
        }

        private void Timer_Tick(object sender, object e)
        {
            this._elapsedSeconds += 0.1d;
            if (this._elapsedSeconds > 1.0d)
            {
                this.ResetAnimate();
                this._elapsedSeconds = 0.0d;
                this._times += 1;
            }
            this._elapsedSeconds = Math.Round(this._elapsedSeconds, 1);// 修正 javascript 浮点数精确度。

            // 重复运行动画 4 次。
            if (this._times > 3)
            {
                // 重置计数器和计时器。
                this._times = 0;
                this.ResetAnimate();
                this._timer.Stop();
                return;
            }

            if (this._elapsedSeconds == 0.3d)
            {
                this.imgSpeak1.Visibility = Visibility.Collapsed;
                this.imgSing2.Visibility = Visibility.Visible;
            }
            else if (this._elapsedSeconds == 0.6d)
            {
                this.imgSing2.Visibility = Visibility.Collapsed;
                this.imgSing1.Visibility = Visibility.Visible;
            }
            else if (this._elapsedSeconds == 1.0d)
            {
                this.imgSing1.Visibility = Visibility.Collapsed;
                this.imgSpeak2.Visibility = Visibility.Visible;
            }
        }
    }
}