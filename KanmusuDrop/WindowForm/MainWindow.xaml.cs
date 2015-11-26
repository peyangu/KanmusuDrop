using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TweetSharp;

namespace KanmusuDrop
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        // TODO: Twitterの認証情報の保持の仕方(毎回認証？)
        
        /// <summary>
        /// 艦娘リスト
        /// </summary>
        List<DatKanmusuData> kanmusuList = new List<DatKanmusuData>();

        /// <summary>
        /// 艦娘ドロップリスト
        /// </summary>
        readonly List<DatDropData> dropKanmusuList = new List<DatDropData>();

        /// <summary>
        /// Twitter情報のプロパティ
        /// </summary>
        static TwitterService MainService { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            // 艦娘データ,ドロップデータを読み込む。
            kanmusuList = XMLWork.KanmusuDataLoad();
            dropKanmusuList = XMLWork.DropDataLoad();
            UpdateDispList();
        }

        /// <summary>
        /// 表示用リストを再設定
        /// </summary>
        private void UpdateDispList()
        {
            try
            {
                List<DatDropData> bindList = new List<DatDropData>(dropKanmusuList);
                // emptyデータ削除
                bindList.RemoveAt(0);
                this.DropGrid.ItemsSource = new ReadOnlyCollection<DatDropData>(bindList);
            }
            catch (Exception ex)
            {
                ClsLogWrite.LogWrite(this.Name + "でエラーが起きました", ex);
            }
        }

        /// <summary>
        /// 艦娘名テキストボックスKeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KanmusuNameText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    // 勝利判定の値取得
                    string win = WinDecisionCommbo.SelectionBoxItem.ToString();

                    // 艦娘リストに登録されている場合、その情報を取得する
                    // *間違って入力した場合(木曽：木曾みたいな)に情報が取ってこれない。
                    // todo:パケットキャプチャでドロップ艦の情報取得
                    DatKanmusuData kanmusu = kanmusuList.Find(kan => kan.KanmusuName == KanmusuNameText.Text);
                    // ドロップ回数取得
                    int dropCount = dropKanmusuList.FindAll(kan => kan.DropKanmusuName == KanmusuNameText.Text).Count + 1;
                    // 艦種とレアリティの設定(kanmusuがnullの場合は、空白と0をdropKanmusuListに入れる)
                    string shipCls = "";
                    short rare = 0;
                    if (kanmusu != null)
                    {
                        shipCls = ClsDataCreate.GetShipClass(kanmusu.ShipClass);
                        rare = kanmusu.Rarity;
                    }

                    dropKanmusuList.Add(new DatDropData(win, shipCls, KanmusuNameText.Text, rare, dropCount));
                    UpdateDispList();
                    // ツイート投稿
                    if(MainService != null)
                        MainService.SendTweet(new SendTweetOptions { Status = KanmusuNameText.Text + "を入手しました。 #かんろく #かんろくテスト" });

                    KanmusuNameText.Text = String.Empty;
                }
                catch (Exception ex)
                {
                    ClsLogWrite.LogWrite(this.Name + "でエラーが起きました", ex);
                }
            }
        }

        /// <summary>
        /// ドロップ情報削除ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DropDelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DropGrid.SelectedIndex == -1) 
                {
                    MessageBox.Show("一覧で選択してからボタンを押してください。");
                    return;
                }

                // SelectedIndex == dropKanmusuList.Countだとエラーが出る
                if (DropGrid.SelectedIndex < dropKanmusuList.Count)
                {
                    dropKanmusuList.RemoveAt(DropGrid.SelectedIndex + 1);
                    UpdateDispList();
                }
            }
            catch (Exception ex)
            {
                ClsLogWrite.LogWrite(this.Name + "でエラーが起きました", ex);
            }
        }

        /// <summary>
        /// 出力ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExcelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClsOutputCSV.OutputCsv(dropKanmusuList);
            }
            catch (Exception ex)
            {
                ClsLogWrite.LogWrite(this.Name + "でエラーが起きました", ex);
            }
        }

        /// <summary>
        /// リストリセットボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DropReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 全件削除
                dropKanmusuList.RemoveAll(drop => drop.DropKanmusuName == drop.DropKanmusuName);

                // 0件のまま保存すると、読み込み時にエラーが出るので空データを入れておく。
                dropKanmusuList.Add(new DatDropData("empty", "empty", "empty", 0, 0));
                UpdateDispList();
            }
            catch (Exception ex)
            {
                ClsLogWrite.LogWrite(this.Name + "でエラーが起きました", ex);
            }
        }

        #region キャプションバーボタンイベント
        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        /// <summary>
        /// 最大化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        /// <summary>
        /// 復元
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RestorationButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }
        #endregion

        #region ボタンカラー変更メソッド
        private void DropDelButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Border b = (Border)DropDelButton.Template.FindName("ButtonBorder", DropDelButton);
            b.Background = Brushes.Gray;
        }

        private void DropDelButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Border b = (Border)DropDelButton.Template.FindName("ButtonBorder", DropDelButton);
            b.Background = Brushes.Black;
        }

        private void ExcelButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Border b = (Border)ExcelButton.Template.FindName("ButtonBorder", ExcelButton);
            b.Background = Brushes.Gray;
        }

        private void ExcelButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Border b = (Border)ExcelButton.Template.FindName("ButtonBorder", ExcelButton);
            b.Background = Brushes.Black;
        }

        private void DropReset_MouseEnter(object sender, MouseEventArgs e)
        {
            Border b = (Border)DropReset.Template.FindName("ButtonBorder", DropReset);
            b.Background = Brushes.Gray;
        }

        private void DropReset_MouseLeave(object sender, MouseEventArgs e)
        {
            Border b = (Border)DropReset.Template.FindName("ButtonBorder", DropReset);
            b.Background = Brushes.Black;
        }
        #endregion

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            // 閉じる時に確実に起こるように。
            XMLWork.DropDataSave(dropKanmusuList);
        }

        /// <summary>
        /// Twitter情報取得
        /// </summary>
        /// <param name="service"></param>
        public static void ServicePropSet(TwitterService service)
        {
            MainService = service;
        }

        /// <summary>
        /// twitterアイコンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            TwitterWindow win = new TwitterWindow();
            win.Show();
        }
    }
}
