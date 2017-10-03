using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Zombiers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int pid = 0;
        int address1 = 0;
        private IntPtr m_hProcess = IntPtr.Zero;   //这个保存打开了个进程句柄
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                pid = MemoryHelp.GetPidByProcessName("Xbox");
            }
            catch
            {
                MessageBox.Show("抱歉，没有找到程序！");
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ba = tx_baseadd.Text.Trim();
            if(string.IsNullOrEmpty(ba))
            {
                MessageBox.Show("请填入基地址");
                return;
            }
            int jdz = 0;
            try
            {
                jdz = Convert.ToInt32("0x" + ba, 16);
            }
            catch { MessageBox.Show("填入的数据不正确"); return; }
            address1 = MemoryHelp.ReadMemoryValue(jdz, pid);
            address1 = address1 + 0x768;                                                    //获取2级地址
            address1 = MemoryHelp.ReadMemoryValue(address1, pid);
            address1 = address1 + 0x5560;                                                 //获取存放阳光数值的地址
            MemoryHelp.WriteMemoryValue(address1, 0x1869F, pid);                          //写入数据到地址（0x1869F表示99999）
        }
    }

    public static class MemoryHelp
    {
        /// <summary>
        /// 读取内存
        /// </summary>
        /// <param name="lpProcess"></param>
        /// <param name="lpBaseAddress"></param>
        /// <param name="lpBuffer"></param>
        /// <param name="nSize"></param>
        /// <param name="BytesRead"></param>
        /// <returns></returns>
        [DllImportAttribute("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        public static extern bool ReadProcessMemory(IntPtr lpProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, IntPtr BytesRead);

        /// <summary>
        /// 打开进程-kernel32.dll系统动态链接库    
        /// </summary>
        /// <param name="iAccess"></param>
        /// <param name="Handle"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        [DllImportAttribute("kernel32.dll", EntryPoint = "OpenProcess")]
        public static extern IntPtr OpenProcess(int iAccess, bool Handle, int ProcessID);

        /// <summary>
        /// 关闭句柄
        /// </summary>
        /// <param name="hObject"></param>
        [DllImport("kernel32.dll", EntryPoint = "CloseHandle")]
        private static extern void CloseHandle(IntPtr hObject);

        /// <summary>
        /// 写入内存
        /// </summary>
        /// <param name="lpProcess"></param>
        /// <param name="lpBaseAddress"></param>
        /// <param name="lpBuffer"></param>
        /// <param name="nSize"></param>
        /// <param name="BytesWrite"></param>
        /// <returns></returns>
        [DllImportAttribute("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        public static extern bool WriteProcessMemory(IntPtr lpProcess, IntPtr lpBaseAddress, int[] lpBuffer, int nSize, IntPtr BytesWrite);

        /// <summary>
        /// 写入内存
        /// </summary>
        /// <param name="lpProcess"></param>
        /// <param name="lpBaseAddress"></param>
        /// <param name="lpBuffer"></param>
        /// <param name="nSize"></param>
        /// <param name="BytesWrite"></param>
        /// <returns></returns>
        [DllImportAttribute("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        public static extern bool WriteProcessMemory(IntPtr lpProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, IntPtr BytesWrite);

        /// <summary>
        /// GetModuleHandle是获取一个应用程序或动态链接库的模块句柄
        /// </summary>
        /// <param name="lpModuleName"></param>
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);


        /// <summary>
        /// 修改内存
        /// </summary>
        /// <param name="hProcess">修改内存的句柄</param>
        /// <param name="lpAddress">要修改的起始地址</param>
        /// <param name="dwSize">页区域大小</param>
        /// <param name="flNewProtect">访问方式</param>
        /// <param name="lpflOldProtect">用于保护改变前的保护属性</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, int flNewProtect, ref IntPtr lpflOldProtect);


        //根据进程名获取PID    
        public static int GetPidByProcessName(string processName)
        {
            Process[] ArrayProcess = Process.GetProcessesByName(processName);
            foreach(Process pro in ArrayProcess)
            {
                return pro.Id;
            }
            return 0;
        }

        //读内存模块    
        public static IntPtr ReadModule(string ModuleName)
        {
            return GetModuleHandle(ModuleName);
        }

        #region 读取内存
        //读取内存的值    

        public static int ReadMemoryValue(int baseAddress, string ProcessName = "", int ProcessID = 0)
        {
            return ReadMemoryValue(baseAddress, ProcessID, ProcessName);
        }
        public static int ReadMemoryValue(int baseAddress, int ProcessID = 0, string ProcessName = "")
        {
            try
            {
                if(ProcessID == 0 && ProcessName == "")
                    throw new Exception("ProcessID and ProcessName must have one of them");
                byte[] buffer = new byte[4];
                IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                IntPtr hProcess = OpenProcess(0x1F0FFF, false, ProcessID == 0 ? GetPidByProcessName(ProcessName) : ProcessID);
                ReadProcessMemory(hProcess, (IntPtr)baseAddress, byteAddress, 4, IntPtr.Zero);
                CloseHandle(hProcess);
                return Marshal.ReadInt32(byteAddress);
            }
            catch
            {
                return 0;
            }
        }
        public static long ReadMemoryValue(long baseAddress, string ProcessName)
        {
            try
            {
                string temp = ((IntPtr)baseAddress).ToString("x");
                byte[] buffer = new byte[4];
                IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                IntPtr hProcess = OpenProcess(0x1F0FFF, false, GetPidByProcessName(ProcessName));
                ReadProcessMemory(hProcess, (IntPtr)baseAddress, byteAddress, 4, IntPtr.Zero);
                CloseHandle(hProcess);
                string ss = ((IntPtr)baseAddress).ToString("x");
                return Marshal.ReadInt32(byteAddress);
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 写内存方式   
        public static void WriteMemoryValue(long baseAddress, int value, string ProcessName = "", int ProcessID = 0)
        {
            WriteMemoryValue(baseAddress, value, ProcessID, ProcessName);
        }
        public static void WriteMemoryValue(long baseAddress, int value, int ProcessID = 0, string ProcessName = "")
        {
            if(ProcessID == 0 && ProcessName == "")
                throw new Exception("ProcessID and ProcessName must have one of them");
            //打开进程获得句柄  
            IntPtr hProcess = OpenProcess(0x1F0FFF, false, ProcessID == 0 ? GetPidByProcessName(ProcessName) : ProcessID);
            bool flag;
            int[] Data = new int[] { value };
            flag = WriteProcessMemory(hProcess, (IntPtr)baseAddress, Data, 4, IntPtr.Zero);
            CloseHandle(hProcess);
        }
        //写内存整数型    
        public static void WriteMemoryValue(long baseAddress, string ProcessName, int value)
        {
            //打开进程获得句柄  
            IntPtr hProcess = OpenProcess(0x1F0FFF, false, GetPidByProcessName(ProcessName));
            bool flag;
            int[] Data = new int[] { value };
            flag = WriteProcessMemory(hProcess, (IntPtr)baseAddress, Data, 4, IntPtr.Zero);
            CloseHandle(hProcess);
        }
        //写内存字节型    
        public static void WriteMemoryValue(int baseAddress, string ProcessName, byte[] value)
        {
            //打开进程获得句柄  
            IntPtr hProcess = OpenProcess(0x1F0FFF, false, GetPidByProcessName(ProcessName));
            bool flag;
            //bool flag2;  
            IntPtr adds = (IntPtr)0x33;
            //flag2 = VirtualProtectEx(hProcess, (IntPtr)baseAddress, 4, 0x40, ref adds);  
            flag = WriteProcessMemory(hProcess, (IntPtr)baseAddress, value, value.Length, IntPtr.Zero);
            string temp = ((IntPtr)baseAddress).ToString("x");
            CloseHandle(hProcess);
        }


        #endregion


    }

    public static class win32API
    {
        public const int OPEN_PROCESS_ALL = 2035711;
        public const int PAGE_READWRITE = 4;
        public const int PROCESS_CREATE_THREAD = 2;
        public const int PROCESS_HEAP_ENTRY_BUSY = 4;
        public const int PROCESS_VM_OPERATION = 8;
        public const int PROCESS_VM_READ = 256;
        public const int PROCESS_VM_WRITE = 32;

        private const int PAGE_EXECUTE_READWRITE = 0x4;
        private const int MEM_COMMIT = 4096;
        private const int MEM_RELEASE = 0x8000;
        private const int MEM_DECOMMIT = 0x4000;
        private const int PROCESS_ALL_ACCESS = 0x1F0FFF;



        //查找窗体  
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        //得到目标进程句柄的函数  
        [DllImport("USER32.DLL")]
        public extern static int GetWindowThreadProcessId(int hwnd, ref int lpdwProcessId);
        [DllImport("USER32.DLL")]
        public extern static int GetWindowThreadProcessId(IntPtr hwnd, ref int lpdwProcessId);

        //打开进程  
        [DllImport("kernel32.dll")]
        public extern static int OpenProcess(int dwDesiredAccess, int bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll")]
        public extern static IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId);

        //关闭句柄的函数  
        [DllImport("kernel32.dll", EntryPoint = "CloseHandle")]
        public static extern int CloseHandle(int hObject);

        //读内存  
        [DllImport("Kernel32.dll ")]
        public static extern int ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, int size, out IntPtr lpNumberOfBytesWritten);
        [DllImport("Kernel32.dll ")]
        public static extern int ReadProcessMemory(int hProcess, int lpBaseAddress, ref int buffer,
            //byte[] buffer,  
            int size, int lpNumberOfBytesWritten);
        [DllImport("Kernel32.dll ")]
        public static extern int ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesWritten);

        //写内存  
        [DllImport("kernel32.dll")]
        public static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, int size, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern int WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesWritten);

        //创建线程  
        [DllImport("kernel32", EntryPoint = "CreateRemoteThread")]
        public static extern int CreateRemoteThread(int hProcess, int lpThreadAttributes, int dwStackSize, int lpStartAddress, int lpParameter, int dwCreationFlags, ref int lpThreadId);

        //开辟指定进程的内存空间  
        [DllImport("Kernel32.dll")]
        public static extern int VirtualAllocEx(IntPtr hProcess, int lpAddress, int dwSize, int flAllocationType, int flProtect);

        [DllImport("Kernel32.dll")]
        public static extern int VirtualAllocEx(int hProcess, int lpAddress, int dwSize, int flAllocationType, int flProtect);

        //释放内存空间  
        [DllImport("Kernel32.dll")]
        public static extern int VirtualFreeEx(int hProcess, int lpAddress, int dwSize, int flAllocationType);
    }


}
