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
    public partial class Question4 : Form
    {
        public Question4()
        {
            InitializeComponent();
        }

        Bitmap openImg;
        Bitmap openImgthreshold;
        int[,] buffer;

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
            openImgthreshold = new Bitmap(openFileDialog.FileName);
            buffer = new int[openImg.Height, openImg.Width];
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            for (int row = 0; row < openImg.Height; row++)
            {
                for (int col = 0; col < openImg.Width; col++)
                {
                    // 讀取影像平面上(x,y)的RGB資訊
                    Color RGB = openImg.GetPixel(col, row);
                    // RGB 是 VS 內建的 class 可以直接讀取影像的色彩資訊 R = Red, G = Green, B =Blue                        
                    int invR = Convert.ToInt32(RGB.R);
                    buffer[row, col] = invR;
                }
            }

            for (int row = 1; row < openImg.Height - 1; row++)
            {
                for (int col = 1; col < openImg.Width - 1; col++)
                {

                    if (buffer[row, col] < hScrollBar1.Value)
                         buffer[row, col] = 0;
                    else
                         buffer[row, col] = 255;
                    openImgthreshold.SetPixel(col, row, Color.FromArgb(buffer[row, col], buffer[row, col], buffer[row, col]));

                }
            }
            pictureBox3.Image = openImgthreshold;
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

        private void Question4_Load(object sender, EventArgs e)
        {

        }
    }
}
