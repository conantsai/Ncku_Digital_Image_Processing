using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Question1 : Form
    {
        public Question1()
        {
            InitializeComponent();
        }

        Bitmap openImg;
        Bitmap openImgR;
        Bitmap openImgG;
        Bitmap openImgB;
        Bitmap openImgGray;

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog1.InitialDirectory = "C:";
            openFileDialog.Filter = "All Files|*.*|Bitmap Files (.bmp)|*.bmp|Jpeg File(.jpg)|*.jpg";
            // 選擇我們需要開檔的類型
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            { // 如果成功開檔
                openImg = new Bitmap(openFileDialog.FileName);
                // 宣告存取影像的 bitmap
                pictureBox5.Image = openImg;
                // 讀取的影像展示到 pictureBox
            }

            openImgR = new Bitmap(openFileDialog.FileName);
            openImgG = new Bitmap(openFileDialog.FileName);
            openImgB = new Bitmap(openFileDialog.FileName);
            openImgGray = new Bitmap(openFileDialog.FileName);
            for (int y = 0; y < openImg.Height; y++)
            {
                for (int x = 0; x < openImg.Width; x++)
                {
                    // 讀取影像平面上(x,y)的RGB資訊
                    Color RGB = openImg.GetPixel(x, y);

                    // RGB 是 VS 內建的 class 可以直接讀取影像的色彩資訊 R = Red, G = Green, B =Blue                        
                    int invR = Convert.ToInt32(RGB.R);
                    int invG = Convert.ToInt32(RGB.G);
                    int invB = Convert.ToInt32(RGB.B);

                    //Apply conversion equation
                    byte gray = (byte)(.21 * invR + .71 * invG + .071 * invB);

                    openImgGray.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                    openImgR.SetPixel(x, y, Color.FromArgb(invR, invR, invR));
                    openImgG.SetPixel(x, y, Color.FromArgb(invG, invG, invG));
                    openImgB.SetPixel(x, y, Color.FromArgb(invB, invB, invB));
                }
            }
            pictureBox1.Image = openImgR;
            pictureBox2.Image = openImgG;
            pictureBox3.Image = openImgB;
            pictureBox4.Image = openImgGray;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "All Files|*.*|Bitmap Files (.bmp)|*.bmp|Jpeg File(.jpg)|*.jpg";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                openImg.Save(sfd.FileName);
            }
        }
    }
}
