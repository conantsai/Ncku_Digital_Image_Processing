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
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Question1 f = new Question1();
            f.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Question2 f = new Question2();
            f.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Question3 f = new Question3();
            f.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Question4 f = new Question4();
            f.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Question5 f = new Question5();
            f.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Question6 f = new Question6();
            f.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Question7 f = new Question7();
            f.Visible = true;
        }
    }
}
