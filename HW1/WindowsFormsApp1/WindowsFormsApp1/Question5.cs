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
    public partial class Question5 : Form
    {
        public Question5()
        {
            InitializeComponent();
        }

        Bitmap openImg;
        Bitmap openImgVertical;
        Bitmap openImgHorizontal;
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
            openImgCombined = new Bitmap(openFileDialog.FileName);
            buffer = new int[openImg.Width, openImg.Height];

            int valX, valY;
            int [,]GX = new int[3, 3];
            int [,]GY = new int[3, 3];

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
                  if(col == 0 || col == openImg.Height - 1 || row == 0 || row == openImg.Width - 1)
                    {
                        //openImgVertical.SetPixel(col, row, Color.FromArgb(255,255,255));

                        valX = 0;
                        valY = 0;
                    }
                    else
                    {
                        valX = openImg.GetPixel(row - 1, col - 1).R * GX[0, 0] +
                            openImg.GetPixel(row - 1, col ).R * GX[0, 1] +
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

                        openImgVertical.SetPixel(row, col, Color.FromArgb(valY, valY, valY));
                        openImgHorizontal.SetPixel(row, col, Color.FromArgb(valX, valX, valX));
                        openImgCombined.SetPixel(row, col, Color.FromArgb(gradient, gradient, gradient));
                    }

                }
            }

            pictureBox1.Image = openImgVertical;
            pictureBox2.Image = openImgHorizontal;
            pictureBox3.Image = openImgCombined;

            /*int[] mask1 ={  -1,0,1,
                            -2,0,2,
                            -1,0,1};

            int[] mask2 ={  -1,-2,-1,
                            0,0,0,
                            1,2,1};

            for (int row = 1; row < openImg.Height - 1; row++)
            {
                for (int col = 1; col < openImg.Width - 1; col++)
                {
                    int sobelVR = 0;
                    int sobelHR = 0;
                    int sobelG = 0;
                    int sobelB = 0;

                    int[] sortR = new int[9]{   buffer[0, row - 1, col - 1] ,
                                                buffer[0, row, col - 1] ,
                                                buffer[0, row + 1, col - 1] ,
                                                buffer[0, row - 1, col] ,
                                                buffer[0, row, col] ,
                                                buffer[0, row + 1, col] ,
                                                buffer[0, row - 1, col + 1] ,
                                                buffer[0, row, col + 1] ,
                                                buffer[0, row + 1, col + 1] };

                    for (int i = 1; i < 9; i++)
                    {
                        sobelVR += sortR[i] * mask1[i];
                        if (sobelVR < 0)
                            sobelVR = 0;
                        else if (sobelVR > 255)
                            sobelVR = 255;

                        sobelHR += sortR[i] * mask2[i];
                        if (sobelHR < 0)
                            sobelHR = 0;
                        else if (sobelHR > 255)
                            sobelHR = 255;
                    }

                    openImgVertical.SetPixel(col, row, Color.FromArgb(sobelVR, sobelVR, sobelVR));
                    openImgHorizontal.SetPixel(col, row, Color.FromArgb(sobelHR, sobelHR, sobelHR));

                }
            }
            pictureBox1.Image = openImgVertical;
            pictureBox2.Image = openImgHorizontal;*/

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
