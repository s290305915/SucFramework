using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using DoctorLibrary;

namespace Linq
{
    class Program
    {
        public static string re;
        private string F<T>() => nameof(T);
        static void Main(string[] args)
        {
            var name = "Fanguzai";
            Console.WriteLine($"Hello, {name}");

            string[] a1 = new string[] { "1", "3", "5", "8" };// ｛1，3，5，8｝｛a,b,c,d｝
            string[] a2 = new string[] { "a", "b", "c", "d" };
            string opt = "1+3,5,8";     //a+b,c,d

            var xr = string.Join("", opt.Select(x => a1.ToList().FindIndex(s => s.ToString() == x.ToString()) == -1 ? x.ToString() : x.ToString() + "#"));

            var d = string.Join("", opt.Select(s => a1.ToList().FindIndex(x => x.ToString() == s.ToString()) == -1 ? s.ToString() + "#" :
                a2[a1.ToList().FindIndex(x => x.ToString() == s.ToString())] + "#"));

            Console.WriteLine(xr);


            return;

            Get();
            Console.ReadKey();
            return;
            using(DataClassesDataContext db = new DataClassesDataContext())
            {
                var usr = from s in db.SUC_USER select s;


                Console.WriteLine(usr.ToList()[0].NAME + "," + usr.ToList()[0].LOGIN_NAME);
                string[] s1 = new string[] { "a", "b", "c", "d", "e", "f", "g" };
                string[] s2 = new string[] { "1", "2", "3", "4" };
                string[] s3 = new string[] { "A", "B", "C", "D", "E" };
                List<string[]> ss = new List<string[]>();
                ss.Add(s1);
                ss.Add(s2);
                ss.Add(s3);

                re = "";
                RandNext(ss, 0);
                Console.WriteLine(re);
                Console.ReadKey();

                //"{a,b,c,d,e,f}{1,2,3,4,5}{A,B,C,D}"
            }
        }

        static void Get()
        {
            using(DataClassesDataContext db = new DataClassesDataContext())
            {
                DataTable dt = new DataTable();
                var t = from c in db.SUC_USER select c;
                var tt = from c in db.SUC_USER where c.CREATE_TIME == DateTime.Parse("2016-10-27 10:14:06") select c;
                dt = ToDataTable<SUC_USER>(t.AsEnumerable<SUC_USER>());
                DataRowView drv = dt.DefaultView[0];

            }
        }

        static void RandNext(List<string[]> x, int xindex, bool isstart = false, int max = 0)
        {
            if(xindex == max && isstart)
                return;
            isstart = !isstart;
            foreach(string[] r in x)
            {
                if(r.Length > max)
                    max = r.Length;
                try
                {
                    re += r[xindex];
                    re += ",";
                }
                catch { continue; }
            }
            xindex++;
            re.Remove(re.Length - 1);
            RandNext(x, xindex, true, max);
        }


        /// <summary>
        ///  下面通过一个方法来实现返回DataTable类型 
        /// LINQ返回DataTable类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="varlist"></param>
        /// <returns></returns>
        static public DataTable ToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();
            // column names 
            PropertyInfo[] oProps = null;
            if(varlist == null)
                return dtReturn;
            foreach(T rec in varlist)
            {
                if(oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach(PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;
                        if((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                             == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                DataRow dr = dtReturn.NewRow();
                foreach(PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
    }
}
