using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Blog_Stick
{
    public partial class HikkiMurmur : UserControl
    {
        private static string[] HikkiMurmurs = new string[] { "小光是Silverlight4的親善大使喔!", "Visual Studio是藍澤家左執事", "Expression Studio是藍澤家右執事", "你有沒有去玩”兩個人的聖誕夜”呢?", "新的一年小光請你多多指教囉!" };

        private static string[] HikkiSays = new string[] { "小光最喜歡你了!你今天好嗎?", "今天也要加油喔!!流汗過後才會有甜美的果實", "別忘了好好休息一下喔!今天辛苦你了!", "累的時候特別想吃甜食, 你會不會呢?", "一切還順利嗎?喝個茶休息一下吧!", "聽音樂可以紓解壓力,你喜歡聽什麼音樂呢?", "你並不寂寞,小光都會在這裡陪你呢!", "不可以偷懶唷!!快打起精神吧! ", "聽說吃納豆對身體好,但是小光好怕納豆的味道喔!", "注意身體不要感冒喔!現在感冒不容易好呢!" };

        private DispatcherTimer Murmur = new DispatcherTimer();

        public HikkiMurmur()
        {
            this.InitializeComponent();
            this.Speak_MouseLeftButtonDown(this, null);
            this.Murmur.Interval = new TimeSpan(0, 0, 15);
            this.Murmur.Tick += this.SetHikkiMurmur;
        }

        private void _animate_Completed(object sender, EventArgs e)
        {
            ((Storyboard)sender).Stop();
            this.Murmur.Start();
            this.blink.Visibility = Visibility.Visible;
            this.speak.Visibility = Visibility.Collapsed;
        }

        private string GetRandomSentence(int i)
        {
            Random random = new Random(DateTime.Now.Millisecond);
            if (i == 0)
            {
                return HikkiSays[(int)Math.Floor((double)random.Next(HikkiSays.Length))];
            }
            if (i == 1)
            {
                return HikkiMurmurs[(int)Math.Floor((double)random.Next(HikkiMurmurs.Length))];
            }
            return "";
        }

        private void SetHikkiMurmur(object sender, EventArgs e)
        {
            this.SetHikkiTalk(this.GetRandomSentence(1));
        }

        private void SetHikkiTalk(string s)
        {
            this.dialog.Visibility = Visibility.Collapsed;
            this.Murmur.Stop();
            this.blink.Visibility = Visibility.Collapsed;
            this.speak.Visibility = Visibility.Visible;
            FrameworkElement element = (FrameworkElement)((FrameworkElement)this.LayoutRoot.FindName("dialog")).FindName("HikkiSays");
            Storyboard storyboard = (Storyboard)((FrameworkElement)this.LayoutRoot.FindName("speak")).FindName("SpeakAnimate");
            storyboard.Stop();
            this.DialogAnimate.Stop();
            this.dialog.Visibility = Visibility.Visible;
            ((TextBlock)element).Text = s;
            this.DialogAnimate.Begin();
            storyboard.Begin();
            storyboard.Completed += _animate_Completed;
        }

        private void Speak_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.SetHikkiTalk(this.GetRandomSentence(0));
        }
    }
}