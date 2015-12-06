using KanmusuDrop.Common;
using KanmusuDrop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace KanmusuDrop.ViewModel
{
    /// <summary>
    /// MainWindowのViewModelクラス
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        MainModel _Model;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainViewModel() 
        {
            _Model = new MainModel(this);
            _Model.ConfirmationTwitterAuth();
            EnterKanmusuCom = new RelayCommand(_Model.KeyDownEnter);
            DeleteKanmusuCom = new RelayCommand(_Model.DeleteDrop);
            OutputCsvCom = new RelayCommand(_Model.OutputCsv);
            ListResetCom = new RelayCommand(_Model.ResetDrop);
            OpenTwitterAuthCom = new RelayCommand(_Model.OpenTwitterWindow);
            Close = new RelayCommand(_Model.OnClosing);
        }

        #region コマンド
        /// <summary>
        /// リストから艦娘を削除するコマンド
        /// </summary>
        public ICommand DeleteKanmusuCom { get; private set; }
        /// <summary>
        /// リストの艦娘をCSVに出力するコマンド
        /// </summary>
        public ICommand OutputCsvCom { get; private set; }
        /// <summary>
        /// リストの艦娘をリセットするコマンド
        /// </summary>
        public ICommand ListResetCom { get; private set; }
        /// <summary>
        /// ツイッター認証画面を開くコマンド
        /// </summary>
        public ICommand OpenTwitterAuthCom { get; private set; }
        /// <summary>
        /// Enter押下で入力した艦娘をリストに登録するコマンド
        /// </summary>
        public ICommand EnterKanmusuCom { get; private set; }
        /// <summary>
        /// OnClose
        /// </summary>
        public ICommand Close { get; private set; }
        #endregion

        #region プロパティ
        /// <summary>
        /// MainModel
        /// </summary>
        public MainModel Model { get { return _Model; } }
        #endregion
    }
}
