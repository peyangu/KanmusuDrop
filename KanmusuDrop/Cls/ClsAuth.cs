using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace KanmusuDrop
{
    /// <summary>
    /// 認証情報を管理するクラス
    /// これはGitHubにあげない
    /// </summary>
    public class ClsAuth
    {
        /// <summary>
        /// 認証情報を保存する
        /// </summary>
        /// <param name="token"></param>
        public static void AuthInfoSet(OAuthAccessToken token) 
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("Auth.txt", false)) 
            {
                sw.WriteLine(token.Token);
                sw.WriteLine(token.TokenSecret);
            }
        }

        /// <summary>
        /// 認証情報削除
        /// </summary>
        public static void AuthInfoDelete() 
        {
            System.IO.File.Delete("Auth.txt");
        }

        /// <summary>
        /// 認証情報を取得する
        /// </summary>
        /// <returns></returns>
        public static List<string> AuthInfoGet() 
        {
            List<string> result = new List<string>();
            if (System.IO.File.Exists("Auth.txt") == true)
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader("Auth.txt"))
                {
                    result.Add(sr.ReadLine());
                    result.Add(sr.ReadLine());
                }
            }       

            return result;
        }
    }
}
