using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KanmusuDrop
{
    /// <summary>
    /// CSV出力クラス
    /// </summary>
    public class ClsOutputCSV
    {
        /// <summary>
        /// ドロップ情報をCSVに出力する
        /// </summary>
        /// <param name="dropList"></param>
        public static void OutputCsv(List<DatDropData> dropList) 
        {
            if (dropList.Count == 0) 
            {
                MessageBox.Show("ドロップ情報が0件のため、処理を中止します。");
                return;
            }

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            //保存先の初期値（マイドキュメント）
            dlg.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            dlg.Title = "保存先のファイルを選択してください";
            dlg.FileName = "ドロップ情報";
            dlg.Filter = "CSVファイル(*.csv)|*.csv";
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    using (var sw = new System.IO.StreamWriter(dlg.FileName, false, System.Text.Encoding.GetEncoding("Shift_JIS")))
                    {
                        //ダブルクォーテーションで囲む
                        Func<string, string> dqot = (str) => { return "\"" + str.Replace("\"", "\"\"") + "\""; };
                        foreach (DatDropData d in dropList)
                            sw.WriteLine(dqot(d.DropWinDecision) + "," + dqot(d.DropKanmusuName));
                    }
                    MessageBox.Show("保存しました。");
                }
                catch (SystemException ex)
                {
                    ClsLogWrite.LogWrite("CSV出力中にエラーが起きました。",ex);
                }
            }
        }
    }
}
