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
                    if (!gatewayListBox.Items.Contains(addGateBox.Text.ToUpper()))
                    {


                    gatewayListBox.Items.Add(addGateBox.Text.ToUpper()); addGateBox.Text = String.Empty;
                    }
                    else
                    {
                        MessageBox.Show("Gateway Address 중복.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        addGateBox.Text = String.Empty;
                    }
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
                    if (!deviceListBox.Items.Contains(addDevBox.Text.ToUpper()))
                    {
                        deviceListBox.Items.Add(addDevBox.Text.ToUpper()); addDevBox.Text = String.Empty;
                    }
                    else
                    {
                        MessageBox.Show("Device Address 중복.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        addDevBox.Text = String.Empty;
                    }
                }
                else
                {
                    MessageBox.Show("입력값을 확인하세요.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            addCombobox();
            addressSaveFile();
            gatewaySaveFile();
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
                    MessageBox.Show(".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            addCombobox();
            addressSaveFile();
            gatewaySaveFile();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            addListBox();
            addCombobox();
            addressSaveFile();
            gatewaySaveFile();
            listMake();
        }
        public void addressSaveFile()
        {
            if (!Form1.f1.deFile.Exists)
            {
                Form1.f1.addressItems = new string[] { "24A16057F685", "500291AEBCD9", "500291AEBE4D" };
            }

            FileStream fs = new FileStream("device.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            foreach (string i in deviceListBox.Items.OfType<string>().ToArray())
            {
                sw.Write(i);
                sw.Write(',');
            }
            sw.Close(); addressLoadFile();
        }
        public void addressLoadFile()
        {
            FileStream fs = new FileStream("device.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            string a = sr.ReadToEnd();
            string[] dataArray = a.Split(',');
            dataArray = dataArray.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            deviceListBox.Items.Clear();
            //UserControl1.uc1.comboBox1.Items.Clear();
            deviceListBox.Items.AddRange(dataArray);
            addCombobox();
            sr.Close();
        }
        public void gatewaySaveFile()
        {
            if (!Form1.f1.gaFile.Exists)
            {
                Form1.f1.gatewayItems = new string[] { "0" };
            }


            FileStream fs = new FileStream("gateway.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            foreach (string i in gatewayListBox.Items.OfType<string>().ToArray())
            {
                sw.Write(i);
                sw.Write(',');
            }
            sw.Close();
            gatewayLoadFile();
        }
        public void gatewayLoadFile()
        {

            FileStream fs = new FileStream("gateway.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            string a = sr.ReadToEnd();
            string[] dataArray = a.Split(',');
            dataArray = dataArray.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            gatewayListBox.Items.Clear();
            //UserControl1.uc1.comboBox2.Items.Clear();
            gatewayListBox.Items.AddRange(dataArray);
            addCombobox();
            sr.Close();
        }
        public void addCombobox()
        {
            string[] allList1 = gatewayListBox.Items.OfType<string>().ToArray();
            string[] allList2 = deviceListBox.Items.OfType<string>().ToArray();

            //gateway combobox
            UserControl1.uc1.gatewayBox.Items.Clear();
            UserControl1.uc1.gatewayBox.Items.AddRange(allList1);
            UserControl4.uc4.gatewayBox.Items.Clear();
            UserControl4.uc4.gatewayBox.Items.AddRange(allList1);
            UserControl5.uc5.gatewayBox.Items.Clear();
            UserControl5.uc5.gatewayBox.Items.AddRange(allList1);
            UserControl6.uc6.gatewayBox.Items.Clear();
            UserControl6.uc6.gatewayBox.Items.AddRange(allList1);
            UserControl7.uc7.gatewayBox.Items.Clear();
            UserControl7.uc7.gatewayBox.Items.AddRange(allList1);

            //device combobox
            UserControl1.uc1.deviceBox.Items.Clear();
            UserControl1.uc1.deviceBox.Items.AddRange(allList2);
            UserControl4.uc4.deviceBox.Items.Clear();
            UserControl4.uc4.deviceBox.Items.AddRange(allList2);
            UserControl5.uc5.deviceBox.Items.Clear();
            UserControl5.uc5.deviceBox.Items.AddRange(allList2);
            UserControl6.uc6.deviceBox.Items.Clear();
            UserControl6.uc6.deviceBox.Items.AddRange(allList2);
            UserControl7.uc7.deviceBox.Items.Clear();
            UserControl7.uc7.deviceBox.Items.AddRange(allList2);

            Form1.f1.gatewayItems = allList1;
            Form1.f1.addressItems = allList2;
            Form1.f1.comboboxSelect();
        }
        Dictionary<string, string> listde = new Dictionary<string, string>();
        private void addListBox()
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
                    MessageBox.Show(".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void listMake()
        {
            listde.Clear();
            for (int i = 0; i < deviceListBox.Items.Count; i++)
            {

                listde.Add(Convert.ToString(deviceListBox.Items[i]), Convert.ToString(deviceListBox.Items[i]));

            }
        }
    }
}