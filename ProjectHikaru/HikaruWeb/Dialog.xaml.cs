using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HikaruWeb
{
    public sealed partial class Dialog : UserControl
    {
        public Dialog()
        {
            this.InitializeComponent();
        }

        internal void SetHikkiSaysText(string s)
        {
            this.HikkiSays.Text = s;
        }
    }
}