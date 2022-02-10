using Google.Protobuf;
using Grpc.Core;
using Grpc.Net.Client;
using NetExchange;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;


namespace unit
{
    public partial class Form1 : Form
    {

        public static Form1 f1;

        screen.UserControl1 UserControl1 = new screen.UserControl1();
        screen.UserControl2 UserControl2 = new screen.UserControl2();
        screen.UserControl3 UserControl3 = new screen.UserControl3();
        screen.UserControl4 UserControl4 = new screen.UserControl4();
        screen.UserControl5 UserControl5 = new screen.UserControl5();
        screen.UserControl6 UserControl6 = new screen.UserControl6();
        screen.UserControl7 UserControl7 = new screen.UserControl7();
        screen.UserControl8 UserControl8 = new screen.UserControl8();

        private static GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5044");
        internal static ExProto.ExProtoClient exchange = new ExProto.ExProtoClient(channel);

        internal static AsyncDuplexStreamingCall<RtuMessage, RtuMessage> rtuLink = exchange.MessageRtu();
        internal static AsyncDuplexStreamingCall<ExtMessage, ExtMessage> extLink = exchange.MessageExt();
        internal static AsyncDuplexStreamingCall<CmdMessage, CmdMessage> cmdLink = exchange.MessageCmd();
        internal UInt16 TxCnt;
        internal string[] gatewayItems;
        internal string[] addressItems;

        public UInt32 gateway;
        public UInt64 devicead;


        public bool isUc1 = false;
        public bool isUc4 = false;
        public bool isUc5 = false;
        public bool isUc6 = false;
        public bool isUc7 = false;

        public bool LoadUc1 = false;
        public bool LoadUc2 = false;
        public bool LoadUc3 = false;
        public bool LoadUc4 = false;
        public bool LoadUc5 = false;
        public bool LoadUc6 = false;
        public bool LoadUc7 = false;
        public bool LoadUc8 = false;

        public FileInfo deFile = new FileInfo("device.txt");
        public FileInfo gaFile = new FileInfo("gateway.txt");

        public ulong dataAddress;
        public uint dataGateway;

        public Form1()
        {
            InitializeComponent();
            Task.Run(() => RtuMessageService());
            Task.Run(() => ExtMessageService());
            Task.Run(() => CmdMessageService());
            this.MaximizeBox = false;
            f1 = this;
        }

        //form1_load
        public void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(761, 632);



            if (deFile.Exists)
            {
                screen.UserControl2.uc2.addressLoadFile();
            }
            else
            {
                /*서울 "308398D9E8F5" ,"500291AEBD15"*/
                /*양양 "24A16057F685", "500291AEBCD9", "500291AEBE4D"*/
                string[] aa = { "24A16057F685", "500291AEBCD9", "500291AEBE4D" };
                screen.UserControl2.uc2.deviceListBox.Items.AddRange(aa);
            }
            if (gaFile.Exists)
            {
                screen.UserControl2.uc2.gatewayLoadFile();
            }
            else
            {
                string[] aa = { "0" };
                screen.UserControl2.uc2.gatewayListBox.Items.AddRange(aa);
            }
            panel3.Controls.Add(UserControl1);
            screen.UserControl1.uc1.getAddress((uint)int.Parse(screen.UserControl2.uc2.gatewayListBox.Items[0].ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(screen.UserControl2.uc2.deviceListBox.Items[0].ToString(), System.Globalization.NumberStyles.HexNumber));
            dataAddress = ulong.Parse(screen.UserControl2.uc2.deviceListBox.Items[0].ToString(), System.Globalization.NumberStyles.HexNumber);
        }

