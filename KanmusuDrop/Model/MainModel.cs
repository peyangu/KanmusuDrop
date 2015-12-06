using KanmusuDrop.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interactivity;
using TweetSharp;

namespace KanmusuDrop.Model
{
    /// <summary>
    /// MainWindowのModelクラス
    /// </summary>
    public class MainModel :INotifyPropertyChanged
    {
        public MainModel(MainViewModel viewModel) 
        {
            // 艦娘データ,ドロップデータを読み込む。
            KanmusuList = ClsXMLWork.KanmusuDataLoad();
            DropKanmusuList = ClsXMLWork.DropDataLoad();
            UpdateDispList();
        }

        #region プロパティ
        private string _KanmusuName;
        /// <summary>
        /// 艦娘名
        /// </summary>
        public string KanmusuName
        {
            get { return _KanmusuName; }
            set 
            {
                if (_KanmusuName != value) 
                {
                    _KanmusuName = value;
                    NotifyPropertyChanged("KanmusuName");
                }
            }
        }

        private List<DatKanmusuData> _KanmusuList = new List<DatKanmusuData>();
        /// <summary>
        /// 艦娘リスト
        /// </summary>
        public List<DatKanmusuData> KanmusuList
        {
            get { return _KanmusuList; }
            set 
            {
                if (_KanmusuList != value) 
                {
                    _KanmusuList = value;
                    NotifyPropertyChanged("KanmusuList");
                }
            }
        }

        private List<DatDropData> _DropKanmusuList = new List<DatDropData>();
        /// <summary>
        /// ドロップリスト
        /// </summary>
        public List<DatDropData> DropKanmusuList 
        {
            get { return _DropKanmusuList; }
            set 
            {
                if (_DropKanmusuList != value) 
                {
                    _DropKanmusuList = value;
                }
            }
        }

        private List<DatDropData> _BindList = new List<DatDropData>();
        /// <summary>
        /// 表示用ドロップリスト
        /// </summary>
        public List<DatDropData> BindList
        {
            get { return _BindList; }
            set
            {
                if (_BindList != value)
                {
                    _BindList = value;
                    NotifyPropertyChanged("BindList");
                }
            }
        }

        private int _WinDecision = 0;
        /// <summary>
        /// 勝利判定
        /// </summary>
        public int WinDecision 
        {
            get { return _WinDecision; }
            set
            {
                if (_WinDecision != value)
                {
                    _WinDecision = value;
                    NotifyPropertyChanged("WinDecision");
                }
            }
        }

        /// <summary>
        /// Twitter情報のプロパティ
        /// </summary>
        static TwitterService MainService { get; set; }

        private Visibility _TwitterAuth = Visibility.Hidden;
        /// <summary>
        /// Twitter連携中の文字列のVisibility設定
        /// </summary>
        public Visibility TwitterAuth 
        {
            get { return _TwitterAuth; }
            set
            {
                if (_TwitterAuth != value)
                {
                    _TwitterAuth = value;
                    NotifyPropertyChanged("TwitterAuth");
                }
            }
        }

        private int _GridIndex = -1;
        /// <summary>
        /// DropGridのSelectedIndex
        /// </summary>
        public int GridIndex 
        {
            get { return _GridIndex; }
            set
            {
                if (_GridIndex != value)
                {
                    _GridIndex = value;
                    NotifyPropertyChanged("GridIndex");
                }
            }
        }

        /// <summary>
        /// 閉じる処理
        /// </summary>
        public Action CloseAction { get; set; }

        /// <summary>
        /// テキストボックスEnter処理
        /// 名前の取得用
        /// </summary>
        public Action TextBoxEnterAction { get; set; }
        #endregion

        #region メソッド
        /// <summary>
        /// Twitterの認証情報確認
        /// </summary>
        public void ConfirmationTwitterAuth() 
        {
            List<string> auth = ClsAuth.AuthInfoGet();

            if (auth.Count == 2)
            {
                MainService = new TwitterService(ClsConst.ConsumerKey, ClsConst.ConsumerSecret);
                MainService.AuthenticateWith(auth[0], auth[1]);
                TwitterAuth = Visibility.Visible;
            }
            else
                TwitterAuth = Visibility.Hidden;
        }

