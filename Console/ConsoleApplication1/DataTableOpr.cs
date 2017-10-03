using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SucLib.Data.IDal;
using SucLib.Data.Factory;
using System.Security.Cryptography;
using System.Web.Script.Serialization;


namespace ConsoleApplication1
{
    public class C
    {
        public string C_VALUE { get; set; }

        public void fu(int n)
        {
            string s = Enumerable.Range(1, n).ToList().Sum().ToString();
            string px = "";
            Enumerable.Range(1, n).ToList().ForEach(x => px += "," + x.ToString());
            string rr = string.Join(",", Enumerable.Range(1, n).ToList().ConvertAll<string>(x => x.ToString()).Select((d, i) => d + "!" + i.ToString()));
            List<string> strs = new List<string>() { "77", "33", "54", "60" };
            string pxs = string.Join(",", strs.Select<string, int>(x => Convert.ToInt32(x)).ToList().OrderBy(x => x));
            Console.WriteLine(pxs);
            Console.ReadKey();
        }
    }

    public static class DataTableOpr
    {

        //strSystemId:
        //system_WideBand_01
        //strPassword:
        //WideBandSystem@2016
        //SecretKey:
        //gmccLS@2016!

        //strToken: MD5(strSystemId + strPassword + reqTimeStamp.ToString("yyyyMMddHHmmss") + SecretKey)

        public static void Aida()
        {

            C c = new C();
            c.fu(10);

            return;


            IDBHelp db = DBFactory.Create();
            DataTable dt = db.GetDataTable("SELECT * FROM SUC_USER");

            DataTable dts = dt.Clone();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dts.NewRow();
                dr = dt.Rows[i];
                dts.ImportRow(dr);
            }

            string dv = string.Join(",", dt.AsEnumerable().Select(x => x[0].ToString()));
            Console.WriteLine(dv);
            return;

            List<object> lo = new List<object>() { 1, "x", 0.5F };

            string[] lst = new string[] { "aa", "bb", "cc" };
            if (lst.Count() > 0)
            {
                foreach (string s in lst)
                {
                    Console.WriteLine("字符串是：" + s);
                }

                for (int i = 0; i < lst.Count(); i++)
                {
                    Console.WriteLine("The String is:" + lst[i]);
                }
            }


            Console.ReadKey();
            return;

            C rc = new C();
            rc.C_VALUE = "xxx";


            string para = string.Join(",", typeof(FORM_JSWH_WIDEBANDMAINT).GetProperties().AsEnumerable().Select((d, i) => "A" + (i + 1).ToString()));
            LogHelper.Write(para);
            //IDBHelp db = DBFactory.Create();
            //DataTable dt = db.GetDataTable("select top 1000 * from suc_news");
            //var t = SplitDT(dt, 100);

