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

            comboBox3.DisplayMember = "Text";
            comboBox3.ValueMember = "Value";
            var ritems = new[] {
                new { Text = "Word Read[03]", Value = 3},
         
            };

            comboBox3.DataSource = ritems;

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
      /*  private void Test()
        {
            ComboboxItem item = new ComboboxItem();
            item.Text = "Item text1";
            item.Value = 12;

            comboBox1.Items.Add(item);

            comboBox1.SelectedIndex = 0;

            MessageBox.Show((comboBox1.SelectedItem as ComboboxItem).Value.ToString());
        }*/
        private void UserControl4_Load(object sender, EventArgs e)
        {
            Form1.f1.addCombobox();
            comboBox3.SelectedIndex = 0; 
            deviceBox.SelectedIndex = 0; 
            gatewayBox.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(gatewayBox.SelectedItem.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture,out _) && !string.IsNullOrWhiteSpace(textBox3.Text) && !string.IsNullOrWhiteSpace(gatewayBox.Text) && !string.IsNullOrWhiteSpace(deviceBox.Text) && !string.IsNullOrWhiteSpace(comboBox3.Text))
            {
                Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.SelectedItem.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.SelectedItem.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
                {
                    Convert.ToByte(textBox1.Text),Convert.ToByte(comboBox3.SelectedValue),
                    (byte)(Int32.Parse(textBox2.Text) >> 8),
                    (byte)Int32.Parse(textBox2.Text) , 0x00,
                    byte.Parse(textBox3.Text),
                });
            }
            else if (string.IsNullOrWhiteSpace(textBox1.Text)) MessageBox.Show("Slave Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(textBox2.Text)) MessageBox.Show("Start Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(textBox3.Text)) MessageBox.Show("Length를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(gatewayBox.Text)) MessageBox.Show("Gateway Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(deviceBox.Text)) MessageBox.Show("Device Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(comboBox3.Text)) MessageBox.Show("Commend를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }
    }
}
