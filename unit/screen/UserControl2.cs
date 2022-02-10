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
            string[] gatewayArray = gatewayListBox.Items.OfType<string>().ToArray();
            string[] deviceArray = deviceListBox.Items.OfType<string>().ToArray();

            //gateway combobox
            UserControl1.uc1.gatewayBox.Items.Clear();
            UserControl1.uc1.gatewayBox.Items.AddRange(gatewayArray);
            UserControl4.uc4.gatewayBox.Items.Clear();
            UserControl4.uc4.gatewayBox.Items.AddRange(gatewayArray);
            UserControl5.uc5.gatewayBox.Items.Clear();
            UserControl5.uc5.gatewayBox.Items.AddRange(gatewayArray);
            UserControl6.uc6.gatewayBox.Items.Clear();
            UserControl6.uc6.gatewayBox.Items.AddRange(gatewayArray);
            UserControl7.uc7.gatewayBox.Items.Clear();
            UserControl7.uc7.gatewayBox.Items.AddRange(gatewayArray);
            UserControl8.uc8.gatewayBox.Items.Clear();
            UserControl8.uc8.gatewayBox.Items.AddRange(gatewayArray);

            //device combobox
            UserControl1.uc1.deviceBox.Items.Clear();
            UserControl1.uc1.deviceBox.Items.AddRange(deviceArray);
            UserControl4.uc4.deviceBox.Items.Clear();
            UserControl4.uc4.deviceBox.Items.AddRange(deviceArray);
            UserControl5.uc5.deviceBox.Items.Clear();
            UserControl5.uc5.deviceBox.Items.AddRange(deviceArray);
            UserControl6.uc6.deviceBox.Items.Clear();
            UserControl6.uc6.deviceBox.Items.AddRange(deviceArray);
            UserControl7.uc7.deviceBox.Items.Clear();
            UserControl7.uc7.deviceBox.Items.AddRange(deviceArray);
            UserControl8.uc8.deviceBox.Items.Clear();
            UserControl8.uc8.deviceBox.Items.AddRange(deviceArray);

            Form1.f1.gatewayItems = gatewayArray;
            Form1.f1.addressItems = deviceArray;
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            /*        KeyValuePair<string, int> item = (KeyValuePair<string, int>)e.ListItem;
                    e.Value = string.Format("{0}({1})", item.Key, item.Value); 
                    foreach (KeyValuePair<string, int> kvp in myDictionary)
                    {
                        lbx.Items.Add(String.Format("{0}({1})", kvp.Key, kvp.Value.ToString()));
                    }*/
        }
        public partial class tester : UserControl2
        {
            public tester()
            {
                InitializeComponent();
                List<MyObject> myObjects = new List<MyObject>();
                MyObject testObject = new MyObject("A", "10");
                myObjects.Add(testObject);
                BindingSource bindingSource = new BindingSource(myObjects, null);
                listBox1.DisplayMember = "DisplayValue";
                listBox1.DataSource = bindingSource;
                MyObject myObject =new MyObject();

            }
        }

        public class MyObject
        {
            private string _key;
            private string _value;
            Dictionary<string, string> keyValuePairs;
            public MyObject( string value, string key)
            {
                _value = value;
                _key = key;
                keyValuePairs.Add(Key, value);
                keyValuePairs.Remove("A");
            }
            public MyObject()
            {

            }
            public string Key
            {
                get { return _key; }
            }

            public string Value
            {
                get { return _value; }
            }

            public string DisplayValue
            {
                get { return string.Format("{0} ({1})", _key, _value); }
                set { _value = value; }
            }
        }
    }
}