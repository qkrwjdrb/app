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


        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (addDevBox.Text.Length != 0) deviceListBox.Items.Add(addDevBox.Text); addDevBox.Text = String.Empty;
            if (addGateBox.Text.Length != 0) gatewayListBox.Items.Add(addGateBox.Text); addGateBox.Text = String.Empty;

            Form1.f1.addCombobox();
            Form1.f1.addressSaveFile(); 
            Form1.f1.gatewaySaveFile();
        }

  

        private void button2_Click(object sender, EventArgs e)
        {

            if (gatewayListBox.SelectedIndex != -1)
            {
                gatewayListBox.Items.RemoveAt(gatewayListBox.SelectedIndex);
            }
            Form1.f1.addCombobox();
            Form1.f1.addressSaveFile();
            Form1.f1.gatewaySaveFile();

        }
        private void button3_Click(object sender, EventArgs e)
        {

            if (deviceListBox.SelectedIndex != -1)
            {
                deviceListBox.Items.RemoveAt(deviceListBox.SelectedIndex);
            }
            Form1.f1.addCombobox();
            Form1.f1.addressSaveFile();
            Form1.f1.gatewaySaveFile();
        }

        private void addDevBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back) || "ABCDEF0123456789abcdef".IndexOf(e.KeyChar) != -1))
            {
                e.Handled = true;
            }
        }

        private void addGateBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back) || "ABCDEF0123456789abcdef".IndexOf(e.KeyChar) != -1))
            {
                e.Handled = true;
            }
        }
    }
}