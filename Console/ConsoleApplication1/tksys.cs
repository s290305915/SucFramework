using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class tksys : DataBase<tksys>
    {
        public int ID { get; set; }
        public tkuser user { get; set; }
        public string Title { get; set; }

        public string Poupose { get; set; }

        public string Content { get; set; }

        public string Explain { get; set; }

        public DateTime PublishDate { get; set; }

        public int ClickCount { get; set; }

        public int UpdateCount { get; set; }

        public int Type { get; set; }

        public string Link { get; set; }

        public string DivName { get; set; }

    }
}
