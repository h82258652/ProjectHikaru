using Windows.UI.Xaml.Controls;

namespace HikaruWeb
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            // Enter construction logic here...
#warning try fix in next CSHtml5 version
            // get querystring and set mode


        }

        public string GetQueryString()
        {
            if (CSharpXamlForHtml5.Environment.IsRunningInJavaScript)
            {
                string url = JSIL.Verbatim.Expression("window.location.toString()");
                string queryString = (url.IndexOf('?') >= 0 ? url.Substring(url.IndexOf('?') + 1) : "");
                return queryString;
            }
            else
            {
                // The query string cannot be obtained when running inside the Simulator.
                return "";
            }
        }
    }
}