        private async void RtuMessageService()
        {
            try
            {
                while (await rtuLink.ResponseStream.MoveNext(cancellationToken: CancellationToken.None))
                {
                    var response = rtuLink.ResponseStream.Current;
                    byte[] payload = new byte[response.DataUnit.Length];
                    response.DataUnit.CopyTo(payload, 0);
                    RxRtu((UInt16)response.SequenceNumber, response.GwId, response.DeviceId, payload);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                Application.Exit();
            }
        }

        private async void ExtMessageService()
        {
            try
            {
                while (await extLink.ResponseStream.MoveNext(cancellationToken: CancellationToken.None))
                {
                    var response = extLink.ResponseStream.Current;
                    byte[] payload = new byte[response.DataUnit.Length];
                    response.DataUnit.CopyTo(payload, 0);
                    RxExt(response.Context, response.GwId, response.DeviceId, payload);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                Application.Exit();
            }
        }

        private async void CmdMessageService()
        {
            try
            {
                while (await cmdLink.ResponseStream.MoveNext(cancellationToken: CancellationToken.None))
                {
                    var response = cmdLink.ResponseStream.Current;
                    byte[] payload = new byte[response.Payload.Length];
                    response.Payload.CopyTo(payload, 0);
                    RxCmd((UInt16)response.OpCode, response.Route, response.Argument, response.GwId, response.DeviceId, payload);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                Application.Exit();
            }
        }
        private String GetProtocolChannelName(UInt16 channel)
        {
            switch (channel)
            {
                case 0:
                    return "Modbus";
            }

            return "Unknown probotol";
        }
        static private readonly ushort[] wCRCTable =
        {
            0x0000, 0xC0C1, 0xC181, 0x0140, 0xC301, 0x03C0, 0x0280, 0xC241, 0xC601, 0x06C0,
            0x0780, 0xC741, 0x0500, 0xC5C1, 0xC481, 0x0440, 0xCC01, 0x0CC0, 0x0D80, 0xCD41,
            0x0F00, 0xCFC1, 0xCE81, 0x0E40, 0x0A00, 0xCAC1, 0xCB81, 0x0B40, 0xC901, 0x09C0,
            0x0880, 0xC841, 0xD801, 0x18C0, 0x1980, 0xD941, 0x1B00, 0xDBC1, 0xDA81, 0x1A40,
            0x1E00, 0xDEC1, 0xDF81, 0x1F40, 0xDD01, 0x1DC0, 0x1C80, 0xDC41, 0x1400, 0xD4C1,
            0xD581, 0x1540, 0xD701, 0x17C0, 0x1680, 0xD641, 0xD201, 0x12C0, 0x1380, 0xD341,
            0x1100, 0xD1C1, 0xD081, 0x1040, 0xF001, 0x30C0, 0x3180, 0xF141, 0x3300, 0xF3C1,
            0xF281, 0x3240, 0x3600, 0xF6C1, 0xF781, 0x3740, 0xF501, 0x35C0, 0x3480, 0xF441,
            0x3C00, 0xFCC1, 0xFD81, 0x3D40, 0xFF01, 0x3FC0, 0x3E80, 0xFE41, 0xFA01, 0x3AC0,
            0x3B80, 0xFB41, 0x3900, 0xF9C1, 0xF881, 0x3840, 0x2800, 0xE8C1, 0xE981, 0x2940,
            0xEB01, 0x2BC0, 0x2A80, 0xEA41, 0xEE01, 0x2EC0, 0x2F80, 0xEF41, 0x2D00, 0xEDC1,
            0xEC81, 0x2C40, 0xE401, 0x24C0, 0x2580, 0xE541, 0x2700, 0xE7C1, 0xE681, 0x2640,
            0x2200, 0xE2C1, 0xE381, 0x2340, 0xE101, 0x21C0, 0x2080, 0xE041, 0xA001, 0x60C0,
            0x6180, 0xA141, 0x6300, 0xA3C1, 0xA281, 0x6240, 0x6600, 0xA6C1, 0xA781, 0x6740,
            0xA501, 0x65C0, 0x6480, 0xA441, 0x6C00, 0xACC1, 0xAD81, 0x6D40, 0xAF01, 0x6FC0,
            0x6E80, 0xAE41, 0xAA01, 0x6AC0, 0x6B80, 0xAB41, 0x6900, 0xA9C1, 0xA881, 0x6840,
            0x7800, 0xB8C1, 0xB981, 0x7940, 0xBB01, 0x7BC0, 0x7A80, 0xBA41, 0xBE01, 0x7EC0,
            0x7F80, 0xBF41, 0x7D00, 0xBDC1, 0xBC81, 0x7C40, 0xB401, 0x74C0, 0x7580, 0xB541,
            0x7700, 0xB7C1, 0xB681, 0x7640, 0x7200, 0xB2C1, 0xB381, 0x7340, 0xB101, 0x71C0,
            0x7080, 0xB041, 0x5000, 0x90C1, 0x9181, 0x5140, 0x9301, 0x53C0, 0x5280, 0x9241,
            0x9601, 0x56C0, 0x5780, 0x9741, 0x5500, 0x95C1, 0x9481, 0x5440, 0x9C01, 0x5CC0,
            0x5D80, 0x9D41, 0x5F00, 0x9FC1, 0x9E81, 0x5E40, 0x5A00, 0x9AC1, 0x9B81, 0x5B40,
            0x9901, 0x59C0, 0x5880, 0x9841, 0x8801, 0x48C0, 0x4980, 0x8941, 0x4B00, 0x8BC1,
            0x8A81, 0x4A40, 0x4E00, 0x8EC1, 0x8F81, 0x4F40, 0x8D01, 0x4DC0, 0x4C80, 0x8C41,
            0x4400, 0x84C1, 0x8581, 0x4540, 0x8701, 0x47C0, 0x4680, 0x8641, 0x8201, 0x42C0,
            0x4380, 0x8341, 0x4100, 0x81C1, 0x8081, 0x4040
        };
        public byte[] fn_makeCRC16_byte(byte[] bytes)
        {
            int ilen = bytes.Length;
            int icrc = 0xFFFF;
            for (int i = 0; i < ilen; i++)
            {
                icrc = (icrc >> 8) ^ wCRCTable[(icrc ^ bytes[i]) & 0xff];
            }
            byte[] ret = BitConverter.GetBytes((ushort)icrc);

            return ret;
        }
        public void TxRtu(UInt16 sequenceNumber, UInt32 gatewayId, UInt64 deviceId, byte[] payload)
        {

            //addressSaveFile();
            //gatewaySaveFile();
            UInt16 channel = 0;
            var list = new List<byte>();
            list.AddRange(payload);
            list.AddRange(fn_makeCRC16_byte(payload));
            payload = list.ToArray();

            this.Invoke((MethodInvoker)delegate ()
            {
                //     if (!screen.UserControl6.uc6.checkBox2.Checked&&!isUc6)
                if (sequenceNumber == screen.UserControl8.uc8.UC8sequence)
                {
                    
                }




                if (sequenceNumber == 0)
                {

                }
                else
                {
                    dataGridView1.Rows.Add(sequenceNumber, "Tx", gatewayId.ToString("X12"), deviceId.ToString("X12"), DateTime.Now, BitConverter.ToString(payload).Replace("-", " "));


                    if (isUc4)
                    {

                        screen.UserControl4.uc4.uc4textBox1.Text = "TxRtu(" + GetProtocolChannelName(channel) + ")";
                        screen.UserControl4.uc4.uc4textBox1.AppendText(Environment.NewLine + $"RequestStream.Channel={channel}");
                        screen.UserControl4.uc4.uc4textBox1.AppendText(Environment.NewLine + $"RequestStream.SequenceNumber={sequenceNumber}");
                        screen.UserControl4.uc4.uc4textBox1.AppendText(Environment.NewLine + $"RequestStream.GatewayId=" + gatewayId.ToString("X6"));
                        screen.UserControl4.uc4.uc4textBox1.AppendText(Environment.NewLine + $"RequestStream.DeviceId=" + deviceId.ToString("X12"));
                        screen.UserControl4.uc4.uc4textBox1.AppendText(Environment.NewLine + $"RequestStream.Tdu.Length={payload.Length}");
                        screen.UserControl4.uc4.uc4textBox1.AppendText(Environment.NewLine + $"RequestStream.Tdu={BitConverter.ToString(payload).Replace("-", string.Empty)}");
                        screen.UserControl4.uc4.uc4textBox1.AppendText(Environment.NewLine);
                        screen.UserControl4.uc4.uc4textBox2.Text = "Awaiting response...";

                    }
                    else if (isUc5)
                    {

                        screen.UserControl5.uc5.uc5textBox1.Text = "TxRtu(" + GetProtocolChannelName(channel) + ") RequestStream";
                        screen.UserControl5.uc5.uc5textBox1.AppendText(Environment.NewLine + $"Channel={channel}");
                        screen.UserControl5.uc5.uc5textBox1.AppendText(Environment.NewLine + $"SequenceNumber={sequenceNumber}");
                        screen.UserControl5.uc5.uc5textBox1.AppendText(Environment.NewLine + $"GatewayId=" + gatewayId.ToString("X6"));
                        screen.UserControl5.uc5.uc5textBox1.AppendText(Environment.NewLine + $"DeviceId=" + deviceId.ToString("X12"));
                        screen.UserControl5.uc5.uc5textBox1.AppendText(Environment.NewLine + $"Tdu.Length={payload.Length}");
                        screen.UserControl5.uc5.uc5textBox1.AppendText(Environment.NewLine + $"Tdu={BitConverter.ToString(payload).Replace("-", " ")}");
                        screen.UserControl5.uc5.uc5textBox1.AppendText(Environment.NewLine);
                        screen.UserControl5.uc5.uc5textBox2.Text = "Awaiting response...";

                    }
                    else if (isUc6 && !screen.UserControl6.uc6.checkBox2.Checked)
                    {
                        screen.UserControl6.uc6.uc6textBox1.Text = "TxRtu(" + GetProtocolChannelName(channel) + ") RequestStream";
                        screen.UserControl6.uc6.uc6textBox1.AppendText(Environment.NewLine + $"Channel={channel}");
                        screen.UserControl6.uc6.uc6textBox1.AppendText(Environment.NewLine + $"SequenceNumber={sequenceNumber}");
                        screen.UserControl6.uc6.uc6textBox1.AppendText(Environment.NewLine + $"GatewayId=" + gatewayId.ToString("X6"));
                        screen.UserControl6.uc6.uc6textBox1.AppendText(Environment.NewLine + $"DeviceId=" + deviceId.ToString("X12"));
                        screen.UserControl6.uc6.uc6textBox1.AppendText(Environment.NewLine + $"Tdu.Length={payload.Length}");
                        screen.UserControl6.uc6.uc6textBox1.AppendText(Environment.NewLine + $"Tdu={BitConverter.ToString(payload).Replace("-", " ")}");
                        screen.UserControl6.uc6.uc6textBox1.AppendText(Environment.NewLine);
                        screen.UserControl6.uc6.uc6textBox2.Text = "Awaiting response...";

                    }
                    else if (isUc7)
                    {
                        screen.UserControl7.uc7.uc7textBox1.Text = "TxRtu(" + GetProtocolChannelName(channel) + ") RequestStream";
                        screen.UserControl7.uc7.uc7textBox1.AppendText(Environment.NewLine + $"Channel={channel}");
                        screen.UserControl7.uc7.uc7textBox1.AppendText(Environment.NewLine + $"SequenceNumber={sequenceNumber}");
                        screen.UserControl7.uc7.uc7textBox1.AppendText(Environment.NewLine + $"GatewayId=" + gatewayId.ToString("X6"));
                        screen.UserControl7.uc7.uc7textBox1.AppendText(Environment.NewLine + $"DeviceId=" + deviceId.ToString("X12"));
                        screen.UserControl7.uc7.uc7textBox1.AppendText(Environment.NewLine + $"Tdu.Length={payload.Length}");
                        screen.UserControl7.uc7.uc7textBox1.AppendText(Environment.NewLine + $"Tdu={BitConverter.ToString(payload).Replace("-", " ")}");
                        screen.UserControl7.uc7.uc7textBox1.AppendText(Environment.NewLine);
                        screen.UserControl7.uc7.uc7textBox2.Text = "Awaiting response...";

                    }
                    else
                    {
                        screen.UserControl1.uc1.uc1textBox1.Text = "TxRtu(" + GetProtocolChannelName(channel) + ") RequestStream";
                        screen.UserControl1.uc1.uc1textBox1.AppendText(Environment.NewLine + $"Channel={channel}");
                        screen.UserControl1.uc1.uc1textBox1.AppendText(Environment.NewLine + $"SequenceNumber={sequenceNumber}");
                        screen.UserControl1.uc1.uc1textBox1.AppendText(Environment.NewLine + $"GatewayId=" + gatewayId.ToString("X6"));
                        screen.UserControl1.uc1.uc1textBox1.AppendText(Environment.NewLine + $"DeviceId=" + deviceId.ToString("X12"));
                        screen.UserControl1.uc1.uc1textBox1.AppendText(Environment.NewLine + $"Tdu.Length={payload.Length}");
                        screen.UserControl1.uc1.uc1textBox1.AppendText(Environment.NewLine + $"Tdu={BitConverter.ToString(payload).Replace("-", " ")}");
                        screen.UserControl1.uc1.uc1textBox1.AppendText(Environment.NewLine);
                        screen.UserControl1.uc1.uc1textBox2.Text = "Awaiting response...";
                    }
                }
            });

            rtuLink.RequestStream.WriteAsync(new RtuMessage
            {
                Channel = channel,
                SequenceNumber = sequenceNumber,
                GwId = gatewayId,
                DeviceId = deviceId,
                DataUnit = ByteString.CopyFrom(payload[0..payload.Length])
            });
        }

        public byte deviceCount = 30;
        public void RxRtu(UInt16 acknowledgeNumber, UInt32 gatewayId, UInt64 deviceId, byte[] payload)
        {
           
            UInt16 channel = 0;

            this.Invoke((MethodInvoker)delegate ()
            {


                if (acknowledgeNumber == screen.UserControl8.uc8.UC8sequence)
                {
                    screen.UserControl8.uc8.InfoLowAdd();
                }

































                // if (!screen.UserControl6.uc6.checkBox2.Checked && !isUc6)
                if (acknowledgeNumber == 0)
                {
                    if (screen.UserControl6.uc6.checkBox2.Checked)
                    {

                        screen.UserControl1.uc1.StateDataOutput(payload, deviceId.ToString("X12"));
                    }
                    if (screen.UserControl8.uc8.uc8rtu)
                    {
                        screen.UserControl8.uc8.screenrtu(acknowledgeNumber, gatewayId, deviceId, payload);

                    }
                }

                else
                {
                    dataGridView1.Rows.Add(acknowledgeNumber, "Rx", gatewayId.ToString("X12"), deviceId.ToString("X12"), DateTime.Now, BitConverter.ToString(payload).Replace("-", " "));


                    if (isUc4)
                    {
                        screen.UserControl4.uc4.uc4textBox2.Text = "RxRtu(" + GetProtocolChannelName(channel) + ")";
                        screen.UserControl4.uc4.uc4textBox2.AppendText(Environment.NewLine + $"response.Channel={channel}");
                        screen.UserControl4.uc4.uc4textBox2.AppendText(Environment.NewLine + $"response.AcknowledgeNumber={acknowledgeNumber}");
                        screen.UserControl4.uc4.uc4textBox2.AppendText(Environment.NewLine + $"response.GatewayId=" + gatewayId.ToString("X6"));
                        screen.UserControl4.uc4.uc4textBox2.AppendText(Environment.NewLine + $"response.DeviceId=" + deviceId.ToString("X12"));
                        screen.UserControl4.uc4.uc4textBox2.AppendText(Environment.NewLine + $"response.Tdu.Length={payload.Length}");
                        screen.UserControl4.uc4.uc4textBox2.AppendText(Environment.NewLine + BitConverter.ToString(payload));
                        screen.UserControl4.uc4.uc4textBox2.AppendText(Environment.NewLine);
                        screen.UserControl4.uc4.uc4textBox1.Text += "Responsed... ";
                    }
                    else if (isUc5)
                    {
                        screen.UserControl5.uc5.uc5textBox2.Text = "RxRtu(" + GetProtocolChannelName(channel) + ")";
                        screen.UserControl5.uc5.uc5textBox2.AppendText(Environment.NewLine + $"response.Channel={channel}");
                        screen.UserControl5.uc5.uc5textBox2.AppendText(Environment.NewLine + $"response.AcknowledgeNumber={acknowledgeNumber}");
                        screen.UserControl5.uc5.uc5textBox2.AppendText(Environment.NewLine + $"response.GatewayId=" + gatewayId.ToString("X6"));
                        screen.UserControl5.uc5.uc5textBox2.AppendText(Environment.NewLine + $"response.DeviceId=" + deviceId.ToString("X12"));
                        screen.UserControl5.uc5.uc5textBox2.AppendText(Environment.NewLine + $"response.Tdu.Length={payload.Length}");
                        screen.UserControl5.uc5.uc5textBox2.AppendText(Environment.NewLine + BitConverter.ToString(payload));
                        screen.UserControl5.uc5.uc5textBox2.AppendText(Environment.NewLine);
                        screen.UserControl5.uc5.uc5textBox1.Text += "Responsed... ";

                    }
                    else if (isUc6)
                    {
                        screen.UserControl6.uc6.uc6textBox2.Text = "RxRtu(" + GetProtocolChannelName(channel) + ")";
                        screen.UserControl6.uc6.uc6textBox2.AppendText(Environment.NewLine + $"response.Channel={channel}");
                        screen.UserControl6.uc6.uc6textBox2.AppendText(Environment.NewLine + $"response.AcknowledgeNumber={acknowledgeNumber}");
                        screen.UserControl6.uc6.uc6textBox2.AppendText(Environment.NewLine + $"response.GatewayId=" + gatewayId.ToString("X6"));
                        screen.UserControl6.uc6.uc6textBox2.AppendText(Environment.NewLine + $"response.DeviceId=" + deviceId.ToString("X12"));
                        screen.UserControl6.uc6.uc6textBox2.AppendText(Environment.NewLine + $"response.Tdu.Length={payload.Length}");
                        screen.UserControl6.uc6.uc6textBox2.AppendText(Environment.NewLine + BitConverter.ToString(payload));
                        screen.UserControl6.uc6.uc6textBox2.AppendText(Environment.NewLine);
                        screen.UserControl6.uc6.uc6textBox1.Text += "Responsed... ";
                    }
                    else if (isUc7)
                    {
                        screen.UserControl7.uc7.uc7textBox2.Text = "RxRtu(" + GetProtocolChannelName(channel) + ")";
                        screen.UserControl7.uc7.uc7textBox2.AppendText(Environment.NewLine + $"response.Channel={channel}");
                        screen.UserControl7.uc7.uc7textBox2.AppendText(Environment.NewLine + $"response.AcknowledgeNumber={acknowledgeNumber}");
                        screen.UserControl7.uc7.uc7textBox2.AppendText(Environment.NewLine + $"response.GatewayId=" + gatewayId.ToString("X6"));
                        screen.UserControl7.uc7.uc7textBox2.AppendText(Environment.NewLine + $"response.DeviceId=" + deviceId.ToString("X12"));
                        screen.UserControl7.uc7.uc7textBox2.AppendText(Environment.NewLine + $"response.Tdu.Length={payload.Length}");
                        screen.UserControl7.uc7.uc7textBox2.AppendText(Environment.NewLine + BitConverter.ToString(payload));
                        screen.UserControl7.uc7.uc7textBox2.AppendText(Environment.NewLine);
                        screen.UserControl7.uc7.uc7textBox1.Text += "Responsed... ";
                    }
                    else
                    {
                        screen.UserControl1.uc1.uc1textBox2.Text = "RxRtu(" + GetProtocolChannelName(channel) + ")";
                        screen.UserControl1.uc1.uc1textBox2.AppendText(Environment.NewLine + $"response.Channel={channel}");
                        screen.UserControl1.uc1.uc1textBox2.AppendText(Environment.NewLine + $"response.AcknowledgeNumber={acknowledgeNumber}");
                        screen.UserControl1.uc1.uc1textBox2.AppendText(Environment.NewLine + $"response.GatewayId=" + gatewayId.ToString("X6"));
                        screen.UserControl1.uc1.uc1textBox2.AppendText(Environment.NewLine + $"response.DeviceId=" + deviceId.ToString("X12"));
                        screen.UserControl1.uc1.uc1textBox2.AppendText(Environment.NewLine + $"response.Tdu.Length={payload.Length}");
                        screen.UserControl1.uc1.uc1textBox2.AppendText(Environment.NewLine + BitConverter.ToString(payload));
                        screen.UserControl1.uc1.uc1textBox2.AppendText(Environment.NewLine);
                        screen.UserControl1.uc1.uc1textBox1.Text += "Responsed... ";

                        if (addressEnd && payload.Length == (deviceCount * 2 + 5))
                        {
                            addressArray = payload;

                            addressEnd = false;

                            getData(dataGateway, dataAddress);
                        }
                        if (dataEnd && payload.Length == (deviceCount * 6 + 7))
                        {
                            dataArray = payload;

                            dataEnd = false;

                            screen.UserControl1.uc1.sensorDataOutput(addressArray, dataArray, deviceId.ToString("X12"));
                        }
                        if (isNode)
                        {
                            screen.UserControl1.uc1.NodeDataOutput(payload, deviceId.ToString("X12"));
                            isNode = false;
                        }
                        if (isState)
                        {
                            screen.UserControl1.uc1.StateDataOutput(payload, deviceId.ToString("X12"));
                            isState = false;
                        }

                    }
                }
                /*      if (isTimer && payload.Length == 31)
                      {
                          screen.UserControl1.uc1.StateDataOutput(payload, deviceId.ToString("X12"));
                          isTimer = false;
                      }*/

            });

            switch (channel)
            {
                case 0:
                    /* Modbus protocol */
                    break;
                default:
                    /* Unknown protocol */
                    break;
            }
        }

        public void TxExt(UInt64 context, UInt32 gatewayId, UInt64 deviceId, byte[] payload)
        {
            UInt16 channel = (UInt16)context;

            this.Invoke((MethodInvoker)delegate ()
            {
                screen.UserControl1.uc1.uc1textBox2.Text = "TxExt(" + GetProtocolChannelName(channel) + ")";
                screen.UserControl1.uc1.uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.Context=" + context.ToString("X16"));
                screen.UserControl1.uc1.uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.GatewayId=" + gatewayId.ToString("X6"));
                screen.UserControl1.uc1.uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.DeviceId=" + deviceId.ToString("X12"));
                screen.UserControl1.uc1.uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.Tdu.Length={payload.Length}");
                screen.UserControl1.uc1.uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.Tdu={BitConverter.ToString(payload).Replace("-", string.Empty)}");
                screen.UserControl1.uc1.uc1textBox2.AppendText(Environment.NewLine);
                screen.UserControl1.uc1.uc1textBox3.Text = "Replied...";

            });

            extLink.RequestStream.WriteAsync(new ExtMessage
            {
                Context = context,
                GwId = gatewayId,
                DeviceId = deviceId,
                DataUnit = ByteString.CopyFrom(payload[0..payload.Length])
            });

        }

        public void RxExt(UInt64 context, UInt32 gatewayId, UInt64 deviceId, byte[] payload)
        {
            UInt16 channel = (UInt16)context;

            this.Invoke((MethodInvoker)delegate ()
            {
                screen.UserControl1.uc1.uc1textBox3.Text = "RxExt(" + GetProtocolChannelName(channel) + ")";
                screen.UserControl1.uc1.uc1textBox3.AppendText(Environment.NewLine + $"RequestStream.Context=" + context.ToString("X16"));
                screen.UserControl1.uc1.uc1textBox3.AppendText(Environment.NewLine + $"response.GatewayId=" + gatewayId.ToString("X6"));
                screen.UserControl1.uc1.uc1textBox3.AppendText(Environment.NewLine + $"response.DeviceId=" + deviceId.ToString("X12"));
                screen.UserControl1.uc1.uc1textBox3.AppendText(Environment.NewLine + $"response.Tdu.Length={payload.Length}");
                screen.UserControl1.uc1.uc1textBox3.AppendText(Environment.NewLine + BitConverter.ToString(payload));
                screen.UserControl1.uc1.uc1textBox3.AppendText(Environment.NewLine);
                screen.UserControl1.uc1.uc1textBox3.Text += "Awaiting processing...";
            });

            switch (channel)
            {
                case 0: /* Modbus Salve Processing */
                    TxExt(context, gatewayId, deviceId, new byte[] { /* Response Message */ });
                    break;
            }
        }
        public void TxCmd(UInt16 opCode, UInt32 route, UInt32 argument, UInt32 gatewayId, UInt64 deviceId, byte[] payload)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                screen.UserControl1.uc1.uc1textBox2.Text = "TxExt()";
                screen.UserControl1.uc1.uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.OpCode=" + opCode.ToString("X4"));
                screen.UserControl1.uc1.uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.Route=" + route.ToString("X8"));
                screen.UserControl1.uc1.uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.Argument=" + argument.ToString("X8"));
                screen.UserControl1.uc1.uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.GatewayId=" + gatewayId.ToString("X6"));
                screen.UserControl1.uc1.uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.DeviceId=" + deviceId.ToString("X12"));
                screen.UserControl1.uc1.uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.Tdu.Length={payload.Length}");
                screen.UserControl1.uc1.uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.Tdu={BitConverter.ToString(payload).Replace("-", string.Empty)}");
                screen.UserControl1.uc1.uc1textBox2.AppendText(Environment.NewLine);
                screen.UserControl1.uc1.uc1textBox3.Text = "Replied...";
            });

            cmdLink.RequestStream.WriteAsync(new CmdMessage
            {
                OpCode = opCode,
                Route = route,
                Argument = argument,
                GwId = gatewayId,
                DeviceId = deviceId,
                Payload = ByteString.CopyFrom(payload[0..payload.Length])
            });
        }

