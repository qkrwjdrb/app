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
    public partial class UserControl3 : UserControl
    {
        public UserControl3()
        {
            InitializeComponent();
            panel1.Controls.Add(ucMod1);
            panel2.Controls.Add(ucSelect1);

            ucPanel.uc7Segmant.eventdelSender += UcScreen2_eventdelSender;
            device.ucSelect.gatewaySender += Select_gatewaySender;
            device.ucSelect.deviceSender += Select_deviceSender;
            ucPanel.ucModbus.eventModSender += UcModbus_eventModSender;
            ucPanel.ucText.textSender += UcText_textSender;
            ucPanel.ucLED.ledSender += UcLED_eventModSender;

  
        }
        string[] save_gateways = new string[5];

        public UInt32 gateway;
        public UInt64 devicead;
        string textData;
        byte[] digits = new byte[12] { 0xfC, 0x60, 0xda, 0xf2, 0x66, 0xb6, 0xbe, 0xe0, 0xfe, 0xe6, 0x02, 0x00 };
        byte[] alpha = new byte[26] { 0xEE, 0x3E, 0x1A, 0x7A, 0x9E, 0x8E, 0xBC, 0x6E, 0xc, 0x78, 0x5E, 0x1C, 0xAA, 0x2A, 0x3A, 0xCE, 0xE6, 0x0A, 0xB6, 0x1E, 0x38, 0x7E, 0x56, 0x6C, 0x76, 0x92 };

        ucPanel.ucModbus ucMod1 = new ucPanel.ucModbus();
        ucPanel.uc7Segmant ucSeg2 = new ucPanel.uc7Segmant();
        ucPanel.ucLED ucLED3 = new ucPanel.ucLED();
        ucPanel.ucText ucText4 = new ucPanel.ucText();

        device.ucSelect ucSelect1 = new device.ucSelect();
        device.ucAdd device2 = new device.ucAdd();
        device.ucDelete ucDelete3 = new device.ucDelete();

    
   
        // select_combo_gateway();

        //networkService
        /* public async void NetworkService()
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
         }*/


        //form1_load






        private int UcScreen2_eventdelSender(string strText)
        {

            led_Return(strText);
            return 0;
        }


        private void UcLED_eventModSender(byte[] ledData)
        {

            /*    TxMbRtu(0, gateway, devicead, new byte[] { 0x01, 0x10, 0x01, 0x9a, 0x00, 0x04, 0x08,
                     0,  ledData[6],  ledData[5], ledData[4] ,
                     0,  ledData[2],  ledData[1],  ledData[0],
                     0xAD, 0xDE });*/
        }

        private void UcText_textSender(byte[] sender)
        {

            /*    TxMbRtu(0, gateway, devicead, new byte[] { 0x01, 0x10, 0x01, 0xa2, 0x00, 0x0a, 0x14,
                   sender[0],sender[1],sender[2],sender[3],sender[4],sender[5],sender[6],sender[7],sender[8],sender[9],0x41,0x42,0x43,0x44,0x45,0x46,0x47,0x48,0x49,0x50,
                    0xAD, 0xDE });*/
        }

        private void UcModbus_eventModSender(object a, object b, object c, object d, string e)
        {
            modbus_sender(a, b, c, d, e);
        }

        private void Select_deviceSender(object sender)
        {
            select_combo_devicead((ComboBox)sender);
        }

        private void Select_gatewaySender(object sender)
        {

            select_combo_gateway((ComboBox)sender);
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

            // TxMbRtu(0, gateway, devicead, new byte[] { 0x01, 0x10, 0x01, 0x9E, 0x00, 0x02, 0x04, segmentData[1], segmentData[2], 0x00, segmentData[0], 0xAD, 0xDE });
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

            //  TxMbRtu(0, gateway, devicead, new byte[] { adress, fc, 0x00, startad, 0x00, length, crc1, crc2 });
        }

    

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            panel2.Controls.Add(ucSelect1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            panel2.Controls.Add(device2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            panel2.Controls.Add(ucDelete3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(ucMod1);
        }

        private void button5_Click(object sender, EventArgs e)
        {

            panel1.Controls.Clear();
            panel1.Controls.Add(ucSeg2);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(ucLED3);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(ucText4);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            remove_rich(richTextBox1);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            remove_rich(richTextBox2);
        }
    }
}
