using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using SucLib.Data.IDal;
using SucLib.Data.Factory;
using System.Web.Script.Serialization;
using SucLib.Model;
using System.Linq.Expressions;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            int int16 = 0x000014221E;
            Console.WriteLine(HexStringToASCII(int16.ToString()));
            Console.ReadKey();
            return;

            Expression<Func<int, bool>> funcExpression = num => num == 0;

            //开始解析 funcExpression 表达式
            ParameterExpression pExpression = funcExpression.Parameters[0]; //lambda 表达式参数
            BinaryExpression body = (BinaryExpression)funcExpression.Body;  //lambda 表达式主体：num == 0

            Console.WriteLine($"解析：{pExpression.Name} => {body.Left} {body.NodeType} {body.Right}");

            //创建表达式树
            Expression<Action<int>> actionExpression = n => Console.WriteLine(n);
            Expression<Func<int, bool>> funcExpression1 = (n) => n < 0;
            Expression<Func<int, int, bool>> funcExpression2 = (n, m) => n - m == 0;


            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            //此示例展示了如何通过将输入参数括在括号中来指定多个输入参数。
            var firstSmallNumbers = numbers.TakeWhile((n, index) => n >= index);

            //下面一行代码将生成一个序列，其中包含 numbers 数组中在 9 左侧的所有元素，因为它是序列中第一个不满足条件的数字：
            var firstNumbersLessThan6 = numbers.TakeWhile(n => n < 6);

            //此处显示了一个标准查询运算符，Count<TSource> 方法： (该参数类型为 Expression<Func> )
            int oddNumbers = numbers.Count(n => n % 2 == 1);

            // 当调用下面的 Func 委托时，该委托将返回 true 或 false 以指示输入参数是否等于 5:
            Func<int, bool> myFunc = x => x == 5;
            bool myFuncRe = myFunc(4);


            return;
            CGSopt cg = new CGSopt();
            cg.ReadCGS();
            return;

            FierBli npoi_test = new FierBli("C:/Users/s2903/Desktop/xx.xls");
            npoi_test.AddNewSheet("xx");

            return;













            TTT ttt1 = new TTT();
            var sst = ttt1.Getay();

            return;
            DataTableOpr.Aida();

            return;
            TTT ttt = new TTT();

            #region 其他测试

            return;
            DlgTest dlg = new DlgTest();
            //BugTicketEventHandler bg = new BugTicketEventHandler(dlg.BugTicket);
            dlg.BugTicket(1, "xx");//
            //TODO: 未实现
            return;

            return;
            string[] vvs = new string[3];
            // ("xx", "vv", "zz")
            return;

            string xcrr = "{\"d\":\"[{\"Ent_Cnt\":\"8\",\"Ent_Comp\":\"0\",\"Ent_No\":\"0\"},{\"Ent_Cnt\":\"4\",\"Ent_Comp\":\"0\",\"Ent_No\":\"0\"}]\"}";
            xcrr = xcrr.Substring(0, xcrr.LastIndexOf("]") + 1);
            xcrr = xcrr.Substring(xcrr.IndexOf("["));

            string xx = string.Format("{0:E2}", 2);
            Console.WriteLine(xx);
            return;


            string tb = "KTL_KG001_DC_USERS_201605";
            tb = tb.Substring(tb.LastIndexOf("_") + 1);
            Console.WriteLine(tb);
            return;

            string sqar = GetQuar("20160522");
            Console.WriteLine(sqar);
            Console.ReadKey();
            return;
            //Test.gtest();
            Test.ymdiv("201603", "201605");
            return;


            double Balance = 0;
            double Rate = 0;
            int Year = 0;
            double TargetBalance = 0;
            Console.WriteLine("请输入你的本金");
            Balance = double.Parse(Console.ReadLine());
            Console.WriteLine("请输入当前的利率百分比");
            Rate = double.Parse(Console.ReadLine());
            Console.WriteLine("请输入你的目标收益");
            TargetBalance = double.Parse(Console.ReadLine());
            do
            {
                Balance *= (Rate / 100 + 1);
                Year++;
            }
            while(Balance < TargetBalance);
            Console.WriteLine("您将在{0}年内，获得{1}元的收益", Year, Balance);
            Console.ReadKey();



            return;
            tksys t = new tksys();
            string sql = t.Query();
            return;
            //pp();
            //return;
            //GuessAge();
            FindPass();

            return;

            OdbcConnection DB2Connection = new OdbcConnection("Driver={IBM DB2 ODBC DRIVER};Database=MT_GY;hostname=221.182.121.126;port=50000;protocol=TCPIP; uid=MT_GY; pwd=Gy###321");

            DB2Connection.Open();

            return;
            string encrypt = "356A192B7913B04C54574D18C28D46E6395428AB";//=1
            string voter, key;
            Utility.SymmetricEncryptor se = new Utility.SymmetricEncryptor(encrypt, encrypt);

            string Uentrypt = se.Decrypt(encrypt);

            Console.WriteLine(encrypt);
            Console.WriteLine(Uentrypt);
            Console.ReadKey();
            return;
            //
            string id = "I12345";
            id = id.Substring(0, 0);// id.Length - 1

            Console.WriteLine(id);
            Console.ReadKey();
            return;

            string a = "PZH_IBAS";
            a = a.Substring(0, a.IndexOf("_"));
            Console.WriteLine(a);
            Console.ReadKey();
            System.Diagnostics.EventLog.WriteEntry("tariff_detail_dt" + DateTime.Now.ToString("yyyyMMddHH24mmss"), "明细:", System.Diagnostics.EventLogEntryType.Information);

            return;
            Testdate dt = new Testdate();
            dt.bb();
            return;
            //aa();
            //testnpoi();
            string b = "ass";
            int i = 0;
            //reftest(ref i, out b);
            Assembly assembly = Assembly.Load("ConsoleApplication1");
            Type type = assembly.GetType("Example");
            object o = Activator.CreateInstance(type);
            Console.WriteLine(i.ToString() + "," + b + "," + o.ToString());

            #endregion
        }
        #region 其他方法
        /// <summary>
        /// 分割datatable
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        static DataTable[] SplitDT(DataTable dt, int p)
        {
            var totalRows = dt.Rows.Count; //总行数

            if(totalRows == 0) //一行没有就直接返回空的
            {
                return new[]
                       {
                           dt.Clone()
                       };
            }

            var totalTables = (totalRows - 1) / p + 1; //要返回的表的个数
            var result = new DataTable[totalTables]; //要返回的结果

            for(var i = 0;i < totalTables;i++)
            {
                var thisDT = result[i] = dt.Clone();
                thisDT.BeginLoadData();
                var end = Math.Max(i + i * p, totalRows);
                for(var j = i * p;j < end;j++)
                {
                    thisDT.Rows.Add(dt.Rows[j].ItemArray);
                }
                thisDT.EndLoadData();
            }

            return result;
        }
        /// <summary>
        /// 获取季度
        /// </summary>
        /// <param name="optime"></param>
        /// <returns></returns>
        private static string GetQuar(string optime)
        {
            int opt = Convert.ToInt32(optime.Substring(4, 2));
            if(opt >= 1 && opt <= 3)
            {
                return optime.Substring(0, 4) + "001";
            }
            else if(opt >= 4 && opt < 6)
            {
                return optime.Substring(0, 4) + "002";
            }
            else if(opt >= 7 && opt < 9)
            {
                return optime.Substring(0, 4) + "003";
            }
            else
            {
                return optime.Substring(0, 4) + "004";
            }
        }


        private static void aadii()
        {
            DataTable dt = new DataTable();

            List<ChannelTargetOrderList> ctos = new List<ChannelTargetOrderList>();
            ChannelTargetOrderList cto = new ChannelTargetOrderList();
            List<ChannelTarget> cts = new List<ChannelTarget>();
            int dd = 0;
            if(dt.Rows.Count > 0)
            {
                for(int i = 0;i < dt.Rows.Count;i++)
                {
                    string ddname = dt.Rows[i]["NAME"].ToString();

                    ChannelTarget ct = new ChannelTarget();
                    ct.ENTITY_TYPE = dt.Rows[i]["NAME"].ToString();
                    ct.ENTITY_ID = dt.Rows[i]["ENTITY_ID"].ToString();
                    ct.TVALUE = dt.Rows[i]["TVALUE"].ToString();
                    ct.SVALUE = dt.Rows[i]["SVALUE"].ToString();
                    ct.TSQ = dt.Rows[i]["TSQ"].ToString();
                    ct.YEAR_MONTH = dt.Rows[i]["YEAR_MONTH"].ToString();
                    ct.QUAR = dt.Rows[i]["QUAR"].ToString();
                    ct.NAME = dt.Rows[i]["NAME"].ToString();
                    ct.ORD = dt.Rows[i]["ORD"].ToString();
                    cts.Add(ct);

                    if(dd > 0)
                    {
                        if(ctos.Count > 0)
                        {
                            cto = ctos[dd - 1];
                            if(ddname == cto.NAME)
                            {
                                cto.NAME = ddname;
                            }
                            else
                            {
                                cto.CTList = cts;
                                ctos.Add(cto);
                                dd += 1;
                            }
                        }
                    }
                    else
                    {
                        if(dd == 0)
                        {
                            cto.NAME = ddname;
                            cto.CTList = cts;
                            ctos.Add(cto);
                            dd = 1;
                        }
                    }

                }
            }
            else
            {

            }

        }

        private static void GuessAge()
        {
            for(int i = 1;i < 200;i++)
            {
                double a = Math.Pow(i, 3);
                double b = Math.Pow(i, 4);
                string x = Convert.ToInt32(a).ToString() + Convert.ToInt32(b).ToString();
                if(x.Length <= 10 && x.Contains("0") && x.Contains("1")
                    && x.Contains("2") && x.Contains("3")
                    && x.Contains("4") && x.Contains("5")
                    && x.Contains("6") && x.Contains("7")
                    && x.Contains("8") && x.Contains("9"))
                {
                    Console.WriteLine(string.Format(@"年龄：{0},三次方：{1},四次方：{2}", i, a, b));
                }
            }
            Console.ReadKey();
        }

        private static void pp()
        {
            int c, s, c1 = 0, s1 = 0;
            string input = Console.ReadLine();
            string[] inps = input.Split(' ');
            int n = inps.Length; //Convert.ToInt32(Console.ReadLine());
            int n1 = n % 2 != 0 ? n / 2 : n / 2 - 1;
            int[] nums = new int[n];
            for(int k = 0;k < nums.Length;k++)
            {
                nums[k] = Convert.ToInt32(inps[k]);
            }
            //for (int i = 0; i < nums.Length; i++)
            //{
            //    nums[i] = Convert.ToInt32(Console.ReadLine());
            //}
            for(c = n1;c > 0;c--)
            {
                if(nums[c] > nums[n1])
                    c1++;
            }
            for(s = n1;s < n;s++)
            {
                if(nums[s] > nums[n1])
                    s1++;
            }
            Console.WriteLine("中间数为:{0},后面有{1}个数比它大", nums[n1], s1 + c1);
            Console.ReadKey();
            //pp();
        }

        private static void FindPass()
        {
            int t = 0;
            for(int i = 10000;i < 20000;i++)
            {
                if(t > 3)
                    break;
                for(int j = 1;j < i;j++)
                {
                    double u = Convert.ToInt32(i);
                    double p = Convert.ToInt32(j);
                    double re = Math.Log(u, p);
                    re = Math.Truncate(re * u * p);
                    if((re % 9988998) == 0)
                    {
                        int re1 = Convert.ToInt32(re);
                        for(int k = re1 - 1;k > 2;k--)
                        {
                            if(re1 % k == 0)
                                continue;
                            else
                            {
                                Console.WriteLine("\r\t" + re1 + "->约束：" + i + ",\t" + j);
                                t++;
                                break;
                            }
                        }
                    }
                }
            }
            Console.WriteLine("完毕");
            Console.ReadKey();
        }


        /// <summary>
        /// reftest
        /// </summary>
        /// <param name="a"></param>
        private static void reftest(ref int a, out string b)
        {
            a = 2;
            b = "nihao !";
        }

        /// <summary>
        /// npoi合并单元格测试 
        /// </summary>
        private static void testnpoi()
        {
            string filetime = DateTime.Now.ToString("yyyyMMddmm");
            string _webpath = AppDomain.CurrentDomain.BaseDirectory + "\\Excel\\EndMarketing";
            if(!Directory.Exists(_webpath))
                Directory.CreateDirectory(_webpath);
            string filename = "(年终营销活动)_" + filetime + ".xls";
            string path = _webpath + "\\" + filename;

            MemoryStream ms = new MemoryStream();
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("年终营销活动-");
            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            sheet.AddMergedRegion(new CellRangeAddress(0, 1, 0, 0));
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 1, 2));
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 3, 4));
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 5, 6));
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 7, 8));
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 9, 10));
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 11, 12));
            IRow row1 = sheet.CreateRow(0);

            row1.CreateCell(0).SetCellValue("区县");
            row1.CreateCell(1).SetCellValue("区县1");
            row1.CreateCell(3).SetCellValue("区县3");
            row1.CreateCell(5).SetCellValue("区县5");
            row1.CreateCell(7).SetCellValue("区县7");
            row1.CreateCell(9).SetCellValue("区县9");
            row1.CreateCell(11).SetCellValue("区县11");

            IRow row2 = sheet.CreateRow(1);
            row2.CreateCell(1).SetCellValue("区县1");
            row2.CreateCell(2).SetCellValue("区县2");
            row2.CreateCell(3).SetCellValue("区县3");
            row2.CreateCell(4).SetCellValue("区县4");
            row2.CreateCell(5).SetCellValue("区县5");
            row2.CreateCell(6).SetCellValue("区县6");
            row2.CreateCell(7).SetCellValue("区县7");
            row2.CreateCell(8).SetCellValue("区县8");
            row2.CreateCell(9).SetCellValue("区县9");
            row2.CreateCell(10).SetCellValue("区县10");
            row2.CreateCell(11).SetCellValue("区县11");
            row2.CreateCell(12).SetCellValue("区县12");
            workbook.Write(ms);
            ms.Position = 0;
            ms.Close();
            ms.Flush();
            SaveToFile(ms, path);
        }

        private static void SaveToFile(MemoryStream ms, string fileName)
        {
            using(FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();

                fs.Write(data, 0, data.Length);
                fs.Flush();

                data = null;
            }
        }

        /// <summary>
        /// 实体类转dt等
        /// </summary>
        private static void aa()
        {
            ModelHandler<tksys> mh = new ModelHandler<tksys>();
            tkuser user = new tkuser()
            {
                LoginName = "aa",
                Mail = "dgae",
                Name = "gegg"
            };
            tksys sys = new tksys()
            {
                ClickCount = 0,
                Content = "dagdafafd",
                DivName = "agdafe",
                Explain = "",
                ID = 1,
                PublishDate = DateTime.Now,
                Link = "fadfa",
                Poupose = "agefda",
                Title = "agefadfae",
                Type = 1,
                UpdateCount = 0,
                user = user
            };
            List<tksys> syss = new List<tksys>();
            syss.Add(sys);
            DataTable dt = mh.FillDataTable(syss);
            for(int i = 0;i <= dt.Rows.Count;i++)
            {
                Console.WriteLine(dt.Rows[0][i].ToString());
            }
            Console.WriteLine("Pause");
        }

        private static void bb()
        {
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("ID", Type.GetType("System.Int32"));
            tblDatas.Columns[0].AutoIncrement = true;
            tblDatas.Columns[0].AutoIncrementSeed = 1;
            tblDatas.Columns[0].AutoIncrementStep = 1;

            tblDatas.Columns.Add("Product", Type.GetType("System.String"));
            tblDatas.Columns.Add("Version", Type.GetType("System.String"));
            tblDatas.Columns.Add("Description", Type.GetType("System.String"));

            tblDatas.Rows.Add(new object[] { null, "a", "b", "c" });
            tblDatas.Rows.Add(new object[] { null, "a", "b", "c" });
            tblDatas.Rows.Add(new object[] { null, "a", "b", "c" });
            tblDatas.Rows.Add(new object[] { null, "a", "b", "c" });
            tblDatas.Rows.Add(new object[] { null, "a", "b", "c" });

        }

        #endregion


        #region 十六进制ASCII转换

        /// <summary>
        /// 将一条十六进制字符串转换为ASCII
        /// </summary>
        /// <param name="hexstring">一条十六进制字符串</param>
        /// <returns>返回一条ASCII码</returns>
        public static string HexStringToASCII(string hexstring)
        {
            try
            {
                byte[] bt = HexStringToBinary(hexstring);
                string lin = "";
                for(int i = 0;i < bt.Length;i++)
                {
                    lin = lin + bt[i] + " ";
                }


                string[] ss = lin.Trim().Split(new char[] { ' ' });
                char[] c = new char[ss.Length];
                int a;
                for(int i = 0;i < c.Length;i++)
                {
                    a = Convert.ToInt32(ss[i]);
                    c[i] = Convert.ToChar(a);
                }

                string b = new string(c);
                return b;
            }
            catch(Exception ex) { return ex + ":" + hexstring; }
        }

        /**/
        /// <summary>
        /// 16进制字符串转换为二进制数组
        /// </summary>
        /// <param name="hexstring">用空格切割字符串</param>
        /// <returns>返回一个二进制字符串</returns>
        public static byte[] HexStringToBinary(string hexstring)
        {

            string[] tmpary = hexstring.Trim().Split(' ');
            byte[] buff = new byte[tmpary.Length];
            for(int i = 0;i < buff.Length;i++)
            {
                buff[i] = Convert.ToByte(tmpary[i], 16);
            }
            return buff;
        }

        #endregion
    }
    #region 其他类

    public class aa
    {
        public void aad()
        {
            Program p = new Program();
            //Program.intenal ss = new Program.intenal();
            //ss.a = "";
        }
    }

    public class ChannelTargetOrderList
    {
        public string NAME
        {
            get; set;
        }
        public List<ChannelTarget> CTList
        {
            get; set;
        }
    }

    public class ChannelTarget
    {
        public string ENTITY_TYPE
        {
            get; set;
        }
        public string ENTITY_ID
        {
            get; set;
        }
        public string TVALUE
        {
            get; set;
        }
        public string SVALUE
        {
            get; set;
        }
        public string TSQ
        {
            get; set;
        }
        public string YEAR_MONTH
        {
            get; set;
        }
        public string QUAR
        {
            get; set;
        }
        public string NAME
        {
            get; set;
        }
        public string ORD
        {
            get; set;
        }
    }

    #endregion

    public delegate void BugTicketEventHandler(int a, string b);
    public class DlgTest
    {
        public void BugTicket()
        {
            Console.WriteLine("这是调用委托的内容！无参数");
        }
        public void BugTicket(int a)
        {
            Console.WriteLine("这是调用委托的内容！有数字参数：{0}", a);
        }
        public void BugTicket(int a, string b)
        {
            Console.WriteLine("这是调用委托的内容！有数字参数：{0},字符参数：{1}", a, b);
        }
        public void BugTicket(string b)
        {
            Console.WriteLine("这是调用委托的内容！有字符参数：{0}", b);
        }
    }

    public class TTT
    {
        public TTT()
        {

        }
        public object Get(ref Type t)
        {
            List<SUC_USER> us = new SUC_USER().FindAll();
            t = typeof(SUC_USER);
            object o = us;
            return o;
        }

        public dynamic Getay()
        {
            return new SUC_ROLE().FindAll();
        }
    }



}
