using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanmusuDrop.ViewModel
{
    /// <summary>
    /// INotifyPropertyChangedを継承したViewModelのbaseクラス
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
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
