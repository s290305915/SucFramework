using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public static class Test
    {
        public static void gtest()
        {
            string s = "kkk";
            s = aa();
            s.Remove(0, 1);
            for (int i = 1; i <= 50; i++)
            {
                Console.Write(i % 2 == 0 ? i.ToString() : s);
            }
            Console.ReadKey();
            //三目表达式     判断 ? ture(): false()
        }

        public static string aa()
        {
            return "kkk";
        }

        public static void ymdiv(string yearmonth, string yearmonthe)
        {
            if (Convert.ToInt32(yearmonthe) - Convert.ToInt32(yearmonth) > 0)
            {
                int s = Convert.ToInt32(yearmonthe) - Convert.ToInt32(yearmonth);
                int re = Convert.ToInt32(yearmonthe);
                int[] ys = new int[s];
                for (int i = 0; i < s; i++)
                {
                    ys[i] = re;
                    re--;
                }
            }
        }
    }

}
