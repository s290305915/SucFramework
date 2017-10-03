using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poco
{
    public partial class Form1 : Form
    {
        const int milliSeconds = 100;
        public Form1()
        {
            InitializeComponent();
            this.pro_all.Maximum = 100;
        }
        bool isrun = false;
        bool isover = false;

        private void btn_start_Click(object sender, EventArgs e)
        {
            if(isover)
            {
                MessageBox.Show("运行完成！");
                return;
            }
            if(!isrun)
            {
                isrun = !isrun;
                btn_start.Text = "正在运行";
                Task.Factory.StartNew(() => {
                    DoWork();
                }).ContinueWith(x => {
                    if(x.IsFaulted)
                    {
                        //错误日志
                    }
                });
            }
            else
            {
                MessageBox.Show("正在运行中！");
            }
        }

        public void DoWork()
        {
            int progressCount = 4;
            //第一步
            Task step1 = new Task(new Action(() => {
                this.Invoke(new Action(() => {
                    ChangeCheckBox(chk1);
                }));
                //执行操作-传过去当前进度
                Do_Step1(100 / progressCount);
            }));
            step1.Start();
            //第二步-这里发回来一个string的参数
            Task<string> step2 = step1.ContinueWith<string>(new Func<Task, string>(t => {
                if(t.IsFaulted)
                    throw new Exception();
                this.Invoke(new Action(() => {
                    ChangeCheckBox(chk2);
                }));
                return Do_Step2(100 * 2 / progressCount);
            }));
            //第三步
            Task<bool> step3 = step2.ContinueWith<bool>(new Func<Task<string>, bool>(t => {
                if(t.IsFaulted)
                    throw new Exception();
                this.Invoke(new Action(() => {
                    ChangeCheckBox(chk3);
                }));
                return Do_Step3(100 * 3 / progressCount, step2.Result);
            }));
            //第四步，结束
            step3.ContinueWith(new Action<Task<bool>>(b => {
                this.Invoke(new Action(() => {
                    ChangeCheckBox(chk4);
                }));
                Do_Step4(100, step3.Result);
            }));

        }

        #region 具体每一步要操作的方法

        /// <summary>
        /// 具体实现 第一步操作
        /// </summary>
        /// <param name="v">进度条进度</param>
        private void Do_Step1(int v)
        {
            proANDlbox("准备开始！");
            proANDlbox("正在加载数据");
            Thread.Sleep(1000);
            Task progressTask1 = new Task(new Action(() => {
                UpdateProgress(v);
            }));
            proANDlbox("正在读取文件");
            progressTask1.Start();
            //各种操作代码
            proANDlbox("准备工作完成！");
        }

        /// <summary>
        /// 第二步的方法
        /// </summary>
        /// <param name="v">进度条进度</param>
        /// <returns>返回一个字符串（解释怎样在线程中返回数据）</returns>
        private string Do_Step2(int v)
        {
            proANDlbox("第二步开始执行");
            Thread.Sleep(1000);
            Task progressTask1 = new Task(new Action(() => {
                UpdateProgress(v);
            }));
            progressTask1.Start();
            proANDlbox("正在操作文件，请稍等");
            return "OK";
        }

        /// <summary>
        /// 第三步的方法
        /// </summary>
        /// <param name="v">进度条进度</param>
        /// <param name="result">传入参数（解释怎样通过线程传入参数）</param>
        /// <returns>返回一个布尔型（同上面方法2，返回操作的）</returns>
        private bool Do_Step3(int v, string result)
        {
            proANDlbox("第三步开始执行");
            for(int i = 0;i < 100;i++)
            {
                DateTime startTime = DateTime.Now.AddDays(-new Random().Next(0, 10));
                proANDlbox($"正在读取,标识：{ (long)(DateTime.Now - startTime).TotalSeconds}，id:" + Guid.NewGuid().ToString());
            }
            Thread.Sleep(1000);
            Task progressTask1 = new Task(new Action(() => {
                UpdateProgress(v);
            }));
            progressTask1.Start();
            proANDlbox($"读取完成");
            if(result.Contains("OK"))
                return true;
            return false;
        }

        /// <summary>
        /// 第四步，结束方法
        /// </summary>
        /// <param name="v">进度条进度</param>
        /// <param name="result">传入上面方法三的参数（解释传入参数，没有返回值的方法）</param>
        private void Do_Step4(int v, bool result)
        {
            proANDlbox($"正在保存，请稍后");
            Thread.Sleep(1000);
            Task progressTask1 = new Task(new Action(() => {
                UpdateProgress(v);
            }));
            progressTask1.Start();
            if(result)
            {
                proANDlbox($"保存完成，请关闭窗口！");
                MessageBox.Show("完成！");
                isover = !isover;
                return;
            }
        }
        
        #endregion

        #region 进行时，修改界面
        /// <summary>
        /// 更新进度条
        /// </summary>
        /// <param name="max"></param>
        void UpdateProgress(int max)
        {
            try
            {
                int v = 0;
                do
                {
                    this.Invoke(new Action(() => {
                        pro_all.Value += 1;
                        v = pro_all.Value;
                    }));

                    if(v >= max)
                    {
                        break;
                    }

                    Thread.Sleep(milliSeconds / 10);
                } while(true);
            }
            catch(Exception ex)
            {
                //写入日志
            }
        }
        /// <summary>
        /// 往列表里写入内容
        /// </summary>
        /// <param name="lbitem"></param>
        public void proANDlbox(string lbitem)
        {
            this.Invoke(new Action(() => {
                object item = lbitem;
                listBox1.Items.Add(item);
                listBox1.SetSelected(listBox1.Items.Count - 1, true);
            }));
        }
        /// <summary>
        /// 改变checkbox样式
        /// </summary>
        /// <param name="cBox"></param>
        public void ChangeCheckBox(CheckBox cBox)
        {
            cBox.Checked = true;
            cBox.Enabled = false;
            cBox.ForeColor = Color.Red;
            cBox.Font = new Font(cBox.Font, FontStyle.Bold);
        }
        #endregion


    }
}
