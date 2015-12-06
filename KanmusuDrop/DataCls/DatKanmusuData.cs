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
