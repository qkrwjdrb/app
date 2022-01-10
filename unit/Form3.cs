using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace unit
{
    public partial class Form3 : Form
    {
        public static Form3 f3;
        public Form3()
        {
            f3 = this;
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button bt = new Button();
            bt.Text = string.Format("{0}번 버튼", flowLayoutPanel1.Controls.Count);
        bt.Name =string.Format("_Button_{0}", flowLayoutPanel1.Controls.Count);
            bt.Click += new_Button_click;
            flowLayoutPanel1.Controls.Add(bt)
;        }

        private void new_Button_click(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }
    }
}
