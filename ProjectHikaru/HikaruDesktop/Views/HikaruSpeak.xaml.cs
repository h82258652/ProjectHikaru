using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace HikaruDesktop.Views
{
    /// <summary>
    /// HikaruSpeak.xaml 的交互逻辑
    /// </summary>
    public partial class HikaruSpeak : UserControl
    {
        private Storyboard _speakAnimate;

        internal Storyboard SpeakAnimate
        {
            get
            {
                if (this._speakAnimate == null)
                {
                    this._speakAnimate = (Storyboard)this.FindResource("SpeakAnimate");
                }
                return _speakAnimate;
            }
        }

        public HikaruSpeak()
        {
            this.InitializeComponent();
        }
    }
}