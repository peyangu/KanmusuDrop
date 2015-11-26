using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanmusuDrop
{
    /// <summary>
    /// 艦娘の1隻ごとのデータクラス
    /// </summary>
    public class DatKanmusuData
    {
        /// <summary>
        /// 艦種
        /// 0:駆逐 1:軽巡 2:重巡 3:戦艦 4:正規空母 5:軽空母 6:水母 7:潜水艦 8:潜水空母 9:その他
        /// </summary>
        public int ShipClass { get; set; }

        /// <summary>
        /// 艦娘名
        /// </summary>
        public string KanmusuName { get; set; }

        /// <summary>
        /// レアリティ
        /// 0:白 1:銀 2:金 3:虹
        /// </summary>
        public short Rarity { get; set; }
    }
}
