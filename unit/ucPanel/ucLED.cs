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
    public partial class ucLED : UserControl
    {
        byte[] led = new byte[8];
        public delegate void delEvent(byte[] ledData);
        static public event delEvent ledSender;
        byte brightness = 1;
        public ucLED()
        {
            InitializeComponent();
        }

        private void ucScreen3_Load(object sender, EventArgs e)
        {

            comboBox1.Items.Add("Red");
            comboBox1.Items.Add("Orange");
            comboBox1.Items.Add("Yellow");
            comboBox1.Items.Add("Green");
            comboBox1.Items.Add("Blue");
            comboBox1.Items.Add("Puple");
            comboBox1.Items.Add("Pink");
            comboBox1.Items.Add("Brown");
            comboBox1.Items.Add("White");
            comboBox1.Items.Add("Off");


            comboBox2.Items.Add("Red");
            comboBox2.Items.Add("Orange");
            comboBox2.Items.Add("Yellow");
            comboBox2.Items.Add("Green");
            comboBox2.Items.Add("Blue");
            comboBox2.Items.Add("Puple");
            comboBox2.Items.Add("Pink");
            comboBox2.Items.Add("Brown");
            comboBox2.Items.Add("White");
            comboBox2.Items.Add("Off");
            trackBar1.Enabled = false;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {



            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 1;
            #region led
            string v = Convert.ToString(comboBox1.Text);
            brightness = 1;
            led[0] = 0; led[1] = 0; led[2] = 0; led[3] = 0;
            switch (v)
            {
                case "Red":
                    led[2] = Convert.ToByte(255 * brightness / 10);
                    led[0] = Convert.ToByte(0 * brightness / 10);
                    led[1] = Convert.ToByte(0 * brightness / 10);
                    red1.Text = Convert.ToString(led[2]);
                    green1.Text = Convert.ToString(led[0]);
                    blue1.Text = Convert.ToString(led[1]);


                    break;

                case "Orange":
                    led[2] = Convert.ToByte(255 * brightness / 10);
                    led[0] = Convert.ToByte(165 * brightness / 10);
                    led[1] = Convert.ToByte(0 * brightness / 10);
                    red1.Text = Convert.ToString(led[2]);
                    green1.Text = Convert.ToString(led[0]);
                    blue1.Text = Convert.ToString(led[1]); break;

                case "Yellow":
                    led[2] = Convert.ToByte(255 * brightness / 10);
                    led[0] = Convert.ToByte(255 * brightness / 10);
                    led[1] = Convert.ToByte(0 * brightness / 10);
                    red1.Text = Convert.ToString(led[2]);
                    green1.Text = Convert.ToString(led[0]);
                    blue1.Text = Convert.ToString(led[1]); break;

                case "Green":
                    led[2] = Convert.ToByte(0 * brightness / 10);
                    led[0] = Convert.ToByte(255 * brightness / 10);
                    led[1] = Convert.ToByte(0 * brightness / 10);
                    red1.Text = Convert.ToString(led[2]);
                    green1.Text = Convert.ToString(led[0]);
                    blue1.Text = Convert.ToString(led[1]); break;
                case "Blue":
                    led[2] = Convert.ToByte(0 * brightness / 10);
                    led[0] = Convert.ToByte(0 * brightness / 10);
                    led[1] = Convert.ToByte(255 * brightness / 10);
                    red1.Text = Convert.ToString(led[2]);
                    green1.Text = Convert.ToString(led[0]);
                    blue1.Text = Convert.ToString(led[1]); break;

                case "Puple":
                    led[2] = Convert.ToByte(128 * brightness / 10);
                    led[0] = Convert.ToByte(0 * brightness / 10);
                    led[1] = Convert.ToByte(128 * brightness / 10);
                    red1.Text = Convert.ToString(led[2]);
                    green1.Text = Convert.ToString(led[0]);
                    blue1.Text = Convert.ToString(led[1]); break;
                case "Pink":
                    led[2] = Convert.ToByte(255 * brightness / 10);
                    led[0] = Convert.ToByte(182 * brightness / 10);
                    led[1] = Convert.ToByte(193 * brightness / 10);
                    red1.Text = Convert.ToString(led[2]);
                    green1.Text = Convert.ToString(led[0]);
                    blue1.Text = Convert.ToString(led[1]); break;

                case "Brown":
                    led[2] = Convert.ToByte(165 * brightness / 10);
                    led[0] = Convert.ToByte(42 * brightness / 10);
                    led[1] = Convert.ToByte(42 * brightness / 10);
                    red1.Text = Convert.ToString(led[2]);
                    green1.Text = Convert.ToString(led[0]);
                    blue1.Text = Convert.ToString(led[1]); break;
                case "White":
                    led[2] = Convert.ToByte(255 * brightness / 10);
                    led[0] = Convert.ToByte(255 * brightness / 10);
                    led[1] = Convert.ToByte(255 * brightness / 10);
                    red1.Text = Convert.ToString(led[2]);
                    green1.Text = Convert.ToString(led[0]);
                    blue1.Text = Convert.ToString(led[1]); break;
                case "Off":
                    led[2] = 0;
                    led[0] = 0;
                    led[1] = 0;
                    break;


                default:
                    led[2] = Convert.ToByte(0 * brightness / 10);
                    led[0] = Convert.ToByte(0 * brightness / 10);
                    led[1] = Convert.ToByte(0 * brightness / 10);
                    break;
            }

            string w = Convert.ToString(comboBox2.Text);
            led[4] = 0; led[5] = 0; led[6] = 0; led[7] = 0;
            switch (w)
            {
                case "Red":
                    led[6] = Convert.ToByte(255 * brightness / 10);
                    led[4] = Convert.ToByte(0 * brightness / 10);
                    led[5] = Convert.ToByte(0 * brightness / 10);
                    red2.Text = Convert.ToString(led[6]);
                    green2.Text = Convert.ToString(led[4]);
                    blue2.Text = Convert.ToString(led[5]); break;

                case "Orange":
                    led[6] = Convert.ToByte(255 * brightness / 10);
                    led[4] = Convert.ToByte(165 * brightness / 10);
                    led[5] = Convert.ToByte(0 * brightness / 10);
                    red2.Text = Convert.ToString(led[6]);
                    green2.Text = Convert.ToString(led[4]);
                    blue2.Text = Convert.ToString(led[5]); break;

                case "Yellow":
                    led[6] = Convert.ToByte(255 * brightness / 10);
                    led[4] = Convert.ToByte(255 * brightness / 10);
                    led[5] = Convert.ToByte(0 * brightness / 10);
                    red2.Text = Convert.ToString(led[6]);
                    green2.Text = Convert.ToString(led[4]);
                    blue2.Text = Convert.ToString(led[5]); break;

                case "Green":
                    led[6] = Convert.ToByte(0 * brightness / 10);
                    led[4] = Convert.ToByte(255 * brightness / 10);
                    led[5] = Convert.ToByte(0 * brightness / 10);
                    red2.Text = Convert.ToString(led[6]);
                    green2.Text = Convert.ToString(led[4]);
                    blue2.Text = Convert.ToString(led[5]); break;
                case "Blue":
                    led[6] = Convert.ToByte(0 * brightness / 10);
                    led[4] = Convert.ToByte(0 * brightness / 10);
                    led[5] = Convert.ToByte(255 * brightness / 10);
                    red2.Text = Convert.ToString(led[6]);
                    green2.Text = Convert.ToString(led[4]);
                    blue2.Text = Convert.ToString(led[5]); break;

                case "Puple":
                    led[6] = Convert.ToByte(128 * brightness / 10);
                    led[4] = Convert.ToByte(0 * brightness / 10);
                    led[5] = Convert.ToByte(128 * brightness / 10);
                    red2.Text = Convert.ToString(led[6]);
                    green2.Text = Convert.ToString(led[4]);
                    blue2.Text = Convert.ToString(led[5]); break;
                case "Pink":
                    led[6] = Convert.ToByte(255 * brightness / 10);
                    led[4] = Convert.ToByte(182 * brightness / 10);
                    led[5] = Convert.ToByte(193 * brightness / 10);
                    red2.Text = Convert.ToString(led[6]);
                    green2.Text = Convert.ToString(led[4]);
                    blue2.Text = Convert.ToString(led[5]); break;

                case "Brown":
                    led[6] = Convert.ToByte(165 * brightness / 10);
                    led[4] = Convert.ToByte(42 * brightness / 10);
                    led[5] = Convert.ToByte(42 * brightness / 10);
                    red2.Text = Convert.ToString(led[6]);
                    green2.Text = Convert.ToString(led[4]);
                    blue2.Text = Convert.ToString(led[5]); break;
                case "White":
                    led[6] = Convert.ToByte(255 * brightness / 10);
                    led[4] = Convert.ToByte(255 * brightness / 10);
                    led[5] = Convert.ToByte(255 * brightness / 10);
                    red2.Text = Convert.ToString(led[6]);
                    green2.Text = Convert.ToString(led[4]);
                    blue2.Text = Convert.ToString(led[5]); break;
                case "Off":
                    led[6] = 0;
                    led[4] = 0;
                    led[5] = 0;
                    break;

                default:
                    led[6] = Convert.ToByte(0 * brightness / 10);
                    led[4] = Convert.ToByte(0 * brightness / 10);
                    led[5] = Convert.ToByte(0 * brightness / 10);
                    break;
            }

            ledSender(led);
            #endregion
            trackBar1.Enabled = true;
           
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            brightness = Convert.ToByte(trackBar1.Value);
            #region led


            string v = Convert.ToString(comboBox1.Text);

            led[0] = 0; led[1] = 0; led[2] = 0; led[3] = 0;
            switch (v)
            {
                case "Red":
                    led[2] = Convert.ToByte(255 * brightness / 10);
                    led[0] = Convert.ToByte(0 * brightness / 10);
                    led[1] = Convert.ToByte(0 * brightness / 10);
                    red1.Text = Convert.ToString(led[2]);
                    green1.Text = Convert.ToString(led[0]);
                    blue1.Text = Convert.ToString(led[1]);


                    break;

                case "Orange":
                    led[2] = Convert.ToByte(255 * brightness / 10);
                    led[0] = Convert.ToByte(165 * brightness / 10);
                    led[1] = Convert.ToByte(0 * brightness / 10);
                    red1.Text = Convert.ToString(led[2]);
                    green1.Text = Convert.ToString(led[0]);
                    blue1.Text = Convert.ToString(led[1]); break;

                case "Yellow":
                    led[2] = Convert.ToByte(255 * brightness / 10);
                    led[0] = Convert.ToByte(255 * brightness / 10);
                    led[1] = Convert.ToByte(0 * brightness / 10);
                    red1.Text = Convert.ToString(led[2]);
                    green1.Text = Convert.ToString(led[0]);
                    blue1.Text = Convert.ToString(led[1]); break;

                case "Green":
                    led[2] = Convert.ToByte(0 * brightness / 10);
                    led[0] = Convert.ToByte(255 * brightness / 10);
                    led[1] = Convert.ToByte(0 * brightness / 10);
                    red1.Text = Convert.ToString(led[2]);
                    green1.Text = Convert.ToString(led[0]);
                    blue1.Text = Convert.ToString(led[1]); break;
                case "Blue":
                    led[2] = Convert.ToByte(0 * brightness / 10);
                    led[0] = Convert.ToByte(0 * brightness / 10);
                    led[1] = Convert.ToByte(255 * brightness / 10);
                    red1.Text = Convert.ToString(led[2]);
                    green1.Text = Convert.ToString(led[0]);
                    blue1.Text = Convert.ToString(led[1]); break;

                case "Puple":
                    led[2] = Convert.ToByte(128 * brightness / 10);
                    led[0] = Convert.ToByte(0 * brightness / 10);
                    led[1] = Convert.ToByte(128 * brightness / 10);
                    red1.Text = Convert.ToString(led[2]);
                    green1.Text = Convert.ToString(led[0]);
                    blue1.Text = Convert.ToString(led[1]); break;
                case "Pink":
                    led[2] = Convert.ToByte(255 * brightness / 10);
                    led[0] = Convert.ToByte(182 * brightness / 10);
                    led[1] = Convert.ToByte(193 * brightness / 10);
                    red1.Text = Convert.ToString(led[2]);
                    green1.Text = Convert.ToString(led[0]);
                    blue1.Text = Convert.ToString(led[1]); break;

                case "Brown":
                    led[2] = Convert.ToByte(165 * brightness / 10);
                    led[0] = Convert.ToByte(42 * brightness / 10);
                    led[1] = Convert.ToByte(42 * brightness / 10);
                    red1.Text = Convert.ToString(led[2]);
                    green1.Text = Convert.ToString(led[0]);
                    blue1.Text = Convert.ToString(led[1]); break;
                case "White":
                    led[2] = Convert.ToByte(255 * brightness / 10);
                    led[0] = Convert.ToByte(255 * brightness / 10);
                    led[1] = Convert.ToByte(255 * brightness / 10);
                    red1.Text = Convert.ToString(led[2]);
                    green1.Text = Convert.ToString(led[0]);
                    blue1.Text = Convert.ToString(led[1]); break;


                default:
                    led[2] = Convert.ToByte(0 * brightness / 10);
                    led[0] = Convert.ToByte(0 * brightness / 10);
                    led[1] = Convert.ToByte(0 * brightness / 10);
                    break;
            }

            string w = Convert.ToString(comboBox2.Text);
            led[4] = 0; led[5] = 0; led[6] = 0; led[7] = 0;
            switch (w)
            {
                case "Red":
                    led[6] = Convert.ToByte(255 * brightness / 10);
                    led[4] = Convert.ToByte(0 * brightness / 10);
                    led[5] = Convert.ToByte(0 * brightness / 10);
                    red2.Text = Convert.ToString(led[6]);
                    green2.Text = Convert.ToString(led[4]);
                    blue2.Text = Convert.ToString(led[5]); break;

                case "Orange":
                    led[6] = Convert.ToByte(255 * brightness / 10);
                    led[4] = Convert.ToByte(165 * brightness / 10);
                    led[5] = Convert.ToByte(0 * brightness / 10);
                    red2.Text = Convert.ToString(led[6]);
                    green2.Text = Convert.ToString(led[4]);
                    blue2.Text = Convert.ToString(led[5]); break;

                case "Yellow":
                    led[6] = Convert.ToByte(255 * brightness / 10);
                    led[4] = Convert.ToByte(255 * brightness / 10);
                    led[5] = Convert.ToByte(0 * brightness / 10);
                    red2.Text = Convert.ToString(led[6]);
                    green2.Text = Convert.ToString(led[4]);
                    blue2.Text = Convert.ToString(led[5]); break;

                case "Green":
                    led[6] = Convert.ToByte(0 * brightness / 10);
                    led[4] = Convert.ToByte(255 * brightness / 10);
                    led[5] = Convert.ToByte(0 * brightness / 10);
                    red2.Text = Convert.ToString(led[6]);
                    green2.Text = Convert.ToString(led[4]);
                    blue2.Text = Convert.ToString(led[5]); break;
                case "Blue":
                    led[6] = Convert.ToByte(0 * brightness / 10);
                    led[4] = Convert.ToByte(0 * brightness / 10);
                    led[5] = Convert.ToByte(255 * brightness / 10);
                    red2.Text = Convert.ToString(led[6]);
                    green2.Text = Convert.ToString(led[4]);
                    blue2.Text = Convert.ToString(led[5]); break;

                case "Puple":
                    led[6] = Convert.ToByte(128 * brightness / 10);
                    led[4] = Convert.ToByte(0 * brightness / 10);
                    led[5] = Convert.ToByte(128 * brightness / 10);
                    red2.Text = Convert.ToString(led[6]);
                    green2.Text = Convert.ToString(led[4]);
                    blue2.Text = Convert.ToString(led[5]); break;
                case "Pink":
                    led[6] = Convert.ToByte(255 * brightness / 10);
                    led[4] = Convert.ToByte(182 * brightness / 10);
                    led[5] = Convert.ToByte(193 * brightness / 10);
                    red2.Text = Convert.ToString(led[6]);
                    green2.Text = Convert.ToString(led[4]);
                    blue2.Text = Convert.ToString(led[5]); break;

                case "Brown":
                    led[6] = Convert.ToByte(165 * brightness / 10);
                    led[4] = Convert.ToByte(42 * brightness / 10);
                    led[5] = Convert.ToByte(42 * brightness / 10);
                    red2.Text = Convert.ToString(led[6]);
                    green2.Text = Convert.ToString(led[4]);
                    blue2.Text = Convert.ToString(led[5]); break;
                case "White":
                    led[6] = Convert.ToByte(255 * brightness / 10);
                    led[4] = Convert.ToByte(255 * brightness / 10);
                    led[5] = Convert.ToByte(255 * brightness / 10);
                    red2.Text = Convert.ToString(led[6]);
                    green2.Text = Convert.ToString(led[4]);
                    blue2.Text = Convert.ToString(led[5]); break;


                default:
                    led[6] = Convert.ToByte(0 * brightness / 10);
                    led[4] = Convert.ToByte(0 * brightness / 10);
                    led[5] = Convert.ToByte(0 * brightness / 10);
                    break;
            }
            ledSender(led);
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            led[0] = 0;
            led[1] = 0;
            led[2] = 0;
            led[4] = 0;
            led[5] = 0;
            led[6] = 0;
        red1.Text = "0";
      green1.Text = "0";
       blue1.Text = "0";
        red2.Text = "0";        
       blue2.Text = "0";
      green2.Text = "0";
            ledSender(led);
            trackBar1.Enabled = false; trackBar1.Value = 1;
        }
    }
}

