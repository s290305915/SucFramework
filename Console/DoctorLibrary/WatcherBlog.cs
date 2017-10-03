using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DoctorLibrary
{
    /// <summary>
    /// 观察者模式实现自动订阅
    /// 到这里，观察者模式就讲完了，
    /// 观察者模式定义了一种一对多的依赖关系，
    /// 让多个观察者对象可以同时监听某一个主题对象，
    /// 这个主题对象在发生状态变化时，会通知所有观察者对象，
    /// 使它们能够自动更新自己，
    /// 因此在一些需求上是当一个对象的改变需要同时改变多个其他对象的时候，
    /// 且不知道多少个对象需要去通知改变的时候，
    /// 观察者模式就成了首选，这种模式的用的最多的，
    /// 在我的开发经历中便是windows phone手机客户端的开发了，
    /// 经常要用到这类的委托事件的处理，用多了后发现就习以为常，这种模式也就没那么稀奇了。
    /// </summary>
    /// 

    // 充当订阅者接口的委托
    public delegate void NotifyEventHandler(object sender);

    //抽象订阅号
    public class Blog
    {
        public NotifyEventHandler NotifyEvent;
        public string Symbol
        {
            get; set;
        }//描写订阅号的相关信息
        public string Info
        {
            get; set;
        }//描写此次update的信息
        public Blog(string symbol, string info)
        {
            this.Symbol = symbol;
            this.Info = info;
        }

        public void AddObserver(NotifyEventHandler ob) => NotifyEvent += ob;    //增加订阅消息
        public void RemoveObserver(NotifyEventHandler ob) => NotifyEvent -= ob; //去除订阅消息 

        public void Update() => NotifyEvent(this);  //更新订阅消息
    }


    public class MyBlog : Blog  // 具体订阅号类
    {
        public MyBlog(string symbol, string info) : base(symbol, info) { }
    }

    public class SubScriber
    {
        public string Name
        {
            get; set;
        }

        public SubScriber(string name)
        {
            this.Name = name;
        }
        public void Receive(object o)
        {
            Blog xmf = (o ?? null) as Blog;
            Console.WriteLine($"订阅者{Name}观察到了{xmf.Symbol}{xmf.Info}");
            Trace.WriteLine($"订阅者{Name}观察到了{xmf.Symbol}{xmf.Info}");
        }
    }

    /// <summary>
    /// 调用类
    /// </summary>
    public class AA
    {
        public AA()
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
        }
    }
}
