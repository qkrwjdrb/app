using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;

namespace unit.screen
{
    public partial class UserControl8 : UserControl
    {
        public static UserControl8 uc8;
        public UserControl8()
        {
            InitializeComponent();
            uc8 = this;
        }
        private void UserControl8_Load(object sender, EventArgs e)
        {
            UserControl2.uc2.addCombobox();
            /*          deviceBox.SelectedIndex = 0;
                      gatewayBox.SelectedIndex = 0;*/
            Form1.f1.LoadUc8 = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            addControl();
        }
        private void addControl()
        {
            Button bt = new Button();
            bt.Text = string.Format("{0}번 버튼", flowLayoutPanel1.Controls.Count + 1);
            bt.Name = string.Format("_Button_{0}", flowLayoutPanel1.Controls.Count + 1);
            bt.Click += new_Button_click;
            flowLayoutPanel1.Controls.Add(bt);
        }

        private void new_Button_click(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            screenInfo();
        }
        public bool uc8rtu = false;
        void screenInfo()
        {
            //  UserControl6.uc6.checkBox2.Checked = false;

            Form1.f1.TxRtu(
             0,
                (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber),
                ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber),
                new byte[] { 0x01, 0x03, 0x00, 1, 0x00, 8, }
                );
            uc8rtu = true;
        }
        public void screenrtu(UInt16 acknowledgeNumber, UInt32 gatewayId, UInt64 deviceId, byte[] payload)
        {
            // button4.Text = Convert.ToString(payload[14]);
            uc8rtu = false;
            flowLayoutPanel1.Controls.Clear();
            for (int i = 0; i < payload[14]; i++)
            {
                addControl();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            influxdbTest();
        }

        private void influxdbTest()
        {
            var influxDBClient = InfluxDBClientFactory.Create("http://localhost:8086", "t7HUaHSBPVheH6QK351z1qpwuCX_rhq2vsX_-V7kqagcu7cNfRgR-wL0gzM6csRXIE0W8_r3I4AWETwmfSNRVQ==");

            var point = PointData.Measurement("co_21234")
                               .Field("value", (float)88);
            influxDBClient.GetWriteApi().WritePoint("farmcare", "saltanb", point);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
        }
    }
}
