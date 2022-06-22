using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Protobuf;
using Grpc.Core;
using Grpc.Net.Client;
using NetExchange;

namespace WFormsUserApp
{
    internal partial class Form1 : Form
    {
        private static GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5044");
        internal static ExProto.ExProtoClient exchange = new ExProto.ExProtoClient(channel);
        internal static AsyncDuplexStreamingCall<RtuMessage, RtuMessage> rtuLink = exchange.MessageRtu();
        private static UInt16 TxCnt;

        internal Form1()
        {
            InitializeComponent();
            Task.Run(() => RtuMessageService());       
        }

        private async void RtuMessageService()
        {
            try
            {
                while (await rtuLink.ResponseStream.MoveNext(cancellationToken: CancellationToken.None))
                {
                    var response = rtuLink.ResponseStream.Current;

                    byte protocol = (byte)response.Channel;
                    UInt16 clientId = (UInt16)(response.Channel >> 8);
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        richTextBox2.Text = "RxRtu(" + GetProtocolChannelName(protocol) + ")";
                        richTextBox2.AppendText(Environment.NewLine + $"Client ID={clientId}");
                    });

                    switch (protocol)
                    {
                        case 0:
                            /* Modbus protocol */
                            byte[] payload = new byte[response.DataUnit.Length];
                            response.DataUnit.CopyTo(payload, 0);
                            RxRtu((UInt16)response.SequenceNumber, response.GwId, response.DeviceId, payload);
                            break;
                        default:
                            /* Unknown protocol */
                            break;
                    }
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


        private static UInt16 TxClient;
        public void TxRtu(UInt16 sequenceNumber, UInt32 gatewayId, UInt64 deviceId, byte[] payload)
        {
            byte protocol = 0;
            UInt16 clientID = --TxClient;

            this.Invoke((MethodInvoker)delegate ()
            {
                richTextBox1.Text = "TxRtu(" + GetProtocolChannelName(protocol) + ")";
                richTextBox1.AppendText(Environment.NewLine + $"Client ID={clientID}");
                richTextBox1.AppendText(Environment.NewLine + $"RequestStream.SequenceNumber={sequenceNumber}");
                richTextBox1.AppendText(Environment.NewLine + $"RequestStream.GatewayId=" + gatewayId.ToString("X6"));
                richTextBox1.AppendText(Environment.NewLine + $"RequestStream.DeviceId=" + deviceId.ToString("X12"));
                richTextBox1.AppendText(Environment.NewLine + $"RequestStream.Tdu.Length={payload.Length}");
                richTextBox1.AppendText(Environment.NewLine + $"RequestStream.Tdu={BitConverter.ToString(payload).Replace("-", string.Empty)}");
                richTextBox1.AppendText(Environment.NewLine);
                richTextBox2.Text = "Awaiting response...";
            });

            rtuLink.RequestStream.WriteAsync(new RtuMessage
            {
                Channel = ((UInt32)clientID << 8) | protocol,
                SequenceNumber = sequenceNumber,
                GwId = gatewayId,
                DeviceId = deviceId,
                DataUnit = ByteString.CopyFrom(payload[0..payload.Length])
            });
        }

        public void RxRtu(UInt16 acknowledgeNumber, UInt32 gatewayId, UInt64 deviceId, byte[] payload)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                richTextBox2.AppendText(Environment.NewLine + $"response.AcknowledgeNumber={acknowledgeNumber}");
                richTextBox2.AppendText(Environment.NewLine + $"response.GatewayId=" + gatewayId.ToString("X6"));
                richTextBox2.AppendText(Environment.NewLine + $"response.DeviceId=" + deviceId.ToString("X12"));
                richTextBox2.AppendText(Environment.NewLine + $"response.Tdu.Length={payload.Length}");
                richTextBox2.AppendText(Environment.NewLine + BitConverter.ToString(payload));
                richTextBox2.AppendText(Environment.NewLine);
                richTextBox1.Text += "Responsed... ";
            });
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            TxRtu(++TxCnt, 0, 0, new byte[] { 0x01, 0x03, 0x00, 0x01, 0x00, 0x08, 0x15, 0xCC });
        }

        /* 0x308398CAFCA1 */

        /* 0x500291A45D3D */

        /* 0x24A16057F685 */
        /* 0x500291AEBCD9 */
        /* 0x500291AEBE4D */

        /* 0x4C7525891309 */
        /* 0x4C75258916ED */
        /* 0x4C7525891709 */
        /* 0x4C7525891719 */
        /* 0x4C752589171D */

        /* 0x4C75258914D9 */
        /* 0x4C75258914DD */
        /* 0x4C752589150D */
        /* 0x500291A40AF5 */

        /* 0x4C7525C1CF79 */
        /* 0x4C7525C1CF89 */
        /* 0x4C7525C1CF95 */

        /* 0x24A1605818D9 */

        private void button2_Click(object sender, EventArgs e)
        {
#if false
            TxRtu(++TxCnt, 0, 0x500291AEBEF5, new byte[] { 0x01, 0x03, 0x00, 0x01, 0x00, 0x08, 0x15, 0xCC });
#elif true // DC Motor
            TxRtu(++TxCnt, 0, 0x24A16057C915, new byte[] { 0x01, 0x10, 0x01, 0xF5, 0x00, 0x02, 0x04, 0x00, 0x01, 0x00, 0x01, 0xAD, 0xDE });
#elif true // 2CH Switch
            TxRtu(++TxCnt, 0, 0x500291AEBEF5, new byte[] { 0x01, 0x10, 0x01, 0xF5, 0x00, 0x02, 0x04, 0x00, 0x01, 0x00, 0x01, 0xAD, 0xDE });
#endif
        }

        private void button3_Click(object sender, EventArgs e)
        {
#if false
            TxRtu(++TxCnt, 0, 0x500291AEBEF5, new byte[] { 0x01, 0x03, 0x00, 0x65, 0x00, 0x28, 0x55, 0xCB });
#elif true // DC Motor
            TxRtu(++TxCnt, 0, 0x500291AEBEF1, new byte[] { 0x01, 0x10, 0x01, 0xF7, 0x00, 0x04, 0x08, 0x01, 0x2F, 0x00, 0x02, 0x00, 0x02, 0x00, 0x00, 0xAD, 0xDE });
#elif true // 2CH Switch
            TxRtu(++TxCnt, 0, 0x500291AEBEF5, new byte[] { 0x01, 0x10, 0x01, 0xF7, 0x00, 0x08, 0x10, 0x00, 0xC9, 0x00, 0x02, 0x00, 0x02, 0x00, 0x00, 0x00, 0xCA, 0x00, 0x02, 0x00, 0x02, 0x00, 0x00, 0xAD, 0xDE });
#endif
        }

        private void button4_Click(object sender, EventArgs e)
        {
#if false
            TxRtu(++TxCnt, 0, 0x500291AEBEF5, new byte[] { 0x01, 0x03, 0x00, 0xC9, 0x00, 0x6F, 0xD5, 0xD8 });
#elif true // DC Motor
            TxRtu(++TxCnt, 0, 0x500291AEBEF1, new byte[] { 0x01, 0x10, 0x01, 0xF7, 0x00, 0x04, 0x08, 0x01, 0x30, 0x00, 0x03, 0x00, 0x02, 0x00, 0x00, 0xAD, 0xDE });
#elif true // 2CH Switch
            TxRtu(++TxCnt, 0, 0x500291AEBEF5, new byte[] { 0x01, 0x10, 0x01, 0xF7, 0x00, 0x08, 0x10, 0x00, 0xCA, 0x00, 0x03, 0x00, 0x02, 0x00, 0x00, 0x00, 0xC9, 0x00, 0x03, 0x00, 0x02, 0x00, 0x00, 0xAD, 0xDE });
#endif
        }

        private void button5_Click(object sender, EventArgs e)
        {
          // TxExt(0, 0, 0, new byte[] { 0x01, 0x03, 0x00, 0x01, 0x00, 0x08, 0x15, 0xCC });
        }
    }
}