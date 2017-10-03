using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WebApplication1.Handler
{
    public class QueryManager
    {
        public DataSet Query(string sql)
        {
            return null;
        }

        public int ExcuteNonQuery(string sql)
        {
            return 1000;
        }

        internal int Execute(string sqls)
        {
            throw new NotImplementedException();
        }
    }
}
