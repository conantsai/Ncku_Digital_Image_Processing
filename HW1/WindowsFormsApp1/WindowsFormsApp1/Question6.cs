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
    public partial class Question6 : Form
    {
        public Question6()
        {
            InitializeComponent();
        }

        Bitmap openImg;
        Bitmap openImgVertical;
        Bitmap openImgHorizontal;
        Bitmap openImgSobel;
        Bitmap openImgThresholding;
        Bitmap openImgCombined;
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

            openImgVertical = new Bitmap(openFileDialog.FileName);
            openImgHorizontal = new Bitmap(openFileDialog.FileName);
            openImgSobel = new Bitmap(openFileDialog.FileName);
            openImgThresholding = new Bitmap(openFileDialog.FileName);
            openImgCombined = new Bitmap(openFileDialog.FileName);
            buffer = new int[openImg.Width, openImg.Height];

            int valX, valY;
            int[,] GX = new int[3, 3];
            int[,] GY = new int[3, 3];

            GX[0, 0] = -1; GX[0, 1] = 0; GX[0, 2] = 1;
            GX[1, 0] = -2; GX[1, 1] = 0; GX[1, 2] = 2;
            GX[2, 0] = -1; GX[2, 1] = 0; GX[2, 2] = 1;

            GY[0, 0] = -1; GY[0, 1] = -2; GY[0, 2] = -1;
            GY[1, 0] = 0; GY[1, 1] = 0; GY[1, 2] = 0;
            GY[2, 0] = 1; GY[2, 1] = 2; GY[2, 2] = 1;

            for (int col = 0; col < openImg.Height; col++)
            {
                for (int row = 0; row < openImg.Width; row++)
                {
                    if (col == 0 || col == openImg.Height - 1 || row == 0 || row == openImg.Width - 1)
                    {
                        //openImgVertical.SetPixel(col, row, Color.FromArgb(255,255,255));
                        valX = 0;
                        valY = 0;
                    }
                    else
                    {
                        valX = openImg.GetPixel(row - 1, col - 1).R * GX[0, 0] +
                            openImg.GetPixel(row - 1, col).R * GX[0, 1] +
                            openImg.GetPixel(row - 1, col + 1).R * GX[0, 2] +
                            openImg.GetPixel(row, col - 1).R * GX[1, 0] +
                            openImg.GetPixel(row, col).R * GX[1, 1] +
                            openImg.GetPixel(row, col + 1).R * GX[1, 2] +
                            openImg.GetPixel(row + 1, col - 1).R * GX[2, 0] +
                            openImg.GetPixel(row + 1, col).R * GX[2, 1] +
                            openImg.GetPixel(row + 1, col + 1).R * GX[2, 2];

                        valY = openImg.GetPixel(row - 1, col - 1).R * GY[0, 0] +
                            openImg.GetPixel(row - 1, col).R * GY[0, 1] +
                            openImg.GetPixel(row - 1, col + 1).R * GY[0, 2] +
                            openImg.GetPixel(row, col - 1).R * GY[1, 0] +
                            openImg.GetPixel(row, col).R * GY[1, 1] +
                            openImg.GetPixel(row, col + 1).R * GY[1, 2] +
                            openImg.GetPixel(row + 1, col - 1).R * GY[2, 0] +
                            openImg.GetPixel(row + 1, col).R * GY[2, 1] +
                            openImg.GetPixel(row + 1, col + 1).R * GY[2, 2];

                        valX = (int)Math.Abs(valX);
                        valY = (int)Math.Abs(valY);
                        if (valX < 0) valX = 0;
                        if (valX > 255) valX = 255;
                        if (valY < 0) valY = 0;
                        if (valY > 255) valY = 255;

                        int gradient = valX + valY;
                        if (gradient < 0) gradient = 0;
                        if (gradient > 255) gradient = 255;

                        
                        int input = int.Parse(textBox1.Text);

                        int thresholding = valX + valY;
                        if (thresholding < input) thresholding = 0;
                        if (thresholding > input) thresholding = 255;

                        if (thresholding == 255)
                        {
                            openImgCombined.SetPixel(row, col, Color.FromArgb(0, 255 ,0));
                        }

                        openImgVertical.SetPixel(row, col, Color.FromArgb(valY, valY, valY));
                        openImgHorizontal.SetPixel(row, col, Color.FromArgb(valX, valX, valX));
                        openImgSobel.SetPixel(row, col, Color.FromArgb(gradient, gradient, gradient));
                        openImgThresholding.SetPixel(row, col, Color.FromArgb(thresholding, thresholding, thresholding));

                    }
                }
            }

            pictureBox1.Image = openImgSobel;
            pictureBox2.Image = openImgThresholding;
            pictureBox3.Image = openImgCombined;
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

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
