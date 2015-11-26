using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using TweetSharp;

namespace KanmusuDrop
{
    /// <summary>
    /// TwitterWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class TwitterWindow : Window
    {
        TwitterService service = new TwitterService("JbRIp6qEScnk8CFl4ji4l324v", "BsXwcrPfEBnO7y62DmoS6LiyoE8RepCTZKsSi3rZ576votE8Xq");

        OAuthRequestToken requestToken;

        public TwitterWindow()
        {
            InitializeComponent();
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            requestToken = service.GetRequestToken();

            Uri uri = service.GetAuthorizationUri(requestToken);
            Process.Start(uri.ToString());
        }

        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            string code = PinCodeText.Text;

            OAuthAccessToken access = service.GetAccessToken(requestToken, code);
            service.AuthenticateWith(access.Token, access.TokenSecret);
            MessageBox.Show("認証が完了しました。");
            MainWindow.ServicePropSet(service);
            this.Close();
        }
    }
}
