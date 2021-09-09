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
    public partial class uc7Segmant : UserControl

    {
        // public DataPushEventHandler DataSendEvent;
        //Form1 f1;
        //public delegate void DataPushEventHandler(object value);
        //public event DataPushEventHandler SendEvent;

        public delegate int delEvent(string strText);
        static public event delEvent eventdelSender;

        public uc7Segmant()
        {
            InitializeComponent();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            // TxMbRtu(0, gateway, devicead, new byte[] { 0x01, 0x10, 0x01, 0x9E, 0x00, 0x02, 0x04, segmentData[1], segmentData[2], 0x00, segmentData[0], 0xAD, 0xDE });

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //f1.richTextBox2.Text = this.textBox12.Text;
            //Form1 form1 = new Form1(textBox12.Text);
            //Form1.ShowDialog();
            //this.SendEvent(textBox12.Text);
            eventdelSender(textBox12.Text);
        }

        private void ucScreen2_Load(object sender, EventArgs e)
        {

        }
    }
}
