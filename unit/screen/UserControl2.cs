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
            if (Form1.f1.deFile.Exists)
            {
                Form1.f1.addressLoadFile();
            }
            else
            {
                string[] aa = { "24A16057F685", "500291AEBCD9", "500291AEBE4D" };
                listBox2.Items.AddRange(aa);
            }
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
      
        private void button1_Click(object sender, EventArgs e)
        {
        
            if (addDevBox.Text.Length != 0 &&  notEmptyTest(addGateBox.Text)) listBox2.Items.Add(addDevBox.Text); addDevBox.Text = String.Empty; 
            if (addGateBox.Text.Length != 0 && notEmptyTest(addGateBox.Text)) listBox1.Items.Add(addGateBox.Text); addGateBox.Text = String.Empty; 

            addCombobox();
            Form1.f1.addressSaveFile();
        }
        bool notEmptyTest(string s)
        {
            if (String.IsNullOrEmpty(s))
               return true;
            else
                return false;
        }
        public void addCombobox()
        {

            UserControl1.uc1.comboBox1.Items.Clear();
            string[] allList1 = listBox2.Items.OfType<string>().ToArray();
            UserControl1.uc1.comboBox1.Items.AddRange(allList1);

            UserControl1.uc1.comboBox2.Items.Clear();
            string[] allList2 = listBox1.Items.OfType<string>().ToArray();    
            UserControl1.uc1.comboBox2.Items.AddRange(allList2);
            Form1.f1.addressItems = allList2;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (listBox1.SelectedIndex != -1)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
            addCombobox();
            Form1.f1.addressSaveFile();
        
        }
        private void button3_Click(object sender, EventArgs e)
        {

            if (listBox2.SelectedIndex != -1)
            {
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
            }
            addCombobox(); Form1.f1.addressSaveFile();
        }

        private void addDevBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back) ||
                  "ABCDEF0123456789abcdef".IndexOf(e.KeyChar) != -1))
            {
                e.Handled = true;
            }
        }

        private void addGateBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back) ||
      "ABCDEF0123456789abcdef".IndexOf(e.KeyChar) != -1)) 
            {
                e.Handled = true;
            }
        }
    }

}