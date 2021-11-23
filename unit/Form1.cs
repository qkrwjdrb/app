 using System;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using Google.Protobuf;
using Grpc.Core;
using Grpc.Net.Client;
using NetExchange;

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

        private static GrpcChannel channel = GrpcChannel.ForAddress("http://192.168.0.219:5054");
        internal static ExProto.ExProtoClient exchange = new ExProto.ExProtoClient(channel);

        internal static AsyncDuplexStreamingCall<RtuMessage, RtuMessage> rtuLink = exchange.MessageRtu();
        internal static AsyncDuplexStreamingCall<ExtMessage, ExtMessage> extLink = exchange.MessageExt();
        internal static AsyncDuplexStreamingCall<CmdMessage, CmdMessage> cmdLink = exchange.MessageCmd();
        internal UInt16 TxCnt;
        internal string[] gatewayItems;
        internal string[] addressItems; 

        string[] save_gateways = new string[5];

        public UInt32 gateway;
        public UInt64 devicead;
        string textData;

        public bool isUc4 = false;
        public bool isUc5 = false;

        public FileInfo deFile = new FileInfo("device.txt");
        public FileInfo gaFile = new FileInfo("gateway.txt");

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
                addressLoadFile();
            }
            else
            {
                string[] aa = { "24A16057F685", "500291AEBCD9", "500291AEBE4D" };
                screen.UserControl2.uc2.listBox2.Items.AddRange(aa);
            }
            if (gaFile.Exists)
            {
                gatewayLoadFile();
            }
            else
            {
                string[] aa = { "0" };
                screen.UserControl2.uc2.listBox1.Items.AddRange(aa);
            }
            panel3.Controls.Add(UserControl1);
            getAddress(ulong.Parse(screen.UserControl2.uc2.listBox2.Items[0].ToString(), System.Globalization.NumberStyles.HexNumber));
            dataAddress = ulong.Parse(screen.UserControl2.uc2.listBox2.Items[0].ToString(), System.Globalization.NumberStyles.HexNumber);
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
        0X0000, 0XC0C1, 0XC181, 0X0140, 0XC301, 0X03C0, 0X0280, 0XC241, 0XC601, 0X06C0,
        0X0780, 0XC741, 0X0500, 0XC5C1, 0XC481, 0X0440, 0XCC01, 0X0CC0, 0X0D80, 0XCD41,
        0X0F00, 0XCFC1, 0XCE81, 0X0E40, 0X0A00, 0XCAC1, 0XCB81, 0X0B40, 0XC901, 0X09C0,
        0X0880, 0XC841, 0XD801, 0X18C0, 0X1980, 0XD941, 0X1B00, 0XDBC1, 0XDA81, 0X1A40,
        0X1E00, 0XDEC1, 0XDF81, 0X1F40, 0XDD01, 0X1DC0, 0X1C80, 0XDC41, 0X1400, 0XD4C1,
        0XD581, 0X1540, 0XD701, 0X17C0, 0X1680, 0XD641, 0XD201, 0X12C0, 0X1380, 0XD341,
        0X1100, 0XD1C1, 0XD081, 0X1040, 0XF001, 0X30C0, 0X3180, 0XF141, 0X3300, 0XF3C1,
        0XF281, 0X3240, 0X3600, 0XF6C1, 0XF781, 0X3740, 0XF501, 0X35C0, 0X3480, 0XF441,
        0X3C00, 0XFCC1, 0XFD81, 0X3D40, 0XFF01, 0X3FC0, 0X3E80, 0XFE41, 0XFA01, 0X3AC0,
        0X3B80, 0XFB41, 0X3900, 0XF9C1, 0XF881, 0X3840, 0X2800, 0XE8C1, 0XE981, 0X2940,
        0XEB01, 0X2BC0, 0X2A80, 0XEA41, 0XEE01, 0X2EC0, 0X2F80, 0XEF41, 0X2D00, 0XEDC1,
        0XEC81, 0X2C40, 0XE401, 0X24C0, 0X2580, 0XE541, 0X2700, 0XE7C1, 0XE681, 0X2640,
        0X2200, 0XE2C1, 0XE381, 0X2340, 0XE101, 0X21C0, 0X2080, 0XE041, 0XA001, 0X60C0,
        0X6180, 0XA141, 0X6300, 0XA3C1, 0XA281, 0X6240, 0X6600, 0XA6C1, 0XA781, 0X6740,
        0XA501, 0X65C0, 0X6480, 0XA441, 0X6C00, 0XACC1, 0XAD81, 0X6D40, 0XAF01, 0X6FC0,
        0X6E80, 0XAE41, 0XAA01, 0X6AC0, 0X6B80, 0XAB41, 0X6900, 0XA9C1, 0XA881, 0X6840,
        0X7800, 0XB8C1, 0XB981, 0X7940, 0XBB01, 0X7BC0, 0X7A80, 0XBA41, 0XBE01, 0X7EC0,
        0X7F80, 0XBF41, 0X7D00, 0XBDC1, 0XBC81, 0X7C40, 0XB401, 0X74C0, 0X7580, 0XB541,
        0X7700, 0XB7C1, 0XB681, 0X7640, 0X7200, 0XB2C1, 0XB381, 0X7340, 0XB101, 0X71C0,
        0X7080, 0XB041, 0X5000, 0X90C1, 0X9181, 0X5140, 0X9301, 0X53C0, 0X5280, 0X9241,
        0X9601, 0X56C0, 0X5780, 0X9741, 0X5500, 0X95C1, 0X9481, 0X5440, 0X9C01, 0X5CC0,
        0X5D80, 0X9D41, 0X5F00, 0X9FC1, 0X9E81, 0X5E40, 0X5A00, 0X9AC1, 0X9B81, 0X5B40,
        0X9901, 0X59C0, 0X5880, 0X9841, 0X8801, 0X48C0, 0X4980, 0X8941, 0X4B00, 0X8BC1,
        0X8A81, 0X4A40, 0X4E00, 0X8EC1, 0X8F81, 0X4F40, 0X8D01, 0X4DC0, 0X4C80, 0X8C41,
        0X4400, 0X84C1, 0X8581, 0X4540, 0X8701, 0X47C0, 0X4680, 0X8641, 0X8201, 0X42C0,
        0X4380, 0X8341, 0X4100, 0X81C1, 0X8081, 0X4040
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
                dataGridView1.Rows.Add(acknowledgeNumber, "Rx",gatewayId.ToString("X12"), deviceId.ToString("X12"), DateTime.Now, BitConverter.ToString(payload).Replace("-", " "));

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

                    if (addressEnd == true && payload.Length == (deviceCount * 2 + 5))
                    {
                        addressArray = payload;

                        addressEnd = false;

                        getData(dataAddress);
                    }
                    if (dataEnd == true && payload.Length == (deviceCount * 6 + 7))
                    {
                        dataArray = payload;

                        dataEnd = false;

                        screen.UserControl1.uc1.sensorDataOutput(addressArray, dataArray, deviceId.ToString("X12"));
                    }

                }

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

        public void addressSaveFile()
        {
            if (deFile.Exists)
            {

            }
            else
            {
                addressItems = new string[] { "24A16057F685", "500291AEBCD9", "500291AEBE4D" };
            }


            FileStream fs = new FileStream("device.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            foreach (string i in screen.UserControl2.uc2.listBox2.Items.OfType<string>().ToArray())
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
            dataArray = dataArray.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            screen.UserControl2.uc2.listBox2.Items.Clear();
            //screen.UserControl1.uc1.comboBox1.Items.Clear();
            screen.UserControl2.uc2.listBox2.Items.AddRange(dataArray);
            addCombobox();
            sr.Close();
        }
        public void gatewaySaveFile()
        {
            if (gaFile.Exists)
            {

            }
            else
            {
                gatewayItems = new string[] { "0" };
            }


            FileStream fs = new FileStream("gateway.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            foreach (string i in screen.UserControl2.uc2.listBox1.Items.OfType<string>().ToArray())
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
            dataArray = dataArray.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            screen.UserControl2.uc2.listBox1.Items.Clear();
            //screen.UserControl1.uc1.comboBox2.Items.Clear();
            screen.UserControl2.uc2.listBox1.Items.AddRange(dataArray);
            addCombobox();
            sr.Close();
        }
        public void addCombobox()
        {

            screen.UserControl1.uc1.comboBox1.Items.Clear();
            string[] allList1 = screen.UserControl2.uc2.listBox2.Items.OfType<string>().ToArray();
            screen.UserControl1.uc1.comboBox1.Items.AddRange(allList1);

            screen.UserControl1.uc1.comboBox2.Items.Clear();
            string[] allList2 = screen.UserControl2.uc2.listBox1.Items.OfType<string>().ToArray();
            screen.UserControl1.uc1.comboBox2.Items.AddRange(allList2);

            Form1.f1.addressItems = allList1;
            Form1.f1.gatewayItems = allList2;
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
        bool addressEnd = false;
        byte[] addressArray;
        public void getAddress(ulong address)
        {
            addressEnd = true;
            TxRtu(++TxCnt, 0, address, new byte[] {   0x01, 0x03,
          0x00, 101, 0x00,deviceCount,
        // 0xAD, 0xDE
            });
        }
        bool dataEnd = false;
        byte[] dataArray;
        public void getData(ulong address)
        {

            TxRtu(++TxCnt, 0, address, new byte[] {   0x01, 0x03,
         0x00,202, 0x00, Convert.ToByte(deviceCount*3+1),
         //0xAD, 0xDE
            });
            dataEnd = true;
        }
        //float GetModbusFloat(byte[] receiveData, Int32 offset)
        //{
        //    byte[] rData = new byte[4];

        //    rData[0] = receiveData[offset + 1];
        //    rData[1] = receiveData[offset + 0];
        //    rData[2] = receiveData[offset + 3];
        //    rData[3] = receiveData[offset + 2];

        //    return BitConverter.ToSingle(rData, 0);
        //}

        //float GetModbusFloat(byte[] receiveData)
        //{
        //    return GetModbusFloat(receiveData, 0);
        //}

        //Int16 GetModbusInt16(byte[] receiveData, Int32 offset)
        //{
        //    return (Int16)((receiveData[offset + 1] << 8) | receiveData[offset + 0]);
        //}

        //Int16 GetModbusInt16(byte[] receiveData)
        //{
        //    return GetModbusInt16(receiveData, 0);
        //}
        
        public ulong dataAddress;

        private void button2_Click(object sender, EventArgs e)
        {
            isUc4 = false;
            isUc5 = false;

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
            isUc4 = true;
            isUc5 = false;

            panel3.Controls.Clear();
            panel3.Controls.Add(UserControl4);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            isUc5 = true;
            isUc4 = false;

            panel3.Controls.Clear();
            panel3.Controls.Add(UserControl5);
        }

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
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            panel3.Controls.Add(UserControl6);
        }
    }
}