using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MARGINS
    {
        public int Left;
        public int Right;
        public int Top;
        public int Bottom;
    }
    public class LayouKind : Form
    {

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmExtendFrameIntoClientArea
        (IntPtr hwnd, ref MARGINS margins);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        protected override void OnLoad(EventArgs e)
        {
            if (LayouKind.DwmIsCompositionEnabled())
            {
                MARGINS margin = new MARGINS();
                margin.Right = margin.Left = margin.Bottom = margin.Top = -1;
                LayouKind.DwmExtendFrameIntoClientArea(this.Handle, ref margin);
            }
            base.OnLoad(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (LayouKind.DwmIsCompositionEnabled())
            {
                e.Graphics.Clear(Color.White);
            }
        }
    }
}
