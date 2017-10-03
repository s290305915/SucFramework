using System;
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
    public partial class FormPLC : Form
    {
        private IPEndPoint serverFullAddr;
        private Socket sock;
        private Socket newSocket;
        System.Windows.Forms.Timer timer;
        private Thread myThead;

        public FormPLC()
        {
            InitializeComponent();
        }

        private void FormPLC_Load(object sender, EventArgs e)
        {
            SetMsg("已开始服务！");
            myThead = new Thread(new ThreadStart(BeginListen));
            myThead.Start();
        }

        private void BeginListen()
        {
            Thread.Sleep(5000);
            try
            {
                IPAddress serverIP = IPAddress.Parse("192.168.200.239");   //IP  
                serverFullAddr = new IPEndPoint(serverIP, 81);//设置IP，端口   int.Parse(tx_port.Text.Trim())
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

                    SetMsg("实时数据：" + mess);
                    if(mess.Contains("BJDC.CALC"))
                        UpdateData(mess);
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
                    if(this.listBox1.TopIndex == this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight))
                        scroll = true;
                    listBox1.Items.Add(msg);
                    if(scroll)
                        this.listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                }));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        public void UpdateData(string data)
        {
            try
            {
                List<string> datas = data.Split(',').ToList();
                datas.ForEach(x => {
                    SetMsg("更新数据：" + x);
                    if(x.IndexOf(":") > 0)
                    {
                        List<string> dt = x.Split(':').ToList();
                        if(dt[0].Contains("BJDC.CALC.AISM"))
                        {
                            try
                            {
                                string sql = $"UPDATE FILEINFO SET CONTENT='{dt[1]}' where IP='{dt[0]}'";
                                //,TIME=to_char(sysdate,'yyyy-mm-dd hh:mm:ss')
                                new QueryManager().Execute(sql);
                            }
                            catch(Exception ex)
                            {
                                SetMsg("更新数据库失败：->" + ex.Message);
                            }
                        }
                    }
                });
                SetMsg("更新数据库成功!");
            }
            catch(Exception ex)
            {
                SetMsg("更新数据库失败：->" + ex.Message);
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            ListBox lsb = sender as ListBox;
            if(lsb.SelectedItem != null)
                MessageBox.Show(lsb.SelectedItem.ToString());
        }

        private void FormPLC_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetMsg("正在停止服务，正在关闭进程");
            newSocket.Disconnect(true);
            newSocket.Dispose();
            sock.Disconnect(true);
            sock.Dispose();
            myThead.Abort();
        }
    }
}
