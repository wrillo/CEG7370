using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_Processing
{
    public partial class Form1 : Form
    {
        Bitmap bmpOrig;
        Bitmap bmpProc;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "C:\\Images";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                bmpOrig = new Bitmap(ofd.FileName);
                pic1.Image = bmpOrig;
            }
        }

        private void btnConvertToGrayUnsafe_Click(object sender, EventArgs e)
        {
            bmpProc = new Bitmap(bmpOrig);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            unsafe
            {
                BitmapData bitmapData = bmpProc.LockBits(new Rectangle(0, 0, bmpProc.Width, bmpProc.Height),
                    ImageLockMode.ReadWrite, bmpProc.PixelFormat);
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bmpProc.PixelFormat) / 8;
                int height = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* ptr = (byte*)bitmapData.Scan0;  // point to first pixel
                for (int y = 0; y < height; y++)
                {
                    byte* currentLine = ptr + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int blue = currentLine[x];
                        int green = currentLine[x + 1];
                        int red = currentLine[x + 2];
                        int gray = (int)(0.299 * red + 0.587 * green + 0.114 * blue);
                        currentLine[x] = (byte)(gray);
                        currentLine[x + 1] = (byte)(gray);
                        currentLine[x + 2] = (byte)(gray);
                    }
                }
                bmpProc.UnlockBits(bitmapData);
            }
            sw.Stop();
            pic1.Image = bmpProc;
            MessageBox.Show("Time taken = " + sw.ElapsedMilliseconds.ToString());
        }

        private void btnConvertToGrayParallel_Click(object sender, EventArgs e)
        {
            bmpProc = new Bitmap(bmpOrig);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            unsafe
            {
                BitmapData bitmapData = bmpProc.LockBits(new Rectangle(0, 0, bmpProc.Width, bmpProc.Height),
                    ImageLockMode.ReadWrite, bmpProc.PixelFormat);
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bmpProc.PixelFormat) / 8;
                int height = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* ptr = (byte*)bitmapData.Scan0;  // point to first pixel
                Parallel.For(0, height, y =>
                {
                    byte* currentLine = ptr + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int blue = currentLine[x];
                        int green = currentLine[x + 1];
                        int red = currentLine[x + 2];
                        int gray = (int)(0.299 * red + 0.587 * green + 0.114 * blue);
                        currentLine[x] = (byte)(gray);
                        currentLine[x + 1] = (byte)(gray);
                        currentLine[x + 2] = (byte)(gray);
                    }
                });
                bmpProc.UnlockBits(bitmapData);
            }
            sw.Stop();
            pic1.Image = bmpProc;
            MessageBox.Show("Time taken = " + sw.ElapsedMilliseconds.ToString());
        }

        private void btnConvertToGray_Click_1(object sender, EventArgs e)
        {
            bmpProc = new Bitmap(bmpOrig.Width, bmpOrig.Height);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int Width = bmpOrig.Width;
            int Height = bmpOrig.Height;
            //for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
            {
                object olock = new object();
                //   Parallel.For(0, Width, x =>   // x,y loop runs slow as compared to y,x
                // Parallel.For(0, Height, y =>
                {
                    for (int y = 0; y < Height; y++)
                    // for (int x = 0; x < Width; x++)
                    {
                        Color CurrentPixel;
                        lock (olock)
                        {
                            CurrentPixel = bmpOrig.GetPixel(x, y);
                        }
                        int red = CurrentPixel.R;
                        int green = CurrentPixel.G;
                        int blue = CurrentPixel.B;
                        int gray = (int)(0.299 * red + 0.587 * green + 0.114 * blue);
                        lock (olock)
                        {
                            bmpProc.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                        }
                    }
                }//});
                 // }
            }
            sw.Stop();
            pic1.Image = bmpProc;
            MessageBox.Show("Time taken = " + sw.ElapsedMilliseconds.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

}
