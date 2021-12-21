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
    public partial class UserControl7 : UserControl
    {
        public static UserControl7 uc7;
        public UserControl7()
        {
            InitializeComponent();
            uc7 = this;
        }

        private void UserControl7_Load(object sender, EventArgs e)
        {
           UserControl2.uc2.addCombobox();
            deviceBox.SelectedIndex = 0;
            gatewayBox.SelectedIndex = 0;
            Form1.f1.LoadUc7 = true;
            Form1.f1.comboboxSelect();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            label1.Text = textBox9.Text;
            label2.Text = Convert.ToString(Convert.ToInt32(textBox9.Text) + 1);
            label3.Text = Convert.ToString(Convert.ToInt32(textBox9.Text) + 2);
            label4.Text = Convert.ToString(Convert.ToInt32(textBox9.Text) + 3);
            label5.Text = Convert.ToString(Convert.ToInt32(textBox9.Text) + 4);
            label6.Text = Convert.ToString(Convert.ToInt32(textBox9.Text) + 5);
            label7.Text = Convert.ToString(Convert.ToInt32(textBox9.Text) + 6);
            label8.Text = Convert.ToString(Convert.ToInt32(textBox9.Text) + 7);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(gatewayBox.Text) && !string.IsNullOrWhiteSpace(deviceBox.Text)) Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
            {
                0x01, 0x06, (byte)(Convert.ToInt32(label1.Text) >> 8),(byte)Convert.ToInt32(label1.Text),(byte)(Convert.ToInt32(textBox1.Text) >> 8),  (byte)Convert.ToInt32(textBox1.Text)
            });
            else if (string.IsNullOrWhiteSpace(gatewayBox.Text)) MessageBox.Show("Gateway Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(deviceBox.Text)) MessageBox.Show("Device Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else MessageBox.Show("Value를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox2.Text)) Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
            {
                0x01, 0x06, (byte)(Convert.ToInt32(label2.Text) >> 8),(byte)Convert.ToInt32(label2.Text),(byte)(Convert.ToInt32(textBox2.Text) >> 8),  (byte)Convert.ToInt32(textBox2.Text)
            });
            else if (string.IsNullOrWhiteSpace(gatewayBox.Text)) MessageBox.Show("Gateway Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(deviceBox.Text)) MessageBox.Show("Device Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else MessageBox.Show("Value를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox3.Text)) Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
            {
                0x01, 0x06, (byte)(Convert.ToInt32(label3.Text) >> 8),(byte)Convert.ToInt32(label3.Text),(byte)(Convert.ToInt32(textBox3.Text) >> 8),  (byte)Convert.ToInt32(textBox3.Text)
            });
            else if (string.IsNullOrWhiteSpace(gatewayBox.Text)) MessageBox.Show("Gateway Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(deviceBox.Text)) MessageBox.Show("Device Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else MessageBox.Show("Value를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox4.Text)) Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
            {
                0x01, 0x06, (byte)(Convert.ToInt32(label4.Text) >> 8),(byte)Convert.ToInt32(label4.Text),(byte)(Convert.ToInt32(textBox4.Text) >> 8),  (byte)Convert.ToInt32(textBox4.Text)
            });
            else if (string.IsNullOrWhiteSpace(gatewayBox.Text)) MessageBox.Show("Gateway Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(deviceBox.Text)) MessageBox.Show("Device Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else MessageBox.Show("Value를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox5.Text)) Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
            {
                0x01, 0x06, (byte)(Convert.ToInt32(label5.Text) >> 8),(byte)Convert.ToInt32(label5.Text),(byte)(Convert.ToInt32(textBox5.Text) >> 8),  (byte)Convert.ToInt32(textBox5.Text)
            });
            else if (string.IsNullOrWhiteSpace(gatewayBox.Text)) MessageBox.Show("Gateway Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(deviceBox.Text)) MessageBox.Show("Device Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else MessageBox.Show("Value를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox6.Text)) Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
            {
                0x01, 0x06, (byte)(Convert.ToInt32(label6.Text) >> 8),(byte)Convert.ToInt32(label6.Text),(byte)(Convert.ToInt32(textBox6.Text) >> 8),  (byte)Convert.ToInt32(textBox6.Text)
            });
            else if (string.IsNullOrWhiteSpace(gatewayBox.Text)) MessageBox.Show("Gateway Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(deviceBox.Text)) MessageBox.Show("Device Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else MessageBox.Show("Value를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox7.Text)) Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
            {
                0x01, 0x06, (byte)(Convert.ToInt32(label7.Text) >> 8),(byte)Convert.ToInt32(label7.Text),(byte)(Convert.ToInt32(textBox7.Text) >> 8),  (byte)Convert.ToInt32(textBox7.Text)
            });
            else if (string.IsNullOrWhiteSpace(gatewayBox.Text)) MessageBox.Show("Gateway Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(deviceBox.Text)) MessageBox.Show("Device Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else MessageBox.Show("Value를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox8.Text)) Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
            {
                0x01, 0x06, (byte)(Convert.ToInt32(label8.Text) >> 8),(byte)Convert.ToInt32(label8.Text),(byte)(Convert.ToInt32(textBox8.Text) >> 8),  (byte)Convert.ToInt32(textBox8.Text)
            });
            else if (string.IsNullOrWhiteSpace(gatewayBox.Text)) MessageBox.Show("Gateway Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(deviceBox.Text)) MessageBox.Show("Device Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else MessageBox.Show("Value를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void gatewayBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form1.f1.getewayBoxIndex = gatewayBox.SelectedIndex;
        }
        private void deviceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form1.f1.deviceBoxIndex = deviceBox.SelectedIndex;
        }
    }
}
