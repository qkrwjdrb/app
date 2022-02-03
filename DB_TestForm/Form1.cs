using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InfluxDB.Client.Writes;
using InfluxDB.Client;

namespace DB_TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            influxdbTest();
        }
        private void influxdbTest()
        {
            var influxDBClient = InfluxDBClientFactory.Create("http://localhost:8086", "t7HUaHSBPVheH6QK351z1qpwuCX_rhq2vsX_-V7kqagcu7cNfRgR-wL0gzM6csRXIE0W8_r3I4AWETwmfSNRVQ==");

            var point = PointData.Measurement("test_measurement")
                               .Field("value", (float)88);
            influxDBClient.GetWriteApi().WritePoint("testdb", "saltanb", point);

        }
    }
}
