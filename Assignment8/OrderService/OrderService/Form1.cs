

namespace OrderManagement
{
    
    public partial class Form1 : Form
    {
        Form2 f2;
        public static int selectedOrder;
        public static int orderId;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView2.AutoGenerateColumns = false;
            orderBindingSource.DataSource = OrderService.SearchOrder(Request.All, "");
            orderDetailsBindingSource.DataSource = null;
            orderId = 0;
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
            //OrderService.AddOrder(new Order(orderId++, 0, DateTime.Now.ToString("yyyy-MM-dd"), 0));
            f2 = new Form2(-1);
            f2.ShowDialog();
            orderBindingSource.DataSource = null; 
            orderDetailsBindingSource.DataSource = null;
            orderBindingSource.DataSource = OrderService.SearchOrder(Request.All, "");
            
            
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
            orderDetailsBindingSource.DataSource = OrderService.SearchOrderDetail(OrderService.Seq2Id(int.Parse(s)));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string s = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value.ToString();
            OrderService.DeleteOrder(int.Parse(s));
            orderBindingSource.DataSource = null;
            orderBindingSource.DataSource = OrderService.SearchOrder(Request.All, "");
            orderDetailsBindingSource.DataSource = null;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string s = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value.ToString();
            int id = OrderService.Seq2Id(int.Parse(s));
            f2 = new Form2(id);
            f2.ShowDialog();
            orderBindingSource.DataSource = null;
            orderDetailsBindingSource.DataSource = null;
            orderBindingSource.DataSource = OrderService.SearchOrder(Request.All, "");
            orderDetailsBindingSource.DataSource = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Request r = (Request)comboBox1.SelectedIndex;
                orderBindingSource.DataSource = null;


                List<SearchOrderResults> results = OrderService.SearchOrder(r, OrderService.Seq2Id(int.Parse(textBox1.Text)).ToString());
                orderBindingSource.DataSource = results;
                orderDetailsBindingSource.DataSource = null;
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}