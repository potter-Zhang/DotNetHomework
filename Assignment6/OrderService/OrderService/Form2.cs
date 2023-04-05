using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderService
{
    using OrderManagement;
    public partial class Form2 : Form
    {
       
        public Form2()
        {
            InitializeComponent();
            //OrderDetails = OrderService.Orders[OrderService.Orders.Count - 1].OrderDetails as BindingList<OrderDetail>;
            dataGridView1.AutoGenerateColumns = false;
            OrderDesignBindingSource.DataSource = OrderService.Orders[Form1.selectedOrder].OrderDetails;
            label4.Text = DateTime.Now.ToString("yyyy-MM-dd");
            textBox1.Text = OrderService.Orders[Form1.selectedOrder].ID.ToString();
            comboBox1.Text = OrderService.Orders[Form1.selectedOrder].CustomerName;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrderDetail detail = new OrderDetail(new Good("default", 0), 0);
            OrderService.AddOrderDetail(-1, detail);
            OrderDesignBindingSource.DataSource = null;
            OrderDesignBindingSource.DataSource = OrderService.Orders[Form1.selectedOrder].OrderDetails;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
                OrderService.DeleteOrderDetail(-1, row.Cells[0].Value as string);
            OrderDesignBindingSource.DataSource = null;
            OrderDesignBindingSource.DataSource = OrderService.Orders[Form1.selectedOrder].OrderDetails;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Order ID is missing!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = int.Parse(textBox1.Text);
            if (id < 0)
            {
                MessageBox.Show("Order ID must not be negative!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox1.Text == "")
            {
                MessageBox.Show("Customer is missing!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var order = OrderService.Orders[OrderService.Orders.Count - 1];
            order.ID = int.Parse(textBox1.Text);
            order.Customers.Name = comboBox1.Text;
            order.Customers.Id = comboBox1.SelectedIndex;
            order.Date = label4.Text;
            
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void orderDetailBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
