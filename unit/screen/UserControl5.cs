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
            comboBox1.Items.Add("03");
            comboBox1.Items.Add("06");
            comboBox1.SelectedIndex = 0; 

          //  comboBox2.SelectedIndex = 0;
        }

        private void UserControl5_Load(object sender, EventArgs e)
        {
            comboBox2.Items.AddRange(Form1.f1.addressItems);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form1.f1.TxRtu(++Form1.f1.TxCnt, 0, ulong.Parse(comboBox2.SelectedItem.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
            {   Convert.ToByte(textBox1.Text),  Convert.ToByte(comboBox1.Text),
                 0x00,Convert.ToByte(textBox2.Text), 0x00, Convert.ToByte(textBox3.Text),
                 0xAD, 0xDE});
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
