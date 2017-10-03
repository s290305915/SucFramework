using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{

    public class gruop
    {
        public string appgroupname { get; set; }
        public int appgroupid { get; set; }
        public int appgrouptype { get; set; }
        public string applist { get; set; }
    }
    public class MySpace : gruop
    {
    }

    public class publicspace : gruop
    {
        public int appgroupnum { get; set; }
    }

    public class spaces
    {
        public List<MySpace> ms { get; set; }
        public List<publicspace> ps { get; set; }
    }

    public class test
    {
        public void test1()
        {
            List<publicspace> aa = new List<publicspace>(){ new publicspace()
            {
                appgroupnum = 1,
                appgroupid = 1,
                appgroupname = "",
                appgrouptype = 1,
                applist = ""
            }};
            List<MySpace> bb = new List<MySpace>() { new MySpace() 
            { 
                appgroupid = 1,
                appgroupname = "",
                appgrouptype = 1, 
                applist = "" 
            }};
            string Kid=string.Empty;

            string sql = @"SELECT ROWNUM ID,
                                    bipml.kid,
                                    bipml.klid,
                                    bipml.digit_from,
                                    bipml.digit_to,
                                    bipml.check_type,
                                    bipml.value,
                                    bipml.refer_digit_from,
                                    bipml.digit_to,
                                    bipml.enable_flag
                                FROM biz_item_packge_model_line bipml
                                WHERE bipml.kid='"+Kid+"'";
        }
    }
}
