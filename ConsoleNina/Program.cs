using System;
using System.Diagnostics;
using SucLib.Data.Factory;
using SucLib.Data.IDal;
using SucLib.Model;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;
using System.Threading;

namespace ConsoleNina
{
    class Program
    {
        private static readonly string wenanTxt = $@"{AppDomain.CurrentDomain.BaseDirectory}setting\wenan.txt";
        static IDBHelp db = DBFactory.Create(DataBaseType.SqlServer, "Data Source=.;Initial Catalog=SUCMSF1;User ID=sa;Pwd=suchi12345");
        [STAThread]
        static void Main(string[] args)
        {
            //DataBase<Gift> dbs = new DataBase<Gift>();
            //List<Gift> lst = new DataBase<Gift>().GetList();
            DBcreate.Lnumber2();

            return;


            DBcreate.Coin();
            Console.ReadKey();
            return;


            var PushMessages = new PushMessage().ReadTxt(wenanTxt);
            //[{"ID":"1","Msg":"老师！作者终于交作业啦！《{comic_name}》已更新，速来批阅！"}]
            for (int i = 0; i <= 10; i++)
            {
                Console.WriteLine(PushMessages[new Random().Next(0, PushMessages.Count)].Msg.Replace("{comic_name}", "笑哈哈"));
                Thread.Sleep(100);
            }
            Console.ReadKey();
            return;
            //DBcreate.Effect();
            //
            //
            //return;
            //var soruce = 198427;
            //var dba = soruce / 100000;
            //var tb = soruce % 100;
            //Console.WriteLine(dba+"," +tb);
            //Console.ReadKey();

            //string a= "1012491820erciyuan71CE30F7-3D12-4E42-B660-FCFF9C803C66"; //加密前数据
            string a = "qw189(*!awikw"; //加密前数据
            string b; //加密后数据
            b = RSAencrypt.encode(a);//System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(a, "MD5");
            Console.WriteLine(b);
            //b = RSAencrypt.decode(b);
            string frommail = "service1@yyhao.com";
            string password = b;
            string[] mailto = new string[] { "290305915@qq.com" };
            string title = "这是一封测试邮件";
            string content = "这是一封测试邮件";
            bool f = SendMessage(frommail, RSAencrypt.decode(password), mailto, title, content, "yyhao.com", 2526);
            Console.WriteLine(b);
            Console.ReadKey();




            return;
            //存了10000
            //当天 3.8的收益
            //存了30天
            //var jieguo = ((10000 * 0.038) * 30)/ 360;
            //Console.WriteLine("收益：((10000 * 0.038) * 30) / 360=" + jieguo);
            //Console.WriteLine("本息合计：" + (10000+jieguo));
            //Console.ReadKey();

            //return;


            //string sql = DBcreate.AddClumnWithTableName("user_log_no_", "user_log", "Ceffect", "int", "((0))");
            //File.WriteAllText("d:\\sql_addusr.txt", sql);
            //DBcreate.Label();

            return;

            string allpath = @"e:\temp\20170710\ecc5192-ec61-e711-86f3-1c1b0d98bce7\1\1.jpg";
            string xx = allpath.Replace("0", "xxx");
            Console.WriteLine(xx);
            Console.ReadKey();
            string path = Regex.Match(allpath, @".*\d{5,}\\").Value;
            string filename = string.Join(">", allpath.Substring(path.Length).Split('\\'));
            Console.WriteLine(path + filename);
            Console.ReadKey();

            Interview();
            return;

            string Name = "Nina";
            string Age = "13";
            string Result = $"{Name.ToUpper()} is {Age} Now";
            Trace.WriteLine(Result);
            Console.WriteLine(Result);
            //Console.ReadKey();

            DataTable dt = db.GetDataTable("select * from SUC_MODULE");
            string s = string.Join(",", dt.AsEnumerable().Select(x => x.Field<string>("NAME")));
            Trace.WriteLine(s);
            Console.WriteLine(s);
            //Console.ReadKey();

            List<SUC_USER> u = new SUC_USER().FindByCondition(new SUC_USER() { LOGIN_NAME = "admin" });
            u.ForEach(x =>
            {
                Console.WriteLine(x.NAME + "，角色：" + x.ROLE.NAME + "在" + x.LOGIN.LOCKED_DATE + "登陆了系统");
            });
        }


        private static bool SendMessage(string fromaccount, string frompassword, string[] mailto, string title, string content, string smtp = "smtp.qq.com", int port = 25)
        {
            try
            {
                var emailAcount = fromaccount;
                var emailPassword = frompassword;
                MailMessage message = new MailMessage();
                message.Sender = new MailAddress("djaflkdjalfjlajl@yyhao.com");
                message.ReplyTo = new MailAddress("djaflkdjalfjlajl@yyhao.com");
                //设置发件人,发件人需要与设置的邮件发送服务器的邮箱一致
                MailAddress fromAddr = new MailAddress(fromaccount);
                message.From = fromAddr;
                //设置收件人,可添加多个,添加方法与下面的一样
                foreach (string s in mailto)
                    message.To.Add(s);
                //设置抄送人
                //message.CC.Add("290305915@qq.com");
                //设置邮件标题
                message.Subject = title;
                //设置邮件内容
                message.Body = content;
                //设置邮件发送服务器,服务器根据你使用的邮箱而不同,可以到相应的 邮箱管理后台查看
                SmtpClient client = new SmtpClient(smtp, port);
                //设置发送人的邮箱账号和密码
                client.Credentials = new NetworkCredential(emailAcount, emailPassword);
                //启用ssl,也就是安全发送
                client.EnableSsl = false;
                //发送邮件
                client.Send(message);
                return true;
            }
            catch (Exception x)
            {
                return false;
            }
        }



