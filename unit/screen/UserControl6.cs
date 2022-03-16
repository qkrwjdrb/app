using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace unit.screen
{
    public partial class UserControl6 : UserControl
    {
        public static UserControl6 uc6;
        public UserControl6()
        {
            InitializeComponent();
            uc6 = this;
        }

        private void UserControl6_Load(object sender, EventArgs e)
        {
            deviceBox.SelectedIndex = 0;
            gatewayBox.SelectedIndex = 0;
            Form1.f1.LoadUc6 = true;
            Form1.f1.comboboxSelect();
            timerSec = 500;
            textBox4.Text = "0.5";
        }
        ushort opid = 0;
        // 구동기 모터 주소 : 0x4C752589170d
        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = Convert.ToString(opid);
            //리셋
            if (int.TryParse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _)
                    && ulong.TryParse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _)
                  )
            {
                if (checkBox1.Checked && !string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
                    {
                    0x01, 0x10, 0x01, 0xF5, 0x00, 0x02, 0x04,
                    0x00, 0x01, (byte)Convert.ToInt32(textBox3.Text), (byte)(Convert.ToInt32(textBox1.Text)>>8),
                    });
                }
                else if (checkBox1.Checked && string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("OPID를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
                    {
                    0x01, 0x10, 0x01, 0xF5, 0x00, 0x02, 0x04,
                    0x00, 0x01, (byte)(opid>>8),(byte)opid
                    });
                    opid++;
                }
            }
            else
            {
                MessageBox.Show("입력값을 확인하세요.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //좌회전
            if (int.TryParse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _)
               && ulong.TryParse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _)
            )
            {
                if (checkBox1.Checked && !string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
                       {
                    0x01, 0x10, 0x01, 0xF7, 0x00, 0x04, 0x08,
                    0x01,0x2d,  (byte)Convert.ToInt32(textBox3.Text), (byte)(Convert.ToInt32(textBox1.Text)>>8), 0x00, 0x00, 0x00, 0x00

                       });
                }
                else if (checkBox1.Checked && string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("OPID를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
                    {
                    0x01, 0x10, 0x01, 0xF7, 0x00, 0x04, 0x08,
                    0x01,0x2d, (byte)(opid>>8),(byte)opid, 0x00, 0x00, 0x00, 0x00
                    });
                    opid++;
                }
            }
            else
            {
                MessageBox.Show("입력값을 확인하세요.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //우회전
            if (int.TryParse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _)
              && ulong.TryParse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _)
            )
            {

                if (checkBox1.Checked && !string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
                    {
                    0x01, 0x10, 0x01, 0xF7, 0x00, 0x04, 0x08,
                    0x01, 0x2e,  (byte)Convert.ToInt32(textBox3.Text), (byte)(Convert.ToInt32(textBox1.Text)>>8),0x00, 0x00, 0x00, 0x00
                    });
                }
                else if (checkBox1.Checked && string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("OPID를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
                    {

                    0x01, 0x10, 0x01, 0xF7, 0x00, 0x04, 0x08,
                    0x01, 0x2e, (byte)(opid>>8),(byte)opid, 0x00, 0x00, 0x00, 0x00
                    });
                    opid++;
                }
            }
            else
            {
                MessageBox.Show("입력값을 확인하세요.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //타이머 좌회전
            if (int.TryParse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _)
              && ulong.TryParse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _)
            )
            {

                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    if (checkBox1.Checked && !string.IsNullOrWhiteSpace(textBox3.Text))
                    {
                        Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
                        {
                        0x01, 0x10, 0x01, 0xF7, 0x00, 0x04, 0x08,
                        0x01, 0x2F, 0x00, (byte)Convert.ToInt32(textBox3.Text), (byte)(Convert.ToInt32(textBox1.Text)>>8),(byte)Convert.ToInt32(textBox1.Text), 0x00, 0x00
                        });
                    }
                    else if (checkBox1.Checked && string.IsNullOrWhiteSpace(textBox3.Text))
                    {
                        MessageBox.Show("OPID를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
                        {
                        0x01, 0x10, 0x01, 0xF7, 0x00, 0x04, 0x08,
                        0x01, 0x2F,(byte)(opid>>8),(byte)opid, (byte)(Convert.ToInt32(textBox1.Text)>>8),(byte)Convert.ToInt32(textBox1.Text), 0x00, 0x00
                        });
                        opid++;
                    }

                }
                else
                {
                    MessageBox.Show("시간을 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("입력값을 확인하세요.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //타이머 우회전
            if (int.TryParse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _)
            && ulong.TryParse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _)
            )
            {

                if (!string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    if (checkBox1.Checked && !string.IsNullOrWhiteSpace(textBox3.Text))
                    {
                        Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
                        {
                        0x01, 0x10, 0x01, 0xF7, 0x00, 0x04, 0x08,
                        0x01, 0x30, 0x00, (byte)Convert.ToInt32(textBox3.Text),(byte)(Convert.ToInt32(textBox2.Text)>>8), (byte)Convert.ToInt32(textBox2.Text), 0x00, 0x00
                        });
                    }
                    else if (checkBox1.Checked && string.IsNullOrWhiteSpace(textBox3.Text))
                    {
                        MessageBox.Show("OPID를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
                        {
                        0x01, 0x10, 0x01, 0xF7, 0x00, 0x04, 0x08,
                        0x01, 0x30, (byte)(opid>>8),(byte)opid,  (byte)(Convert.ToInt32(textBox2.Text)>>8), (byte)Convert.ToInt32(textBox2.Text), 0x00, 0x00
                        });
                        opid++;
                    }
                }
                else
                {
                    MessageBox.Show("시간을 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("입력값을 확인하세요.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            //정지
            if (int.TryParse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _)
                && ulong.TryParse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _)
            )
            {

                if (checkBox1.Checked && !string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
                    {
                    0x01, 0x10, 0x01, 0xF7, 0x00, 0x04, 0x08,
                    0x00, 0x00,  (byte)Convert.ToInt32(textBox3.Text), (byte)(Convert.ToInt32(textBox1.Text)>>8),0x00,0x00,0x00,0x00

                    });
                }
                else if (checkBox1.Checked && string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("OPID를 입력해주세요.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Form1.f1.TxRtu(++Form1.f1.TxCnt, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[]
                    {
                    0x01, 0x10, 0x01, 0xF7, 0x00, 0x04, 0x08,
                    0x00, 0x00, (byte)(opid>>8),(byte)opid,0x00,0x00,0x00,0x00
                    });
                    opid++;
                }
            }
            else
            {
                MessageBox.Show("입력값을 확인하세요.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            statescreen();
        }
        private void gatewayBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form1.f1.getewayBoxIndex = gatewayBox.SelectedIndex;
        }
        private void deviceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form1.f1.deviceBoxIndex = deviceBox.SelectedIndex;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //   Form3.f3.label1.Text = "fadsaaaaaaaaaaaaas";
            //  statescreen();
        }
        void statescreen()
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            updateState();
        }
        public int timerSec;
        public Timer timer1 = new Timer();
        void updateState()
        {
            if (checkBox2.Checked)
            {
                timer1.Interval = timerSec;
                timer1.Tick += new EventHandler(Timer_Tick);
                timer1.Start();
            }
            else
            {
                timerStop();
            }
        }
        public void timerStop()
        {

            timer1.Stop();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {

            Debug.WriteLine("TimerTick");

            Form1.f1.TxRtu(0, (uint)int.Parse(gatewayBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), ulong.Parse(deviceBox.Text.ToString(), System.Globalization.NumberStyles.HexNumber), new byte[] {   0x01, 0x03,
                0x00, 203, 0x00,13,
            });
        }

        private void button7_Click_1(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(textBox4.Text))
            {
                float b;

                if (float.TryParse(textBox4.Text.ToString(), out b))
                {
                    // button7.Text = Convert.ToString((int)(b * 1000));
                    if ((int)(b * 1000) > 0)
                    {


                        timer1.Interval = (int)(b * 1000);
                    }
                    else
                    {

                        MessageBox.Show("숫자가 너무 작습니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("입력값을 확인하세요.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("입력값을 확인하세요.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
        }
    }
}