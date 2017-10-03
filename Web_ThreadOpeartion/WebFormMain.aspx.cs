using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Collections;

namespace Web_ThreadOpeartion
{
    public partial class WebFormMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ym = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //对应时间 2017-04-05 14:43:01
             ym = DateTime.Now.ToString("yyyy-MM-dd HH:mm"); //对应时间 2017-04-05 14:43   不要秒
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            ArrayList procList = new ArrayList();
            string tempName = "";
            int begpos;
            int endpos;
            //获取每个进程
            foreach(Process thisProc in System.Diagnostics.Process.GetProcesses())
            {
                tempName = thisProc.ToString();
                begpos = tempName.IndexOf("(") + 1;
                endpos = tempName.IndexOf(")");
                tempName = tempName.Substring(begpos, endpos - begpos);
                procList.Add(tempName);
            }
            procname.DataSource = procList;
            procname.DataBind();
        }

        protected void procname_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ptname = procname.SelectedItem.Text;
            Process[] p = Process.GetProcessesByName(ptname);
            Process pro = p[0];
            foreach(ProcessThread thread in pro.Threads)
            {
                listthread.Items.Add(String.Format("ID:{0} ThreadState{1} WaitReason:{2}", thread.Id, thread.ThreadState, thread.WaitReason.ToString()));
            }
        }
    }
}