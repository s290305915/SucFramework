using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OracleFromBase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Thread myThead = null;
        Thread myTheadA = null;
        private IPAddress serverIP = IPAddress.Parse("127.0.00.1");
        private IPEndPoint serverFullAddr;
        private Socket sock;
        private Socket sockA;
        private Socket newSocket;
        private Socket newSocketA;
        string oldting = "";
        string oldtingA = "";
        System.Windows.Forms.Timer timer;
        FormPLC plc;
        private void Form1_Load(object sender, EventArgs e)
        {
            plc = new FormPLC();
            plc.Show();
            btn_close.Enabled = false;
            tx_port.Text = "9009";

            SetMsg("已开始服务！");
            btn_start.Enabled = false;
            btn_close.Enabled = true;

            myThead = new Thread(new ThreadStart(BeginListen));
            myTheadA = new Thread(new ThreadStart(BeginListenA));
            myThead.Start();
            myTheadA.Start();

        }

        private void btn_start_Click(object sender, EventArgs e)
        {

            //timer = new System.Windows.Forms.Timer();
            //timer.Interval = 5000;
            //timer.Tick += new EventHandler(timer_Tick);
            //timer.Start();
        }

        //判断下线用户  
        void timer_Tick(object sender, EventArgs e)
        {
            myThead = new Thread(new ThreadStart(BeginListen));
            myTheadA = new Thread(new ThreadStart(BeginListenA));
            myThead.Start();
            myTheadA.Start();
        }

        public string kg = "";
        public string kgA = "";
        private void BeginListen()
        {
            try
            {
                IPAddress serverIP = IPAddress.Parse("192.168.200.239");   //IP  
                serverFullAddr = new IPEndPoint(serverIP, 9009);//设置IP，端口   int.Parse(tx_port.Text.Trim())
                if(sock != null)
                {
                    goto goon;
                }
                if(newSocket == null)
                    sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //指定本地主机地址和端口号  
                sock.Bind(serverFullAddr);
                SetMsg("启动成功 时间:" + DateTime.Now);
                goon:
                byte[] message = new byte[1024];
                string mess = "";
                while(true)
                {
                    sock.Listen(20);                                                              //backlog 参数指定队列中最多可容纳的等待接受的传入连接数。  
                    if(newSocket == null)
                        newSocket = sock.Accept();                                    //为新建连接创建新的socket。sock这个socket是用来监听的，当他有连接请求的时候，将地址给新的socket来接收，这样不影响他继续监听原本的socket。  
                    int bytes = newSocket.Receive(message);                     //用刚才newsocket接收数据  
                    mess = Encoding.Default.GetString(message, 0, bytes); //对接收字节编码（S与C 两端编码格式必须一致不然中文乱码）（当接收的字节大于1024的时候 这应该是循环接收，测试就没有那样写了）  s
                                                                          //获取客户端的IP和端口  
                    if(mess.Length > 12)
                    {
                        mess = mess.Substring(0, 10);
                        if(!oldting.Equals(mess))
                        {
                            oldting = mess;
                            kg = oldting.Replace("wn", "").Replace("kg", "");
                            string ip11 = ((IPEndPoint)newSocket.RemoteEndPoint).Address.ToString();//ip
                            string port11 = ((IPEndPoint)newSocket.RemoteEndPoint).Port.ToString();//端口
                            mess = " 来自：" + ip11 + ":" + port11 + "，端口：9010  数据：" + mess + ",称重：" + kg + "kg 当前时间为：" + DateTime.Now;     //处理数据  
                            SetMsg(mess);
                            UpdateData(9009, kg);
                        }
                    }
                    //newSocket.Send(Encoding.Default.GetBytes(mess));//向客户端发送数据  
                }
            }
            catch(Exception ex)
            {
                SetMsg(ex.Message);
                //btn_close_Click(null, new EventArgs());
                //btn_start_Click(null, new EventArgs());
            }
        }


        private void BeginListenA()
        {
            try
            {
                IPAddress serverIP = IPAddress.Parse("192.168.200.239");   //IP  
                serverFullAddr = new IPEndPoint(serverIP, 9010);//设置IP，端口   int.Parse(tx_port.Text.Trim())
                if(sockA != null)
                {
                    goto goon;
                }
                if(newSocketA == null)
                    sockA = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //指定本地主机地址和端口号  
                sockA.Bind(serverFullAddr);
                SetMsg("启动成功 时间:" + DateTime.Now);
                goon:
                byte[] message = new byte[1024];
                string mess = "";
                while(true)
                {
                    sockA.Listen(20);                                                              //backlog 参数指定队列中最多可容纳的等待接受的传入连接数。  
                    if(newSocketA == null)
                        newSocketA = sockA.Accept();                                    //为新建连接创建新的socket。sock这个socket是用来监听的，当他有连接请求的时候，将地址给新的socket来接收，这样不影响他继续监听原本的socket。  
                    int bytes = newSocketA.Receive(message);                     //用刚才newsocket接收数据  
                    mess = Encoding.Default.GetString(message, 0, bytes); //对接收字节编码（S与C 两端编码格式必须一致不然中文乱码）（当接收的字节大于1024的时候 这应该是循环接收，测试就没有那样写了）  s
                                                                          //获取客户端的IP和端口  
                    if(mess.Length > 12)
                    {
                        mess = mess.Substring(0, 12);
                        if(!oldtingA.Equals(mess))
                        {
                            oldtingA = mess;
                            kgA = oldtingA.Substring(2, 9);
                            string[] arrys = string.Join(",", kgA.Select(x => x)).Split(',');//new string[kgA.Length];//string.Join(",", kgA.Select(x => x)).Split(',');
                            //for(int i = 0;i <= kgA.Length;i++)
                            //    arrys[i] = kgA[i].ToString();
                            string kgx = "";
                            if(arrys.Length > 2)
                            {
                                for(int i = 0;i <= arrys.Length;i++)
                                {
                                    if(i != 3)  //第四位
                                        kgx += arrys[i];
                                    else
                                        kgx += arrys[i] + ".";
                                    if(i > 5)
                                        break;
                                }
                            }
                            string ip11 = ((IPEndPoint)newSocketA.RemoteEndPoint).Address.ToString();//ip
                            string port11 = ((IPEndPoint)newSocketA.RemoteEndPoint).Port.ToString();//端口
                            mess = " 来自：" + ip11 + ":" + port11 + "，端口：9009  数据：" + mess + ",称重：" + kgx + "kg 当前时间为：" + DateTime.Now;     //处理数据  
                            SetMsg(mess);
                            UpdateData(9010, kgx);
                        }
                    }
                    //newSocket.Send(Encoding.Default.GetBytes(mess));//向客户端发送数据  
                }
            }
            catch(Exception ex)
            {
                SetMsg(ex.Message);
                //btn_close_Click(null, new EventArgs());
                //btn_start_Click(null, new EventArgs());
            }
        }

        public void SetMsg(string msg)
        {
            try
            {
                ///移动到最下面并添加新行
                this.Invoke(new Action(() => {
                    bool scroll = false;
                    if(this.lst_msg.TopIndex == this.lst_msg.Items.Count - (int)(this.lst_msg.Height / this.lst_msg.ItemHeight))
                        scroll = true;
                    lst_msg.Items.Add(msg);
                    if(scroll)
                        this.lst_msg.TopIndex = this.lst_msg.Items.Count - (int)(this.lst_msg.Height / this.lst_msg.ItemHeight);
                }));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            myThead.Abort();
            myTheadA.Abort();
            timer.Stop();
            SetMsg("已暂停服务！");
            btn_start.Enabled = true;
            btn_close.Enabled = false;
            try
            {
                if(sock != null)
                {

                    sock.Close();
                    sockA.Close();
                    newSocket.Close();
                    newSocketA.Close();
                    sock = null;
                    sockA = null;
                    newSocket = null;
                    newSocketA = null;
                }
            }
            catch(Exception ex)
            {
                //SetMsg(ex.Message);
            }
        }

        private void lst_msg_DoubleClick(object sender, EventArgs e)
        {
            ListBox lsb = sender as ListBox;
            if(lsb.SelectedItem != null)
                MessageBox.Show(lsb.SelectedItem.ToString());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetMsg("正在停止服务，正在关闭进程");
            try
            {
                if(sock != null)
                {
                    myThead.Abort();
                    myTheadA.Abort();
                    timer.Stop();
                    newSocket.Close();
                    newSocketA.Close();
                    sock.Close();
                    sockA.Close();
                    sock = null;
                    sockA = null;
                    newSocket = null;
                    newSocketA = null;
                }
            }
            catch { }
        }

        public void UpdateData(int port, string data)
        {
            //makeWeight     制样台秤   192.168.200.183 8000  9009
            //makeWeight2    制样台秤2  192.168.200.183   8004  9010
            string ip = "";
            if(port == 9009)
                ip = "makeWeight";
            else if(port == 9010)
                ip = "makeWeight2";
            else
                return;
            try
            {
                string sql = $"UPDATE FILEINFO SET CONTENT='{data}' WHERE IP='{ip}'";   //,TIME=to_char(sysdate,'yyyy-mm-dd hh:mm:ss')
                new QueryManager().Execute(sql);
                SetMsg("更新数据库成功：" + ip + "->" + data);
            }
            catch(Exception ex)
            {
                SetMsg("更新数据库失败：" + ip + "->" + data + "->" + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(plc == null)
            {
                plc = new FormPLC();
                plc.Show();
            }
            else
            {
                MessageBox.Show("已经打开PLC数据监听！");
            }
        }
    }
}
