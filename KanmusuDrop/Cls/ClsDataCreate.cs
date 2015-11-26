using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanmusuDrop
{
    /// <summary>
    /// データ加工クラス
    /// ドロップ情報の登録に必要な値への変換を担当
    /// </summary>
    public class ClsDataCreate
    {
        /// <summary>
        /// 引数から艦種の文字列を返す
        /// </summary>
        /// <param name="shipC"></param>
        /// <returns></returns>
        public static string GetShipClass(int shipC)
        {
            List<DatShipClass> clsList = XMLWork.ShipClassLoad();

            return clsList.Find(cls => cls.Id == shipC).ShipClass;
        }

        /// <summary>
        /// 引数から艦種のIDを返す
        /// </summary>
        /// <param name="cls"></param>
        /// <returns></returns>
        public static int GetShipClassId(string cls) 
        {
            List<DatShipClass> clsList = XMLWork.ShipClassLoad();

            return clsList.Find(clsL=> clsL.ShipClass == cls).Id;
        }
    }
}
