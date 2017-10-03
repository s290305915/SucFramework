using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Web.Script.Serialization;
using System.Text;

namespace LifeAccountServices
{
    /// <summary>
    /// LifeAccountWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://microsoft.com/webservices/")]//http://tempuri.org/
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class LifeAccountWebService : System.Web.Services.WebService
    {
        [WebMethod]
        public string GetTestTable()
        {
            string json = JsonHelper.DataTableToJSON(GetDataTable("select * from Test"));
            return json;
        }
        JavaScriptSerializer jscriptSeri = new JavaScriptSerializer();

        [WebMethod]
        public void Login()
        {
            string username = Context.Request["username"];
            string pwd = Context.Request["pwd"];
            string deviceId = Context.Request["deviceId"];
            Login loginResult = new Login();
            StringBuilder sb = new StringBuilder();
            DataTable dt = GetDataTable(@"SELECT L.ID,
L.NAME,
L.`PASSWORD`,
L.`GROUP`,
L.TURENAME,
L.PHONE,
L.IS_OUTER,
R.ID GID,
R.NAME GNAME FROM
`user`  L LEFT JOIN
`group` R ON L.GROUP=R.ID
WHERE
L.NAME =  '{0}' AND
L.`PASSWORD` =  '{1}'
", username, pwd);
            if(dt != null && dt.Rows.Count > 0)
            {
                //更新设备ID
                ExcuteSQL(string.Format(@"update `user` set device='{0}' where id={1}", deviceId, dt.Rows[0]["ID"].ToString()));
                loginResult.LoginState = true;
                loginResult.LoginRemark = "登陆成功!";
                loginResult.ID = dt.Rows[0]["ID"].ToString();
                loginResult.Name = dt.Rows[0]["NAME"].ToString();
                loginResult.Img = "";
                loginResult.GroupID = dt.Rows[0]["GID"].ToString();
                loginResult.GroupName = dt.Rows[0]["GNAME"].ToString();
                loginResult.TureName = dt.Rows[0]["TURENAME"].ToString();
                loginResult.Phone = dt.Rows[0]["PHONE"].ToString();
            }
            else
            {
                loginResult.LoginState = false;
                loginResult.LoginRemark = "登陆失败!用户名或密码不正确";
            }
            jscriptSeri.Serialize(loginResult, sb);
            Context.Response.Write(sb.ToString());
        }

        [WebMethod]
        public void Register()
        {
            string username = Context.Request["username"];
            //先判断重复
            string pwd = Context.Request["pwd"];
            string sturename = Context.Request["turename"];
            string phone = Context.Request["phone"];
            string deviceId = Context.Request["deviceId"];
            string id = "";
            string sql = "";
            DataTable dtexsit = GetDataTable("select id from `user` where name='{0}'", username);
            if(dtexsit.Rows.Count > 0)//用户名已存在
            {
                Context.Response.Write("[{\"STATE\":\"0\",\"ISHAVE\":\"1\",\"TITLE\":\"用户名已存在\",\"MESSAGE\":\"请重新输入用户名！\"}]");
                return;
            }
            sql = string.Format(@"insert into `user` values(0,'{0}','{1}','{2}','{3}',null,null)", username, pwd, sturename, phone);
            ExcuteSQL(sql);
            id = GetDataTable("select max(id) id from `user` where name='{0}' and `password`='{1}' and phone='{2}'", username, pwd, phone).Rows[0][0].ToString();
            Context.Response.Write("[{\"STATE\":\"1\",\"ISHAVE\":\"0\",\"TITLE\":\"注册成功\",\"MESSAGE\":\"请登录！\"}]");
        }

        /// <summary>
        /// 首页用户信息
        /// </summary>
        [WebMethod]
        public void GetUserInfo()
        {

            string userid = Context.Request["userid"]; //3
            string ym = DateTime.Now.ToString("yyyyMM");//本月
            DataTable dt = GetDataTable(@"select turename,phone,'' head,`group` from `user` where id={0}", userid);
            try
            {
                if(dt.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder("[{");
                    double result = 0.00;
                    sb.AppendFormat("\"TURENAME\":\"{0}\",", dt.Rows[0][0].ToString() + "-" + dt.Rows[0][1].ToString());
                    sb.AppendFormat("\"HEAD\":\"{0}\",", dt.Rows[0][2].ToString());
                    DataTable dtxf = GetDataTable(@"select * from user_account where `user_id`={0} and `year_month`={1}", userid, ym);
                    if(dtxf.Rows.Count > 0)
                    {
                        string gids = string.Join(",", dtxf.AsEnumerable().Select(x => x.Field<int>("list_id")).ToList());
                        DataTable dtaccs = GetDataTable(@"select account,manyman from account_list where `id` in({0})", gids);
                        foreach(DataRow dr in dtaccs.Rows)
                        {
                            result += Convert.ToDouble(dr[0].ToString()) / Convert.ToInt32(dr[1].ToString());
                        }
                    }
                    sb.AppendFormat("\"XIAOFEI\":\"{0}\"", Math.Round(result, 2).ToString());
                    sb.Append("}]");
                    Context.Response.Write(sb.ToString());
                }
                else
                {
                    Context.Response.Write("[{\"TURENAME\":\"未命名\",\"HEAD\":\"\",\"XIAOFEI\":\"0.00\"}]");
                }
            }
            catch(Exception ex)
            {
                Context.Response.Write(ex.StackTrace);
            }
        }

        #region 账单
        [WebMethod]
        public void GetMonthList()
        {
            try
            {
                string userid = Context.Request["userid"]; //4
                                                           //参与消费的月份
                StringBuilder sb = new StringBuilder("[");
                DataTable dtym = GetDataTable(@"select distinct `year_month` from user_account where user_id={0} order by `year_month` desc", userid);
                if(dtym.Rows.Count > 0)
                {
                    DataTable dtuser = GetDataTable(@"select * from `user` where id={0}", userid);
                    foreach(DataRow dr in dtym.Rows)
                    {
                        DataTable dt_allxf = GetDataTable(@"select distinct list_id from user_account where group_id={0} and `year_month`='{1}'", dtuser.Rows[0]["group"].ToString(), dr[0].ToString());
                        string lst_ids = string.Join(",", dt_allxf.AsEnumerable().Select(x => x.Field<int>("list_id")));
                        //    //总账
                        string amount = GetDataTable(@"select sum(account) amont from account_list where id in({0})", lst_ids).Rows[0][0].ToString();
                        if(Convert.ToDouble(amount) <= 0)
                        {
                            sb.Append("{\"ISDATA\":\"0\",\"YEAR\":\"0001\",\"MONTH\":\"01\",\"UID\":\"0\",\"GID\":\"0\",\"AMOUNT\":\"0\"}]");
                            Context.Response.Write(sb.ToString());
                            return;
                        }
                        sb.Append("{");
                        sb.AppendFormat("\"ISDATA\":\"1\",\"YEAR\":\"{0}\",\"MONTH\":\"{1}\",\"UID\":\"{2}\",\"GID\":\"{3}\",\"AMOUNT\":\"{4}\"",
                             dr[0].ToString().Substring(0, 4), dr[0].ToString().Substring(4, 2), userid, dtuser.Rows[0]["group"].ToString(), amount);
                        sb.Append("},");
                    }
                    sb.Length = sb.Length - 1;
                    sb.Append("]");
                }
                else
                {
                    sb.Append("{\"ISDATA\":\"0\",\"YEAR\":\"0001\",\"MONTH\":\"01\",\"UID\":\"0\",\"GID\":\"0\",\"AMOUNT\":\"0\"}]");
                }
                Context.Response.Write(sb.ToString());
            }
            catch(Exception ex)
            {
                Context.Response.Write(ex.Message + "," + ex.StackTrace);
            }
        }
        /// <summary>
        /// 账务结算详情
        /// </summary>
        [WebMethod]
        public void GetZDDetailList()
        {
            string userid = Context.Request["userid"]; //4
            string gid = Context.Request["gid"]; //4
            string ym = Context.Request["ym"]; //4

            //先根据月份和Gid查出当月总消费并除以gid的人数
            //然后再查出当月gid的每个人的总消费去减去上面查出的平均值
            //得出每个人的正负金额，即为出账和入账金额

            DataTable dtusers = GetDataTable(@"select * from `user` where `group`={0}", gid);
            double all_amount = Convert.ToDouble(GetDataTable(@"select sum(account) amount from account_list where group_id={0} and `year_month`='{1}'", gid, ym).Rows[0][0].ToString());
            double average = all_amount;// dtusers.Rows.Count;
            //查询单个人当月总消费金额
            StringBuilder sb = new StringBuilder("[");
            foreach(DataRow drxf in dtusers.Rows)
            {
                //我所给的钱
                string uname = drxf["turename"].ToString();
                int cur_userid = Convert.ToInt32(drxf["id"].ToString());
                if(userid == cur_userid.ToString())
                {
                    uname = "我";
                }
                string should = "";
                //应收的减去应给的
                double result = 0;
                double gei = 0;
                double shou = 0;
                //我参与的
                DataTable dt_all = GetDataTable(@"select distinct list_id from user_account where user_id={0} and `year_month`={1}", cur_userid, ym);
                if(dt_all.Rows.Count <= 0)
                {
                    //Context.Response.Write("re is 0," + gid + "," + ym);
                    result = 0;
                }
                else
                {
                    //我给的钱
                    DataTable dt_shou = GetDataTable(@"select (account/manyman)*(manyman-1) amount,account,manyman from account_list where actor={0} and id in({1})", cur_userid, string.Join(",", dt_all.AsEnumerable().Select(x => x.Field<int>("list_id"))));
                    //我该给的钱
                    DataTable dt_gei = GetDataTable(@"select account/manyman amount,account,manyman from account_list where actor<>{0} and id in({1})", cur_userid, string.Join(",", dt_all.AsEnumerable().Select(x => x.Field<int>("list_id"))));
                    if(dt_shou.Rows.Count > 0)
                    {
                        foreach(DataRow dr in dt_shou.Rows)
                            gei += Convert.ToDouble(dr[0].ToString());
                    }
                    else
                    {
                        gei = 0;
                    }
                    if(dt_gei.Rows.Count > 0)
                    {
                        foreach(DataRow dr in dt_gei.Rows)
                            shou += Convert.ToDouble(dr[0].ToString());
                    }
                    else
                    {
                        shou = 0;
                    }
                    result = gei - shou;
                    result = Math.Round(result, 2); //保留两位小数
                }
                if(result > 0)
                {
                    should = "收入";
                }
                else
                {
                    should = "给出";
                }
                sb.Append("{");
                sb.AppendFormat("\"YINGCHU\":\"{3}\",\"YINGSHOU\":\"{4}\",\"NAME\":\"{0}\",\"SHOULD\":\"{1}\",\"MUCH\":\"{2}\"", uname, should, result.ToString(), gei.ToString(), shou.ToString());
                sb.Append("},");
            }
            sb.Length = sb.Length - 1;
            sb.Append("]");
            Context.Response.Write(sb.ToString());
        }

        #endregion

        #region 时间轴

        /// <summary>
        /// 加载时间轴列表-按组按时间
        /// </summary>
        [WebMethod]
        public void TimeLine_List()
        {
            string userid = Context.Request["userid"]; //4
            string page = Context.Request["page"];
            string rows = Context.Request["rows"];
            int row = Convert.ToInt32(rows);
            int startindex = Convert.ToInt32(page) * row;   //0
            int endindex = (Convert.ToInt32(page) + 1) * row;   //10
            try
            {
                string groupid = GetDataTable(@"select `group` from `user` where id={0}", userid).Rows[0][0].ToString();
                if(string.IsNullOrEmpty(groupid))
                {
                    Context.Response.Write("[{\"OVER\":\"1\",\"MESSAGE\":\"您尚未加入群组，暂无数据可以查看！\",\"AOVER\":\"true\"}]");
                    return;
                }
                DataTable dtacclist = GetDataTable(@"select * from account_list where group_id={0} order by id desc limit {1},{2}", groupid, startindex, endindex);
                if(dtacclist.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder("[");
                    //[{"UNAME":"张玉芳","TITLE":"买的菜花了120元","CONTENT":"今天在屋头煮猪脑壳，买了好多菜。","ACTOR":"波帅，秋妹儿，宝坤，苏翅。","DATE":"2017-02-09","ACC_ID":"25"}]
                    //TITLE:将数据库中的title和account合并拼接起来
                    //ACTOR:先在user_account去找单条list_id的user_id再得出turename,然后拼接起来。
                    foreach(DataRow dracc in dtacclist.Rows)
                    {
                        string user_name = GetDataTable(@"select turename from `user` where id={0}", dracc["ACTOR"].ToString()).Rows[0][0].ToString();
                        string title = dracc["NAME"].ToString() + " 花费了" + dracc["ACCOUNT"].ToString() + "元";
                        string content = dracc["DETAILS"].ToString();
                        string list_id = dracc["ID"].ToString();
                        string date = Convert.ToDateTime(dracc["CREATETIME"].ToString()).ToString("yyyy-MM-dd");
                        DataTable dtuser_id = GetDataTable(@"select user_id from user_account where list_id={0}", list_id);
                        bool hasmyself = false;
                        foreach(DataRow dr in dtuser_id.Rows)
                            if(dr[0].ToString() == userid)
                                hasmyself = true;
                        if(hasmyself == false)  //剔除跟我没关系的消费
                            continue;
                        string ids = string.Join(",", dtuser_id.AsEnumerable().Select(x => x.Field<int>("user_id")));
                        DataTable dtuser = GetDataTable(@"select turename from `user` where id in({0})", ids);
                        string names = string.Join(",", dtuser.AsEnumerable().Select(x => x.Field<string>("turename")));
                        sb.AppendFormat("{{\"OVER\":\"0\",\"UNAME\":\"{0}\",\"TITLE\":\"{1}\",\"CONTENT\":\"{2}\",\"ACTOR\":\"{3}\",\"DATE\":\"{4}\",\"ACC_ID\":\"{5}\"}},",
                            user_name, title, content, names, date, list_id);
                    }
                    sb.Length = sb.Length - 1;
                    sb.Append("]");
                    Context.Response.Write(sb.ToString());
                }
                else
                {
                    Context.Response.Write("[{\"OVER\":\"1\",\"MESSAGE\":\"没有更多数据了！\",\"AOVER\":\"true\"}]");
                }
            }
            catch(Exception ex)
            {
                Context.Response.Write("[{\"OVER\":\"1\",\"MESSAGE\":\"" + ex.Message + "\",\"AOVER\":\"true\"}]");
            }
        }

        #endregion

        #region 记录
        /// <summary>
        /// 获取当前群组其他成员
        /// </summary>
        [WebMethod]
        public void GetCurrentGroupActorByCurrentUser_Jizhang()
        {
            string userid = Context.Request["userid"];
            try
            {
                string groupid = GetDataTable(@"select `group` from `user` where id={0}", userid).Rows[0][0].ToString();
                if(string.IsNullOrEmpty(groupid))
                {
                    Context.Response.Write("[{\"ID\":\"0\",\"NAME\":\"暂无其他成员\"}]");
                    return;
                }
                else
                {
                    DataTable dt = GetDataTable(@"select * from `user` where `group`={0} and id<>{1}", groupid, userid);
                    if(dt.Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder("[");
                        foreach(DataRow dr in dt.Rows)
                        {
                            sb.AppendFormat("{{\"ID\":\"{0}\",", dr[0]);
                            sb.AppendFormat("\"NAME\":\"{0}\"}},", dr["TURENAME"]);
                        }
                        sb.Length = sb.Length - 1;
                        sb.Append("]");
                        Context.Response.Write(sb.ToString());
                    }
                    else
                    {
                        Context.Response.Write("[{\"ID\":\"0\",\"NAME\":\"暂无其他成员！\"}]");
                        return;
                    }
                }
            }
            catch(Exception ex)
            {
                Context.Response.Write("[{\"ID\":\"0\",\"NAME\":\"暂无其他成员\",\"EX\":\"" + ex.Message + "\"}]");
            }
        }

        /// <summary>
        /// 保存单次消费记录
        /// </summary>
        [WebMethod]
        public void SaveBill_Jizhang()
        {
            try
            {
                string userid = Context.Request["userid"];
                string users = Context.Request["users"];
                string title = Context.Request["title"];
                string account = Context.Request["account"];
                string content = Context.Request["content"];
                content = content.Replace("\n", ",");
                content = content.Replace("'", "*");
                content = content.Replace("\\", "$");
                content = content.Replace("\\x", "$");
                title = title.Replace("\n", ",");
                title = title.Replace("'", "*");
                title = title.Replace("\\", "$");
                title = title.Replace("\\x", "$");
                DateTime dtim = DateTime.Now;
                List<string> ids = new List<string>();
                if(users.Length > 0)
                {
                    users = users.Substring(0, users.Length - 1);
                    ids = users.Split(',').ToList();
                }
                ids.Add("c" + userid);  //无论情况 把自己添加上
                string gid = GetDataTable("select `group` from `user` where id={0}", userid).Rows[0][0].ToString();
                string sql = string.Format(@"insert into account_list values(0,'{0}',{1},'{2}',{3},'{4}',{5},{6},'{7}')"
                    , title, account, content, userid, dtim.ToString("yyyyMM"), gid, ids.Count, dtim.ToString());    //先插入数据到account_list
                ExcuteSQL(sql);
                string maxid = GetDataTable("select id from account_list where actor={0} order by id desc", userid).Rows[0][0].ToString();
                sql = "insert into user_account values";

                ids.ForEach(x => {
                    sql += "(" + x.Remove(0, 1) + "," + maxid + ",'" + dtim.ToString("yyyyMM") + "'," + gid + "),";
                });
                //sql += "(" + userid + "," + maxid + "," + dtim.ToString("yyyyMM") + ")";
                sql = sql.Substring(0, sql.Length - 1);
                //Context.Response.Write(sql);
                //return;
                ExcuteSQL(sql);
                Context.Response.Write("[{\"SUCCESS\":\"1\",\"TITLE\":\"保存成功！\",\"MESSAGE\":\"请在时间轴中查看！\"}]");
            }
            catch(Exception ex)
            {
                Context.Response.Write("[{\"SUCCESS\":\"0\",\"TITLE\":\"保存失败！\",\"MESSAGE\":\"'" + ex.Message.Replace("\\", "") + "'！\"}]");
            }
        }


        #endregion

        #region 群组操作
        /// <summary>
        /// 获取当前群组其他成员
        /// </summary>
        [WebMethod]
        public void GetCurrentGroupActorByCurrentUser()
        {
            string userid = Context.Request["userid"];
            try
            {
                string groupid = GetDataTable(@"select `group` from `user` where id={0}", userid).Rows[0][0].ToString();
                if(string.IsNullOrEmpty(groupid))
                {
                    Context.Response.Write("[{\"ID\":\"0\",\"NAME\":\"您尚未加入群组！\"}]");
                    return;
                }
                else
                {
                    DataTable dt = GetDataTable(@"select * from `user` where `group`={0} and id<>{1}", groupid, userid);
                    if(dt.Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder("[");
                        foreach(DataRow dr in dt.Rows)
                        {
                            sb.AppendFormat("{{\"ID\":\"{0}\",", dr[0]);
                            sb.AppendFormat("\"NAME\":\"{0}\"}},", dr["TURENAME"]);
                        }
                        sb.Length = sb.Length - 1;
                        sb.Append("]");
                        Context.Response.Write(sb.ToString());
                    }
                    else
                    {
                        Context.Response.Write("[{\"ID\":\"0\",\"NAME\":\"您所在的群组仅您一个人！\"}]");
                        return;
                    }
                }
            }
            catch(Exception ex)
            {
                Context.Response.Write("[{\"ID\":\"0\",\"NAME\":\"您尚未加入群组！\",\"EX\":\"" + ex.Message + "\"}]");
            }
        }

        /// <summary>
        /// 根据用户获取群组信息
        /// </summary>
        [WebMethod]
        public void GetGroupByUser()
        {
            string userid = Context.Request["userid"];
            try
            {
                string groupid = GetDataTable(@"select `group` from `user` where id={0}", userid).Rows[0][0].ToString();
                string isouter = GetDataTable(@"select is_outer from `user` where id={0}", userid).Rows[0][0].ToString();
                if(string.IsNullOrEmpty(groupid))
                {
                    Context.Response.Write("[{\"CURRGID\":\"0\",\"CURRGNAME\":\"您尚未加入群组！\",\"IS_HAVEGROUP\":\"0\",\"IS_OUTER\":\"" + isouter + "\"}]");
                    return;
                }
                else
                {
                    DataTable dt = GetDataTable(@"select * from `group` where id={0}", groupid);
                    if(dt.Rows.Count > 0)
                    {
                        Context.Response.Write("[{\"CURRGID\":\"" + dt.Rows[0][0] + "\",\"CURRGNAME\":\"" + dt.Rows[0][1] + "\",\"IS_HAVEGROUP\":\"1\",\"IS_OUTER\":\"" + isouter + "\"}]");
                    }
                }
            }
            catch(Exception ex)
            {
                Context.Response.Write("[{\"CURRGNAME\":\"\",\"IS_HAVEGROUP\":\"0\",\"IS_OUTER\":\"1\",\"EX\":\"" + ex.Message + "\"}]");
            }
        }

        [WebMethod]
        public void AddNewActor()
        {
            string userid = Context.Request["userid"]; //我的id
            string userid_other = Context.Request["userid_other"];  //名字或电话
            DataTable dt = GetDataTable(@"select `group`,id from `user` where turename='{0}' or phone='{0}'", userid_other);
            if(dt.Rows.Count > 0)
            {
                string gid_oth = dt.Rows[0][0].ToString();
                string gid = GetDataTable(@"select `group` from `user` where id in({0})", userid).Rows[0][0].ToString();
                if(!string.IsNullOrEmpty(gid_oth))  //对方有群组
                {
                    if(gid == gid_oth)
                    {
                        Context.Response.Write("[{\"TITLE\":\"无需邀请！\",\"MESSAGE\":\"对方已在您所在的群组！\"}]");
                        return;
                    }
                    Context.Response.Write("[{\"TITLE\":\"对方已有群组！\",\"MESSAGE\":\"无法加入其他群组！\"}]");
                }
                else
                {
                    //判断是否有组，判断是否为其他组，本组
                    string sql = string.Format(@"update `user` set `group`={0} where id='{1}'", gid, dt.Rows[0]["id"].ToString());
                    ExcuteSQL(sql);
                    Context.Response.Write("[{\"TITLE\":\"邀请成功！\",\"MESSAGE\":\"邀请用户“" + userid_other + "”加入成功！\"}]");
                }
            }
            else
            {
                Context.Response.Write("[{\"TITLE\":\"邀请失败！\",\"MESSAGE\":\"请检查名字或电话是否正确！\"}]");

            }
        }

        /// <summary>
        /// 退出当前群组
        /// </summary>
        [WebMethod]
        public void OutCurrGroup()
        {
            string userid = Context.Request["userid"];
            string sql = string.Format(@"update `user` set `group`=null where id in({0})", userid);
            ExcuteSQL(sql);
            Context.Response.Write("[{\"TITLE\":\"退出成功！\",\"MESSAGE\":\"请做好账务交涉！\"}]");
        }

        /// <summary>
        /// 加入群组
        /// </summary>
        [WebMethod]
        public void IntoGroup()
        {
            StringBuilder sb = new StringBuilder("[");
            string userid = Context.Request["userid"];
            string groupname = Context.Request["groupname"];
            DataTable dtr = GetDataTable(@"select id from `group` where name in('{0}')", groupname);
            if(dtr.Rows.Count > 0)
            {
                string gid = dtr.Rows[0][0].ToString();
                string sql = string.Format(@"update `user` set `group`={0} where id='{1}'", gid, userid);
                ExcuteSQL(sql);
                sb.Append("{\"TITLE\":\"加入成功！\",\"MESSAGE\":\"" + groupname + "\"}]");
                Context.Response.Write(sb.ToString());
            }
            else
            {
                sb.Append("{\"TITLE\":\"群组名不存在\",\"MESSAGE\":\"请检查！\"}]");
                Context.Response.Write(sb.ToString());
            }
        }

        /// <summary>
        /// 创建群组
        /// </summary>
        [WebMethod]
        public void CreatGroup()
        {
            StringBuilder sb = new StringBuilder("[");
            string userid = Context.Request["userid"];
            string groupname = Context.Request["groupname"];
            if(string.IsNullOrEmpty(groupname))
            {
                sb.Append("{\"TITLE\":\"创建失败\",\"MESSAGE\":\"群组名不能为空！\"}]");
                Context.Response.Write(sb.ToString());
                return;
            }
            try
            {
                DataTable dtr = GetDataTable(@"select * from `group` where name in('{0}')", groupname);
                if(dtr.Rows.Count > 0)
                {
                    sb.Append("{\"TITLE\":\"已存在的群组名\",\"MESSAGE\":\"请重新输入！\"}]");
                    Context.Response.Write(sb.ToString());
                    return;
                }
                string sql = string.Format(@"insert into `group` values(0,'{0}',1,0,'{1}',{2})", groupname, DateTime.Now.ToString().Replace("/", "-"), userid);
                //Context.Response.Write(sql);
                //return;
                ExcuteSQL(sql);
                sql = string.Format(@"select id from `group` where name in('{0}')", groupname);
                string gid = GetDataTable(sql).Rows[0][0].ToString();
                sql = string.Format(@"update `user` set `group`={0} where id in({1})", gid, userid);
                ExcuteSQL(sql);
                sb.Append("{\"TITLE\":\"创建成功！\",\"MESSAGE\":\"" + groupname + "\"}]");
                Context.Response.Write(sb.ToString());
            }
            catch(Exception ex)
            {
                sb.Append("{\"TITLE\":\"发生错误！\",\"MESSAGE\":\"" + ex.Message + "\"}]");
                Context.Response.Write(sb.ToString());
            }

        }
        #endregion

        #region 获取数据
        private DataTable GetDataTable(string sql, params object[] args)
        {
            return GetDataTable(string.Format(sql, args));
        }
        private DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            string sqls = sql;
            dt = MySqlHelper.GetDataSet(MySqlHelper.Conn, CommandType.Text, sqls, null).Tables[0];
            return dt;
        }

        private void ExcuteSQL(string sql, params object[] args)
        {
            ExcuteSQL(sql);
        }
        private void ExcuteSQL(string sql)
        {
            MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, sql, null);
        }


        #endregion
    }

    public class Login
    {
        public string ID
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public string GroupID
        {
            get; set;
        }
        public string Img
        {
            get; set;
        }
        public string GroupName
        {
            get; set;
        }
        public bool LoginState
        {
            get;
            internal set;
        }
        public string LoginRemark
        {
            get;
            internal set;
        }
        public string TureName
        {
            get;
            internal set;
        }
        public string Phone
        {
            get;
            internal set;
        }
    }
}
