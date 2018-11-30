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
    public partial class Question3 : Form
    {
        public Question3()
        {
            InitializeComponent();
        }

        Bitmap openImg;

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

                Bitmap img = new Bitmap(openImg.Width, openImg.Height);
                int[] map = new int[256];
                int[] g_ori = new int[256];
                int[] g_trans = new int[256];
                for (int i = 0; i < openImg.Height; i++)
                {
                    for (int j = 0; j < openImg.Width; j++)
                    {
                        map[openImg.GetPixel(j, i).R]++;
                        g_ori[openImg.GetPixel(j, i).R]++;
                    }
                }

                double sum = 0;
                for (int i = 0; i < 256; i++)
                {
                    sum += (double)map[i] / (openImg.Width * openImg.Height) * 255;
                    map[i] = (int)Math.Round(sum, 0, MidpointRounding.AwayFromZero);
                }
                for (int i = 0; i < openImg.Height; i++)
                {
                    for (int j = 0; j < openImg.Width; j++)
                    {
                        img.SetPixel(j, i, Color.FromArgb(map[(int)openImg.GetPixel(j, i).R], map[(int)openImg.GetPixel(j, i).R], map[(int)openImg.GetPixel(j, i).R]));
                        g_trans[map[(int)openImg.GetPixel(j, i).R]]++;
                    }
                }
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox3.Image = img;

                for (int i = 1; i < 256; i++)
                {
                    chart1.Series["Series1"].Points.AddXY(i, g_ori[i]);
                    chart2.Series["Series2"].Points.AddXY(i, g_trans[i]);
                }
            }

            /*openImgMean = new Bitmap(openFileDialog.FileName);
            openImgMedian = new Bitmap(openFileDialog.FileName);
            buffer = new int[openImg.Height, openImg.Width];

            double[] count1 = new double[256];
            double[] count2 = new double[256];
            double[] p = new double[256];
            double[] gray_level = new double[256];
            double[] newp = new double[256];
            int[] map = new int[256];
            int[] g_trans = new int[256];

            for (int row = 0; row < openImg.Height; row++)
            {
                for (int col = 0; col < openImg.Width; col++)
                {
                    // 讀取影像平面上(x,y)的RGB資訊
                    Color RGB = openImg.GetPixel(col, row);
                    // RGB 是 VS 內建的 class 可以直接讀取影像的色彩資訊 R = Red, G = Green, B =Blue                        
                    int invR = Convert.ToInt32(RGB.R);

                    buffer[row, col] = invR;

                    for (int i = 1; i < 256; i++)  //0~255種變化 
	                {
                        if (buffer[row, col] == i) 
	                            { 
	                                count1[i] = count1[i] + 1;//累加 
	                            } 
                    }  
                }
            }
            for (int i = 1; i < 256; i++)
            {
                chart1.Series["Series1"].Points.AddXY(i, count1[i]);
            }

            double sum = 0;
            for (int i = 0; i < 256; i++)
            {
                sum += (double)map[i] / (openImg.Width * openImg.Height) * 255;
                map[i] = (int)Math.Round(sum, 0, MidpointRounding.AwayFromZero);
            }
            for (int i = 0; i < openImg.Height; i++)
            {
                for (int j = 0; j < openImg.Width; j++)
                {
                    openImgMean.SetPixel(j, i, Color.FromArgb(map[(int)openImg.GetPixel(j, i).R], map[(int)openImg.GetPixel(j, i).R], map[(int)openImg.GetPixel(j, i).R]));
                    g_trans[map[(int)openImg.GetPixel(j, i).R]]++;
                }
            }
            pictureBox3.Image = openImgMean;


            for (int i = 1; i < 256; i++)
            {
                chart2.Series["Series2"].Points.AddXY(i, g_trans[i]);
            }*/

        }

        private void chart1_Click(object sender, EventArgs e)
        {

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

