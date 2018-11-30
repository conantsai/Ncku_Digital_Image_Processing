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
    public partial class Question2 : Form
    {
        public Question2()
        {
            InitializeComponent();
        }

        Bitmap openImg;
        Bitmap openImgMean;
        Bitmap openImgMedian;
        int[,,] buffer;

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

            openImgMean = new Bitmap(openFileDialog.FileName);
            openImgMedian = new Bitmap(openFileDialog.FileName);
            buffer = new int[3, openImg.Height, openImg.Width];

            for (int row = 0; row < openImg.Height; row++)
            {
                for (int col = 0; col < openImg.Width; col++)
                {
                    // 讀取影像平面上(x,y)的RGB資訊
                    Color RGB = openImg.GetPixel(col, row);
                    // RGB 是 VS 內建的 class 可以直接讀取影像的色彩資訊 R = Red, G = Green, B =Blue                        
                    int invR = Convert.ToInt32(RGB.R);
                    int invG = Convert.ToInt32(RGB.G);
                    int invB = Convert.ToInt32(RGB.B);

                    buffer[0, row, col] = invR;
                    buffer[1, row, col] = invG;
                    buffer[2, row, col] = invB;
                }
            }

            // Mean
            for (int row = 1; row < openImg.Height - 1; row++)
            {
                for (int col = 1; col < openImg.Width - 1; col++)
                {
                    int R = buffer[0, row - 1, col - 1] +
                            buffer[0, row - 1, col] +
                            buffer[0, row - 1, col + 1] +
                            buffer[0, row, col - 1] +
                            buffer[0, row, col] +
                            buffer[0, row, col + 1] +
                            buffer[0, row + 1, col - 1] +
                            buffer[0, row + 1, col] +
                            buffer[0, row + 1, col + 1];

                    /*int G = buffer[1, row - 1, col - 1] +
                            buffer[1, row - 1, col] +
                            buffer[1, row - 1, col + 1] +
                            buffer[1, row, col - 1] +
                            buffer[1, row, col] +
                            buffer[1, row, col + 1] +
                            buffer[1, row + 1, col - 1] +
                            buffer[1, row + 1, col] +
                            buffer[1, row + 1, col + 1];

                    int B = buffer[2, row - 1, col - 1] +
                            buffer[2, row - 1, col] +
                            buffer[2, row - 1, col + 1] +
                            buffer[2, row, col - 1] +
                            buffer[2, row, col] +
                            buffer[2, row, col + 1] +
                            buffer[2, row + 1, col - 1] +
                            buffer[2, row + 1, col] +
                            buffer[2, row + 1, col + 1];*/

                    openImgMean.SetPixel(col, row, Color.FromArgb(R/9, R/9, R/9));
                }
            }
            pictureBox1.Image = openImgMean;

            // Median
            for (int row = 1; row < openImg.Height-1; row++)
            {
                for (int col = 1; col < openImg.Width-1; col++)
                { 
                    int[] sortR = new int[9]{   buffer[0, row - 1, col - 1] ,
                                                buffer[0, row - 1, col] ,
                                                buffer[0, row - 1, col + 1] ,
                                                buffer[0, row, col - 1] ,
                                                buffer[0, row, col] ,
                                                buffer[0, row, col + 1] ,
                                                buffer[0, row + 1, col - 1] ,
                                                buffer[0, row + 1, col] ,
                                                buffer[0, row + 1, col + 1] };

                    /*int[] sortG = new int[9]{   buffer[1, row - 1, col - 1] ,
                                                buffer[1, row - 1, col] ,
                                                buffer[1, row - 1, col + 1] ,
                                                buffer[1, row, col - 1] ,
                                                buffer[1, row, col] ,
                                                buffer[1, row, col + 1] ,
                                                buffer[1, row + 1, col - 1] ,
                                                buffer[1, row + 1, col] ,
                                                buffer[1, row + 1, col + 1] };

                    int[] sortB = new int[9]{   buffer[2, row - 1, col - 1] ,
                                                buffer[2, row - 1, col] ,
                                                buffer[2, row - 1, col + 1] ,
                                                buffer[2, row, col - 1] ,
                                                buffer[2, row, col] ,
                                                buffer[2, row, col + 1] ,
                                                buffer[2, row + 1, col - 1] ,
                                                buffer[2, row + 1, col] ,
                                                buffer[2, row + 1, col + 1] };*/

                    Array.Sort(sortR);
                    //Array.Sort(sortG);
                    //Array.Sort(sortB);

                    openImgMedian.SetPixel(col, row, Color.FromArgb(sortR[4], sortR[4], sortR[4]));
                }
            }
            pictureBox2.Image = openImgMedian;
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
