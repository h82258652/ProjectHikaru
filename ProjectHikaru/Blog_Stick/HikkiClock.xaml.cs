using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Blog_Stick
{
    public partial class HikkiClock : UserControl
    {
        private Storyboard _animate;

        public HikkiClock()
        {
            this.InitializeComponent();
            this.clock.MouseLeftButtonDown += this.clock_MouseLeftButtonDown;
            this.blink.MouseLeftButtonDown += this.blink_MouseLeftButtonDown;
            this._animate = (Storyboard)((FrameworkElement)this.FindName("speak")).FindName("SpeakAnimate");
            this._animate.Completed += _animate_Completed;
        }

        private void _animate_Completed(object sender, EventArgs e)
        {
            this._animate.Stop();
            this._animate.Begin();
        }

        private void blink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this._animate.Stop();
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(this.GetHelloASXString())))
            {
                this.player.SetSource(stream);
            }
            this.player.MediaEnded += player_clockTime;
            this.blink.Visibility = Visibility.Collapsed;
            this.speak.Visibility = Visibility.Visible;
            this.player.Play();
            this._animate.Begin();
        }

        private void clock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this._animate.Stop();
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(this.GetClockASXString())))
            {
                this.player.SetSource(stream);
            }
            this.player.MediaEnded += player_clockTime;
            this.blink.Visibility = Visibility.Collapsed;
            this.speak.Visibility = Visibility.Visible;
            this.player.Play();
            this._animate.Begin();
        }

        private string GetClockASXString()
        {
            DateTime now = DateTime.Now;
            string hour = now.ToString("hh");
            string asx = "<ASX Version=\"3.0\"><Entry><Ref href=\"./clock/now.jpg\"/></Entry>";
            if (hour[0] != '0')
            {
                asx = asx + "<Entry><Ref href=\"./clock/10.jpg\"/></Entry>";
            }
            if (hour[1] > '0')
            {
                asx = string.Concat(new object[] { asx, "<Entry><Ref href=\"./clock/", '0', hour[hour.Length - 1], ".jpg\"/></Entry>" });
            }
            asx = asx + "<Entry><Ref href=\"./clock/time.jpg\"/></Entry>";
            int minute = now.Minute;
            if ((minute / 10) > 1)
            {
                var temp = minute / 10;
                asx = asx + "<Entry><Ref href=\"./clock/" + temp.ToString("00") + ".jpg\"/></Entry><Entry><Ref href=\"./clock/10.jpg\"/></Entry>";
            }
            if ((minute / 10) == 1)
            {
                asx = asx + "<Entry><Ref href=\"./clock/10.jpg\"/></Entry>";
            }
            if ((minute % 10) >= 1)
            {
                asx = asx + "<Entry><Ref href=\"./clock/" + (minute % 10).ToString("00") + ".jpg\"/></Entry>";
            }
            if (minute > 0)
            {
                asx = asx + "<Entry><Ref href=\"./clock/mins.jpg\"/></Entry>";
            }
            return asx + "</ASX>";
        }

        private string GetHelloASXString()
        {
            DateTime now = DateTime.Now;
            int hour = now.Hour;
            Random random = new Random(now.Millisecond);
            string temp = "";
            string asx = "<ASX Version=\"3.0\">";
            if ((hour >= 6) && (hour <= 9))
            {
                temp = "moring" + (random.Next(2) + 1).ToString("00") + ".jpg";
            }
            else if ((hour >= 12) && (hour <= 14))
            {
                temp = "noon" + (random.Next(2) + 1).ToString("00") + ".jpg";
            }
            else if ((hour >= 0x16) || (hour <= 6))
            {
                temp = "night" + (random.Next(2) + 1).ToString("00") + ".jpg";
            }
            else
            {
                temp = "ap" + (random.Next(20) + 1).ToString("00") + ".jpg";
            }
            return asx + "<Entry><Ref href=\"./ap/" + temp + "\"/></Entry></ASX>";
        }

        private void player_clockTime(object sender, RoutedEventArgs e)
        {
            this.blink.Visibility = Visibility.Visible;
            this.speak.Visibility = Visibility.Collapsed;
            this.player.Stop();
            this._animate.Stop();
        }
    }
}