namespace Crawler
{
    public partial class Form1 : Form
    {
        Crawler crawler = new Crawler();
        public Form1()
        {
            InitializeComponent();
        }

        bool Check(string url)
        {
            if (url == null || url == "") 
            {
                MessageBox.Show("please enter a url");
                return false;
            }

            if (url.Substring(0, 8) != "https://")
            {
                textBox1.Text = "https://" + textBox1.Text;
            }

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Check(textBox1.Text))
                return;
            crawler.Add(textBox1.Text);
            crawler.SetPath(textBox2.Text);
            MessageBox.Show(crawler.Crawl());
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

      

        private void textBox2_MouseDown(object sender, MouseEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox2.Text = fbd.SelectedPath;
            }
            else
            {
                textBox2.Text = "";
            }
        }
    }
}