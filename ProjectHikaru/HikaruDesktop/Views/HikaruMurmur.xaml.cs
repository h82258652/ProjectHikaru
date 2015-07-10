using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace HikaruDesktop.Views
{
    /// <summary>
    /// HikaruMurmur.xaml 的交互逻辑
    /// </summary>
    public partial class HikaruMurmur : UserControl
    {
        private static string[] _hikaruMurmurs = new string[] {
            "小光是Silverlight的亲善大使喔！",
            "Visual Studio是蓝泽家左执事",
            "Expression Studio是蓝泽家右执事",
            "你有沒有去玩“两个人的圣诞夜”呢？",
            "新的一年小光请你多多指教啰！"
        };

        private static string[] _hikaruSays = new string[] {
            "小光最喜欢你了！你今天好吗？",
            "今天也要加油喔！！流汗过后才会有甜美的果实",
            "别忘了好好休息一下喔！今天辛苦你了！",
            "累的时候特别想吃甜食，你会不会呢？",
            "一切还顺利吗？喝个茶休息一下吧！",
            "听音乐可以纾解压力，你喜欢听什么音乐呢？",
            "你并不寂寞，小光都会在这里陪你呢！",
            "不可以偷懒唷！！快打起精神吧！",
            "听说吃纳豆对身体好，但是小光好怕纳豆的味道喔！",
            "注意身体不要感冒喔！现在感冒不容易好呢！"
        };

        private Storyboard _dialogAnimate;

        private DispatcherTimer _murmurTimer = new DispatcherTimer()
        {
            Interval = new TimeSpan(0, 0, 15)
        };

        public HikaruMurmur()
        {
            this.InitializeComponent();
            this.SetHikaruTalk(this.GetRandomSentence(0));
            this._murmurTimer.Tick += (sender, e) =>
            {
                this.SetHikaruTalk(this.GetRandomSentence(1));
            };
        }

        private Storyboard DialogAnimate
        {
            get
            {
                if (this._dialogAnimate == null)
                {
                    this._dialogAnimate = (Storyboard)this.dialog.FindResource("DialogAnimate");
                }
                return this._dialogAnimate;
            }
        }

        private string GetRandomSentence(int mode)
        {
            Random rand = new Random();
            if (mode == 0)
            {
                return _hikaruSays[rand.Next(_hikaruSays.Length)];
            }
            if (mode == 1)
            {
                return _hikaruMurmurs[rand.Next(_hikaruMurmurs.Length)];
            }
            return string.Empty;
        }

        private void SetHikaruTalk(string s)
        {
            this.dialog.Visibility = Visibility.Collapsed;
            this._murmurTimer.Stop();
            this.blink.Visibility = Visibility.Collapsed;
            this.speak.Visibility = Visibility.Visible;

            this.speak.SpeakAnimate.Stop();

            this.DialogAnimate.Stop();

            this.dialog.Visibility = Visibility.Visible;
            this.dialog.SetHikaruSays(s);

            this.DialogAnimate.Begin();

            this.speak.SpeakAnimate.Begin();

            this.speak.SpeakAnimate.Completed += (sender, e) =>
            {
                this.speak.SpeakAnimate.Stop();
                this._murmurTimer.Start();
                this.blink.Visibility = Visibility.Visible;
                this.speak.Visibility = Visibility.Collapsed;
            };
        }

        private void Speak_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.SetHikaruTalk(this.GetRandomSentence(0));
        }
    }
}