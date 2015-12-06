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
        public MainWindow()
        {
            InitializeComponent();

            var mView = new ViewModel.MainViewModel();
            this.All.DataContext = mView;
            // OnClose時に処理を行いたいので、Model側で閉じられるようにthis.CloseをVMに渡す。
            if(mView.Model.CloseAction == null)
                mView.Model.CloseAction = new Action(() => this.Close());
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
    }
}
