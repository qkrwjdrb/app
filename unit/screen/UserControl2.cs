using System;
using System.IO;
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
    public partial class UserControl2 : UserControl
    {
        public static UserControl2 uc2;
        public UserControl2()
        {
            InitializeComponent();
            uc2 = this;
        }

        private void UserControl2_Load(object sender, EventArgs e)
        {
            Form1.f1.LoadUc2 = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(addGateBox.Text))
            {
                if (int.TryParse(addGateBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _))
                {
                    gatewayListBox.Items.Add(addGateBox.Text.ToUpper()); addGateBox.Text = String.Empty;
                }
                else
                {
                    MessageBox.Show("입력값을 확인하세요.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (!string.IsNullOrWhiteSpace(addDevBox.Text))
            {
                if (ulong.TryParse(addDevBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _))
                {
                    deviceListBox.Items.Add(addDevBox.Text.ToUpper()); addDevBox.Text = String.Empty;
                }
                else
                {
                    MessageBox.Show("입력값을 확인하세요.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


            Form1.f1.addCombobox();
            Form1.f1.addressSaveFile();
            Form1.f1.gatewaySaveFile();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            Form1.f1.deviceBoxIndex = 0;
            Form1.f1.getewayBoxIndex = 0;
            if (gatewayListBox.SelectedIndex != -1)
            {
                if (gatewayListBox.Items.Count >= 1)
                {
                    gatewayListBox.Items.RemoveAt(gatewayListBox.SelectedIndex);
                }
                else
                {
                    MessageBox.Show("입력값을 확인하세요.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            Form1.f1.addCombobox();
            Form1.f1.addressSaveFile();
            Form1.f1.gatewaySaveFile();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form1.f1.deviceBoxIndex = 0;
            Form1.f1.getewayBoxIndex = 0;
            if (deviceListBox.SelectedIndex != -1)
            {
                if (deviceListBox.Items.Count >= 1)
                {

                    deviceListBox.Items.RemoveAt(deviceListBox.SelectedIndex);
                }
                else
                {
                    MessageBox.Show("입력값을 확인하세요.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            Form1.f1.addCombobox();
            Form1.f1.addressSaveFile();
            Form1.f1.gatewaySaveFile();
        }
    }
}