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

        }
        ushort opid = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = Convert.ToString(opid);
            //리셋
            if (checkBox1.Checked && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                Form1.f1.TxRtu(++Form1.f1.TxCnt, 0, 0x4C752589170d, new byte[]
                  {
                    0x01, 0x10, 0x01, 0xF5, 0x00, 0x02, 0x04,
                    0x00, 0x01, (byte)Convert.ToInt32(textBox3.Text), (byte)(Convert.ToInt32(textBox1.Text)>>8),
                  });
            }
            else
            {
                Form1.f1.TxRtu(++Form1.f1.TxCnt, 0, 0x4C752589170d, new byte[]
                {
                    0x01, 0x10, 0x01, 0xF5, 0x00, 0x02, 0x04,
                    0x00, 0x01, (byte)(opid>>8),(byte)opid
                });
                opid++;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //좌회전
            if (checkBox1.Checked && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                Form1.f1.TxRtu(++Form1.f1.TxCnt, 0, 0x4C752589170d, new byte[]
                   {
                    0x01, 0x10, 0x01, 0xF7, 0x00, 0x04, 0x08,
                    0x01,0x2d,  (byte)Convert.ToInt32(textBox3.Text), (byte)(Convert.ToInt32(textBox1.Text)>>8), 0x00, 0x00, 0x00, 0x00

                   });
            }
            else
            {
                Form1.f1.TxRtu(++Form1.f1.TxCnt, 0, 0x4C752589170d, new byte[]
                {
                    0x01, 0x10, 0x01, 0xF7, 0x00, 0x04, 0x08,
                    0x01,0x2d, (byte)(opid>>8),(byte)opid, 0x00, 0x00, 0x00, 0x00

                });
                opid++;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //우회전
            if (checkBox1.Checked && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                Form1.f1.TxRtu(++Form1.f1.TxCnt, 0, 0x4C752589170d, new byte[]
                   {

                    0x01, 0x10, 0x01, 0xF7, 0x00, 0x04, 0x08,
                    0x01, 0x2e,  (byte)Convert.ToInt32(textBox3.Text), (byte)(Convert.ToInt32(textBox1.Text)>>8),0x00, 0x00, 0x00, 0x00
                   });
            }
            else
            {
                Form1.f1.TxRtu(++Form1.f1.TxCnt, 0, 0x4C752589170d, new byte[]
                {

                    0x01, 0x10, 0x01, 0xF7, 0x00, 0x04, 0x08,
                    0x01, 0x2e, (byte)(opid>>8),(byte)opid, 0x00, 0x00, 0x00, 0x00
                });
                opid++;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //타이머 좌회전
            if ( !string.IsNullOrWhiteSpace(textBox1.Text))
            {
                if (checkBox1.Checked && !string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    Form1.f1.TxRtu(++Form1.f1.TxCnt, 0, 0x4C752589170d, new byte[]
                    {
                    0x01, 0x10, 0x01, 0xF7, 0x00, 0x04, 0x08,
                    0x01, 0x2F, 0x00, (byte)Convert.ToInt32(textBox3.Text), (byte)(Convert.ToInt32(textBox1.Text)>>8),(byte)Convert.ToInt32(textBox1.Text), 0x00, 0x00

                    });
                }
                else
                {
                    Form1.f1.TxRtu(++Form1.f1.TxCnt, 0, 0x4C752589170d, new byte[]
                    {
                    0x01, 0x10, 0x01, 0xF7, 0x00, 0x04, 0x08,
                    0x01, 0x2F,(byte)(opid>>8),(byte)opid, (byte)(Convert.ToInt32(textBox1.Text)>>8),(byte)Convert.ToInt32(textBox1.Text), 0x00, 0x00

                    });
                    opid++;

                }
            }
            else
            {
                //빈칸알림
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //타이머 우회전
            if ( !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                if (checkBox1.Checked && !string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    Form1.f1.TxRtu(++Form1.f1.TxCnt, 0, 0x4C752589170d, new byte[]
                   {
                     0x01, 0x10, 0x01, 0xF7, 0x00, 0x04, 0x08,
                   0x01, 0x30, 0x00, (byte)Convert.ToInt32(textBox3.Text),(byte)(Convert.ToInt32(textBox2.Text)>>8), (byte)Convert.ToInt32(textBox2.Text), 0x00, 0x00
                   });
                }
                else
                {
                    Form1.f1.TxRtu(++Form1.f1.TxCnt, 0, 0x4C752589170d, new byte[]
                    {
                     0x01, 0x10, 0x01, 0xF7, 0x00, 0x04, 0x08,
                    0x01, 0x30, (byte)(opid>>8),(byte)opid,  (byte)(Convert.ToInt32(textBox2.Text)>>8), (byte)Convert.ToInt32(textBox2.Text), 0x00, 0x00
                    });
                    opid++;
                }

            }
            else
            {
 //               MessageBox.Show((String msg) "Hello Mablang World!");
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            //정지

            if (checkBox1.Checked && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                Form1.f1.TxRtu(++Form1.f1.TxCnt, 0, 0x4C752589170d, new byte[]
                   {
                0x01, 0x10, 0x01, 0xF7, 0x00, 0x04, 0x08,
                0x00, 0x00,  (byte)Convert.ToInt32(textBox3.Text), (byte)(Convert.ToInt32(textBox1.Text)>>8),0x00,0x00,0x00,0x00

                   });
            }
            else
            {
                Form1.f1.TxRtu(++Form1.f1.TxCnt, 0, 0x4C752589170d, new byte[]
                {
                0x01, 0x10, 0x01, 0xF7, 0x00, 0x04, 0x08,
                0x00, 0x00, (byte)(opid>>8),(byte)opid,0x00,0x00,0x00,0x00

                });
                opid++;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {


        }

    }
}
