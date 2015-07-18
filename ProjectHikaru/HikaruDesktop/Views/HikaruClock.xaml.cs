using HikaruDesktop.Extensions;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HikaruDesktop.Views
{
    /// <summary>
    /// HikaruClock.xaml 的交互逻辑
    /// </summary>
    public partial class HikaruClock : UserControl
    {
        public HikaruClock()
        {
            InitializeComponent();

            this.speak.SpeakAnimate.Completed += delegate
            {
                this.speak.SpeakAnimate.Stop();
                this.speak.SpeakAnimate.Begin();
            };
        }

        private async void Blink_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.speak.SpeakAnimate.Stop();

            this.blink.Visibility = Visibility.Collapsed;
            this.speak.Visibility = Visibility.Visible;
            this.speak.SpeakAnimate.Begin();

            foreach (var helloVoice in GetHelloVoices())
            {
                this.player.Source = new Uri(helloVoice, UriKind.Relative);
                await this.player.WaitForMediaEndedAsync();
            }
            this.PlayerEnded();
        }

        private async void Clock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.speak.SpeakAnimate.Stop();

            this.blink.Visibility = Visibility.Collapsed;
            this.speak.Visibility = Visibility.Visible;
            this.speak.SpeakAnimate.Begin();

            foreach (var clockVoice in GetClockVoices())
            {
                this.player.Source = new Uri(clockVoice, UriKind.Relative);
                await this.player.WaitForMediaEndedAsync();
            }
            this.PlayerEnded();
        }

        private void PlayerEnded()
        {
            this.blink.Visibility = Visibility.Visible;
            this.speak.Visibility = Visibility.Collapsed;
            this.speak.SpeakAnimate.Stop();
        }

        private IEnumerable<string> GetHelloVoices()
        {
            DateTime now = DateTime.Now;
            int hour = now.Hour;
            Random rand = new Random();
            if (hour >= 6 && hour <= 9)
            {
                yield return "Voices/Hello/moring" + (rand.Next(2) + 1).ToString("00") + ".asf";
            }
            else if (hour >= 12 && hour <= 14)
            {
                yield return "Voices/Hello/noon" + (rand.Next(2) + 1).ToString("00") + ".asf";
            }
            else if (hour >= 22 || hour <= 6)
            {
                yield return "Voices/Hello/night" + (rand.Next(2) + 1).ToString("00") + ".asf";
            }
            else
            {
                yield return "Voices/Hello/ap" + (rand.Next(20) + 1).ToString("00") + ".asf";
            }
        }

        private IEnumerable<string> GetClockVoices()
        {
            DateTime now = DateTime.Now;
            string hour = now.ToString("hh");

            yield return "Voices/Clock/now.asf";

            if (hour[0] != '0')
            {
                yield return "Voices/Clock/10.asf";
            }
            if (hour[1] > '0')
            {
                yield return string.Format("Voices/Clock/{0}{1}.asf", '0', hour[1]);
            }

            yield return "Voices/Clock/time.asf";

            int minute = now.Minute;
            if (minute >= 20)
            {
                yield return string.Format("Voices/Clock/{0}{1}.asf", '0', minute / 10);
            }

            if (minute >= 10)
            {
                yield return "Voices/Clock/10.asf";
            }

            if (minute % 10 >= 1)
            {
                yield return string.Format("Voices/Clock/{0}.asf", (minute % 10).ToString("00"));
            }

            if (minute > 0)
            {
                yield return "Voices/Clock/mins.asf";
            }
        }
    }
}