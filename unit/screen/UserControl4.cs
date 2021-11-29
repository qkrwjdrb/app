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

            uc4 = this;

            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";
            var ritems = new[] {
                new { Text = "Word Read[03]", Value = 3},
         
            };

            comboBox1.DataSource = ritems;

            comboBox1.SelectedIndex = 0;
        }

        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        private void Test()
        {
            ComboboxItem item = new ComboboxItem();
            item.Text = "Item text1";
            item.Value = 12;

            comboBox1.Items.Add(item);

            comboBox1.SelectedIndex = 0;

            MessageBox.Show((comboBox1.SelectedItem as ComboboxItem).Value.ToString());
        }
        private void UserControl4_Load(object sender, EventArgs e)
        {
            Form1.f1.addCombobox();
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Form1.f1.TxRtu(++Form1.f1.TxCnt, 0, ulong.Parse(comboBox2.SelectedItem.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
                {
                    Convert.ToByte(textBox1.Text),Convert.ToByte(comboBox1.SelectedValue),
                    (byte)(Convert.ToInt32(textBox2.Text) >> 8), 
                    (byte)Convert.ToInt32(textBox2.Text) , 0x00,
                    Convert.ToByte(textBox3.Text),
                });
        }
    }
}
