

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

        public static Form1 form1;

        screen.UserControl1 UserControl1 = new screen.UserControl1();
        screen.UserControl2 UserControl2 = new screen.UserControl2();
        screen.UserControl3 UserControl3 = new screen.UserControl3();
        screen.UserControl4 UserControl4 = new screen.UserControl4();
        screen.UserControl5 UserControl5 = new screen.UserControl5();


        private static GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5054");
        internal static ExProto.ExProtoClient exchange = new ExProto.ExProtoClient(channel);

        internal static AsyncDuplexStreamingCall<RtuMessage, RtuMessage> rtuLink = exchange.MessageRtu();
        internal static AsyncDuplexStreamingCall<ExtMessage, ExtMessage> extLink = exchange.MessageExt();
        internal static AsyncDuplexStreamingCall<CmdMessage, CmdMessage> cmdLink = exchange.MessageCmd();
        internal UInt16 TxCnt;

        string[] save_gateways = new string[5];

        public UInt32 gateway;
        public UInt64 devicead;
        string textData;

        public bool isUc4 = false;
        public bool isUc5 = false;

        public string[] addressItems = { "24A16057F685", "500291AEBCD9", "500291AEBE4D" };

        public Form1()
        {
            InitializeComponent();
            Task.Run(() => RtuMessageService());
            Task.Run(() => ExtMessageService());
            Task.Run(() => CmdMessageService());

            form1 = this;

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

        public void TxRtu(UInt16 sequenceNumber, UInt32 gatewayId, UInt64 deviceId, byte[] payload)
        {
            UInt16 channel = 0;

            this.Invoke((MethodInvoker)delegate ()
            {

                dataGridView1.Rows.Add(sequenceNumber, "Tx", deviceId.ToString("X12"), DateTime.Now, BitConverter.ToString(payload).Replace("-", " "));

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

        byte deviceCount = 30;
        public void RxRtu(UInt16 acknowledgeNumber, UInt32 gatewayId, UInt64 deviceId, byte[] payload)
        {
            UInt16 channel = 0;

            this.Invoke((MethodInvoker)delegate ()
            {
                dataGridView1.Rows.Add(acknowledgeNumber, "Rx", deviceId.ToString("X12"), DateTime.Now, BitConverter.ToString(payload).Replace("-", " "));

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
        bool addressEnd = false;
        byte[] addressArray;
        public void getAddress(ulong address)
        {
            addressEnd = true;
            TxRtu(++TxCnt, 0, address, new byte[] {   0x01, 0x03,
          0x00, 101, 0x00,deviceCount,
         0xAD, 0xDE});
        }
        bool dataEnd = false;
        byte[] dataArray;
        public void getData(ulong address)
        {

            TxRtu(++TxCnt, 0, address, new byte[] {   0x01, 0x03,
         0x00,202, 0x00, Convert.ToByte(deviceCount*3+1),
         0xAD, 0xDE});
            dataEnd = true;
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
                clicked = false;
                this.Size = new Size(761, 632);
            }
            else
            {
                clicked = true;
                this.Size = new Size(1588, 632);
            }

        }
    }

}

