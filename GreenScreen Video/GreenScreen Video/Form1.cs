using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Controls;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Video.FFMPEG;

namespace GreenScreen_Video
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int keyR = 0;
            int keyG = 128;
            int keyB = 0;

            Color Key = new Color();
            Key = Color.FromArgb(keyR, keyG, keyB);

            double KCb = -(0.168736 * Key.R) - (0.331264 * Key.G) + (0.5 * Key.B);
            double KCr = 0.5 * Key.R - 0.418688 * Key.G - 0.081312 * Key.B;

            VideoFileWriter videoWriter = new VideoFileWriter();
            VideoFileReader videoReader = new VideoFileReader();

            videoReader.Open(@"C:\Users\Adżi\source\repos\GreenScreen Video\GreenScreen Video\chicken.mp4");
            videoWriter.Open(@"C:\Users\Adżi\source\repos\GreenScreen Video\GreenScreen Video\chicken.mp4", videoReader.Width, videoReader.Height, 25, VideoCodec.MPEG4);

            Bitmap frame = new Bitmap(videoReader.Width, videoReader.Height, PixelFormat.Format24bppRgb);

            for (int i = 0; i < 1000; i++){
                frame = videoReader.ReadVideoFrame();
                for (int y = 0; y < videoReader.Height; y++) {
                    for (int x = 0; x < videoReader.Width; x++){
                        Color c = frame.GetPixel(x, y);

                        double Pb = -0.168736 * c.R - 0.331264 * c.G + 0.5 * c.B;
                        double Pr = 0.5 * c.R - 0.418688 * c.G - 0.081312 * c.B;

                
                        double dist = Math.Sqrt((KCb - Pb) * (KCb - Pb) + (KCr - Pr) * (KCr - Pr));
                        double point = 1;
                  

                        int p1 = 40;
                        int p2 = 60;

                        if (dist <= p1)
                        {
                            point = 0;
                        }
                        else if (dist >= p2)
                        {
                            point = 1;
                        }
                        else if (dist > p1 && dist < p2)
                        {
                            point = (dist - p1) / (p2 - p1);
                        }

                        double q = 1 - point;

                        double PwR = Math.Min(Math.Max(c.R - q * Key.R, 0) + q * Color.White.R, 255);
                        double PwG = Math.Min(Math.Max(c.G - q * Key.G, 0) + q * Color.White.G, 255);
                        double PwB = Math.Min(Math.Max(c.B - q * Key.B, 0) + q * Color.White.B, 255);

                        Color FColor = new Color();
                        FColor = Color.FromArgb((int)PwR, (int)PwG, (int)PwB);

                        frame.SetPixel(x, y, FColor);
                    }
                }
                videoWriter.WriteVideoFrame(frame);
                frame.Dispose();
            }
            videoWriter.Close();
            videoReader.Close();
        

    }
    }
}