        public void RxCmd(UInt16 opCode, UInt32 route, UInt32 argument, UInt32 gatewayId, UInt64 deviceId, byte[] payload)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                screen.UserControl1.uc1.uc1textBox3.Text = "RxCmd()";
                screen.UserControl1.uc1.uc1textBox3.AppendText(Environment.NewLine + $"RequestStream.OpCode=" + opCode.ToString("X4"));
                screen.UserControl1.uc1.uc1textBox3.AppendText(Environment.NewLine + $"RequestStream.Route=" + route.ToString("X8"));
                screen.UserControl1.uc1.uc1textBox3.AppendText(Environment.NewLine + $"RequestStream.Argument=" + argument.ToString("X8"));
                screen.UserControl1.uc1.uc1textBox3.AppendText(Environment.NewLine + $"response.GatewayId=" + gatewayId.ToString("X6"));
                screen.UserControl1.uc1.uc1textBox3.AppendText(Environment.NewLine + $"response.DeviceId=" + deviceId.ToString("X12"));
                screen.UserControl1.uc1.uc1textBox3.AppendText(Environment.NewLine + $"response.Tdu.Length={payload.Length}");
                screen.UserControl1.uc1.uc1textBox3.AppendText(Environment.NewLine + BitConverter.ToString(payload));
                screen.UserControl1.uc1.uc1textBox3.AppendText(Environment.NewLine);
            });

            switch (opCode)
            {
                case 0:
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        screen.UserControl1.uc1.uc1textBox2.Text += "Gateway... ";
                    });
                    break;
            }
        }

        public int deviceBoxIndex = 0;
        public int getewayBoxIndex = 0;

        public void comboboxSelect()
        {
            if (true)
            {
                if (LoadUc1)
                {
                    screen.UserControl1.uc1.deviceBox.SelectedIndex = deviceBoxIndex;
                    screen.UserControl1.uc1.gatewayBox.SelectedIndex = getewayBoxIndex;
                }
                if (LoadUc4)
                {
                    screen.UserControl4.uc4.deviceBox.SelectedIndex = deviceBoxIndex;
                    screen.UserControl4.uc4.gatewayBox.SelectedIndex = getewayBoxIndex;
                }
                if (LoadUc5)
                {
                    screen.UserControl5.uc5.deviceBox.SelectedIndex = deviceBoxIndex;
                    screen.UserControl5.uc5.gatewayBox.SelectedIndex = getewayBoxIndex;
                }
                if (LoadUc6)
                {
                    screen.UserControl6.uc6.deviceBox.SelectedIndex = deviceBoxIndex;
                    screen.UserControl6.uc6.gatewayBox.SelectedIndex = getewayBoxIndex;
                }
                if (LoadUc7)
                {
                    screen.UserControl7.uc7.deviceBox.SelectedIndex = deviceBoxIndex;
                    screen.UserControl7.uc7.gatewayBox.SelectedIndex = getewayBoxIndex;
                }
                if (LoadUc8)
                {
                    screen.UserControl8.uc8.deviceBox.SelectedIndex = deviceBoxIndex;
                    screen.UserControl8.uc8.gatewayBox.SelectedIndex = getewayBoxIndex;
                }
            }
        }






        //0x4588177F, 0x24A160581B59,
        //0x51894B30, 0x24A16057F6BD,


        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            panel3.Controls.Add(UserControl3);
        }
        public bool addressEnd = false;
        byte[] addressArray;
        public bool isNode = false;
        public bool dataEnd = false;
        public bool isState = false;
        public bool isTimer = true;
        byte[] dataArray;
        public void getData(uint gatewayID, ulong deviceID)
        {
            dataEnd = true;
            TxRtu(++TxCnt, gatewayID, deviceID, new byte[] {   0x01, 0x03,
                0x00,202, 0x00, Convert.ToByte(deviceCount*3+1),
            });
        }
        //센서 데이터 버튼
        private void button2_Click(object sender, EventArgs e)
        {
            isUc1 = true;
            isUc4 = false;
            isUc5 = false;
            isUc6 = false;
            isUc7 = false;

            panel3.Controls.Clear();
            panel3.Controls.Add(UserControl1); comboboxSelect();
        }
        //장치 목록 편집 버튼
        private void button4_Click(object sender, EventArgs e)
        {
            isUc6 = false;
            panel3.Controls.Clear();
            panel3.Controls.Add(UserControl2); comboboxSelect();
        }
        //모드버스 읽기 버튼
        private void button3_Click(object sender, EventArgs e)
        {
            isUc4 = true;
            isUc1 = false;
            isUc5 = false;
            isUc6 = false;
            isUc7 = false;

            panel3.Controls.Clear();
            panel3.Controls.Add(UserControl4); comboboxSelect();
        }
        //모드버스 쓰기 버튼
        private void button5_Click(object sender, EventArgs e)
        {
            isUc5 = true;
            isUc1 = false;
            isUc4 = false;
            isUc6 = false;
            isUc7 = false;

            panel3.Controls.Clear();
            panel3.Controls.Add(UserControl5); comboboxSelect();
        }

        //기록 펼치기 버튼
        bool clicked = false;
        private void button6_Click(object sender, EventArgs e)
        {
            if (clicked)
            {
                button6.Text = "기록 펼치기";
                clicked = false;
                this.Size = new Size(761, 632);
            }
            else
            {
                button6.Text = "기록 접기";
                clicked = true;
                this.Size = new Size(1588, 632);
            }
            comboboxSelect();
        }
        //구동기 제어
        private void button7_Click(object sender, EventArgs e)
        {
            isUc6 = true;
            isUc1 = false;
            isUc4 = false;
            isUc5 = false;
            isUc7 = false;

            panel3.Controls.Clear();
            panel3.Controls.Add(UserControl6); comboboxSelect();
        }
        //멀티 쓰기
        private void button9_Click(object sender, EventArgs e)
        {
            isUc7 = true;
            isUc1 = false;
            isUc4 = false;
            isUc5 = false;
            isUc6 = false;

            panel3.Controls.Clear();
            panel3.Controls.Add(UserControl7); comboboxSelect();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            //  isUc6 = false;
            //  panel3.Controls.Clear();

            //  panel3.Controls.Add(UserControl3); 
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            screen.UserControl6.uc6.timerStop();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            panel3.Controls.Add(UserControl8); comboboxSelect();
        }
    }
}






