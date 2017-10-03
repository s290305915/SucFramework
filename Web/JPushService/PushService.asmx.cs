using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using cn.jpush.api;
using cn.jpush.api.push.mode;
using cn.jpush.api.push.notification;

namespace JPushService
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class PushService : System.Web.Services.WebService
    {
        public String REGISTRATION_ID = "";//0900e8d85ef
        public String app_key = "00eb7ed5824fc5a18e0bb737";
        public String master_secret = "1bbc73dcdd74199553d6b2fb";

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        /// <summary>
        /// 极光推送 改变上面app_key和master_secret就OK
        /// </summary>
        [WebMethod]
        public void PushNews(string t, string i)
        {
            JPushClient client = new JPushClient(AppKey, MasterSecret);//app_key, master_secret);
            PushPayload payload = PushObject_All_All_Alert();
            payload.platform = Platform.all();
            payload.audience = Audience.all();
            payload.options.apns_production = true;
            try
            {
                string title = t;// Context.Request["t"];
                string ID = i;// Context.Request["id"];
                var notification = new Notification();
                notification.IosNotification = new IosNotification().setAlert(title).setBadge(1).setSound("default").AddExtra("ID", ID);
                notification.AndroidNotification = new AndroidNotification().setAlert(title).AddExtra("ID", ID);
                payload.notification = notification;

                var result = client.SendPush(payload);
                Context.Response.Write("[{\"STATE\":\"SUCCESS\",\"MESSAGE\":\"" + result + "\"}]");
            }
            catch (Exception ex)
            {
                Context.Response.Write("[{\"STATE\":\"ERROR\",\"MESSAGE\":\"" + ex.Message + "\"}]");
            }
        }

        [WebMethod]
        public void PushOther()
        {
            JPushClient client = new JPushClient(app_key, master_secret);
            PushPayload payload = PushObject_All_All_Alert();
            payload.platform = Platform.all();
            payload.audience = Audience.all();
            payload.options.apns_production = true;
            try
            {
                string title = Context.Request["t"];    //标题
                string model = Context.Request["m"];    //模块名
                string content = Context.Request["c"];   //内容
                var notification = new Notification();
                notification.IosNotification = new IosNotification().setAlert(title).setBadge(1).setSound("default").AddExtra("c", content).AddExtra("m", model);
                notification.AndroidNotification = new AndroidNotification().setAlert(title).AddExtra("c", content).AddExtra("m", model);
                payload.notification = notification;

                var result = client.SendPush(payload);
                Context.Response.Write("[{\"STATE\":\"SUCCESS\",\"MESSAGE\":\"" + result + "\"}]");
            }
            catch (Exception ex)
            {
                Context.Response.Write("[{\"STATE\":\"ERROR\",\"MESSAGE\":\"" + ex.Message + "\"}]");
            }
        }

        public PushPayload PushObject_All_All_Alert()
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();
            pushPayload.audience = Audience.all();
            //pushPayload.notification = new Notification().setAlert(ALERT);
            return pushPayload;
        }

        /// 极光推送
        /// </summary>
        /// <param name="employeeNumbers">员工工号 all为所有</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="extras">扩展参数 用于接收方处理</param>
        public const string AppKey = "1cc43c14c0a18d0f36139427";
        public const string MasterSecret = "8494d3f5c69f8f97a4dc53fd";
        [WebMethod]
        public void SendMessageListT(string ems, string id, string title, string content)
        {
            try
            {
                string[] employeeNumbers = ems.Split(',');
                var client = new JPushClient(AppKey, MasterSecret);
                PushPayload payload = new PushPayload();// PushObject_All_All_Alert();
                payload.platform = Platform.all();
                payload.audience = Audience.all();
                payload.options.apns_production = true;
                var notify = new Notification
                {
                    //AndroidNotification = new AndroidNotification().setAlert(title).AddExtra("ID", id),
                    //IosNotification = new IosNotification().setAlert(title).AddExtra("ID", id)
                    IosNotification = new IosNotification().setAlert(title).setBadge(1).setSound("default").AddExtra("ID", id).AddExtra("Content", content),
                    AndroidNotification = new AndroidNotification().setAlert(title).AddExtra("ID", id).AddExtra("Content", content)
                };
                payload.notification = notify;
                payload.audience = Audience.s_alias(employeeNumbers);
                //var msg = cn.jpush.api.push.mode.Message.content(content).setTitle(title);
               // var option = new Options
               // {
               //     //sendno = DateTime.Now.Hour + DateTime.Now.Millisecond,
               //     time_to_live = 2 * 24 * 60 * 60,//存活2天  秒
               //     apns_production = true
               // };

                //var payload = new PushPayload
                //{
                //    audience = 
                //    message = msg,
                //    options = option,
                //    notification = notify,
                //    platform = Platform.all(),
                //};

                var result = client.SendPush(payload);
                Context.Response.Write("[{\"STATE\":\"SUCCESS\",\"MESSAGE\":\"" + result + "\"}]");
            }
            catch (Exception ex)
            {
                Context.Response.Write("[{\"STATE\":\"ERROR\",\"MESSAGE\":\"" + ex.Message + "\"}]");
            }
        }

    }
}