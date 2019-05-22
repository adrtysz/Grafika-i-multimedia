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
using AForge.Video;
using AForge.Video.DirectShow;

namespace video
{
    public partial class Form1 : Form
    {
        public event NewFrameEventHandler NewFrame;
        public Bitmap bmp = new Bitmap(@"C:\Users\Adżi\source\repos\video\video\image.png");
        public Form1()
        {
            InitializeComponent();
        }


        public Image SetImageOpacity(Image image, float opacity)
        {
            try
            {
                //create a Bitmap the size of the image provided  
                Bitmap bmp2 = new Bitmap(image.Width, image.Height);

                //create a graphics object from the image  
                Graphics gfx = Graphics.FromImage(bmp2);

                //create a color matrix object  
                ColorMatrix matrix = new ColorMatrix();

                //set the opacity  
                matrix.Matrix33 = opacity;

                //create image attributes  
                ImageAttributes attributes = new ImageAttributes();

                //set the color(opacity) of the image  
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                //now draw the image  
                gfx.DrawImage(image, new Rectangle(0, 0, bmp2.Width, bmp2.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                gfx.Dispose();
                
                return bmp2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }






        private void videoSourcePlayer1_NewFrame(object sender, NewFrameEventArgs eventArgs )
        {
            // get new frame
            Pen blackPen = new Pen(Color.Black, 3);
            Bitmap bitmap = eventArgs.Frame;
            // process the frames
            
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(SetImageOpacity(bmp,0.7f), bitmap.Width-bmp.Width, bitmap.Height-bmp.Height);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileVideoSource fileVideo = new FileVideoSource(@"C:\Users\Adżi\source\repos\video\video\film1.avi");


            fileVideo.NewFrame += new NewFrameEventHandler(videoSourcePlayer1_NewFrame);
            videoSourcePlayer1.VideoSource = fileVideo;
            videoSourcePlayer1.Start();
        }
    }
}
