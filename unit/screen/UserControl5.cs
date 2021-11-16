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
            comboBox1.Items.Add("06"); 
            comboBox1.Items.Add("16");

            comboBox2.Items.Add("24A16057F685");
            comboBox1.SelectedIndex = 0;

            //  comboBox2.SelectedIndex = 0;
        }

        private void UserControl5_Load(object sender, EventArgs e)
        {
            comboBox2.Items.AddRange(Form1.f1.addressItems);
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
  
            Form1.f1.TxRtu(++Form1.f1.TxCnt, 0, ulong.Parse(comboBox2.SelectedItem.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
            {
                Convert.ToByte(textBox1.Text),06,
                //Convert.ToByte(comboBox1.Text),기능코드

                (byte)(Convert.ToInt32(textBox2.Text) >> 8),  (byte)Convert.ToInt32(textBox2.Text) , 0x00, Convert.ToByte(textBox3.Text),
               //0xAD, 0xDE
             });
            //Form1.f1.TxRtu(++Form1.f1.TxCnt, 0, 0x24A16057F685, new byte[]
            //{  
            //    01,0x0,1,0x91,00,03
            //});
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
