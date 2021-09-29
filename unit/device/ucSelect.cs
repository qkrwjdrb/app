using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace unit.device
{
    public partial class ucSelect : UserControl
    {

        public delegate void delEvent(object sender);
        static public event delEvent gatewaySender;
        static public event delEvent deviceSender;
        public static ucSelect f;
        public ucSelect()
        {

            // comboBox6.Text = Properties.Settings.Default.last_gateway;
            // comboBox7.Text = Properties.Settings.Default.last_deviceid;
            InitializeComponent();
            f = this;
        }
        public UInt32 gateway;
        public UInt64 devicead;
        private void select_Load(object sender, EventArgs e)
        {
            comboBox6.Items.Add("51894B30");
            comboBox6.Items.Add("4588177F");
            comboBox6.Items.Add("4588177F");
            //comboBox7.Items.Add(Properties.Settings.Default.last_gateway);

            comboBox7.Items.Add("24A160581869");
            comboBox7.Items.Add("24A160581B59");
            comboBox7.Items.Add(Properties.Settings.Default.last_deviceid);
            comboBox6.SelectedIndex = 0;
            comboBox7.SelectedIndex = 0;
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            gatewaySender(comboBox6);
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            deviceSender(comboBox7);
        }


    }
}
