using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DoctorLibrary
{
    /// 接口工厂实现数字相加
    /// <summary>
    /// 基本类
    /// </summary>
    public class Opreator
    {
        public double A { get; set; } = 0;
        public double B { get; set; } = 0;
        public virtual double GetResult()
        {
            double result = 0;
            return result;
        }
    }

    /// <summary>
    /// 定义接口
    /// 实现方式：IFactory operFactory = new AddOpr()/SubOpr();
    /// 因为在这里IFactory是个接口，其余的SUBOpr是实现了这个接口，也就是在实
    /// 现接口的时候，具体的实现类里面的方法必须和接口一模一样。所以说在这我们既可以
    /// 用具体的实现类SUBOpr 也可以用IFactory，当我们用IFactory的时候更可以达到代
    /// 码复用的效果，当我们想实现加法功能的时候，我们只需要将上面加粗的代码后面的
    /// SUBOpr更改为AddOpr即可。
    /// </summary>
    public interface IOprFactory
    {
        Opreator CreateOperation();
    }

    /// <summary>
    /// 实现类ADD
    /// </summary>
    class AddOpr : Opreator
    {
        public override double GetResult() => A + B;
    }
    /// <summary>
    /// 实现方法SUB
    /// </summary>
    class SubOpr : Opreator
    {
        public override double GetResult() => A - B;
    }

    /// <summary>
    /// 加法工厂
    /// </summary>
    public class AddFactory : IOprFactory
    {
        public Opreator CreateOperation() => new AddOpr();
    }
    /// <summary>
    /// 减法工厂
    /// </summary>
    public class SubFactory : IOprFactory
    {
        public Opreator CreateOperation() => new SubOpr();
    }

    /// <summary>
    /// 调用类
    /// </summary>
    public class OprAA
    {
        public OprAA()
        {
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
        }
    }
}
