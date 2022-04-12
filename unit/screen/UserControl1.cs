using System;
using System.Windows.Forms;

namespace unit.screen
{
    public partial class UserControl1 : UserControl
    {
        public static UserControl1 uc1;
        public UserControl1()
        {
            InitializeComponent();


            uc1 = this;
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            UserControl2.uc2.addCombobox();

            datetypeBox.DisplayMember = "Text";
            datetypeBox.ValueMember = "Value";
            var items = new[] {
                new { Text = "센서 데이터", Value = "센서" },
                new { Text = "노드정보", Value = "노드정보" },
                new { Text = "구동기 상태", Value = "구동기 상태" },
            };

            datetypeBox.DataSource = items;
            deviceBox.SelectedIndex = 0;
            gatewayBox.SelectedIndex = 0;
            datetypeBox.SelectedIndex = 0;
            Form1.f1.LoadUc1 = true;
            Form1.f1.comboboxSelect();
        }


        public void NodeDataOutput(byte[] address, string deviceId)
        {
            uc1textBox3.Text = $"{deviceId} 노드 정보";

            uc1textBox3.AppendText(
                Environment.NewLine + "기관코드 : " + BitConverter.ToUInt16(new byte[2] { address[4], address[3] }, 0)
                + Environment.NewLine + "회사코드 : " + BitConverter.ToUInt16(new byte[2] { address[6], address[5] }, 0)
                + Environment.NewLine + "제품타입 : " + BitConverter.ToUInt16(new byte[2] { address[8], address[7] }, 0)
                + Environment.NewLine + "제품코드 : " + BitConverter.ToUInt16(new byte[2] { address[10], address[9] }, 0)
                + Environment.NewLine + "프로토콜 버전 : " + BitConverter.ToUInt16(new byte[2] { address[12], address[11] }, 0)
                + Environment.NewLine + "연결가능디바이스수 : " + BitConverter.ToUInt16(new byte[2] { address[14], address[13] }, 0)
                + Environment.NewLine + "노드시리얼번호 : " + BitConverter.ToString(new byte[4] { address[18], address[17], address[16], address[15] }, 0)
            );
        }
        public void StateDataOutput(byte[] address, string deviceId)
        {
            uc1textBox3.Text = $"{deviceId} 구동기 상태";

            float voltageFloat = GetFloatState(address[21], address[22], address[23], address[24]);
            float CurrentFloat = GetFloatState(address[13], address[14], address[15], address[16]);

            string stateText = "";
            if (BitConverter.ToUInt16(new byte[2] { address[8], address[7] }, 0) == 0)
            {
                stateText = "STOP";
            }
            if (BitConverter.ToUInt16(new byte[2] { address[8], address[7] }, 0) == 301)
            {
                stateText = "OPEN";
            }
            if (BitConverter.ToUInt16(new byte[2] { address[8], address[7] }, 0) == 302)
            {
                stateText = "CLOSE";
            }
            if (BitConverter.ToUInt16(new byte[2] { address[8], address[7] }, 0) == 303)
            {
                stateText = "TIMED_OPEN";
            }
            if (BitConverter.ToUInt16(new byte[2] { address[8], address[7] }, 0) == 304)
            {
                stateText = "TIMED_CLOSE";
            }
            if (UserControl6.uc6.checkBox2.Checked)
            {
                UserControl6.uc6.상태.Text = stateText;
                UserControl6.uc6.상태코드.Text = Convert.ToString(BitConverter.ToUInt16(new byte[2] { address[8], address[7] }, 0));
                UserControl6.uc6.OPID.Text = Convert.ToString(BitConverter.ToUInt16(new byte[2] { address[6], address[5] }, 0));
                UserControl6.uc6.남은시간.Text = Convert.ToString(BitConverter.ToUInt16(new byte[2] { address[10], address[9] }, 0));
                UserControl6.uc6.전압.Text = Convert.ToString(voltageFloat); 
                UserControl6.uc6.전류.Text = Convert.ToString(CurrentFloat);





            }
            if (Form1.f1.isUc1)
            {
                uc1textBox3.AppendText(
                Environment.NewLine + "상태 : " + stateText
                + Environment.NewLine + "상태코드 : " + BitConverter.ToUInt16(new byte[2] { address[8], address[7] }, 0)
                + Environment.NewLine + "OPID : " + BitConverter.ToUInt16(new byte[2] { address[6], address[5] }, 0)
                + Environment.NewLine + "남은동작시간 : " + BitConverter.ToUInt16(new byte[2] { address[10], address[9] }, 0)
                + Environment.NewLine + "전압 : " + voltageFloat 
                + Environment.NewLine + "전류 : " + CurrentFloat
            );
            }

            float GetFloatState(byte a, byte b, byte c, byte d)
            {
                byte[] rData = new byte[4];
                if (BitConverter.IsLittleEndian) Array.Reverse(rData);
                rData[0] = b;
                rData[1] = a;
                rData[2] = d;
                rData[3] = c;

                return BitConverter.ToSingle(rData, 0);
            }
        }


