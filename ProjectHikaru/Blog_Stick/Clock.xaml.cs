using System;
using System.Net;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;

namespace Blog_Stick
{
    public partial class Clock : UserControl
    {
        public Clock()
        {
            this.InitializeComponent();
            this.ClockTick_Tick(this, null);
            this.SetClock();
        }

        private void ClockTick_Tick(object sender, EventArgs e)
        {
            ((TextBlock)this.FindName("TimeText")).Text = DateTime.Now.ToString("hh:mm");
            if (DateTime.Now.Hour < 12)
            {
                this.PM.Opacity = 0.4;
                this.AM.Opacity = 1;
            }
            else
            {
                this.PM.Opacity = 1;
                this.AM.Opacity = 0.4;
            }
        }

        private void fontDownloader_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
#warning
            ((TextBlock)this.FindName("TimeText")).FontSource = new FontSource(e.Result);
            ((TextBlock)this.FindName("TimeText")).FontFamily = new FontFamily("Pocket Calculator");
        }

        private void SetClock()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += this.ClockTick_Tick;
            timer.Start();
            //WebClient f = new WebClient();
            //f.OpenReadAsync(new Uri("Blog_StickTestPage.aspx"));
            //return;

            WebClient client = new WebClient();
            client.OpenReadCompleted += this.fontDownloader_OpenReadCompleted;
            client.OpenReadAsync(new Uri("Pockc.ttf", UriKind.Relative));
        }        
    }
}