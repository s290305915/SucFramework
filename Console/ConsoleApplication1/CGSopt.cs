using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConsoleApplication1
{

    public class CGSopt
    {
        public void ReadCGS()
        {
            ASD a = new ASD();
            Type t = aa<ASD>();
            PropertyInfo[] ProList;
            ProList = t.GetProperties();
            return;
            //===========
            CGSServiceReference.CGS_ServerSoapClient cgs = new CGSServiceReference.CGS_ServerSoapClient();
            string re = cgs.ExpressQuery("xxx", "123");
            re = cgs.TrackView("123", "123", "a");
        }

        private Type aa<T>() where T : class
        {
            return typeof(T);
        }

        private int TryTest()
        {
            string Msg = "";//消息
            SqlConnection conn = new SqlConnection("");
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from xxx", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int a = 0;
                int b = 3;
                Msg = (b / a).ToString();
                return 1;
            }
            catch(Exception ex)
            {
                Msg = ex.Message;
                return 2;
            }
            finally
            {
                if(conn.State == ConnectionState.Open)
                    conn.Close();
                Console.WriteLine(Msg);
                Console.ReadKey();
            }
        }
    }

    public class ASD
    {
        public string asd
        {
            get; set;
        }
    }
}
