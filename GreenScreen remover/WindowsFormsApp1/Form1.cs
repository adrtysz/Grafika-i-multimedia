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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form{

    
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e){

            Bitmap bmp = new Bitmap(@"C:\Users\Adżi\source\repos\adrtysz\Grafika-i-multimedia\GreenScreen remover\WindowsFormsApp1\green1.png");
               
            for (int i = 0; i < bmp.Height; i++){
                for (int j = 0; j < bmp.Width; j++){

                    Color pixel = bmp.GetPixel(j, i);

                    double Pb = -0.168736 * pixel.R - 0.331264 * pixel.G + 0.5 * pixel.B;
                    double Pr = 0.5 * pixel.R - 0.418688 * pixel.G - 0.081312 * pixel.B;

                    int pixelMax = Math.Max(Math.Max(pixel.R, pixel.G), pixel.B);
                    int pixelMin = Math.Min(Math.Min(pixel.R, pixel.G), pixel.B);
                    if (pixel.G != pixelMin && (pixel.G == pixelMax || pixelMax - pixel.G < 30) && (pixelMax - pixelMin) > 70)
                    {
                        pixel = Color.White;

                        bmp.SetPixel(j, i, pixel);
                    }
                }
            }


            pictureBox1.Image = bmp;
        }

       
    }
}