//--------------------------------
namespace ConsoleApplication1
{
    class Program
    {
        static void Mainn(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

        }
    }

    class DataType1 : Object
    {
        private string _name;
        private string _point;
        public string Name
        {
            // 이전 소스의 ToString()의 내용이 여기를 통해 처리하게 됩니다.
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(_name);
                sb.Append(" [ ");

                sb.Append(" ]");
                return sb.ToString();
            }
            set { _name = Name; }
        }
        /*   public Point Pos
           {
               get { return _point; }
               set { _point = Pos; }
           }*/
        public DataType1(string name, string pos)
        {
            _name = name;
            _point = pos;
        }
        // 이제 이 부분은 더이상 쓰이지 않습니다.
        //public override string ToString()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(_name);
        //    sb.Append(" [ ");
        //    sb.Append(_point.X.ToString());
        //    sb.Append(",");
        //    sb.Append(_point.Y.ToString());
        //    sb.Append(" ]");
        //    return sb.ToString();
        //}
    }
    class MainForm : Form
    {
        ListBox listTest;
        List<DataType1> listDataSource;
        public MainForm()
        {
            listTest = new ListBox();
            listDataSource = new List<DataType1>(); // 데이터 소스 개체 생성

            this.listTest.FormattingEnabled = true;
            this.listTest.Location = new System.Drawing.Point(5, 5);
            this.listTest.Name = "TestListBox";
            this.listTest.Size = new System.Drawing.Size(300, 400);
            this.listTest.TabIndex = 1;
            this.Load += new System.EventHandler(this.OnLoad);
        }
        private void OnLoad(object sender, EventArgs e)
        {
            this.Controls.Add(listTest);
            listDataSource.Add(new DataType1("항목1", "항목1"));
            listDataSource.Add(new DataType1("항목2", "항목1"));
            listDataSource.Add(new DataType1("항목3", "항목1"));
            this.listTest.DataSource = listDataSource; // ListBox와의 바인딩(연결)
            this.listTest.DisplayMember = "Name"; // DataType1의 Name 프로퍼티의 Get 내용이 출력
            this.listTest.ValueMember = "Pos"; // DataType1의 Pos 프로퍼티의 Get 내용이 내장 값
        }
    }
}