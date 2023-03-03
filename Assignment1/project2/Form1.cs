using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace project2
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
            double a;
            double b;

            if (!Double.TryParse(textBox1.Text, out a) || !Double.TryParse(textBox2.Text, out b))
            {
                textBox3.Text = "error: numbers only";
                return;
            }

            try
            {
                if (radioButton1.Checked)
                {
                    textBox3.Text = Convert.ToString(a + b);
                }
                else if (radioButton2.Checked)
                {
                    textBox3.Text = Convert.ToString(a - b);
                }
                else if (radioButton3.Checked)
                {
                    textBox3.Text = Convert.ToString(a * b);
                }
                else if (radioButton4.Checked)
                {
                    textBox3.Text = Convert.ToString(a / b);
                }
                else
                {
                    textBox3.Text = "operator missing";
                }
            }
            catch (Exception ex)
            {
                textBox3.Text = ex.Message;
            }


        }
        
    }


}
