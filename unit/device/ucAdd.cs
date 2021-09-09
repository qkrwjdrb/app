using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace unit.device
{
    public partial class ucAdd : UserControl
    {
        public delegate void delEvent(byte[] sender);
        static public event delEvent textSender;

        public ucAdd()
        {
            InitializeComponent();
        }
        public void add_text_to_combo(TextBox text)
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
            //else
            //{
            //    combo.Items.Add(text.Text);
            //}

            //int count = combo.Items.Count;
            //if (count > 4)
            //{
            //}


        }
        private void add_Load(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ucSelect.f.comboBox6.Items.Add(addGateBox.Text); ucSelect.f.comboBox7.Items.Add(addDevBox.Text);

        }

        private void addGateBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void addDevBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
