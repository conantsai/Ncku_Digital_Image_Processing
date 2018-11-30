using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace WindowsFormsApp1
{
    public partial class Question7 : Form
    {
        public Question7()
        {
            InitializeComponent();
        }

        Bitmap openImg;
        Bitmap newImg = null;
        List<List<string>> listList = new List<List<string>>();

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog1.InitialDirectory = "C:";
            openFileDialog.Filter = "All Files|*.*|Bitmap Files (.bmp)|*.bmp|Jpeg File(.jpg)|*.jpg";
            // 選擇我們需要開檔的類型
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            { // 如果成功開檔
                openImg = new Bitmap(openFileDialog.FileName);
                newImg = openImg.Clone(new Rectangle(0, 0, openImg.Width, openImg.Height), PixelFormat.Format24bppRgb);
                // 宣告存取影像的 bitmap
                pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox5.Image = openImg;
                // 讀取的影像展示到 pictureBox
            }

            int count = 0;
            for (int i = 0; i < openImg.Height; i++)
            {
                for (int j = 0; j < openImg.Width; j++)
                {
                    if ((int)newImg.GetPixel(j, i).R == 0)
                    {
                        regions(i, j);
                        count++;
                    }
                }
            }
            label4.Text = "Numof Connected region : " + count.ToString();
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.Image = newImg;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "All Files|*.*|Bitmap Files (.bmp)|*.bmp|Jpeg File(.jpg)|*.jpg";

        }

        private void regions(int i, int j)
        {
            if ((int)newImg.GetPixel(j, i).R == 255) { return; }
            else if ((int)newImg.GetPixel(j, i).R == 0)
            {
                
                Random rnd = new Random();
                newImg.SetPixel(j, i, Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)));

                if (i > 0) { regions(i - 1, j); }
                if (j > 0) { regions(i, j - 1); }
                if (i > 0 && j > 0) { regions(i - 1, j - 1); }
                if (i < openImg.Height - 1) { regions(i + 1, j); }
                if (j < openImg.Width - 1) { regions(i, j + 1); }
                if (i < openImg.Height - 1 && j < openImg.Width - 1) { regions(i + 1, j + 1); }
                if (i < openImg.Height - 1 && j > 0) { regions(i + 1, j - 1); }
                if (i > 0 && j < openImg.Width - 1) { regions(i - 1, j + 1); }

                //Random rnd = new Random();
                //newImg.SetPixel(j, i, Color.FromArgb(rnd.Next(1, 256), rnd.Next(256), rnd.Next(256)));
                // newImg.SetPixel(j, i, Color.FromArgb(255, 0, 255));
            }
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
