using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHttpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient http;
            using(http = new HttpClient())
            {
                Task<HttpResponseMessage> response = http.GetAsync("http://www.baidu.com");
                HttpResponseMessage x = response.Result;
                Stream Content = x.Content.ReadAsStreamAsync().Result;
                string c = new StreamReader(Content).ReadToEnd();
                Console.WriteLine(c);
                Console.ReadKey();
            }
        }
    }
}
