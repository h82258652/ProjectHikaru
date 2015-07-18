using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace HikaruDesktop.Views
{
    /// <summary>
    /// Clock.xaml 的交互逻辑
    /// </summary>
    public partial class Clock : UserControl
    {
        public Clock()
        {
            InitializeComponent();
            this.Clock_Tick(this, null);
            this.SetClock();
        }

        private void SetClock()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += this.Clock_Tick;
            timer.Start();
        }

        private void Clock_Tick(object sender, EventArgs e)
        {
            txtTime.Text = DateTime.Now.ToString("hh:mm");
            if (DateTime.Now.Hour < 12)
            {
                this.pm.Opacity = 0.4;
                this.am.Opacity = 1;
            }
            else
            {
                this.pm.Opacity = 1;
                this.am.Opacity = 0.4;
            }
        }
    }
}