using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Web;
using Microsoft.Web.WebView2.Core;

namespace LAB5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            webView.EnsureCoreWebView2Async(null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string appID = "51808792";
            var uriStr = @"https://oauth.vk.com/authorize?client_id=" + appID + 
                @"&scope=offline&redirect_uri=https://oauth.vk.com/blank.html&display=page&v=5.6&response_type=token";

            webView.CoreWebView2.Navigate(uriStr);
        }

        private void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            var webView = sender as Microsoft.Web.WebView2.Wpf.WebView2;
            var uri = webView.Source;

            if (uri.AbsoluteUri.Contains(@"oauth.vk.com/blank.html"))
            {
                string fragment = uri.Fragment;
                fragment = fragment.Trim('#');
                var queryStr = HttpUtility.ParseQueryString(fragment);
                string access_token = queryStr.Get("access_token");
                string user_id = queryStr.Get("user_id");

                InfoWindow infoWindow = new InfoWindow(access_token, user_id);
                if (access_token != null)
                    infoWindow.Show();
            }
        }
    }
}
