

using System;
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
        /// <summary>
        /// //////////
        /// </summary>

        private static GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5054");
        internal static ExProto.ExProtoClient exchange = new ExProto.ExProtoClient(channel);
        internal static AsyncDuplexStreamingCall<ExMessage, ExMessage> exlink = exchange.ExLink();
        byte[] digits = new byte[12] { 0xfC, 0x60, 0xda, 0xf2, 0x66, 0xb6, 0xbe, 0xe0, 0xfe, 0xe6, 0x02, 0x00 };
        byte[] alpha = new byte[26] { 0xEE, 0x3E, 0x1A, 0x7A, 0x9E, 0x8E, 0xBC, 0x6E, 0xc, 0x78, 0x5E, 0x1C, 0xAA, 0x2A, 0x3A, 0xCE, 0xE6, 0x0A, 0xB6, 0x1E, 0x38, 0x7E, 0x56, 0x6C, 0x76, 0x92 };

        string[] save_gateways = new string[5];



        public UInt32 gateway;
        public UInt64 devicead;
        string textData;

        ucPanel.ucModbus ucMod1 = new ucPanel.ucModbus();
        ucPanel.uc7Segmant ucSeg2 = new ucPanel.uc7Segmant();
        ucPanel.ucLED ucLED3 = new ucPanel.ucLED();
        ucPanel.ucText ucText4 = new ucPanel.ucText();

        device.ucSelect ucSelect1 = new device.ucSelect();
        device.ucAdd device2 = new device.ucAdd();
        device.ucDelete ucDelete3 = new device.ucDelete();

     



        public Form1()
        {
            InitializeComponent();
            Task.Run(() => NetworkService());
            ucPanel.uc7Segmant.eventdelSender += UcScreen2_eventdelSender;
            device.ucSelect.gatewaySender += Select_gatewaySender;
            device.ucSelect.deviceSender += Select_deviceSender;
            ucPanel.ucModbus.eventModSender += UcModbus_eventModSender;
            ucPanel.ucText.textSender += UcText_textSender;
            ucPanel.ucLED.ledSender += UcLED_eventModSender;
        
        }

        private void UcLED_eventModSender(byte[] ledData)
        {

            TxMbRtu(0, gateway, devicead, new byte[] { 0x01, 0x10, 0x01, 0x9a, 0x00, 0x04, 0x08,
                0,  ledData[6],  ledData[5], ledData[4] ,
                0,  ledData[2],  ledData[1],  ledData[0],
                0xAD, 0xDE });
        }

        private void UcText_textSender(byte[] sender)
        {

            TxMbRtu(0, gateway, devicead, new byte[] { 0x01, 0x10, 0x01, 0xa2, 0x00, 0x0a, 0x14,
               sender[0],sender[1],sender[2],sender[3],sender[4],sender[5],sender[6],sender[7],sender[8],sender[9],0x41,0x42,0x43,0x44,0x45,0x46,0x47,0x48,0x49,0x50,
                0xAD, 0xDE });
        }

        private void UcModbus_eventModSender(object a, object b, object c, object d, string e)
        {
            modbus_sender( a, b, c, d, e);
        }

        private void Select_deviceSender(object sender)
        {
            select_combo_devicead((ComboBox)sender);
        }

        private void Select_gatewaySender(object sender)
        {

            select_combo_gateway((ComboBox)sender);
        }

        private int UcScreen2_eventdelSender(string strText)
        {

            led_Return(strText);
            return 0;
        }
        // select_combo_gateway();

        //networkService
        public async void NetworkService()
        {
            while (await exlink.ResponseStream.MoveNext(cancellationToken: CancellationToken.None))
            {
                var response = exlink.ResponseStream.Current;
                UInt32 route = response.Route;
                byte[] payload = new byte[response.DataUnit.Length];
                response.DataUnit.CopyTo(payload, 0);

                switch ((UInt16)(route >> 16))
                {
                    case ((UInt16)'M' << 8) | 'B':
                        RxMbRtu((UInt16)(route >> 0), response.GwId, response.DeviceId, payload);
                        break;
                    default:
                        byte[] cmd = new byte[2] { (byte)(route >> 24), (byte)(route >> 16) };
                        Console.WriteLine($"Unknown cmd = {System.Text.Encoding.UTF8.GetString(cmd)}");
                        Console.WriteLine();
                        break;
                }
            }




        }
        //TxRTU
        public void TxMbRtu(UInt16 gwGroup, UInt32 gwId, UInt64 deviceId, byte[] payload)
        {
            UInt16 cmd = ((UInt16)'m' << 8) | 'b';

            this.Invoke((MethodInvoker)delegate ()
            {
                /*
                if (deviceId == Convert.ToUInt64("24A1605818B1", 16) || deviceId == Convert.ToUInt64("24A160581869", 16))
                {
                    richTextBox3.Text = "LED request";
                    richTextBox3.AppendText(Environment.NewLine + $"server : " + gwId.ToString("X8") + "/" + deviceId.ToString("X12"));
                    richTextBox3.AppendText(Environment.NewLine + $"RequestStream.Tdu={BitConverter.ToString(payload).Replace("-", string.Empty)}");
                    richTextBox3.AppendText(Environment.NewLine);

                    richTextBox4.Text = "Awaiting response...";
                    richTextBox5.Text = "LED result";
                }
                */

                richTextBox2.Text = "TxMbRtu()";
                richTextBox2.AppendText(Environment.NewLine + $"RequestStream.GwGroup={gwGroup}");
                richTextBox2.AppendText(Environment.NewLine + $"RequestStream.GwId=" + gwId.ToString("X8"));
                richTextBox2.AppendText(Environment.NewLine + $"RequestStream.DeviceId=" + deviceId.ToString("X12"));
                richTextBox2.AppendText(Environment.NewLine + $"RequestStream.Tdu.Length={payload.Length}");
                richTextBox2.AppendText(Environment.NewLine + $"RequestStream.Tdu={BitConverter.ToString(payload).Replace("-", string.Empty)}");
                richTextBox2.AppendText(Environment.NewLine);
                richTextBox1.Text = "Awaiting response...";

            });

            exlink.RequestStream.WriteAsync(new ExMessage
            {
                Route = ((UInt32)cmd << 16) | gwGroup,
                GwId = gwId,
                DeviceId = deviceId,
                DataUnit = ByteString.CopyFrom(payload[0..payload.Length]),
            });
        }
        //RxRTU
        public void RxMbRtu(UInt16 gwGroup, UInt32 gwId, UInt64 deviceId, byte[] payload)
        {
            this.Invoke((MethodInvoker)delegate ()
            {

                if (deviceId == Convert.ToUInt64("24A1605818B1", 16) || deviceId == Convert.ToUInt64("24A160581869", 16))
                {
                    /*richTextBox4.Text = "LED response";
                    richTextBox4.AppendText(Environment.NewLine + $"server : " + gwId.ToString("X8") + "/" + deviceId.ToString("X12"));
                    richTextBox4.AppendText(Environment.NewLine + BitConverter.ToString(payload));
                    richTextBox4.AppendText(Environment.NewLine);
                    richTextBox3.Text += "Responsed... ";

                    richTextBox5.AppendText(Environment.NewLine + "LED1 value : " + textBox1.Text);
                    richTextBox5.AppendText(Environment.NewLine + "LED2 value : " + textBox2.Text);
                    */
                }



                richTextBox1.Text = "RxMbRtu()";
                richTextBox1.AppendText(Environment.NewLine + $"response.GwGroup={gwGroup}");
                richTextBox1.AppendText(Environment.NewLine + $"response.GwId=" + gwId.ToString("X8"));
                richTextBox1.AppendText(Environment.NewLine + $"response.DeviceId=" + deviceId.ToString("X12"));
                richTextBox1.AppendText(Environment.NewLine + $"response.Tdu.Length={payload.Length}");
                richTextBox1.AppendText(Environment.NewLine + BitConverter.ToString(payload));
                richTextBox1.AppendText(Environment.NewLine);
                richTextBox2.Text += "Responsed... ";

            });
        }
        //form1_load
        public void Form1_Load(object sender, EventArgs e)
        {


            panel1.Controls.Add(ucMod1);
            panel2.Controls.Add(ucSelect1);
            string phrase = Properties.Settings.Default.save_gateway; // 변수 이동


           string[] words = phrase.Split(','); // 스플릿 전용 배열 생성

            //foreach (var word in words)
            //{
            //    if (word != "")
            //    {
            //        listBox1.Items.Add(word);
            //    }
            //}

            //string phrase2 = Properties.Settings.Default.save_deviceid; // 변수 이동

            //string[] words2 = phrase2.Split(','); // 스플릿 전용 배열 생성

            //foreach (var word2 in words2)
            //{
            //    if (word2 != "")
            //    {
            //        listBox2.Items.Add(word2);
            //    }
            //}

            //listBox1.Items.Add("51894B30"); listBox1.Items.Add("4588177F");

            //listBox2.Items.Add("24A160581B59"); listBox2.Items.Add("24A16057F6BD"); listBox2.Items.Add("24A16057F615");

            //comboBox1.Items.Add("51894B30"); comboBox1.Items.Add("4588177F");

            //comboBox2.Items.Add("24A160581B59"); comboBox2.Items.Add("24A16057F6BD"); comboBox2.Items.Add("24A16057F615");

            //comboBox3.Items.Add("01"); comboBox3.Items.Add("03"); comboBox3.Items.Add("10");

            //comboBox4.Items.Add("24A1605818B1"); comboBox4.Items.Add("24A160581869"); comboBox5.Items.Add("51894B30");

            //comboBox7.Items.Add("24A1605818B1"); comboBox7.Items.Add("24A160581869"); comboBox6.Items.Add("51894B30");

            //comboBox1.Text = Properties.Settings.Default.last_gateway;
            //comboBox2.Text = Properties.Settings.Default.last_deviceid;
        }

        //function
        public void led_Return(string text)
        {

            textData = text;
            byte[] segmentData = new byte[3];
            int weight = 0;

            for (int i = 0; i < textData.Length && weight < 3; i++)
            {
                char c = Convert.ToChar(textData.Substring(i, 1));

                if (c == '.')
                {
                    if ((segmentData[weight] & 0x01) == 0)
                    {
                        segmentData[weight++] |= 0x01;
                    }
                    else if (++weight < 3)
                    {
                        segmentData[weight] |= 0x01;
                    }
                }
                else if (('0' <= c && c <= '9') || (c == '-'))
                {
                    int index = (c == '-') ? 10 : Convert.ToInt32(textData.Substring(i, 1));

                    if ((segmentData[weight] & 0xFE) == 0)
                    {
                        segmentData[weight] |= digits[index];
                    }
                    else if (++weight < 3)
                    {
                        segmentData[weight] |= digits[index];
                    }
                }
                else if (('A' <= c && c <= 'Z') || ('a' <= c && c <= 'z'))
                {
                    int index = Convert.ToByte(c) - (('a' <= c && c <= 'z') ? 97 : 65);

                    if ((segmentData[weight] & 0xFE) == 0)
                    {
                        segmentData[weight] |= alpha[index];
                    }
                    else if (++weight < 3)
                    {
                        segmentData[weight] |= alpha[index];
                    }
                }
                else
                {
                    weight += (segmentData[weight] & 0xFF) != 0 ? 2 : 1;
                }
            }

            TxMbRtu(0, gateway, devicead, new byte[] { 0x01, 0x10, 0x01, 0x9E, 0x00, 0x02, 0x04, segmentData[1], segmentData[2], 0x00, segmentData[0], 0xAD, 0xDE });
        }
        //0x51894B30
        //0x24A1605818B1devicead
        //0x24A160581869

        public void add_text_to_combo(TextBox text, ComboBox combo)
        {
            int cnt = 0;

            for (int i = 0; i < text.TextLength; i++)
            {

                char p = Convert.ToChar(text.Text.Substring(i, 1));
                if (('A' <= p && p <= 'Z') || ('a' <= p && p <= 'z') || ('0' <= p && p <= '9'))
                {
                    cnt = 0;

                }
                else
                {
                    cnt = 1;

                }
            }
            if (cnt == 1)
            {
                Form2 form2 = new Form2();
                form2.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                form2.ShowDialog();
            }
            else
            {
                combo.Items.Add(text.Text);
            }

            int count = combo.Items.Count;
            if (count > 4)
            {
                //   button4.Enabled = false;
            }


        }

        public void remove_combo(ComboBox combo)
        {
            if (combo.SelectedItem != null)
            {
                combo.Items.Remove(combo.SelectedItem.ToString());
                combo.Text = "";

            }
        }

        public void select_combo_gateway(ComboBox combo)
        {
            if (combo.SelectedItem == "" || combo.SelectedItem == "Input Hex Value")
            {
                Form3 form3 = new Form3();
                form3.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                form3.ShowDialog();
            }

            else
            {
                string gateway1 = Convert.ToString(combo.SelectedItem);
                gateway = Convert.ToUInt32(gateway1, 16);
            }
        }

        public void select_combo_devicead(ComboBox combo)
        {
            if (combo.SelectedItem == "" || combo.SelectedItem == "Input Hex Value")
            {
                Form3 form3 = new Form3();
                form3.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                form3.ShowDialog();
            }
            else
            {
                string devicead1 = Convert.ToString(combo.SelectedItem);
                devicead = Convert.ToUInt64(devicead1, 16);
            }
        }

        public void remove_rich(RichTextBox rich)
        {
            rich.Text = "";
        }
        public void key_Press(KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back) ||
        "ABCDEF0123456789abcdef".IndexOf(e.KeyChar) != -1))
            {
                e.Handled = true;
            }
        }

        public void modbus_sender(object a, object b, object c, object d, string e)
        {
            byte adress = Convert.ToByte(a);
            byte fc = Convert.ToByte(b);
            byte startad = Convert.ToByte(c);
            byte length = Convert.ToByte(d);
            byte crc1 = Convert.ToByte(e.Substring(0, 2), 16);
            byte crc2 = Convert.ToByte(e.Substring(2, 2), 16);

            TxMbRtu(0, gateway, devicead, new byte[] { adress, fc, 0x00, startad, 0x00, length, crc1, crc2 });
        }


        //comboBox1


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void button17_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(ucMod1);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(ucSeg2);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(ucLED3);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(ucText4);
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
        private void button14_Click(object sender, EventArgs e)
        {
            remove_rich(richTextBox2);
        }
        //button15
        private void button15_Click(object sender, EventArgs e)
        {
            remove_rich(richTextBox1);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            panel2.Controls.Add(ucSelect1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            panel2.Controls.Add(device2);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            panel2.Controls.Add(ucDelete3);
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            remove_rich(richTextBox3);

            TxMbRtu(0, 0x51894B30, 0x24A16057F6BD, new byte[] { 0x01, 0x03,
        0x00, 0xCB, 0x00,0x04,
        0xAD, 0xDE});


        }
        //textBox1




    }
}

