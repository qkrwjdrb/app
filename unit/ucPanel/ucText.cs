using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace unit.ucPanel
{
    public partial class ucText : UserControl
    {
        public delegate void delEvent(byte[] sender);
        static public event delEvent textSender;


        byte[] text1 = new byte[10];

        public ucText()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Array.Clear(text1, 0, 9);

            for (int i = 0; i < textBox12.TextLength; i++)
            {
                char p = Convert.ToChar(textBox12.Text.Substring(i, 1));

                text1[i] = Convert.ToByte(p);

            }


            textSender(text1);

        }
    }
    }

