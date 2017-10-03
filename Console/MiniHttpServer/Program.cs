using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace MiniHttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            if(!HttpListener.IsSupported)
            {
                throw new System.InvalidOperationException(
                    "使用 HttpListener 必须为 Windows XP SP2 或 Server 2003 以上系统！");
            }
            // 注意前缀必须以 / 正斜杠结尾
            string[] prefixes = new string[] { "9999" };
            string[] FIles = new string[] { "D:\\web\\" };
            string[] DefaultFile = new string[] { "D:\\web\\Default.aspx" };
            Dictionary<int, string> port2def = new Dictionary<int, string>();
            // 创建监听器.
            HttpListener listener = new HttpListener();
            // 增加监听的前缀.
            int i = 0;
            foreach(string s in prefixes)
            {
                listener.Prefixes.Add($"http://+:{s}/");
                port2def.Add(i, s);
                i++;
            }

            try
            {
                listener.Start();
                Console.WriteLine("监听中>>>>>>,监听端口：" + string.Join(",", prefixes));

            }
            catch(Exception ex)
            {
                Console.WriteLine("  发生了错误：");
                Console.WriteLine("         错误信息：{0}", ex.Message + "--->");
                Console.WriteLine("         信息：{0}", ex.StackTrace);
                if(ex.Message.Contains("拒绝访问"))
                {
                    Console.WriteLine("         可能是由于端口访问控制问题，请尝试使用以下命令：");
                    Console.WriteLine("                 netsh http add urlacl url=http://+:端口号/ user=everyone");
                }
                Console.ReadKey();
            }
            while(true)
            {
                // 注意: GetContext 方法将阻塞线程，直到请求到达
                HttpListenerContext context = listener.GetContext();
                // 取得请求对象
                HttpListenerRequest request = context.Request;

                var port = request.Url.Port;
                var path = request.Url.LocalPath;
                if(path.StartsWith("/") || path.StartsWith("\\"))
                    path = path.Substring(1);
                var sb = new StringBuilder("输入请求:");
                sb.AppendLine(path);
                var visit = path.Split(new char[] { '/', '\\' }, 2);
                if(visit.Length > 0)
                {
                    var cmd = visit[0].ToLower();
                    sb.AppendLine($"执行命令:{cmd}");
                    sb.AppendLine($"另外有{visit.Length - 1 + request.QueryString.Count}个参数");
                }
                sb.AppendLine(DateTime.Now.ToString());

                Console.WriteLine("Request Start --------------------------------------------------->>>>>");
                Console.WriteLine("     >>{0} {1} HTTP/1.1", request.HttpMethod, request.RawUrl);
                Console.WriteLine("     >>Accept: {0}", string.Join(",", request.AcceptTypes));
                Console.WriteLine("     >>Accept-Language: {0}", string.Join(",", request.UserLanguages));
                Console.WriteLine("     >>User-Agent: {0}", request.UserAgent);
                Console.WriteLine("     >>Accept-Encoding: {0}", request.Headers["Accept-Encoding"]);
                Console.WriteLine("     >>Connection: {0}", request.KeepAlive ? "Keep-Alive" : "close");
                Console.WriteLine("     >>Host: {0}", request.UserHostName);
                Console.WriteLine("     >>Pragma: {0}", request.Headers["Pragma"]);
                Console.WriteLine("          >>Response Conent:{0}", sb.ToString());
                Console.WriteLine("<<<<<--------------------------------------------------- Request End");
                // 取得回应对象
                HttpListenerResponse response = context.Response;
                response.AddHeader("Server", "My Server V0.0.1");
                // 构造回应内容

                int idx = port2def.First(x => x.Value == port.ToString()).Key;
                string reppath = "";
                if(!string.IsNullOrEmpty(path))
                {
                    reppath = FIles[idx] + path;
                }
                else
                {
                    reppath = DefaultFile[idx];
                }
                string fileContent = string.Empty;
                try
                {
                    using(var reader = new StreamReader(reppath))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
                catch { }

                string responseString = fileContent;
                response.ContentLength64 = System.Text.Encoding.UTF8.GetByteCount(responseString);
                response.ContentType = "text/html; charset=UTF-8";//html
                // 输出回应内容
                System.IO.Stream output = response.OutputStream;
                System.IO.StreamWriter writer = new System.IO.StreamWriter(output);
                writer.Write(responseString);
                // 关闭输出流
                writer.Close();

                if(Console.KeyAvailable)
                    break;
            }
            listener.Stop();
        }
    }
}
