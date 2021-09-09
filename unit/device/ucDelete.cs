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
    public partial class ucDelete : UserControl
    {
        public ucDelete()
        {
            InitializeComponent();
        }



        private void ucDelete_Load(object sender, EventArgs e)
        {

            for (int i = 0; i < ucSelect.f.comboBox6.Items.Count; i++)
            {

                comboBox1.Items.Add(ucSelect.f.comboBox6.Items[i]);
            }
            for (int i = 0; i < ucSelect.f.comboBox7.Items.Count; i++)
            {

                comboBox2.Items.Add(ucSelect.f.comboBox7.Items[i]);
            }
            //comboBox1.Items.Add();
            //    comboBox2.Items.Add(ucSelect.f.comboBox7.Items);
            comboBox1.Items.Add("(선택안함)");
            comboBox2.Items.Add("(선택안함)");

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
     

          

            if (comboBox1.Text == "(선택안함)")
            {
                ;
            }
            else
            {
                ucSelect.f.comboBox6.Items.Remove(comboBox1.Text);
                comboBox1.Items.Remove(comboBox1.Text);
            }

            if (comboBox2.Text == "(선택안함)")
            {
                ;
            }
            else
            {
                ucSelect.f.comboBox7.Items.Remove(comboBox2.Text);
                comboBox2.Items.Remove(comboBox2.Text);
            }

        }
    }
}
