using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Protobuf;
using Grpc.Core;
using Grpc.Net.Client;
using NetExchange;

namespace unit.screen
{
    public partial class UserControl1 : UserControl
    {

        private static GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5054");
        internal static ExProto.ExProtoClient exchange = new ExProto.ExProtoClient(channel);
        //internal static AsyncDuplexStreamingCall<ExMessage, ExMessage> exlink = exchange.ExLink();
        internal static AsyncDuplexStreamingCall<RtuMessage, RtuMessage> rtuLink = exchange.MessageRtu();
        internal static AsyncDuplexStreamingCall<ExtMessage, ExtMessage> extLink = exchange.MessageExt();
        internal static AsyncDuplexStreamingCall<CmdMessage, CmdMessage> cmdLink = exchange.MessageCmd();
        internal  UInt16 TxCnt;
        public static UserControl1 uc1;
        public bool isUc4 = false;
        public UserControl1()
        {
            InitializeComponent();
            Task.Run(() => RtuMessageService());
            Task.Run(() => ExtMessageService());
            Task.Run(() => CmdMessageService());
            comboBox1.Items.Add("24A16057F685");
            comboBox1.Items.Add("500291AEBCD9");
            comboBox1.Items.Add("500291AEBE4D");
            uc1 = this;
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
                if (isUc4)
                {
                    UserControl4.uc4.uc4textBox1.Text = "TxRtu(" + GetProtocolChannelName(channel) + ")";
                    UserControl4.uc4.uc4textBox1.AppendText(Environment.NewLine + $"RequestStream.Channel={channel}");
                    UserControl4.uc4.uc4textBox1.AppendText(Environment.NewLine + $"RequestStream.SequenceNumber={sequenceNumber}");
                    UserControl4.uc4.uc4textBox1.AppendText(Environment.NewLine + $"RequestStream.GatewayId=" + gatewayId.ToString("X6"));
                    UserControl4.uc4.uc4textBox1.AppendText(Environment.NewLine + $"RequestStream.DeviceId=" + deviceId.ToString("X12"));
                    UserControl4.uc4.uc4textBox1.AppendText(Environment.NewLine + $"RequestStream.Tdu.Length={payload.Length}");
                    UserControl4.uc4.uc4textBox1.AppendText(Environment.NewLine + $"RequestStream.Tdu={BitConverter.ToString(payload).Replace("-", string.Empty)}");
                    UserControl4.uc4.uc4textBox1.AppendText(Environment.NewLine);Text += "Responsed... ";
                    UserControl4.uc4.uc4textBox2.Text = "Awaiting response...";

                }

                else
                {
                    uc1textBox1.Text = "TxRtu(" + GetProtocolChannelName(channel) + ") RequestStream";
                    uc1textBox1.AppendText(Environment.NewLine + $"Channel={channel}");
                    uc1textBox1.AppendText(Environment.NewLine + $"SequenceNumber={sequenceNumber}");
                    uc1textBox1.AppendText(Environment.NewLine + $"GatewayId=" + gatewayId.ToString("X6"));
                    uc1textBox1.AppendText(Environment.NewLine + $"DeviceId=" + deviceId.ToString("X12"));
                    uc1textBox1.AppendText(Environment.NewLine + $"Tdu.Length={payload.Length}");
                    uc1textBox1.AppendText(Environment.NewLine + $"Tdu={BitConverter.ToString(payload).Replace("-", " ")}");
                    uc1textBox1.AppendText(Environment.NewLine);
                    uc1textBox2.Text = "Awaiting response...";
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

                if (isUc4)
                {
                    UserControl4.uc4.uc4textBox2.Text = "RxRtu(" + GetProtocolChannelName(channel) + ")";
                    UserControl4.uc4.uc4textBox2.AppendText(Environment.NewLine + $"response.Channel={channel}");
                    UserControl4.uc4.uc4textBox2.AppendText(Environment.NewLine + $"response.AcknowledgeNumber={acknowledgeNumber}");
                    UserControl4.uc4.uc4textBox2.AppendText(Environment.NewLine + $"response.GatewayId=" + gatewayId.ToString("X6"));
                    UserControl4.uc4.uc4textBox2.AppendText(Environment.NewLine + $"response.DeviceId=" + deviceId.ToString("X12"));
                    UserControl4.uc4.uc4textBox2.AppendText(Environment.NewLine + $"response.Tdu.Length={payload.Length}");
                    UserControl4.uc4.uc4textBox2.AppendText(Environment.NewLine + BitConverter.ToString(payload));
                    UserControl4.uc4.uc4textBox2.AppendText(Environment.NewLine);
                    UserControl4.uc4.uc4textBox1.Text += "Responsed... ";


                }

                else { 
                    uc1textBox2.Text = "RxRtu(" + GetProtocolChannelName(channel) + ")";
                    uc1textBox2.AppendText(Environment.NewLine + $"response.Channel={channel}");
                    uc1textBox2.AppendText(Environment.NewLine + $"response.AcknowledgeNumber={acknowledgeNumber}");
                    uc1textBox2.AppendText(Environment.NewLine + $"response.GatewayId=" + gatewayId.ToString("X6"));
                    uc1textBox2.AppendText(Environment.NewLine + $"response.DeviceId=" + deviceId.ToString("X12"));
                    uc1textBox2.AppendText(Environment.NewLine + $"response.Tdu.Length={payload.Length}");
                    uc1textBox2.AppendText(Environment.NewLine + BitConverter.ToString(payload));
                    uc1textBox2.AppendText(Environment.NewLine);
                    uc1textBox1.Text += "Responsed... ";

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

                        sensorDataOutput(addressArray, dataArray, deviceId.ToString("X12"));
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
                uc1textBox2.Text = "TxExt(" + GetProtocolChannelName(channel) + ")";
                uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.Context=" + context.ToString("X16"));
                uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.GatewayId=" + gatewayId.ToString("X6"));
                uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.DeviceId=" + deviceId.ToString("X12"));
                uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.Tdu.Length={payload.Length}");
                uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.Tdu={BitConverter.ToString(payload).Replace("-", string.Empty)}");
                uc1textBox2.AppendText(Environment.NewLine);
                uc1textBox3.Text = "Replied...";

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
                uc1textBox3.Text = "RxExt(" + GetProtocolChannelName(channel) + ")";
                uc1textBox3.AppendText(Environment.NewLine + $"RequestStream.Context=" + context.ToString("X16"));
                uc1textBox3.AppendText(Environment.NewLine + $"response.GatewayId=" + gatewayId.ToString("X6"));
                uc1textBox3.AppendText(Environment.NewLine + $"response.DeviceId=" + deviceId.ToString("X12"));
                uc1textBox3.AppendText(Environment.NewLine + $"response.Tdu.Length={payload.Length}");
                uc1textBox3.AppendText(Environment.NewLine + BitConverter.ToString(payload));
                uc1textBox3.AppendText(Environment.NewLine);
                uc1textBox3.Text += "Awaiting processing...";
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
                uc1textBox2.Text = "TxExt()";
                uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.OpCode=" + opCode.ToString("X4"));
                uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.Route=" + route.ToString("X8"));
                uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.Argument=" + argument.ToString("X8"));
                uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.GatewayId=" + gatewayId.ToString("X6"));
                uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.DeviceId=" + deviceId.ToString("X12"));
                uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.Tdu.Length={payload.Length}");
                uc1textBox2.AppendText(Environment.NewLine + $"RequestStream.Tdu={BitConverter.ToString(payload).Replace("-", string.Empty)}");
                uc1textBox2.AppendText(Environment.NewLine);
                uc1textBox3.Text = "Replied...";
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
                uc1textBox3.Text = "RxCmd()";
                uc1textBox3.AppendText(Environment.NewLine + $"RequestStream.OpCode=" + opCode.ToString("X4"));
                uc1textBox3.AppendText(Environment.NewLine + $"RequestStream.Route=" + route.ToString("X8"));
                uc1textBox3.AppendText(Environment.NewLine + $"RequestStream.Argument=" + argument.ToString("X8"));
                uc1textBox3.AppendText(Environment.NewLine + $"response.GatewayId=" + gatewayId.ToString("X6"));
                uc1textBox3.AppendText(Environment.NewLine + $"response.DeviceId=" + deviceId.ToString("X12"));
                uc1textBox3.AppendText(Environment.NewLine + $"response.Tdu.Length={payload.Length}");
                uc1textBox3.AppendText(Environment.NewLine + BitConverter.ToString(payload));
                uc1textBox3.AppendText(Environment.NewLine);
            });

            switch (opCode)
            {
                case 0:
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        uc1textBox2.Text += "Gateway... ";
                    });
                    break;
            }
        }
        void sensorDataOutput(byte[] address, byte[] data, string deviceId)
        {

            uc1textBox3.Text = $"{deviceId} 센서 데이터";

            for (int i = 1; i < ((address.Length - 3) / 2); i++)
            {
                //  Console.WriteLine($"1111: {4 + i * 6 -4}");

                //  Console.WriteLine($"2222: {2+i * 2 }");
          
                if (data[4 + i * 6] == 0)
                {
                 
                    if (address[1 + i * 2] == 0x00 && address[2 + i * 2] == 0x01)
                    {
                        float temValue = GetFloat(data[4 + i * 6 - 5], data[4 + i * 6 - 4], data[4 + i * 6 - 3], data[4 + i * 6 - 2]);
                        byte[] temBytes = BitConverter.GetBytes(temValue);

                        uc1textBox3.AppendText(Environment.NewLine + $"{i} : 온도 : {BitConverter.ToSingle(temBytes, 0).ToString("0.00")}");

                    }
                    else if (address[1 + i * 2] == 0x00 && address[2 + i * 2] == 0x02)
                    {
                        float temValue = GetFloat(data[4 + i * 6 - 5], data[4 + i * 6 - 4], data[4 + i * 6 - 3], data[4 + i * 6 - 2]);
                        byte[] temBytes = BitConverter.GetBytes(temValue);
                        uc1textBox3.AppendText(Environment.NewLine + $"{i} : 습도 : {BitConverter.ToSingle(temBytes, 0).ToString("0.00")}");

                    }
                    else if (address[1 + i * 2] == 0x00 && address[2 + i * 2] == 0x0b)
                    {
                        float temValue = GetFloat(data[4 + i * 6 - 5], data[4 + i * 6 - 4], data[4 + i * 6 - 3], data[4 + i * 6 - 2]);
                        byte[] temBytes = BitConverter.GetBytes(temValue);
                        uc1textBox3.AppendText(Environment.NewLine + $"{i} : CO2 : {BitConverter.ToSingle(temBytes, 0).ToString("0.00")}");

                    }
                    else if (address[1 + i * 2] == 0x70 && address[2 + i * 2] == 0x01)
                    {
                        float temValue = GetFloat(data[4 + i * 6 - 5], data[4 + i * 6 - 4], data[4 + i * 6 - 3], data[4 + i * 6 - 2]);
                        byte[] temBytes = BitConverter.GetBytes(temValue);
                        uc1textBox3.AppendText(Environment.NewLine + $"{i} : 암모니아 : {BitConverter.ToSingle(temBytes, 0).ToString("0.00")}");

                    }
                    else if (address[1 + i * 2] == 0x70 && address[2 + i * 2] == 0x02)
                    {
                        float temValue = GetFloat(data[4 + i * 6 - 5], data[4 + i * 6 - 4], data[4 + i * 6 - 3], data[4 + i * 6 - 2]);
                        byte[] temBytes = BitConverter.GetBytes(temValue);
                        uc1textBox3.AppendText(Environment.NewLine + $"{i} : 이산화질소 : {BitConverter.ToSingle(temBytes, 0).ToString("0.00")}");

                    }
                    else if (address[1 + i * 2] == 0x70 && address[2 + i * 2] == 0x03)
                    {
                        float temValue = GetFloat(data[4 + i * 6 - 5], data[4 + i * 6 - 4], data[4 + i * 6 - 3], data[4 + i * 6 - 2]);
                        byte[] temBytes = BitConverter.GetBytes(temValue);
                        uc1textBox3.AppendText(Environment.NewLine + $"{i} : 일산화탄소 : {BitConverter.ToSingle(temBytes, 0).ToString("0.00")}");

                    }
                    else if (address[1 + i * 2] == 0x70 && address[2 + i * 2] == 0x04)
                    {
                        float temValue = GetFloat(data[4 + i * 6 - 5], data[4 + i * 6 - 4], data[4 + i * 6 - 3], data[4 + i * 6 - 2]);
                        byte[] temBytes = BitConverter.GetBytes(temValue);
                        uc1textBox3.AppendText(Environment.NewLine + $"{i} : 조도 : {BitConverter.ToSingle(temBytes, 0).ToString("0.00")}");

                    }
                    else if (address[1 + i * 2] == 0x70 && address[2 + i * 2] == 0x05)
                    {
                        float temValue = GetFloat(data[4 + i * 6 - 5], data[4 + i * 6 - 4], data[4 + i * 6 - 3], data[4 + i * 6 - 2]);
                        byte[] temBytes = BitConverter.GetBytes(temValue);
                        uc1textBox3.AppendText(Environment.NewLine + $"{i} : 자외선센서 : {BitConverter.ToSingle(temBytes, 0).ToString("0.00")}");

                    }






                    else if (address[1 + i * 2] == 0x60 && address[2 + i * 2] == 0x01)
                    {
                        float temValue = GetFloat(data[4 + i * 6 - 5], data[4 + i * 6 - 4], data[4 + i * 6 - 3], data[4 + i * 6 - 2]);
                        byte[] temBytes = BitConverter.GetBytes(temValue);
                        uc1textBox3.AppendText(Environment.NewLine + $"{i} : 암모니아 L : {BitConverter.ToSingle(temBytes, 0).ToString("0.00")}");

                    }
                    else if (address[1 + i * 2] == 0x60 && address[2 + i * 2] == 0x02)
                    {
                        float temValue = GetFloat(data[4 + i * 6 - 5], data[4 + i * 6 - 4], data[4 + i * 6 - 3], data[4 + i * 6 - 2]);
                        byte[] temBytes = BitConverter.GetBytes(temValue);
                        uc1textBox3.AppendText(Environment.NewLine + $"{i} : 암모니아 M : {BitConverter.ToSingle(temBytes, 0).ToString("0.00")}");

                    }
                    else if (address[1 + i * 2] == 0x60 && address[2 + i * 2] == 0x03)
                    {
                        float temValue = GetFloat(data[4 + i * 6 - 5], data[4 + i * 6 - 4], data[4 + i * 6 - 3], data[4 + i * 6 - 2]);
                        byte[] temBytes = BitConverter.GetBytes(temValue);
                        uc1textBox3.AppendText(Environment.NewLine + $"{i} : 암모니아 H : {BitConverter.ToSingle(temBytes, 0).ToString("0.00")}");

                    }
                    else if (address[1 + i * 2] == 0x50 && address[2 + i * 2] == 0x01)
                    {
                        float temValue = GetFloat(data[4 + i * 6 - 5], data[4 + i * 6 - 4], data[4 + i * 6 - 3], data[4 + i * 6 - 2]);
                        byte[] temBytes = BitConverter.GetBytes(temValue);
                        uc1textBox3.AppendText(Environment.NewLine + $"{i} : 이산화질소센서 L : {BitConverter.ToSingle(temBytes, 0).ToString("0.00")}");

                    }
                    else if (address[1 + i * 2] == 0x50 && address[2 + i * 2] == 0x02)
                    {
                        float temValue = GetFloat(data[4 + i * 6 - 5], data[4 + i * 6 - 4], data[4 + i * 6 - 3], data[4 + i * 6 - 2]);
                        byte[] temBytes = BitConverter.GetBytes(temValue);
                        uc1textBox3.AppendText(Environment.NewLine + $"{i} : 이산화질소센서 M : {BitConverter.ToSingle(temBytes, 0).ToString("0.00")}");

                    }
                    else if (address[1 + i * 2] == 0x50 && address[2 + i * 2] == 0x03)
                    {
                        float temValue = GetFloat(data[4 + i * 6 - 5], data[4 + i * 6 - 4], data[4 + i * 6 - 3], data[4 + i * 6 - 2]);
                        byte[] temBytes = BitConverter.GetBytes(temValue);
                        uc1textBox3.AppendText(Environment.NewLine + $"{i} : 이산화질소센서 H : {BitConverter.ToSingle(temBytes, 0).ToString("0.00")}");

                    }
                    else if (address[1 + i * 2] == 0x40 && address[2 + i * 2] == 0x01)
                    {
                        float temValue = GetFloat(data[4 + i * 6 - 5], data[4 + i * 6 - 4], data[4 + i * 6 - 3], data[4 + i * 6 - 2]);
                        byte[] temBytes = BitConverter.GetBytes(temValue);
                        uc1textBox3.AppendText(Environment.NewLine + $"{i} : CO일산화탄소 L : {BitConverter.ToSingle(temBytes, 0).ToString("0.00")}");

                    }
                    else if (address[1 + i * 2] == 0x40 && address[2 + i * 2] == 0x02)
                    {
                        float temValue = GetFloat(data[4 + i * 6 - 5], data[4 + i * 6 - 4], data[4 + i * 6 - 3], data[4 + i * 6 - 2]);
                        byte[] temBytes = BitConverter.GetBytes(temValue);
                        uc1textBox3.AppendText(Environment.NewLine + $"{i} : CO일산화탄소 M : {BitConverter.ToSingle(temBytes, 0).ToString("0.00")}");

                    }
                    else if (address[1 + i * 2] == 0x40 && address[2 + i * 2] == 0x03)
                    {
                        float temValue = GetFloat(data[4 + i * 6 - 5], data[4 + i * 6 - 4], data[4 + i * 6 - 3], data[4 + i * 6 - 2]);
                        byte[] temBytes = BitConverter.GetBytes(temValue);
                        uc1textBox3.AppendText(Environment.NewLine + $"{i} : CO일산화탄소 H : {BitConverter.ToSingle(temBytes, 0).ToString("0.00")}");

                    }




                    else
                    {
                        float temValue = GetFloat(data[4 + i * 6 - 5], data[4 + i * 6 - 4], data[4 + i * 6 - 3], data[4 + i * 6 - 2]); 
                        byte[] temBytes = BitConverter.GetBytes(temValue);
                        string str = Encoding.Default.GetString(temBytes);

                        uc1textBox3.AppendText(Environment.NewLine + $"{i} : 빈 장비 : {BitConverter.ToSingle(temBytes, 0).ToString("0.00")}");
                    
                           
                    
                    }


                }
                else
                {
                    uc1textBox3.AppendText(Environment.NewLine + $"{i} : 빈 장비");
                    //    Console.WriteLine("빈 장비");


                }
            }
            float GetFloat(byte a, byte b, byte c, byte d)
            {
                byte[] rData = new byte[4];
                rData[0] = b;
                rData[1] = a;
                rData[2] = d;
                rData[3] = c;

                return BitConverter.ToSingle(rData, 0);
            }

        }
        /*
                 0x24A16057F685
           0x500291AEBCD9
           0x500291AEBE4D
        */


        bool addressEnd = false;
        byte[] addressArray;
        private void getAddress(ulong address)
        {
            addressEnd = true;
            TxRtu(++TxCnt, 0, address, new byte[] {   0x01, 0x03,
          0x00, 101, 0x00,deviceCount,
         0xAD, 0xDE});
        }
        bool dataEnd = false;
        byte[] dataArray;
        private void getData(ulong address)
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
        ulong dataAddress;
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string aa = comboBox1.SelectedItem.ToString();

                ulong device = ulong.Parse(aa, System.Globalization.NumberStyles.HexNumber);
               
                getAddress(device);
                dataAddress = device;
                
            }
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

            getAddress(0x24A16057F685);

            dataAddress = 0x24A16057F685;
        }

    }
}
