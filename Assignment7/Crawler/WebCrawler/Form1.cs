namespace WebCrawler
{
    public partial class Form1 : Form
    {
        Crawler crawler = new Crawler();
        bool stop = false;
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
            Thread t = new Thread(StartCrawling);
            t.Start();
            ShowProgress();
            
            t.Join();
            stop = false;


        }
        private void StartCrawling()
        {
            crawler.Crawl();
            stop = true;
        }

        private void ShowProgress()
        {
            while (!stop)
            {
                 richTextBox1.Text = crawler.Message;   
            }
            richTextBox1.Text = crawler.Message;
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}