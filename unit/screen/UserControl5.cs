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

            comboBox3.DisplayMember = "Text";
            comboBox3.ValueMember = "Value";
            var items = new[] {
                new { Text = "multi Word Write[10]", Value = 16 },
                new { Text = "One Word Write[06]", Value = 06 },
            };

            comboBox3.DataSource = items;
            comboBox3.SelectedIndex = 0;
        }

        private void UserControl5_Load(object sender, EventArgs e)
        {
            Form1.f1.addCombobox();
            deviceBox.SelectedIndex = 0; gatewayBox.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text)
                && !string.IsNullOrWhiteSpace(textBox2.Text)
                && !string.IsNullOrWhiteSpace(textBox3.Text)
                && !string.IsNullOrWhiteSpace(gatewayBox.Text)
                && !string.IsNullOrWhiteSpace(deviceBox.Text)
                && !string.IsNullOrWhiteSpace(comboBox3.Text)
                )
            {
             
                if (int.TryParse(gatewayBox.SelectedItem.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _)
                    && ulong.TryParse(gatewayBox.SelectedItem.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _)
                    && byte.TryParse(textBox1.Text, out _)
                    && int.TryParse(textBox2.Text, out _)
                    )
                {

                    string[] valueString = textBox3.Text.Split(',');
                    bool valueIsNotNull = true;
                    
                    for (int i = 0; i < valueString.Length; i++) 
                    {
                        if (string.IsNullOrWhiteSpace(valueString[i]) && !int.TryParse(valueString[i], out _) )
                        { 
                            valueIsNotNull = false; 
                        }
                    }
                    byte[] valueByte = new byte[valueString.Length * 2];
                    
                    if (valueIsNotNull)
                    {
                        for (int i = 0; i < valueString.Length; i++)
                        {
                            if (i == 0)
                            {
                                valueByte[0] = (byte)(Convert.ToInt32(valueString[0]) >> 8);
                                valueByte[1] = (byte)Convert.ToInt32(valueString[0]);
                            }
                            else
                            {
                                valueByte[i * 2] = (byte)(int.Parse(valueString[i]) >> 8);
                                valueByte[i * 2 + 1] = (byte)int.Parse(valueString[i]);
                            }
                        }
                    }
                    byte[] multi = { Convert.ToByte(textBox1.Text), Convert.ToByte(comboBox3.SelectedValue), (byte)(Convert.ToInt32(textBox2.Text) >> 8), (byte)Convert.ToInt32(textBox2.Text), 00, (byte)valueString.Length, (byte)valueByte.Length };
                    byte[] pay = new byte[valueByte.Length + multi.Length];
                    Array.Copy(multi, 0, pay, 0, multi.Length);
                    Array.Copy(valueByte, 0, pay, multi.Length, valueByte.Length);
                    if (valueIsNotNull)
                    {
                        if ((int)comboBox3.SelectedValue == 16)
                        {
                            Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.SelectedItem.ToString(), System.Globalization.NumberStyles.HexNumber), pay);
                        }
                        else
                        {
                            Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.SelectedItem.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
                            {
                            Convert.ToByte(textBox1.Text),Convert.ToByte(comboBox3.SelectedValue),
                            (byte)(Convert.ToInt32(textBox2.Text) >> 8),  (byte)Convert.ToInt32(textBox2.Text) ,   valueByte[0],valueByte[1],
                            });

                        }
                    }
                    else
                    {
                        MessageBox.Show("입력값을 확인하세요.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("입력값을 확인하세요.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Slave Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Start Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Value를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(gatewayBox.Text))
            {
                MessageBox.Show("Gateway Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(deviceBox.Text))
            {
                MessageBox.Show("Device Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(comboBox3.Text))
            {
                MessageBox.Show("Commend를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
