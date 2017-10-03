using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                if(args.Length > 0 && args[0].ToLower() == "-c")
                {
                    //NativeConsole.AllocConsole();
                    //Console.WriteLine("控制台已启动");
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Login());
            }
            catch(Exception e)
            {
                //LogHelper.Write(typeof(Program), e);
            }
            // finally
            // {
            //     NativeConsole.FreeConsole();
            // }
        }

    }
    public static class App
    {
        public static string AppPath
        {
            get
            {
                return System.Environment.CurrentDirectory;
            }
        }

    }
}