        /// <summary>
        /// テキストボックスエンター押下処理
        /// </summary>
        public void KeyDownEnter() 
        {
            try
            {
                // 勝利判定の値取得
                string win = String.Empty;
                switch (WinDecision) 
                {
                    case 0:
                        win = "S";
                        break;
                    case 1:
                        win = "A";
                        break;
                    case 2:
                        win = "B";
                        break;
                    case 3:
                        win = "C";
                        break;
                    case 4:
                        win = "D";
                        break;
                }

                // 艦娘リストに登録されている場合、その情報を取得する
                // todo:パケットキャプチャでドロップ艦の情報取得
                DatKanmusuData kanmusu = KanmusuList.Find(kan => kan.KanmusuName == KanmusuName);
                // ドロップ回数取得
                int dropCount = DropKanmusuList.FindAll(kan => kan.DropKanmusuName == KanmusuName).Count + 1;
                // レアリティの設定(kanmusuがnullの場合は、0をdropKanmusuListに入れる)
                short rare = 0;
                if (kanmusu != null)
                {
                    rare = kanmusu.Rarity;
                }
                DropKanmusuList.Add(new DatDropData(win, KanmusuName, rare, dropCount));
                KanmusuName = String.Empty;
                UpdateDispList();

                // ツイート
                if (MainService != null)
                    MainService.SendTweet(new SendTweetOptions() { Status = KanmusuName + "を入手しました。 #かんろく" });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ClsConst.ErrorMessage);
                ClsLogWrite.LogWrite(ex.Message, ex);
            }
        }

        /// <summary>
        /// ドロップ削除
        /// </summary>
        public void DeleteDrop() 
        {
            try
            {
                if (GridIndex == -1)
                {
                    MessageBox.Show("一覧で選択してからボタンを押してください。");
                    return;
                }

                // SelectedIndex == dropKanmusuList.Countだとエラーが出る
                if (GridIndex < DropKanmusuList.Count)
                {
                    DropKanmusuList.RemoveAt(GridIndex + 1);
                    UpdateDispList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ClsConst.ErrorMessage);
                ClsLogWrite.LogWrite(ex.Message, ex);
            }
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        public void OutputCsv() 
        {
            try
            {
                ClsOutputCSV.OutputCsv(DropKanmusuList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ClsConst.ErrorMessage);
                ClsLogWrite.LogWrite(ex.Message, ex);
            }
        }

        /// <summary>
        /// ドロップ情報リセット
        /// </summary>
        public void ResetDrop() 
        {
            try
            {
                // 全件削除
                DropKanmusuList.RemoveAll(drop => drop.DropKanmusuName == drop.DropKanmusuName);

                // 0件のまま保存すると、読み込み時にエラーが出るので空データを入れておく。
                DropKanmusuList.Add(new DatDropData("empty", "empty", 0, 0));
                UpdateDispList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ClsConst.ErrorMessage);
                ClsLogWrite.LogWrite(ex.Message, ex);
            }
        }

        /// <summary>
        /// Twitter連携画面オープン
        /// </summary>
        public void OpenTwitterWindow() 
        {
            try
            {
                TwitterWindow win = new TwitterWindow(MainService == null ? false : true);
                win.ShowDialog();
                if (MainService != null)
                    TwitterAuth = Visibility.Visible;
                else
                    TwitterAuth = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ClsConst.ErrorMessage);
                ClsLogWrite.LogWrite(ex.Message, ex);
            }
        }

        /// <summary>
        /// 表示用リストを再設定
        /// </summary>
        private void UpdateDispList()
        {
            try
            {
                List<DatDropData> bindList = new List<DatDropData>(DropKanmusuList);
                // emptyデータ削除(余分に表示されるのを防ぐ)
                bindList.RemoveAt(0);
                BindList = bindList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ClsConst.ErrorMessage);
                ClsLogWrite.LogWrite(ex.Message, ex);
            }
        }

        /// <summary>
        /// Twitter情報取得
        /// </summary>
        /// <param name="service"></param>
        public static void SetServiceProp(TwitterService service)
        {
            MainService = service;
        }

        /// <summary>
        /// OnClose時
        /// </summary>
        public void OnClosing()
        {
            ClsXMLWork.DropDataSave(DropKanmusuList);
            CloseAction();
        }
        #endregion

        /// <summary>
        /// PropertyChangedの実装
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }
    }
}
