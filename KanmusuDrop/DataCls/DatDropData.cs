using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanmusuDrop
{
    /// <summary>
    /// ドロップ情報表示用データクラス
    /// </summary>
    public class DatDropData
    {
        /// <summary>
        /// 勝利判定
        /// </summary>
        public string DropWinDecision { get; set; }

        /// <summary>
        /// 艦娘名(ドロップ)
        /// </summary>
        public string DropKanmusuName { get; set; }

        /// <summary>
        /// レアリティ(ドロップ)
        /// </summary>
        public short DropRarity { get; set; }

        /// <summary>
        /// ドロップ回数
        /// </summary>
        public int DropCount { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="win">勝利判定</param>
        /// <param name="name">艦娘名</param>
        /// <param name="rare">レアリティ</param>
        public DatDropData(string win, string name, short rare, int count) 
        {
            this.DropWinDecision = win;
            this.DropKanmusuName = name;
            this.DropRarity = rare;
            this.DropCount = count;
        }

        /// <summary>
        /// 初期読み込み用コンストラクタ
        /// DropData読み込み時に上のコンストラクタを使用するとエラーで落ちるので、空のコンストラクタを用意する。
        /// </summary>
        public DatDropData() 
        {

        }
    }
}
