

namespace OrderService
{
    using OrderManagement;
    public partial class Form1 : Form
    {
        Form2 f2;
        public static int selectedOrder;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView2.AutoGenerateColumns = false;
            orderBindingSource.DataSource = OrderService.Orders;
            orderDetailsBindingSource.DataSource = null;
        }


      

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OrderService.AddOrder(new Order(-1, new Customer(1, "harry"), new List<OrderDetail>()));
            selectedOrder = OrderService.Orders.Count - 1;
            f2 = new Form2();
            f2.ShowDialog();
            orderBindingSource.DataSource = null; 
            orderDetailsBindingSource.DataSource = null;
            orderBindingSource.DataSource = OrderService.Orders;
            orderDetailsBindingSource.DataSource = OrderService.Orders[selectedOrder].OrderDetails;

        }

        private void tableLayoutPanel1_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void OrderBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

       

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //string s = Convert.ToString(this.dataGridView2[0, dataGridView2.CurrentCell.RowIndex].Value);
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView2.Rows.Count)
                return;
             string s = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            orderDetailsBindingSource.DataSource = null;
            orderDetailsBindingSource.DataSource = OrderService.Search(Request.ID, s).First().OrderDetails;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string s = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value.ToString();
            OrderService.DeleteOrder(int.Parse(s));
            orderBindingSource.DataSource = null;
            orderBindingSource.DataSource = OrderService.Orders;
            orderDetailsBindingSource = null;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string s = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value.ToString();
            selectedOrder = OrderService.FindOrder(s);
            f2 = new Form2();
            f2.ShowDialog();
            orderBindingSource.DataSource = null;
            orderDetailsBindingSource.DataSource = null;
            orderBindingSource.DataSource = OrderService.Orders;
            orderDetailsBindingSource.DataSource = OrderService.Orders[selectedOrder].OrderDetails;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Request r = (Request)comboBox1.SelectedIndex;
            orderBindingSource.DataSource = OrderService.Search(r, textBox1.Text).ToList<Order>();
            orderDetailsBindingSource.DataSource = null;


        }
    }
}