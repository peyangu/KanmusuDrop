﻿using KanmusuDrop.Model;
using KanmusuDrop.ViewModel;
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
        public TwitterWindow(bool auth)
        {
            InitializeComponent();
            AuthKaijoButton.IsEnabled = auth;

            TwitterViewModel vm = new TwitterViewModel();
            this.DataContext = vm;
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
        private void AuthButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Border b = (Border)AuthButton.Template.FindName("ButtonBorder", AuthButton);
            b.Background = Brushes.Gray;
        }

        private void AuthButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Border b = (Border)AuthButton.Template.FindName("ButtonBorder", AuthButton);
            b.Background = Brushes.Black;
        }

        private void EndButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Border b = (Border)EndButton.Template.FindName("ButtonBorder", EndButton);
            b.Background = Brushes.Gray;
        }

        private void EndButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Border b = (Border)EndButton.Template.FindName("ButtonBorder", EndButton);
            b.Background = Brushes.Black;
        }

        private void AuthKaijoButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Border b = (Border)AuthKaijoButton.Template.FindName("ButtonBorder", AuthKaijoButton);
            b.Background = Brushes.Gray;
        }

        private void AuthKaijoButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Border b = (Border)AuthKaijoButton.Template.FindName("ButtonBorder", AuthKaijoButton);
            b.Background = Brushes.Black;
        }
        #endregion
    }
}
