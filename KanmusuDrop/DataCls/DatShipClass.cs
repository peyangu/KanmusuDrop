using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanmusuDrop
{
	/// <summary>
    /// 艦種のデータクラス
    /// </summary>
    public class DatShipClass
    {
		/// <summary>
        /// ID
        /// </summary>
		public int Id { get; set; }

		/// <summary>
        /// 艦種
        /// </summary>
		public string ShipClass { get; set; }
    }
}
