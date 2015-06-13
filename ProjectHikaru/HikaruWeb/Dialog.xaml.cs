using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HikaruWeb
{
    public sealed partial class Dialog : UserControl
    {
        private DispatcherTimer _hookTextBlockTextWrappingTimer = new DispatcherTimer();

        public Dialog()
        {
            this.InitializeComponent();
#warning try fix in next CSHtml5 version
            // hack TextBlock TextWrapping
            this.HikkiSays.Loaded += delegate
            {
                if (CSharpXamlForHtml5.DomManagement.IsControlInVisualTree(this.HikkiSays))
                {
                    var domHikkiSays = CSharpXamlForHtml5.DomManagement.GetDomElementFromControl(this.HikkiSays);
                    domHikkiSays.style.wordBreak = "break-all";
                    domHikkiSays.style.whiteSpace = "";
                }
            };
        }

        internal void SetHikkiSaysText(string s)
        {
            this.HikkiSays.Text = s;
#warning try fix in next CSHtml5 version
            // hack TextBlock TextWrapping
            if (CSharpXamlForHtml5.DomManagement.IsControlInVisualTree(this.HikkiSays))
            {
                var domHikkiSays = CSharpXamlForHtml5.DomManagement.GetDomElementFromControl(this.HikkiSays);
                domHikkiSays.style.wordBreak = "break-all";
                domHikkiSays.style.whiteSpace = "";
            }
        }
    }
}