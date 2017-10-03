using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace SucLib.Common
{
    /// <summary>
    /// 输入输出控制
    /// </summary>
    public class IOUtil
    {
        /// <summary>
        /// 创建随机时间文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <returns>文件夹路径</returns>
        public static string CreateDateTimeDir(string path)
        {
            DateTime now = DateTime.Now;
            string text = now.Year.ToString();
            string text2 = now.Month.ToString();
            string text3 = now.Day.ToString();
            string[] array = path.Split(new char[]
			{
				'/'
			});
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != "" && array[i] != "~" && i != 0)
                {
                    string text4 = "~/";
                    for (int j = 1; j <= i; j++)
                    {
                        text4 = text4 + array[j] + "/";
                    }
                    IOUtil.CreateDir(text4);
                }
            }
            IOUtil.CreateDir(path + "/" + text);
            IOUtil.CreateDir(string.Concat(new string[]
			{
				path,
				"/",
				text,
				"/",
				text2
			}));
            IOUtil.CreateDir(string.Concat(new string[]
			{
				path,
				"/",
				text,
				"/",
				text2,
				"/",
				text3
			}));
            return string.Concat(new string[]
			{
				path,
				"/",
				text,
				"/",
				text2,
				"/",
				text3
			});
        }
        private static void CreateDir(string path)
        {
            string path2 = HttpContext.Current.Server.MapPath(path);
            if (!Directory.Exists(path2))
            {
                Directory.CreateDirectory(path2);
            }
        }
    }
}
