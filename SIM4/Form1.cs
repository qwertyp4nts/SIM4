﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MccDaq;

namespace SIM4
{
    public partial class Form1 : Form
    {
        static MccBoard DaqBoard = new MccDaq.MccBoard(1);
        Range range;
        ScanOptions ScanOptions;
        int linked = 0;
        int linkedPairTwo = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //comboBox1.SelectedIndex = 0;

            if (Regex.IsMatch(maskedTextBox1.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(0, float.Parse(maskedTextBox1.Text));
                hScrollBar1.Value = SetScrollBar(maskedTextBox1.Text);
            }

            if (Regex.IsMatch(maskedTextBox2.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(1, float.Parse(maskedTextBox2.Text));
                hScrollBar2.Value = SetScrollBar(maskedTextBox2.Text);
            }

            if (Regex.IsMatch(maskedTextBox3.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(2, float.Parse(maskedTextBox3.Text));
                hScrollBar3.Value = SetScrollBar(maskedTextBox3.Text);
            }

            if (Regex.IsMatch(maskedTextBox4.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(3, float.Parse(maskedTextBox4.Text));
                hScrollBar4.Value = SetScrollBar(maskedTextBox4.Text);
            }

            //maskedTextBox5 begin
            if (linked == 1)
            {
                sendVoltage(4, float.Parse(maskedTextBox4.Text));
                hScrollBar5.Value = SetScrollBar(maskedTextBox4.Text);
                maskedTextBox5.Text = maskedTextBox4.Text;
            }

            if (linked == 2)
            {
                decimal toDec = decimal.Parse(maskedTextBox4.Text);
                decimal invertedVal = 5 - toDec;

                sendVoltage(4, float.Parse(invertedVal.ToString()));
                hScrollBar5.Value = SetScrollBar(invertedVal.ToString());
                maskedTextBox5.Text = invertedVal.ToString();
            }

            if (linked == 0)
            {
                if (Regex.IsMatch(maskedTextBox5.Text, @"[0-5]\.[0-9][0-9][0-9]"))
                {
                    sendVoltage(4, float.Parse(maskedTextBox5.Text));
                    hScrollBar5.Value = SetScrollBar(maskedTextBox5.Text);
                }
            }

            //maskedTextBox5 end

            if (Regex.IsMatch(maskedTextBox6.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(5, float.Parse(maskedTextBox6.Text));
                hScrollBar6.Value = SetScrollBar(maskedTextBox6.Text);
            }

            //maskedTextBox7 begin
            if (linkedPairTwo == 1)
            {
                sendVoltage(6, float.Parse(maskedTextBox6.Text));
                hScrollBar7.Value = SetScrollBar(maskedTextBox6.Text);
                maskedTextBox7.Text = maskedTextBox6.Text;
            }

            if (linkedPairTwo == 2)
            {
                decimal toDec = decimal.Parse(maskedTextBox6.Text);
                decimal invertedVal = 5 - toDec;

                sendVoltage(6, float.Parse(invertedVal.ToString()));
                hScrollBar7.Value = SetScrollBar(invertedVal.ToString());
                maskedTextBox7.Text = invertedVal.ToString();
            }

            if (linkedPairTwo == 0)
            {
                if (Regex.IsMatch(maskedTextBox7.Text, @"[0-5]\.[0-9][0-9][0-9]"))
                {
                    sendVoltage(6, float.Parse(maskedTextBox7.Text));
                    hScrollBar7.Value = SetScrollBar(maskedTextBox7.Text);
                }
            }

            //maskedTextBox7 end

            if (Regex.IsMatch(maskedTextBox8.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(7, float.Parse(maskedTextBox8.Text));
                hScrollBar8.Value = SetScrollBar(maskedTextBox8.Text);
            }

            if (Regex.IsMatch(maskedTextBox9.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(8, float.Parse(maskedTextBox9.Text));
                hScrollBar9.Value = SetScrollBar(maskedTextBox9.Text);
            }

            if (Regex.IsMatch(maskedTextBox10.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(9, float.Parse(maskedTextBox10.Text));
                hScrollBar10.Value = SetScrollBar(maskedTextBox10.Text);
            }

            if (Regex.IsMatch(maskedTextBox11.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(10, float.Parse(maskedTextBox11.Text));
                hScrollBar11.Value = SetScrollBar(maskedTextBox11.Text);
            }

            if (Regex.IsMatch(maskedTextBox12.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(11, float.Parse(maskedTextBox12.Text));
                hScrollBar12.Value = SetScrollBar(maskedTextBox12.Text);
            }

            if (Regex.IsMatch(maskedTextBox13.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(12, float.Parse(maskedTextBox13.Text));
                hScrollBar13.Value = SetScrollBar(maskedTextBox13.Text);
            }

            if (Regex.IsMatch(maskedTextBox14.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(13, float.Parse(maskedTextBox14.Text));
                hScrollBar14.Value = SetScrollBar(maskedTextBox14.Text);
            }

            if (Regex.IsMatch(maskedTextBox15.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(14, float.Parse(maskedTextBox15.Text));
                hScrollBar15.Value = SetScrollBar(maskedTextBox15.Text);
            }

            if (Regex.IsMatch(maskedTextBox16.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(15, float.Parse(maskedTextBox16.Text));
                hScrollBar16.Value = SetScrollBar(maskedTextBox16.Text);
            }
        }

        private void maskedTextBox1_TextChanged(object sender, KeyPressEventArgs e)
        {

        }

        public static void sendVoltage(int pinName, float volts)
        {
            double daCounts = volts * 6553.8;
            DaqBoard.AOut(pinName, 0, (ushort)daCounts);
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            string a = ((float)(hScrollBar1.Value) / 1000).ToString("0.000");
            maskedTextBox1.Text = a;
        }

        private static int SetScrollBar(string a)
        {
            string wej = a.Replace(".", "");
            if (int.Parse(wej) > 5000)
            {
                return 5000;
            }
            else
            {
                return int.Parse(wej);
            }
        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            string a = ((float)(hScrollBar2.Value) / 1000).ToString("0.000");
            maskedTextBox2.Text = a;
        }

        private void hScrollBar3_Scroll(object sender, ScrollEventArgs e)
        {
            string a = ((float)(hScrollBar3.Value) / 1000).ToString("0.000");
            maskedTextBox3.Text = a;
        }

        private void hScrollBar4_Scroll(object sender, ScrollEventArgs e)
        {
            string a = ((float)(hScrollBar4.Value) / 1000).ToString("0.000");
            maskedTextBox4.Text = a;
        }
        private void hScrollBar5_Scroll(object sender, ScrollEventArgs e)
        {
            string a = ((float)(hScrollBar5.Value) / 1000).ToString("0.000");
            maskedTextBox5.Text = a;
        }

        private void hScrollBar6_Scroll(object sender, ScrollEventArgs e)
        {
            string a = ((float)(hScrollBar6.Value) / 1000).ToString("0.000");
            maskedTextBox6.Text = a;
        }

        private void hScrollBar7_Scroll(object sender, ScrollEventArgs e)
        {
            string a = ((float)(hScrollBar7.Value) / 1000).ToString("0.000");
            maskedTextBox7.Text = a;
        }

        private void hScrollBar8_Scroll(object sender, ScrollEventArgs e)
        {
            string a = ((float)(hScrollBar8.Value) / 1000).ToString("0.000");
            maskedTextBox8.Text = a;
        }

        private void hScrollBar9_Scroll(object sender, ScrollEventArgs e)
        {
            string a = ((float)(hScrollBar9.Value) / 1000).ToString("0.000");
            maskedTextBox9.Text = a;
        }

        private void hScrollBar10_Scroll(object sender, ScrollEventArgs e)
        {
            string a = ((float)(hScrollBar10.Value) / 1000).ToString("0.000");
            maskedTextBox10.Text = a;
        }

        private void hScrollBar11_Scroll(object sender, ScrollEventArgs e)
        {
            string a = ((float)(hScrollBar11.Value) / 1000).ToString("0.000");
            maskedTextBox11.Text = a;
        }

        private void hScrollBar12_Scroll(object sender, ScrollEventArgs e)
        {
            string a = ((float)(hScrollBar12.Value) / 1000).ToString("0.000");
            maskedTextBox12.Text = a;
        }

        private void hScrollBar13_Scroll(object sender, ScrollEventArgs e)
        {
            string a = ((float)(hScrollBar13.Value) / 1000).ToString("0.000");
            maskedTextBox13.Text = a;
        }

        private void hScrollBar14_Scroll(object sender, ScrollEventArgs e)
        {
            string a = ((float)(hScrollBar14.Value) / 1000).ToString("0.000");
            maskedTextBox14.Text = a;
        }

        private void hScrollBar15_Scroll(object sender, ScrollEventArgs e)
        {
            string a = ((float)(hScrollBar15.Value) / 1000).ToString("0.000");
            maskedTextBox15.Text = a;
        }

        private void hScrollBar16_Scroll(object sender, ScrollEventArgs e)
        {
            string a = ((float)(hScrollBar16.Value) / 1000).ToString("0.000");
            maskedTextBox16.Text = a;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        //    Properties.Settings.Default.Notes1 = "";
            Properties.Settings.Default.Save();
        }

        private void linkButton_Click(object sender, EventArgs e)
        {

            

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                linked = 1;
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
            }

            if (comboBox1.SelectedIndex == 0)
            {
                linked = 0;
                pictureBox2.Visible = false;
                pictureBox1.Visible = true;
            }

            if (comboBox1.SelectedIndex == 2)
            {
                linked = 2;
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 1)
            {
                linkedPairTwo = 1;
                pictureBox4.Visible = false;
                pictureBox3.Visible = true;
            }

            if (comboBox2.SelectedIndex == 0)
            {
                linkedPairTwo = 0;
                pictureBox3.Visible = false;
                pictureBox4.Visible = true;
            }

            if (comboBox2.SelectedIndex == 2)
            {
                linkedPairTwo = 2;
                pictureBox4.Visible = false;
                pictureBox3.Visible = true;
            }
        }

    }
}

/* This code works. I made it fancy now and I have no faith in my abilities, so revert to this if all hell breaks loose.
 *  if (Regex.IsMatch(maskedTextBox2.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(1, float.Parse(maskedTextBox2.Text));
                string wej = maskedTextBox1.Text.Replace(".", "");
                if (int.Parse(wej) > 5000)
                {
                    hScrollBar1.Value = 5000;
                }
                else
                {
                    hScrollBar1.Value = int.Parse(wej);
                }
            }
*/