using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoctorLibrary;
using SucLib.Data.IDal;
using SucLib.Data.Factory;
using System.Diagnostics;

namespace ConsoleApplicationDoctor
{

    public class A
    {
        public string b = "";
        public A()
        {
        }
        public A(string a)
        {
            b = a;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //观察者模式
            Blog xmf = new MyBlog("苏翅", $"发表了一张照片，点击链接查看!");
            SubScriber wnn = new SubScriber("王妮娜");
            SubScriber tmr = new SubScriber("唐马儒");
            SubScriber wmt = new SubScriber("王蜜桃");
            SubScriber anm = new SubScriber("敖尼玛");

            // 添加订阅者
            xmf.AddObserver(new NotifyEventHandler(wnn.Receive));
            xmf.AddObserver(new NotifyEventHandler(tmr.Receive));
            xmf.AddObserver(new NotifyEventHandler(wmt.Receive));
            xmf.AddObserver(new NotifyEventHandler(anm.Receive));

            xmf.Update();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            xmf.RemoveObserver(new NotifyEventHandler(wnn.Receive));
            Console.WriteLine($"移除订阅者{wnn.Name}");
            xmf.Update();

            Console.ReadKey();
            return;
            IOprFactory oprsub = new SubFactory();
            IOprFactory opradd = new AddFactory();
            Opreator otsub = oprsub.CreateOperation();
            otsub.A = 33;
            otsub.B = 22;
            Opreator otadd = opradd.CreateOperation();
            otadd.A = 8;
            otadd.B = 16;
            Trace.WriteLine($"SUB:{otsub.A}-{otsub.B}={otsub.GetResult()}");
            Trace.WriteLine($"ADD:{otadd.A}+{otadd.B}={otadd.GetResult()}");
            //A a = new A("1");
            //A aa = new A() { b = "1" };
            //Console.WriteLine(aa.b + a.b);
            //Console.ReadKey();
            //return;
            IDoctor d = new DoctorProvider();
            Trace.WriteLine($"old driver name is { d.GetDoctorName()} and {d.GetDoctorAge()} age now!!");
        }
    }
}
