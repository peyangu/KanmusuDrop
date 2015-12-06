using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TweetSharp;

namespace KanmusuDrop.Model
{
    /// <summary>
    /// TwitterWindowのModelクラス
    /// </summary>
    public class TwitterModel : INotifyPropertyChanged
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TwitterModel() 
        {

        }

        #region プロパティ
        private TwitterService _Service;
        /// <summary>
        /// TwitterService
        /// </summary>
        public TwitterService Service
        {
            get { return _Service; }
            set
            {
                if (_Service != value)
                {
                    _Service = value;
                    NotifyPropertyChanged("Service");
                }
            }
        }

        private OAuthRequestToken _RequestToken;
        /// <summary>
        /// OAuthRequestToken
        /// </summary>
        public OAuthRequestToken RequestToken
        {
            get { return _RequestToken; }
            set
            {
                if (_RequestToken != value) 
                {
                    _RequestToken = value;
                    NotifyPropertyChanged("RequestToken");
                }
            }
        }

        private bool _ButtonEnabled = false;
        /// <summary>
        /// 認証解除ボタンのEnabled
        /// </summary>
        public bool ButtonEnabled
        {
            get { return _ButtonEnabled; }
            set
            {
                if (_ButtonEnabled != value) 
                {
                    _ButtonEnabled = value;
                    NotifyPropertyChanged("ButtonEnabled");
                }
            }
        }

        private string _PinCode;
        /// <summary>
        /// PINコード
        /// </summary>
        public string PinCode
        {
            get { return _PinCode; }
            set
            {
                if (_PinCode != value) 
                {
                    _PinCode = value;
                    NotifyPropertyChanged("PinCode");
                }
            }
        }
        #endregion

        #region メソッド
        /// <summary>
        /// 認証開始
        /// </summary>
        public void StartAuth() 
        {
            try
            {
                RequestToken = Service.GetRequestToken();

                Uri uri = Service.GetAuthorizationUri(RequestToken);
                Process.Start(uri.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ClsConst.ErrorMessage);
                ClsLogWrite.LogWrite(ClsConst.ErrorMessageEx, ex);
            }
        }

        /// <summary>
        /// 認証完了
        /// </summary>
        public void CompletionAuth() 
        {
            try
            {
                OAuthAccessToken access = Service.GetAccessToken(RequestToken, PinCode);
                Service.AuthenticateWith(access.Token, access.TokenSecret);
                // トークン情報保存(次回起動時に認証済みのままにするため)
                ClsAuth.AuthInfoSet(access);
                MessageBox.Show("認証が完了しました。");
                // 認証情報をメインウインドウで使うため。
                MainModel.SetServiceProp(Service);
                ButtonEnabled = true;
                // 完了したら自動で画面閉じる？
                //this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ClsConst.ErrorMessage);
                ClsLogWrite.LogWrite(ex.Message, ex);
            }
        }

        /// <summary>
        /// 認証解除
        /// </summary>
        public void ReleaseAuth() 
        {
            try
            {
                // 起動時に認証済みのままにしない時
                ClsAuth.AuthInfoDelete();
                Service = null;
                MainModel.SetServiceProp(Service);
                ButtonEnabled = false;
                MessageBox.Show("認証解除をしました。");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ClsConst.ErrorMessage);
                ClsLogWrite.LogWrite(ex.Message, ex);
            }
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
