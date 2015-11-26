using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Reflection;

namespace KanmusuDrop
{
    /// <summary>
    /// XMLの操作クラス
    /// XMLへの保存、読み込みを担当
    /// </summary>
    public class XMLWork
    {
        /// <summary>
        /// ドロップ情報の保存
        /// </summary>
        /// <param name="dropList"></param>
        public static void DropDataSave(List<DatDropData> dropList) 
        {
            //現在のコードを実行しているAssemblyを取得
            Assembly myAssembly = Assembly.GetExecutingAssembly();

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(DatDropData[]));

            // リストをシリアライズ化(配列にしてから)
            using (System.IO.FileStream fs = new System.IO.FileStream("DropData.xml", System.IO.FileMode.Create)) 
            {
                serializer.Serialize(fs, dropList.ToArray());
            }
        }

        /// <summary>
        /// 艦娘データの取得
        /// </summary>
        /// <returns>全艦娘のデータのリスト</returns>
        public static List<DatKanmusuData> KanmusuDataLoad() 
        {
            DatKanmusuData[] loadAry;

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(DatKanmusuData[]));

            using (System.IO.StreamReader sr = new System.IO.StreamReader("KanmusuData.xml", new System.Text.UTF8Encoding(false)))
            {
                loadAry = (DatKanmusuData[])serializer.Deserialize(sr);
            }

            return loadAry.ToList();
        }

        /// <summary>
        /// ドロップデータの取得
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>ドロップリストに表示される艦娘のデータのリスト(初期は読み込み用の1件がある)</returns>
        public static List<DatDropData> DropDataLoad()
        {
            DatDropData[] loadAry;

            // コンストラクタで引数を指定している場合、渡していないとエラーになる。
            // ClsDropDataのコンストラクタ2があるのはここのため
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(DatDropData[]));

            using (System.IO.StreamReader sr = new System.IO.StreamReader("DropData.xml", new System.Text.UTF8Encoding(false)))
            {
                loadAry = (DatDropData[])serializer.Deserialize(sr);
            }

            return loadAry.ToList();
        }

        /// <summary>
        /// 艦種の取得
        /// </summary>
        /// <returns></returns>
        public static List<DatShipClass> ShipClassLoad()
        {
            DatShipClass[] loadAry;

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(DatShipClass[]));

            using (System.IO.StreamReader sr = new System.IO.StreamReader("ShipClass.xml", new System.Text.UTF8Encoding(false)))
            {
                loadAry = (DatShipClass[])serializer.Deserialize(sr);
            }

            return loadAry.ToList();
        }
    }
}
