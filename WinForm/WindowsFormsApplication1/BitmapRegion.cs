using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class BitmapRegion
    {
        public BitmapRegion()
        {
        }

        public static void CreateControlRegion(Control control, Bitmap bitmap)
        {
            if(control == null || bitmap == null)
                return;

            control.Width = bitmap.Width;
            control.Height = bitmap.Height;
            //根据控件类型来设置相应属性
            if(control is Form)
            {
                Form form = (Form)control;
                form.Width = control.Width;
                form.Height = control.Height;

                form.FormBorderStyle = FormBorderStyle.None;
                form.BackgroundImage = bitmap;

                GraphicsPath graphicspath = CalculateControlGraphicsPath(bitmap);

                form.Region = new Region(graphicspath);
            }
            else if(control is Button)
            {
                Button button = (Button)control;
                button.Text = "";
                button.Cursor = Cursors.Hand;
                button.BackgroundImage = bitmap;
                GraphicsPath graphicspath = CalculateControlGraphicsPath(bitmap);
                button.Region = new Region(graphicspath);
            }
            else if(control is PictureBox)
            {
                PictureBox picturebox = (PictureBox)control;
                picturebox.BackgroundImage = bitmap;
                GraphicsPath graphicspath = CalculateControlGraphicsPath(bitmap);
                picturebox.Region = new Region(graphicspath);
            }
            else if(control is Panel)
            {
                Panel panel = (Panel)control;
                panel.BackgroundImage = bitmap;
                GraphicsPath graphicspath = CalculateControlGraphicsPath(bitmap);
                panel.Region = new Region(graphicspath);
            }
        }

        private static GraphicsPath CalculateControlGraphicsPath(Bitmap bitmap)
        {
            GraphicsPath graphicspath = new GraphicsPath();
            Color colorTransparent = bitmap.GetPixel(0, 0);
            int colOpaquePixel = 0;
            for(int row = 0;row < bitmap.Height;row++)
            {
                colOpaquePixel = 0;
                for(int col = 0;col < bitmap.Width;col++)
                {
                    if(bitmap.GetPixel(col, row) != colorTransparent)
                    {
                        colOpaquePixel = col;
                        int colNext = col;
                        for(colNext = colOpaquePixel;colNext < bitmap.Width;colNext++)
                        {
                            if(bitmap.GetPixel(colNext, row) == colorTransparent)
                                break;
                        }
                        graphicspath.AddRectangle(new Rectangle(colOpaquePixel, row, colNext - colOpaquePixel, 1));
                        col = colNext;
                    }

                }
            }
            return graphicspath;
        }
    }
}
