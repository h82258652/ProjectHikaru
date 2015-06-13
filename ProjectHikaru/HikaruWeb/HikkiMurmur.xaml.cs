using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace HikaruWeb
{
    public sealed partial class HikkiMurmur : UserControl
    {
        private static string[] HikkiMurmurs = new string[] {
            "小光是Silverlight的亲善大使喔！",
            "Visual Studio是蓝泽家左执事",
            "Expression Studio是蓝泽家右执事",
            "你有沒有去玩“两个人的圣诞夜”呢？",
            "新的一年小光请你多多指教啰！"
        };

        private static string[] HikkiSays = new string[] {
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

        private DispatcherTimer Murmur = new DispatcherTimer();

        public HikkiMurmur()
        {
            this.InitializeComponent();
            this.SetHikkiTalk(this.GetRandomSentence(0));
            this.Murmur.Interval = new TimeSpan(0, 0, 15);
            this.Murmur.Tick += this.SetHikkiMurmur;
#warning try fix in next CSHtml5 version
            // hack HyperLinkButton Cursor
            this.hyperLinkButtonZone.Loaded += delegate
            {
                if (CSharpXamlForHtml5.DomManagement.IsControlInVisualTree(this.hyperLinkButtonZone))
                {
                    var domHyperLinkButtonZone = CSharpXamlForHtml5.DomManagement.GetDomElementFromControl(this.hyperLinkButtonZone);
                    domHyperLinkButtonZone.style.cursor = "pointer";
                }
            };
#warning try fix in next CSHtml5 version
            // hack Speak Cursor
            this.speakZone.Loaded += delegate
            {
                if (CSharpXamlForHtml5.DomManagement.IsControlInVisualTree(this.speakZone))
                {
                    var domSpeakZone = CSharpXamlForHtml5.DomManagement.GetDomElementFromControl(this.speakZone);
                    domSpeakZone.style.cursor = "pointer";
                }
            };
        }

        private string GetRandomSentence(int mode)
        {
            Random rand = new Random();
            if (mode == 0)
            {
                return HikkiSays[rand.Next(HikkiSays.Length)];
            }
            else if (mode == 1)
            {
                return HikkiMurmurs[rand.Next(HikkiMurmurs.Length)];
            }
            else
            {
                return string.Empty;
            }
        }

        private void HyperLinkButtonZone_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
#warning try fix in next CSHtml5 version
            // hack zone for HyperLinkButton
            JSIL.Verbatim.Expression("window.open(\"http://www.microsoft.com/taiwan/silverlight\");");
        }

        private void SetHikkiMurmur(object sender, object e)
        {
            this.SetHikkiTalk(this.GetRandomSentence(1));
        }

        private void SetHikkiTalk(string s)
        {
            this.dialog.Visibility = Visibility.Collapsed;
            this.Murmur.Stop();
            this.blink.Visibility = Visibility.Collapsed;
            this.speak.SetVisibility(Visibility.Visible);

            this.speak.SpeakAnimateStop();

#warning try fix in next CSHtml5 version
            // No scaleTransform now
            // this.DialogAnimate.Stop();

            this.dialog.Visibility = Visibility.Visible;
            this.dialog.SetHikkiSaysText(s);

#warning try fix in next CSHtml5 version
            // this.DialogAnimate.Begin();

            speak.SpeakAnimateBegin();

            DispatcherTimer delayTimer = new DispatcherTimer();// one shot timer for delay.
            delayTimer.Interval = TimeSpan.FromSeconds(4.0d);
            delayTimer.Tick += delegate
            {
                delayTimer.Stop();

                speak.SpeakAnimateStop();
                this.Murmur.Start();
                this.blink.Visibility = Visibility.Visible;
                this.speak.SetVisibility(Visibility.Collapsed);
            };
            delayTimer.Start();
        }

        private void Speak_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
#warning try fix in next CSHtml5 version
            // use hack zone do this now.
            // see SpeakZone_PointerPressed
        }

        private void SpeakZone_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            this.SetHikkiTalk(this.GetRandomSentence(0));
        }
    }
}