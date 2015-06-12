using System.Windows;
using System.Windows.Controls;

namespace Blog_Stick
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            this.InitializeComponent();
            string blogType = Application.Current.Resources["blogType"].ToString();
            if (string.IsNullOrEmpty(blogType) == false)
            {
                if (int.Parse(blogType) == 0)
                {
                    this.hikkiMurmur.Visibility = Visibility.Visible;
                }
                else
                {
                    this.hikkiClock.Visibility = Visibility.Visible;
                }
            }
        }
    }
}