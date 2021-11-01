using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Protobuf;
using Grpc.Core;
using Grpc.Net.Client;
using NetExchange;


namespace unit.screen
{
    public partial class UserControl4 : UserControl
    {
      


        public static UserControl4 uc4;
        public UserControl4()
        {
            InitializeComponent();
        

         //   Task.Run(() => RtuMessageService());
            comboBox1.Items.Add("24A16057F685");
            comboBox1.Items.Add("500291AEBCD9");
            comboBox1.Items.Add("500291AEBE4D");
            comboBox1.SelectedIndex = 0;
          uc4 = this;
        }
        
               

                private void UserControl4_Load(object sender, EventArgs e)
                {

                }

                private void button1_Click(object sender, EventArgs e)
                {
                    string aa = comboBox1.SelectedItem.ToString();
                    ulong device = ulong.Parse(aa, System.Globalization.NumberStyles.HexNumber);
            UserControl1.uc1.TxRtu(++UserControl1.uc1.TxCnt, 0, device, new byte[] {   0x01, 0x03,
                  0x00,Convert.ToByte(textBox1.Text), 0x00, Convert.ToByte(textBox2.Text),
                 0xAD, 0xDE});
                }

        }
}