        static public void Interview()
        {
            List<int> numberlist = new List<int>();
            int i = 0;
            Random rd = new Random();
            while (true)
            {
                if (i > 100)
                    break;
                numberlist.Add(rd.Next(0, 100));
                i++;
            }
            Console.WriteLine("随机数组(100个)：" + string.Join(",", numberlist));
            Console.WriteLine("=========================================================");
            //排序
            numberlist = order(numberlist);
            Console.WriteLine("排序后：" + string.Join(",", numberlist));
            Console.WriteLine("=========================================================");
            //去重复
            numberlist = filter(numberlist);
            Console.WriteLine("去重后：" + string.Join(",", numberlist));
            Console.WriteLine("=========================================================");
            Console.ReadKey();
        }

        static public List<int> order(List<int> ist)
        {
            int[] list2 = ist.ToArray();
            int temp;
            for (int i = 0; i < list2.Length; i++)
            {
                for (int j = i + 1; j < list2.Length; j++)
                {
                    if (list2[j] < list2[i])
                    {
                        temp = list2[j];
                        list2[j] = list2[i];
                        list2[i] = temp;
                    }
                }
            }
            return new List<int>(list2);
        }

        static public List<int> filter(List<int> ist)
        {
            List<int> newlist = new List<int>();
            foreach (int i in ist)
            {
                if (newlist.Contains(i))
                    continue;
                else
                    newlist.Add(i);
            }
            return newlist;
        }

    }

    public class RSAencrypt
    {
        public string publicKey = "Modulus";
        public string privateKey = "RSAKeyValue";
        public RSAencrypt() { }
        public RSAencrypt(string _privateKey) => privateKey = _privateKey;

        public string Decrypt(string base64code)
        {
            try
            {

                //Create a UnicodeEncoder to convert between byte array and string.
                UnicodeEncoding ByteConverter = new UnicodeEncoding();

                //Create a new instance of RSACryptoServiceProvider to generate
                //public and private key data.
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSA.FromXmlString(privateKey);

                byte[] encryptedData;
                byte[] decryptedData;
                encryptedData = Convert.FromBase64String(base64code);

                //Pass the data to DECRYPT, the private key information 
                //(using RSACryptoServiceProvider.ExportParameters(true),
                //and a boolean flag specifying no OAEP padding.
                decryptedData = RSADecrypt(encryptedData, RSA.ExportParameters(true), false);

                //Display the decrypted plaintext to the console. 
                return ByteConverter.GetString(decryptedData);
            }
            catch (Exception exc)
            {
                //Exceptions.LogException(exc);
                Console.WriteLine(exc.Message);
                return "";
            }
        }

        public string Encrypt(string toEncryptString)
        {
            try
            {
                //Create a UnicodeEncoder to convert between byte array and string.
                UnicodeEncoding ByteConverter = new UnicodeEncoding();

                //Create byte arrays to hold original, encrypted, and decrypted data.
                byte[] dataToEncrypt = ByteConverter.GetBytes(toEncryptString);
                byte[] encryptedData;
                byte[] decryptedData;

                //Create a new instance of RSACryptoServiceProvider to generate
                //public and private key data.
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

                RSA.FromXmlString(privateKey);

                //Pass the data to ENCRYPT, the public key information 
                //(using RSACryptoServiceProvider.ExportParameters(false),
                //and a boolean flag specifying no OAEP padding.
                encryptedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);

                string base64code = Convert.ToBase64String(encryptedData);
                return base64code;
            }
            catch (Exception exc)
            {
                //Catch this exception in case the encryption did
                //not succeed.
                //Exceptions.LogException(exc);
                Console.WriteLine(exc.Message);
                return "";
            }



        }

        public static string encode(string str)
        {
            Encoding encode = Encoding.UTF8;
            string decode = "";
            byte[] bytes = encode.GetBytes(str);
            try
            {
                decode = Convert.ToBase64String(bytes);
            }
            catch
            {
                decode = str;
            }
            return decode;
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

        private byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                //Create a new instance of RSACryptoServiceProvider.
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

                //Import the RSA Key information. This only needs
                //toinclude the public key information.
                RSA.ImportParameters(RSAKeyInfo);

                //Encrypt the passed byte array and specify OAEP padding.  
                //OAEP padding is only available on Microsoft Windows XP or
                //later.  
                return RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                //Exceptions.LogException(e);
                Console.WriteLine(e.Message);

                return null;
            }

        }

        private byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                //Create a new instance of RSACryptoServiceProvider.
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

                //Import the RSA Key information. This needs
                //to include the private key information.
                RSA.ImportParameters(RSAKeyInfo);

                //Decrypt the passed byte array and specify OAEP padding.  
                //OAEP padding is only available on Microsoft Windows XP or
                //later.  
                return RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                //Exceptions.LogException(e);
                Console.WriteLine(e.Message);

                return null;
            }

        }

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


