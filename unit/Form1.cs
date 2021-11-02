

using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Protobuf;
using Grpc.Core;
using Grpc.Net.Client;
using NetExchange;

namespace unit
{
    public partial class Form1 : Form
    {





        string[] save_gateways = new string[5];

        public UInt32 gateway;
        public UInt64 devicead;
        string textData;



        screen.UserControl1 UserControl1 = new screen.UserControl1();
        screen.UserControl2 UserControl2 = new screen.UserControl2();
        screen.UserControl3 UserControl3 = new screen.UserControl3();
        screen.UserControl4 UserControl4 = new screen.UserControl4();


        public Form1()
        {
            InitializeComponent();
     



        }

   
   
   
        //form1_load
        public void Form1_Load(object sender, EventArgs e)
        {

   


            panel3.Controls.Add(UserControl2);
            string phrase = Properties.Settings.Default.save_gateway; // 변수 이동


            string[] words = phrase.Split(','); // 스플릿 전용 배열 생성

    
        }

     


        //comboBox1


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            //ucPanel.ucScreen2.DataSendEvent = new ucPanel.DataPushEventHandler();
        }

        private void b(object sender, MouseEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
      


        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }
        //0x4588177F, 0x24A160581B59,
        //0x51894B30, 0x24A16057F6BD,
    
        
        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            panel3.Controls.Add(UserControl3);
        }
        float GetModbusFloat(byte[] receiveData, Int32 offset)
        {
            byte[] rData = new byte[4];

            rData[0] = receiveData[offset + 1];
            rData[1] = receiveData[offset + 0];
            rData[2] = receiveData[offset + 3];
            rData[3] = receiveData[offset + 2];

            return BitConverter.ToSingle(rData, 0);
        }

        float GetModbusFloat(byte[] receiveData)
        {
            return GetModbusFloat(receiveData, 0);
        }

        Int16 GetModbusInt16(byte[] receiveData, Int32 offset)
        {
            return (Int16)((receiveData[offset + 1] << 8) | receiveData[offset + 0]);
        }

        Int16 GetModbusInt16(byte[] receiveData)
        {
            return GetModbusInt16(receiveData, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserControl1.isUc4 = false;
            panel3.Controls.Clear();
            panel3.Controls.Add(UserControl1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            panel3.Controls.Add(UserControl2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserControl1.isUc4 = true;
            panel3.Controls.Clear();
            panel3.Controls.Add(UserControl4);

        }

    }

}

