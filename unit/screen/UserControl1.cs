﻿using System;
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
        public static UserControl1 uc1;
        public UserControl1()
        {
            InitializeComponent();

            comboBox1.Items.Add("24A16057F685");
            comboBox1.Items.Add("500291AEBCD9");
            comboBox1.Items.Add("500291AEBE4D");
            uc1 = this;
        }








        public void sensorDataOutput(byte[] address, byte[] data, string deviceId)
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



        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string aa = comboBox1.SelectedItem.ToString();

                ulong device = ulong.Parse(aa, System.Globalization.NumberStyles.HexNumber);

                Form1.form1.getAddress(device);
                Form1.form1.dataAddress = device;

            }
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

            Form1.form1.getAddress(0x24A16057F685);

            Form1.form1.dataAddress = 0x24A16057F685;
        }

    }

}