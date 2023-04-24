using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OrderManagement
{

    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Windows.Forms;
    public partial class Form2 : Form
    {

        List<SearchOrderDetailResults> SearchOrderDetailResults;
        
        int orderId;
        int oid;
        int oldid;
        int oldseq;
        Form3 f3;
        public Form2(int id)
        {
            InitializeComponent();
            
            label4.Text = new string(DateTime.Now.ToString("yyyy-MM-dd"));
            if (id == -1) // new
            {
                
                orderId = OrderService.OrderCount() + 1;
                OrderDesignBindingSource.DataSource = null;
                textBox1.Text = "";
                comboBox1.Text = "";

            }

            else 
            {
                orderId = id;
                SearchOrderResults order = OrderService.SearchOrder(Request.ID, orderId.ToString()).First();
                OrderDesignBindingSource.DataSource = OrderService.SearchOrderDetail(orderId).ToList();
                textBox1.Text = order.OrderSeq.ToString();
                comboBox1.Text = order.CustomerName;
            }
            textBox1.Text = orderId.ToString();
            oldseq = int.Parse(textBox1.Text);
            oldid = id;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f3 = new Form3(orderId);
            f3.ShowDialog();
            
            OrderDesignBindingSource.DataSource = null;
            SearchOrderDetailResults = OrderService.SearchOrderDetail(orderId);
            OrderDesignBindingSource.DataSource = SearchOrderDetailResults;
            
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
                OrderService.DeleteOrderDetail(OrderService.FindOrderDetail(orderId, row.Cells[0].Value.ToString()));
            OrderDesignBindingSource.DataSource = null;
            OrderDesignBindingSource.DataSource = OrderService.SearchOrderDetail(orderId).ToList();
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
            double sum = 0;
            
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                sum += double.Parse(row.Cells[3].Value.ToString());

            }
            try
            {
                int seq = 0;
                seq = int.Parse(textBox1.Text);
                OrderService.CheckSeq(seq, oldseq);
                if (oldid == -1)
                {

                
                    OrderService.AddOrder(new Order(orderId, label4.Text, sum, seq));
                    OrderService.AddCustomer(new Customer(orderId, comboBox1.Text));
                }
                else 
                { 


                    OrderService.UpdateOrder(new Order(orderId, label4.Text, sum, oldseq), seq);
                    OrderService.UpdateCustomer(new Customer(orderId, comboBox1.Text));
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            
            
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            if (rows.Count == 0)
            {
                MessageBox.Show("please select a order detail to update");
                return;
            }
            foreach (DataGridViewRow row in rows)
                OrderService.DeleteOrderDetail(OrderService.FindOrderDetail(orderId, row.Cells[0].Value.ToString()));
            f3 = new Form3(orderId);
            f3.ShowDialog();
            OrderDesignBindingSource.DataSource = null;
            OrderDesignBindingSource.DataSource = OrderService.SearchOrderDetail(orderId).ToList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
