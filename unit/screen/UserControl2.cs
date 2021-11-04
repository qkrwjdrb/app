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
    public partial class UserControl2 : UserControl
    {

        public UserControl2()
        {
            InitializeComponent();
            listBox2.Items.AddRange(Form1.form1.addressItems);

        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (addGateBox.Text.Length != 0) { listBox1.Items.Add(addGateBox.Text); addGateBox.Text = String.Empty; }
            if (addDevBox.Text.Length != 0) { listBox2.Items.Add(addDevBox.Text); addDevBox.Text = String.Empty; }

            addCombobox();
        }

        private void addCombobox()
        {

            UserControl1.uc1.comboBox1.Items.Clear();
            string[] allList1 = listBox2.Items.OfType<string>().ToArray();
            UserControl1.uc1.comboBox1.Items.AddRange(allList1);

            UserControl1.uc1.comboBox2.Items.Clear();
            string[] allList2 = listBox1.Items.OfType<string>().ToArray();
            UserControl1.uc1.comboBox2.Items.AddRange(allList2);

        }


        private void button2_Click(object sender, EventArgs e)
        {


            if (listBox1.SelectedIndex != -1)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
            save(); listBox2.Items.Add(listBox2.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
            }
        }

        public void get()
        {


        }
        public void save()
        {
            const string sPath = "save.txt";

            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(sPath);
            foreach (var item in listBox2.Items)
            {
                SaveFile.WriteLine(item.ToString());
            }
            SaveFile.ToString();
            SaveFile.Close();

        }

        private void UserControl2_Load(object sender, EventArgs e)
        {
            get();
        }

    }

}


