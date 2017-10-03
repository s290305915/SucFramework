using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailTest
{
    class Program
    {

        [STAThread]
        public static string wenan(string comic_name, string platform)
        {
            try
            {
                string wenanTxt = $@"{AppDomain.CurrentDomain.BaseDirectory}setting\wenan_{platform}.txt";
                var PushMessages = new PushMessage().ReadTxt(wenanTxt);
                return PushMessages[new Random().Next(0, PushMessages.Count)].Msg.Replace("{comic_name}", comic_name);
            }
            catch
            {
                return $"你订阅的漫画{comic_name}有更新了！";
            }
        }

        static void Main(string[] args)
        {

            for (var d = 0; d <= 10; d++)
            {
                var name = wenan("斗破苍穹", "mht");
                Console.WriteLine(name);
                Thread.Sleep(500);
            }
            Console.ReadKey();
            return;
            string code = encode("vlspkdumopkmgfhc");
            Console.WriteLine(code);
            string deco = decode(code);
            Console.WriteLine(deco);
            Console.ReadKey();
            return;
            var content = "this is a test mail,do not response!";
            //bool f = Mail.SendMessage("s290305915@163.com", "suchi12345", "suchi", "测试", "290305915@qq.com", content, "pop3.163.com");

            try
            {
                var emailAcount = "1463837989@qq.com";// ConfigurationManager.AppSettings["EmailAcount"];
                var emailPassword = "vlspkdumopkmgfhc";// ConfigurationManager.AppSettings["EmailPassword"];
                var reciver = "290305915@qq.com";
                MailMessage message = new MailMessage();
                //设置发件人,发件人需要与设置的邮件发送服务器的邮箱一致
                MailAddress fromAddr = new MailAddress("1463837989@qq.com");
                message.From = fromAddr;
                //设置收件人,可添加多个,添加方法与下面的一样
                message.To.Add(reciver);
                //设置抄送人
                //message.CC.Add("290305915@qq.com");
                //设置邮件标题
                message.Subject = "Test";
                //设置邮件内容
                message.Body = content;
                //设置邮件发送服务器,服务器根据你使用的邮箱而不同,可以到相应的 邮箱管理后台查看,下面是QQ的
                SmtpClient client = new SmtpClient("smtp.qq.com", 25);
                //设置发送人的邮箱账号和密码
                client.Credentials = new NetworkCredential(emailAcount, emailPassword);
                //启用ssl,也就是安全发送
                client.EnableSsl = true;
                //发送邮件
                client.Send(message);
            }
            catch (Exception x) { }
            Console.ReadKey();
        }

        public static string encode(string str)
        {
            Encoding encode = Encoding.UTF8;
            string encrypt = "";
            byte[] bytes = encode.GetBytes(str);
            try
            {
                encrypt = Convert.ToBase64String(bytes);
            }
            catch
            {
                encrypt = str;
            }
            return encrypt;
        }

        public static string decode(string str)
        {
            Encoding encode = Encoding.UTF8;
            string decode = "";
            byte[] bytes = Convert.FromBase64String(str);
            try
            {
                decode = encode.GetString(bytes);
            }
            catch
            {
                decode = str;
            }
            return decode;
        }


        public class PushMessage
        {
            public int ID { get; set; }
            public string Msg { get; set; }

            public List<PushMessage> ReadTxt(string path)
            {
                using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        if (string.IsNullOrEmpty(sr.ReadToEnd()))
                        {
                            using (var sw = new StreamWriter(fs))
                            {
                                var list = new List<PushMessage>()
                            {
                                new PushMessage{
                                    ID = 1,
                                    Msg = ""
                                }
                        }; ;
                                sw.WriteLine(JsonConvert.SerializeObject(list));
                            }
                        }
                    }
                }
                return JsonConvert.DeserializeObject<List<PushMessage>>(File.ReadAllText(path));
            }
        }
    }
}
