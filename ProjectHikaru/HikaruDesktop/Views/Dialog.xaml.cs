using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace HikaruDesktop.Views
{
    /// <summary>
    /// Dialog.xaml 的交互逻辑
    /// </summary>
    public partial class Dialog : UserControl
    {
        public Dialog()
        {
            InitializeComponent();
        }

        internal void SetHikaruSays(string s)
        {
            txtHikaruSays.Text = s;
        }

        private Storyboard _dialogAnimate;

        internal Storyboard DialogAnimate
        {
            get
            {
                if (this._dialogAnimate==null)
                {
                    this._dialogAnimate = (Storyboard)this.FindResource("DialogAnimate");
                }
                return this._dialogAnimate;
            }
        }
    }
}