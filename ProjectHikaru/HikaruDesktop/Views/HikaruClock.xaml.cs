using HikaruDesktop.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;

namespace HikaruDesktop.Views
{
    /// <summary>
    /// HikaruClock.xaml 的交互逻辑
    /// </summary>
    public partial class HikaruClock : UserControl
    {
        private const string TEMP_ASX_FILE_PATH = @"Voices/temp.asx";

        public HikaruClock()
        {
            InitializeComponent();
        }

        private void Blink_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        }

        private async void Clock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CreateClockASXFile();

            this.speak.SpeakAnimate.Stop();

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

        private void CreateClockASXFile()
        {
            DateTime now = DateTime.Now;
            string hour = now.ToString("hh");

            XmlDocument asx = new XmlDocument();

            XmlElement asxRoot = asx.CreateElement("ASX");
            asxRoot.SetAttribute("Version", "3.0");
            asx.AppendChild(asxRoot);

            XmlElement nowEntry = asx.CreateElement("Entry");
            asxRoot.AppendChild(nowEntry);

            XmlElement nowRef = asx.CreateElement("Ref");
            nowRef.SetAttribute("href", "Clock/now.asf");
            nowEntry.AppendChild(nowRef);

            if (hour[0] != '0')
            {
                XmlElement tenHourEntry = asx.CreateElement("Entry");
                asxRoot.AppendChild(tenHourEntry);

                XmlElement tenHourRef = asx.CreateElement("Ref");
                tenHourRef.SetAttribute("href", "Clock/10.asf");
                tenHourEntry.AppendChild(tenHourRef);
            }
            if (hour[1] > '0')
            {
                XmlElement hourEntry = asx.CreateElement("Entry");
                asxRoot.AppendChild(hourEntry);

                XmlElement hourRef = asx.CreateElement("Ref");
                hourRef.SetAttribute("href", string.Format("Clock/{0}{1}.asf", '0', hour[1]));
                hourEntry.AppendChild(hourRef);
            }

            XmlElement timeEntry = asx.CreateElement("Entry");
            asxRoot.AppendChild(timeEntry);

            XmlElement timeRef = asx.CreateElement("Ref");
            timeRef.SetAttribute("href", "Clock/time.asf");
            timeEntry.AppendChild(timeRef);

            int minute = now.Minute;
            if (minute >= 20)
            {
                XmlElement tenMinuteEntry = asx.CreateElement("Entry");
                asxRoot.AppendChild(tenMinuteEntry);

                XmlElement tenMinuteRef = asx.CreateElement("Ref");
                tenMinuteRef.SetAttribute("href", string.Format("Clock/{0}{1}.asf", '0', minute / 10));
                tenMinuteEntry.AppendChild(tenMinuteRef);
            }

            if (minute >= 10)
            {
                XmlElement splitMinuteEntry = asx.CreateElement("Entry");
                asxRoot.AppendChild(splitMinuteEntry);

                XmlElement splitMinuteRef = asx.CreateElement("Ref");
                splitMinuteRef.SetAttribute("href", "Clock/10.asf");
                splitMinuteEntry.AppendChild(splitMinuteRef);
            }

            if (minute % 10 >= 1)
            {
                XmlElement minuteEntry = asx.CreateElement("Entry");
                asxRoot.AppendChild(minuteEntry);

                XmlElement minuteRef = asx.CreateElement("Ref");
                minuteRef.SetAttribute("href", string.Format("Clock/{0}.asf", (minute % 10).ToString("00")));
                minuteEntry.AppendChild(minuteRef);
            }

            if (minute > 0)
            {
                XmlElement minsEntry = asx.CreateElement("Entry");
                asxRoot.AppendChild(minsEntry);

                XmlElement minsRef = asx.CreateElement("Ref");
                minsRef.SetAttribute("href", "Clock/mins.asf");
                minsEntry.AppendChild(minsRef);
            }

            asx.Save(TEMP_ASX_FILE_PATH);
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

        private void CreateHelloASXFile()
        {
            DateTime now = DateTime.Now;
            int hour = now.Hour;
        }
    }
}