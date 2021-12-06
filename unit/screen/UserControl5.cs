﻿using System;
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
            /* comboBox1.Items.Add("06"); 
             comboBox1.Items.Add("16");*/

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
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) && !string.IsNullOrWhiteSpace(gatewayBox.Text) && !string.IsNullOrWhiteSpace(deviceBox.Text) && !string.IsNullOrWhiteSpace(comboBox3.Text))
            {
                if ((int)comboBox3.SelectedValue == 16)
                {
                    string[] qwer = textBox3.Text.Split(',');
                    byte[] a = new byte[qwer.Length * 2];
                    for (int i = 0; i < qwer.Length; i++)
                    {
                        if (i == 0)
                        {
                            a[0] = Convert.ToByte(Convert.ToInt32(qwer[0]) >> 8);
                            a[1] = (byte)Convert.ToInt32(qwer[0]);
                        }
                        else
                        {
                            a[i * 2] = Convert.ToByte(Convert.ToInt32(qwer[i]) >> 8);
                            a[i * 2 + 1] = (byte)Convert.ToInt32(qwer[i]);
                        }
                    }
                    byte[] multi = { Convert.ToByte(textBox1.Text), Convert.ToByte(comboBox3.SelectedValue), (byte)(Convert.ToInt32(textBox2.Text) >> 8), (byte)Convert.ToInt32(textBox2.Text), 00, (byte)qwer.Length, (byte)a.Length };
                    byte[] pay = new byte[a.Length + multi.Length];
                    Array.Copy(multi, 0, pay, 0, multi.Length);
                    Array.Copy(a, 0, pay, multi.Length, a.Length);
                    Form1.f1.TxRtu(++Form1.f1.TxCnt, uint.Parse(gatewayBox.SelectedItem.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.SelectedItem.ToString(), System.Globalization.NumberStyles.HexNumber), pay);
                }
                else
                {
                    Form1.f1.TxRtu(++Form1.f1.TxCnt, uint.Parse(gatewayBox.SelectedItem.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.SelectedItem.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
                    {
                Convert.ToByte(textBox1.Text),Convert.ToByte(comboBox3.SelectedValue),

                (byte)(Convert.ToInt32(textBox2.Text) >> 8),  (byte)Convert.ToInt32(textBox2.Text) , 0x00, Convert.ToByte(textBox3.Text),
                    });


                }
            }
            else if (string.IsNullOrWhiteSpace(textBox1.Text)) MessageBox.Show("Slave Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(textBox2.Text)) MessageBox.Show("Start Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(textBox3.Text)) MessageBox.Show("Value를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(gatewayBox.Text)) MessageBox.Show("Gateway Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(deviceBox.Text)) MessageBox.Show("Device Address를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(comboBox3.Text)) MessageBox.Show("Commend를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
