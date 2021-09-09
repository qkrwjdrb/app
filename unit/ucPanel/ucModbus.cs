using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace unit.ucPanel
{
    public partial class ucModbus : UserControl
    {
        public delegate void delEvent(object a, object b, object c, object d, string e);
        static public event delEvent eventModSender;
        public ucModbus()
        {
            InitializeComponent();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void ucScreen1_Load(object sender, EventArgs e)
        {
            funBox.Items.Add("10");
            funBox.Items.Add("01");
            funBox.Items.Add("03");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            eventModSender(
            slaBox.Text,
            funBox.Text,
            staBox.Text,
            lenBox.Text,
            crcBox.Text);
        }
    }
}