        public void sensorDataOutput(byte[] address, byte[] data, string deviceId)
        {

            uc1textBox3.Text = $"{deviceId} 센서 데이터";

            for (int i = 1; i < ((address.Length - 3) / 2); i++)
            {
                if (data[4 + i * 6] == 0)
                {
                    float dataFloat = GetFloat(data[4 + i * 6 - 5], data[4 + i * 6 - 4], data[4 + i * 6 - 3], data[4 + i * 6 - 2]);
                    byte[] dataBytes = BitConverter.GetBytes(dataFloat);
                    byte[] addressBytes = { address[1 + i * 2], address[2 + i * 2] };
                    if (BitConverter.IsLittleEndian) Array.Reverse(addressBytes);
                    short addressShort = BitConverter.ToInt16(addressBytes, 0);

                    switch (addressShort)
                    {
                        case 0x0001:
                            if (emptyDeviceCheck.Checked)
                                uc1textBox3.AppendText(Environment.NewLine + $"{i} 온도 : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            else
                                uc1textBox3.AppendText(Environment.NewLine + $"온도 : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            break;
                        case 0x0002:
                            if (emptyDeviceCheck.Checked)
                                uc1textBox3.AppendText(Environment.NewLine + $"{i} 습도 : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            else
                                uc1textBox3.AppendText(Environment.NewLine + $"습도 : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            break;
                        case 0x000b:
                            if (emptyDeviceCheck.Checked)
                                uc1textBox3.AppendText(Environment.NewLine + $"{i} CO2 : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            else
                                uc1textBox3.AppendText(Environment.NewLine + $"CO2 : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            break;
                        case 0x7001:
                            if (emptyDeviceCheck.Checked)
                                uc1textBox3.AppendText(Environment.NewLine + $"{i} 암모니아 : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            else
                                uc1textBox3.AppendText(Environment.NewLine + $"암모니아 : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            break;
                        case 0x7002:
                            if (emptyDeviceCheck.Checked)
                                uc1textBox3.AppendText(Environment.NewLine + $"{i} 이산화질소 : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            else
                                uc1textBox3.AppendText(Environment.NewLine + $"이산화질소 : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            break;
                        case 0x7003:
                            if (emptyDeviceCheck.Checked)
                                uc1textBox3.AppendText(Environment.NewLine + $"{i} 일산화탄소 : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            else
                                uc1textBox3.AppendText(Environment.NewLine + $"일산화탄소 : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            break;
                        case 0x7004:
                            if (emptyDeviceCheck.Checked)
                                uc1textBox3.AppendText(Environment.NewLine + $"{i} 조도 : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            else
                                uc1textBox3.AppendText(Environment.NewLine + $"조도 : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            break;
                        case 0x7005:
                            if (emptyDeviceCheck.Checked)
                                uc1textBox3.AppendText(Environment.NewLine + $"{i} 자외선센서 : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            else
                                uc1textBox3.AppendText(Environment.NewLine + $"자외선센서 : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            break;
                        case 0x6001:
                            if (emptyDeviceCheck.Checked)
                                uc1textBox3.AppendText(Environment.NewLine + $"{i} 암모니아 L : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            else
                                uc1textBox3.AppendText(Environment.NewLine + $"암모니아 L : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            break;
                        case 0x6002:
                            if (emptyDeviceCheck.Checked)
                                uc1textBox3.AppendText(Environment.NewLine + $"{i} 암모니아 M : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            else
                                uc1textBox3.AppendText(Environment.NewLine + $"암모니아 M : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            break;
                        case 0x6003:
                            if (emptyDeviceCheck.Checked)
                                uc1textBox3.AppendText(Environment.NewLine + $"{i} 암모니아 H : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            else
                                uc1textBox3.AppendText(Environment.NewLine + $"암모니아 H : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            break;
                        case 0x5001:
                            if (emptyDeviceCheck.Checked)
                                uc1textBox3.AppendText(Environment.NewLine + $"{i} 이산화질소센서 L : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            else
                                uc1textBox3.AppendText(Environment.NewLine + $"이산화질소센서 L : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            break;
                        case 0x5002:
                            if (emptyDeviceCheck.Checked)
                                uc1textBox3.AppendText(Environment.NewLine + $"{i} 이산화질소센서 M : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            else
                                uc1textBox3.AppendText(Environment.NewLine + $"이산화질소센서 M : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            break;
                        case 0x5003:
                            if (emptyDeviceCheck.Checked)
                                uc1textBox3.AppendText(Environment.NewLine + $"{i} 이산화질소센서 H : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            else
                                uc1textBox3.AppendText(Environment.NewLine + $"이산화질소센서 H : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            break;
                        case 0x4001:
                            if (emptyDeviceCheck.Checked)
                                uc1textBox3.AppendText(Environment.NewLine + $"{i} CO일산화탄소 L : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            else
                                uc1textBox3.AppendText(Environment.NewLine + $"CO일산화탄소 L : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            break;
                        case 0x4002:
                            if (emptyDeviceCheck.Checked)
                                uc1textBox3.AppendText(Environment.NewLine + $"{i} CO일산화탄소 M : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            else
                                uc1textBox3.AppendText(Environment.NewLine + $"CO일산화탄소 M : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            break;
                        case 0x4003:
                            if (emptyDeviceCheck.Checked)
                                uc1textBox3.AppendText(Environment.NewLine + $"{i} CO일산화탄소 H : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            else
                                uc1textBox3.AppendText(Environment.NewLine + $"CO일산화탄소 H : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            break;
                        default:
                            if (emptyDeviceCheck.Checked)
                                uc1textBox3.AppendText(Environment.NewLine + $"{i} 미정의 장비 {BitConverter.ToString(addressBytes)} : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            else
                                uc1textBox3.AppendText(Environment.NewLine + $"미정의 장비 {BitConverter.ToString(addressBytes)} : {BitConverter.ToSingle(dataBytes, 0).ToString("0.00")}");
                            break;
                    }
                }
                else if (emptyDeviceCheck.Checked)
                {
                    uc1textBox3.AppendText(Environment.NewLine + $"{i} 빈 장비");
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



        private void button1_Click(object sender, EventArgs e)
        {
            if (datetypeBox.SelectedValue is string)
            {

                if ((string)datetypeBox.SelectedValue == "센서")
                {
                    if (!string.IsNullOrWhiteSpace(deviceNumberBox.Text) && byte.TryParse(deviceNumberBox.Text, out _))
                    {
                        Form1.f1.deviceCount = Convert.ToByte(deviceNumberBox.Text);
                    }
                    else
                    {
                        MessageBox.Show("입력값을 확인하세요.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (deviceBox.Text != null)
                    {
                        uint gateway = (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber);
                        ulong device = ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber);

                        Form1.f1.dataGateway = gateway;
                        Form1.f1.dataAddress = device;
                        getAddress(gateway, device);
                    }
                }
            }
            else MessageBox.Show("입력값을 확인하세요.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if ((string)datetypeBox.SelectedValue == "노드정보")
            {
                string aa = deviceBox.Text.ToString();

                uint gateway = (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber);
                ulong device = ulong.Parse(aa, System.Globalization.NumberStyles.HexNumber);
                getNode(gateway, device);
            }
            if ((string)datetypeBox.SelectedValue == "구동기 상태")
            {
                string aa = deviceBox.Text.ToString();

                uint gateway = (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber);
                ulong device = ulong.Parse(aa, System.Globalization.NumberStyles.HexNumber);
                getState(gateway, device);
            }
        }
        public void getAddress(uint gatewayID, ulong deviceID)
        {
            Form1.f1.addressEnd = true;
            Form1.f1.TxRtu(++Form1.f1.TxCnt, gatewayID, deviceID, new byte[] {   0x01, 0x03,
            0x00, 101, 0x00,Form1.f1.deviceCount,
            });
        }

        private void getNode(uint gatewayID, ulong deviceID)
        {
            Form1.f1.isNode = true;
            Form1.f1.TxRtu(++Form1.f1.TxCnt, gatewayID, deviceID, new byte[] {   0x01, 0x03,
                0x00, 1, 0x00,8,
            });
        }
        public void getState(uint gatewayID, ulong deviceID)
        {


            Form1.f1.isState = true;

            Form1.f1.TxRtu(++Form1.f1.TxCnt, gatewayID, deviceID, new byte[] {   0x01, 0x03,
                0x00, 203, 0x00,13,
            });
        }
        private void gatewayBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form1.f1.getewayBoxIndex = gatewayBox.SelectedIndex;
        }
        private void deviceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form1.f1.deviceBoxIndex = deviceBox.SelectedIndex;
        }

    }
}