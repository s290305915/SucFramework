using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Testdate
    {
        /// <summary>
        /// 日期
        /// </summary>
        public void aa()
        {
            DateTime date = DateTime.Now;
            int year = date.Year;
            int month = date.Month;
            int nextMonth = month + 1;
            IList<ListItem> list = new List<ListItem>();
            for (int i = nextMonth; i > (nextMonth - 24); i--)  //倒推两年 
            {
                if (i == 13)
                {
                    ListItem item = new ListItem();
                    item.Text = (year + 1).ToString() + "年01月";
                    item.Value = (year + 1).ToString() + "01";
                    list.Add(item);
                }
                else if (i <= 0)
                {
                    if (i <= -12)
                    {
                        ListItem item = new ListItem();
                        item.Text = (year - 2).ToString() + "年" + (((24 + i).ToString()).Length == 1 ? "0" + (24 + i).ToString() : (24 + i).ToString()) + "月";
                        item.Value = (year - 2).ToString() + (((24 + i).ToString()).Length == 1 ? "0" + (24 + i).ToString() : (24 + i).ToString());
                        list.Add(item);
                    }
                    else
                    {
                        ListItem item = new ListItem();
                        item.Text = (year - 1).ToString() + "年" + (((12 + i).ToString()).Length == 1 ? "0" + (12 + i).ToString() : (12 + i).ToString()) + "月";
                        item.Value = (year - 1).ToString() + (((12 + i).ToString()).Length == 1 ? "0" + (12 + i).ToString() : (12 + i).ToString());
                        list.Add(item);
                    }
                }
                else
                {
                    ListItem item = new ListItem();
                    item.Text = year.ToString() + "年" + (i.ToString().Length == 1 ? "0" + i.ToString() : i.ToString()) + "月";
                    item.Value = year.ToString() + (i.ToString().Length == 1 ? "0" + i.ToString() : i.ToString());
                    list.Add(item);
                }
            }
            foreach (ListItem lt in list)
            {
                Console.WriteLine(lt.Value.ToString() + "," + lt.Text.ToString());
            }
            Console.WriteLine("pause");
        }

        public void bb()
        {
            List<Ent> oldList = new List<Ent>();
            List<Ent> newList = new List<Ent>();
            oldList.Add(new Ent() { Name = "a", Age = 1 });
            oldList.Add(new Ent() { Name = "b", Age = 2 });
            oldList.Add(new Ent() { Name = "c", Age = 3 });


            newList.Add(new Ent() { Name = "a", Age = 1 });
            newList.Add(new Ent() { Name = "b", Age = 2 });
            newList.Add(new Ent() { Name = "c", Age = 3 });


            bool sequenceEqual = oldList.SequenceEqual(newList);
            bool f = oldList.All(newList.Contains) && oldList.Count == newList.Count;   //需要比较器 通过将两个list<ent>序列化后再比较
        }
    }

    public class Ent
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
