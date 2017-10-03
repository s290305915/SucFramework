using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm_ZSY
{
    public partial class TaskTest : Form
    {
        public TaskTest()
        {
            InitializeComponent();
        }

        private void TaskTest_Load(object sender, EventArgs e)
        {

        }

        private void ShowMsg(string msg, bool ispro = false)
        {
            this.Invoke(new Action(() =>
            {
                //if (ispro)
                //lst_msg.Items.RemoveAt(lst_msg.Items.Count);
                lst_msg.Items.Add(msg);
            }));
        }

        private void StepOne()
        {
            Task t = new Task(new Action(() =>
            {
                MessageBox.Show("这是异步里面的 StepOne！");
            }));
            t.Start();
            ShowMsg("正在打开文件！");
            //todo
            ShowMsg("正在将文件拷贝到相应目录！");
            //tod
            ShowMsg("正在检查文件的完整性！");
        }

        private string StepTwo()
        {
            Task t = new Task(new Action(() =>
            {
                MessageBox.Show("这是异步里面的 StepTwo！");
            }));
            t.Start();
            ShowMsg("正在压缩文件。");
            //todo
            ShowMsg("将压缩文件拷贝到临时文件夹！");
            ShowMsg("准备上传！");
            return "xxx";//path
        }

        private int StepThree()
        {
            Task t = new Task(new Action(() =>
            {
                MessageBox.Show("这是异步里面的 StepThree！");
            }));
            t.Start();
            ShowMsg("正在连接服务器！");
            //todo
            ShowMsg("连接服务器失败，正在重新连接！");
            ShowMsg("连接服务器错误次数超过三次，正在连接备用服务器！");
            ShowMsg("连接成功！");
            return 999;//state or address
        }

        private bool StepFour()
        {
            Task t = new Task(new Action(() =>
            {
                MessageBox.Show("这是异步里面的 StepFour！");
            }));
            t.Start();
            ShowMsg("正在上传文件 0%", true);
            ShowMsg("正在上传文件 10%", true);
            ShowMsg("正在上传文件 20%", true);
            ShowMsg("正在上传文件 30%", true);
            ShowMsg("正在上传文件 40%", true);
            ShowMsg("正在上传文件 50%", true);
            ShowMsg("正在上传文件 60%", true);
            ShowMsg("正在上传文件 70%", true);
            ShowMsg("正在上传文件 80%", true);
            ShowMsg("正在上传文件 90%", true);
            ShowMsg("正在上传文件 100%", true);
            ShowMsg("上传成功！");
            ShowMsg("正在校验文件的合法性。");
            ShowMsg("对比文件成功！");
            return true;
        }

        /// <summary>
        /// 下面这个是同步线程，顺序执行，依次执行
        /// </summary>
        private void StartWork()
        {
            int processCount = 5;   //第一个步骤所在的百分
            Task continuetask = new Task(new Action(() =>
            {
                this.Invoke(new Action(() =>
                {
                    ChangeCHKAndProcess(checkBox1, 100 / processCount);
                }));
                //stepone
                StepOne();
            }));
            continuetask.Start();
            MessageBox.Show("同步第一步完成！");
            //第二步
            Task<string> steptwotask = continuetask.ContinueWith<string>(new Func<Task, string>(x =>
            {
                if (t.IsFaulted)
                {
                    throw t.Exception;
                }
                this.Invoke(new Action(() =>
                {
                    ChangeCHKAndProcess(checkBox2, 100 * 2 / processCount);
                }));
                return StepTwo();
            }));

            MessageBox.Show("同步第二步完成！");
            //第三步
            Task<int> stepthreetask = steptwotask.ContinueWith<int>(new Func<Task, int>(x =>
            {
                if (t.IsFaulted)
                {
                    throw t.Exception;
                }
                this.Invoke(new Action(() =>
                {
                    ChangeCHKAndProcess(checkBox3, 100 * 3 / processCount);
                }));
                return StepThree();
            }));

            MessageBox.Show("同步第三步完成！");
            //第四步
            Task<bool> stepfourtask = stepthreetask.ContinueWith<bool>(new Func<Task, bool>(x =>
            {
                if (t.IsFaulted)
                {
                    throw t.Exception;
                }
                this.Invoke(new Action(() =>
                {
                    ChangeCHKAndProcess(checkBox4, 100 * 4 / processCount);
                }));
                return StepFour();
            }));

            MessageBox.Show("同步第四步完成！");
            stepfourtask.ContinueWith(new Action<Task<bool>>(t =>
            {
                if (t.IsFaulted)
                {
                    throw t.Exception;
                }
                this.Invoke(new Action(() =>
                {
                    ChangeCHKAndProcess(null, 100);
                    MessageBox.Show("任务已完成");
                    this.button1.Text = "开始执行";
                }));
            }));
        }

        private void ChangeCHKAndProcess(CheckBox chk, int procnt)
        {
            if (chk != null)
            {
                string chktx = chk.Text;
                chk.Text = chktx + "完成";
                chk.Checked = true;
                chk.ForeColor = Color.Red;
                chk.Font = new Font(chk.Font, FontStyle.Bold);
            }
            try
            {
                int v = 0;
                do
                {
                    this.Invoke(new Action(() =>
                    {
                        progressBar1.Value += 1;
                        v = progressBar1.Value;
                    }));
                    if (v >= procnt)
                    { break; }
                    //Thread.Sleep(50);
                } while (true);
            }
            catch (Exception ex)
            { }
        }

        bool isrun = false;
        Task t;
        private void button1_Click(object sender, EventArgs e)
        {
            if (isrun)
            {
                isrun = !isrun;
                if (t != null)
                {
                    t.Wait();
                }
            }
            else
            {
                isrun = true;
                button1.Text = "暂停任务";
                if (t != null)
                    if (t.Status == TaskStatus.WaitingToRun)
                    {
                        t.Start();
                    }
                t = Task.Factory.StartNew(() =>
                {
                    this.Invoke(new Action(() =>
                    {
                        StartWork();
                    }));
                }).ContinueWith(x =>
                {
                    if (x.IsFaulted)
                    {
                        MessageBox.Show("线程已被终止,错误信息：" + x.Exception.GetBaseException());
                    }
                });
            }

        }
    }
}
