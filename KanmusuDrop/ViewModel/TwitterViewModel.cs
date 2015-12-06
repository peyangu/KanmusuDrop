using KanmusuDrop.Common;
using KanmusuDrop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TweetSharp;

namespace KanmusuDrop.ViewModel
{
    /// <summary>
    /// TwitterWindowのViewModelクラス
    /// </summary>
    public class TwitterViewModel : ViewModelBase
    {
        TwitterModel _Model;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TwitterViewModel()
        {
            _Model = new TwitterModel();

            _Model.Service = new TwitterService(ClsConst.ConsumerKey, ClsConst.ConsumerSecret);
            _Model.ButtonEnabled = false;

            StartAuthCom = new RelayCommand(_Model.StartAuth);
            CompletionAuthCom = new RelayCommand(_Model.CompletionAuth);
            ReleaseAuthCom = new RelayCommand(_Model.ReleaseAuth);
        }

        #region コマンド
        /// <summary>
        /// 認証開始
        /// </summary>
        public ICommand StartAuthCom { get; private set; }
        /// <summary>
        /// 認証完了
        /// </summary>
        public ICommand CompletionAuthCom { get; private set; }
        /// <summary>
        /// 認証解除
        /// </summary>
        public ICommand ReleaseAuthCom { get; private set; }
        #endregion

        #region プロパティ
        /// <summary>
        /// Modelプロパティ
        /// </summary>
        public TwitterModel Model { get { return _Model; } }
        #endregion
    }
}
