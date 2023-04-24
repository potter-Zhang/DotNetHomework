
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderManagement
{
    
    using System.Linq.Expressions;

    public partial class Form3 : Form
    {
        public static OrderDetail detail;
        public static Good good;
        static int orderId;
        public static int did;
        public Form3(int oId)
        {
            InitializeComponent();
            orderId = oId;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                good = new Good(did, orderId, textBox1.Text, double.Parse(textBox2.Text));
                int num = int.Parse(textBox4.Text);
                detail = new OrderDetail(orderId, did++, num, good.Price * num);
                OrderService.CheckGood(good);
                OrderService.AddGood(good);
                OrderService.AddOrderDetail(detail);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
