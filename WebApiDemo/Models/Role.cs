using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiTest.Models
{
    public class Role
    {
        public int id
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public string Remark
        {
            get; set;
        }
        public int Type
        {
            get; set;
        }
        public int Level
        {
            get; set;
        }
    }
}