            try
            {
                DateTime reqNowTiem = DateTime.Now;
                string reqTimeStamp = reqNowTiem.ToString("yyyyMMddHHmmss"); //(int)(reqNowTiem - startTime).TotalSeconds;//时间戳
                string strPassword = "WideBandSystem@2016";
                string strSystemId = "system_WideBand_01";
                int[] strDataTypes = new int[] { 1, 2, 3 };
                string SecretKey = "gmccLS@2016!";
                string md5Str = strSystemId + strPassword + reqTimeStamp + SecretKey;
                string strToken = md5(md5Str);

                WideBandService.WideBandDataServiceSoapClient clt = new WideBandService.WideBandDataServiceSoapClient();
                foreach (int i in strDataTypes)
                {
                    string json = clt.GetWideBandData(strToken, strSystemId, strPassword, reqNowTiem, i);
                    //DataTable dt = JsonHelper.JsonToDataTable(json);
                    LoadData(i, json);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }


        /// <summary>
        /// 将得到的数据进行处理
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dt"></param>
        static void LoadData(int type, string json)
        {
            json = json.Replace("\n", "").Replace("\r", "");
            string Newjson = "{Data:" + json + "}";
            JavaScriptSerializer JSS = new JavaScriptSerializer();
            ONE maints = new ONE();
            TWO wlans = new TWO();
            THREE coms = new THREE();
            StringBuilder sb = new StringBuilder();
            string tablename = "";
            string tebletemp = "FORM_TEMP";
            string sql = "";
            string para = "";
            //清空临时表
            sql = string.Format(@"TRUNCATE TABLE {0} ", tebletemp);
            Excuete(sql);
            switch (type)
            {
                case 1:
                    maints = JSS.Deserialize<ONE>(Newjson);
                    tablename = "FORM_JSWH_WIDEBANDMAINT";
                    LogHelper.Info("获取数据成功，表：FORM_JSWH_WIDEBANDMAINT。。。。\r\n");
                    para = string.Join(",", typeof(FORM_JSWH_WIDEBANDMAINT).GetProperties().AsEnumerable().Select((d, i) => "A" + (i + 1).ToString()));
                    sb.AppendFormat(@"INSERT INTO {0}({1}) VALUES", tebletemp, para);
                    foreach (FORM_JSWH_WIDEBANDMAINT t in maints.Data)
                    {
                        #region FORM_JSWH_WIDEBANDMAINT
                        sb.AppendFormat(@"('{0}',", t.TASKID);
                        sb.AppendFormat(@"'{0}',", t.SN);
                        sb.AppendFormat(@"'{0}',", t.APPLYTIME);
                        sb.AppendFormat(@"'{0}',", t.APPLYUSER);
                        sb.AppendFormat(@"'{0}',", t.BILLSOURCE);
                        sb.AppendFormat(@"'{0}',", t.CITY);
                        sb.AppendFormat(@"'{0}',", t.VILLAGE);
                        sb.AppendFormat(@"'{0}',", t.AREAFAULT);
                        sb.AppendFormat(@"'{0}',", t.COMMUNITYTYPE);
                        sb.AppendFormat(@"'{0}',", t.COMMUNITYNAME);
                        sb.AppendFormat(@"'{0}',", t.CUSTOMERTEL);
                        sb.AppendFormat(@"'{0}',", t.DISTRICT);
                        sb.AppendFormat(@"'{0}',", t.RESPONSIBLEUNIT);
                        sb.AppendFormat(@"'{0}',", t.EMOSDATE);
                        sb.AppendFormat(@"'{0}',", t.EMOSDELYTIME);
                        sb.AppendFormat(@"'{0}',", t.FAULTTYPE);
                        sb.AppendFormat(@"'{0}',", t.CUSTOMERACCOUNT);
                        sb.AppendFormat(@"'{0}',", t.SYMPTOM);
                        sb.AppendFormat(@"'{0}',", t.LIMITTIME);
                        sb.AppendFormat(@"'{0}',", t.PRESTEP);
                        sb.AppendFormat(@"'{0}',", t.FAULTDETAIL);
                        sb.AppendFormat(@"'{0}',", t.DELYHOURS);
                        sb.AppendFormat(@"'{0}',", t.CALLBACKOPERATION);
                        sb.AppendFormat(@"'{0}',", t.RETURNBACKREASON);
                        sb.AppendFormat(@"'{0}',", t.RESONSFORFAILURE);
                        sb.AppendFormat(@"'{0}',", t.WEATHERACHIVED);
                        sb.AppendFormat(@"'{0}',", t.WORKPERMIT);
                        sb.AppendFormat(@"'{0}',", t.ATTITUDESATISFY);
                        sb.AppendFormat(@"'{0}',", t.RESULTSATISFY);
                        sb.AppendFormat(@"'{0}',", t.OTHERPROBLEMS);
                        sb.AppendFormat(@"'{0}',", t.OTHERPROBLEMDEAL);
                        sb.AppendFormat(@"'{0}',", t.VIEWFAILUREMARK);
                        sb.AppendFormat(@"'{0}'),", t.UNCOVERREASON);
                        #endregion
                    }
                    sql = sb.ToString().Substring(0, sb.ToString().Length - 1); //导入临时表
                    LogHelper.Info("将插入临时表“FORM_TEMP”sql：" + sql + "\r\n");
                    Excuete(sql);
                    LogHelper.Info("导入临时表成功，即将导入正式表：“FORM_JSWH_WIDEBANDMAINT”\r\n");
                    sql = string.Format(@"MERGE INTO {0} A USING(
                                          SELECT * FROM {1}
                                          )B ON A.TASKID=B.A1
                                          WHEN NOT MATCHED THEN
                                          INSERT VALUES({2});", tablename, tebletemp, para);
                    LogHelper.Info("将导入正式表“FORM_JSWH_WIDEBANDMAINT”sql：" + sql + "\r\n");
                    Excuete(sql);
                    LogHelper.Info("导入正式表完成！");
                    break;
                case 2:
                    wlans = JSS.Deserialize<TWO>(Newjson);
                    tablename = "FORM_JSWH_WIDEBANDMAINT_WLAN";
                    LogHelper.Info("获取数据成功，表：FORM_JSWH_WIDEBANDMAINT_WLAN。。。。\r\n");
                    para = string.Join(",", typeof(FORM_JSWH_WIDEBANDMAINT_WLAN).GetProperties().AsEnumerable().Select((d, i) => "A" + (i + 1).ToString()));
                    sb.AppendFormat(@"INSERT INTO {0}({1}) VALUES", tebletemp, para);
                    foreach (FORM_JSWH_WIDEBANDMAINT_WLAN t in wlans.Data)
                    {
                        #region FORM_JSWH_WIDEBANDMAINT_WLAN
                        sb.AppendFormat(@"('{0}',", t.TASKID);
                        sb.AppendFormat(@"'{0}',", t.SN);
                        sb.AppendFormat(@"'{0}',", t.APPLYTIME);
                        sb.AppendFormat(@"'{0}',", t.APPLYUSER);
                        sb.AppendFormat(@"'{0}',", t.BILLSOURCE);
                        sb.AppendFormat(@"'{0}',", t.WLANNAME);
                        sb.AppendFormat(@"'{0}',", t.CUSTOMERTEL);
                        sb.AppendFormat(@"'{0}',", t.BILLCLASS);
                        sb.AppendFormat(@"'{0}',", t.EOMSAPPLYTIME);
                        sb.AppendFormat(@"'{0}',", t.EOMSDELAYTIME);
                        sb.AppendFormat(@"'{0}',", t.DISTRICT);
                        sb.AppendFormat(@"'{0}',", t.RESPONSIBLEUNIT);
                        sb.AppendFormat(@"'{0}',", t.ROOMNUM);
                        sb.AppendFormat(@"'{0}',", t.LIMITTIME);
                        sb.AppendFormat(@"'{0}',", t.FAULTDETAIL);
                        sb.AppendFormat(@"'{0}',", t.DELYHOURS);
                        sb.AppendFormat(@"'{0}',", t.CALLBACKOPERATION);
                        sb.AppendFormat(@"'{0}',", t.RETURNBACKREASON);
                        sb.AppendFormat(@"'{0}',", t.RESONSFORFAILURE);
                        sb.AppendFormat(@"'{0}',", t.WEATHERACHIVED);
                        sb.AppendFormat(@"'{0}',", t.WORKPERMIT);
                        sb.AppendFormat(@"'{0}',", t.ATTITUDESATISFY);
                        sb.AppendFormat(@"'{0}',", t.RESULTSATISFY);
                        sb.AppendFormat(@"'{0}',", t.OTHERPROBLEMS);
                        sb.AppendFormat(@"'{0}',", t.OTHERPROBLEMDEAL);
                        sb.AppendFormat(@"'{0}',", t.VIEWFAILUREMARK);
                        sb.AppendFormat(@"'{0}'),", t.UNCOVERREASON);
                        #endregion
                    }
                    sql = sb.ToString().Substring(0, sb.ToString().Length - 1);
                    LogHelper.Info("将插入临时表“FORM_TEMP”sql：" + sql + "\r\n");
                    Excuete(sql);
                    LogHelper.Info("导入临时表成功，即将导入正式表：“FORM_JSWH_WIDEBANDMAINT_WLAN”\r\n");
                    sql = string.Format(@"MERGE INTO {0} A USING(
                                          SELECT * FROM {1}
                                          )B ON A.TASKID=B.A1
                                          WHEN NOT MATCHED THEN
                                          INSERT VALUES({2});", tablename, tebletemp, para);
                    LogHelper.Info("将导入正式表“FORM_JSWH_WIDEBANDMAINT_WLAN”sql：" + sql + "\r\n");
                    Excuete(sql);
                    LogHelper.Info("导入正式表完成！");
                    break;
                case 3:
                    coms = JSS.Deserialize<THREE>(Newjson);
                    tablename = "FORM_JSWH_WIDEBANDMAINT_COMPLAINTSLINE";
                    LogHelper.Info("获取数据成功，表：FORM_JSWH_WIDEBANDMAINT_COMPLAINTSLINE。。。。\r\n");
                    para = string.Join(",", typeof(FORM_JSWH_WIDEBANDMAINT_COMPLAINTSLINE).GetProperties().AsEnumerable().Select((d, i) => "A" + (i + 1).ToString()));
                    sb.AppendFormat(@"INSERT INTO {0}({1}) VALUES", tebletemp, para);
                    foreach (FORM_JSWH_WIDEBANDMAINT_COMPLAINTSLINE t in coms.Data)
                    {
                        #region FORM_JSWH_WIDEBANDMAINT_COMPLAINTSLINE
                        sb.AppendFormat(@"('{0}',", t.TASKID);
                        sb.AppendFormat(@"'{0}',", t.SN);
                        sb.AppendFormat(@"'{0}',", t.APPLYTIME);
                        sb.AppendFormat(@"'{0}',", t.APPLYUSER);
                        sb.AppendFormat(@"'{0}',", t.LINENAME);
                        sb.AppendFormat(@"'{0}',", t.BILLSOURCE);
                        sb.AppendFormat(@"'{0}',", t.LINETYPE);
                        sb.AppendFormat(@"'{0}',", t.CUSTOMERTEL);
                        sb.AppendFormat(@"'{0}',", t.DISTRICT);
                        sb.AppendFormat(@"'{0}',", t.EOMSAPPLYTIME);
                        sb.AppendFormat(@"'{0}',", t.EOMSDELAYTIME);
                        sb.AppendFormat(@"'{0}',", t.COMMUNITYNAME);
                        sb.AppendFormat(@"'{0}',", t.RESPONSIBLEUNIT);
                        sb.AppendFormat(@"'{0}',", t.LIMITTIME);
                        sb.AppendFormat(@"'{0}',", t.ATTACHMENT);
                        sb.AppendFormat(@"'{0}',", t.FAULTDETAIL);
                        sb.AppendFormat(@"'{0}',", t.CALLBACKOPERATION);
                        sb.AppendFormat(@"'{0}',", t.WEATHERACHIVED);
                        sb.AppendFormat(@"'{0}',", t.ATTITUDESATISFY);
                        sb.AppendFormat(@"'{0}',", t.WORKPERMIT);
                        sb.AppendFormat(@"'{0}',", t.RESULTSATISFY);
                        sb.AppendFormat(@"'{0}',", t.OTHERPROBLEMDEAL);
                        sb.AppendFormat(@"'{0}'),", t.DELYHOURS);
                        #endregion
                    }
                    sql = sb.ToString().Substring(0, sb.ToString().Length - 1);
                    LogHelper.Info("将插入临时表“FORM_TEMP”sql：" + sql + "\r\n");
                    Excuete(sql);
                    LogHelper.Info("导入临时表成功，即将导入正式表：“FORM_JSWH_WIDEBANDMAINT_COMPLAINTSLINE”\r\n");
                    sql = string.Format(@"MERGE INTO {0} A USING(
                                          SELECT * FROM {1}
                                          )B ON A.TASKID=B.A1
                                          WHEN NOT MATCHED THEN
                                          INSERT VALUES({2});", tablename, tebletemp, para);
                    LogHelper.Info("将导入正式表“FORM_JSWH_WIDEBANDMAINT_COMPLAINTSLINE”sql：" + sql + "\r\n");
                    Excuete(sql);
                    LogHelper.Info("导入正式表完成！");
                    break;
            }
        }

        static void Excuete(string sql)
        {
            IDBHelp db = DBFactory.Create();
            try
            {
                db.ExecuteNonQuery(sql);//sql太长导致退出，用merge into 
                LogHelper.Info("插入数据成功！");
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        static String md5(string s)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();

            string ret = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            }

            return ret.PadLeft(32, '0');
        }
    }
}
