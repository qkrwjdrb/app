using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace unit.screen
{
    public partial class UserControl5 : UserControl
    {
        public static UserControl5 uc5;
        public UserControl5()
        {
            InitializeComponent();
            uc5 = this;
        }

        private void UserControl5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.form1.TxRtu(++Form1.form1.TxCnt, 0, 0x24A16057F685, new byte[] {   Convert.ToByte(textBox1.Text), 0x03,
                  0x00,Convert.ToByte(textBox2.Text), 0x00, Convert.ToByte(textBox3.Text),
                 0xAD, 0xDE});
        }
    }
}
