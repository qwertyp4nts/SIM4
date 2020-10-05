using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MccDaq;
using DigitalIO;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Configuration;

namespace SIM4
{
    public partial class Form1 : Form
    {
        static MccBoard DaqBoard = new MccDaq.MccBoard(1);
        Range range;
        ScanOptions ScanOptions;
        int linked = 0;
        int linkedPairTwo = 0;
        int linkedThrottle = 0;

        int NumPorts, NumBits, FirstBit;
        int PortType, ProgAbility;
        string PortName;

        bool threadRunning = false;

        int textBoxLowClampInt;
        int textBoxHiClampInt;
        int textBox4Int;

        //   string[] textBoxCollection;
        string textBox1String, textBox2String, textBox3String, textBox4String, textBox5String, textBox6String, textBox7String, textBox8String, textBox9String, textBox10String, textBox11String, textBox12String, textBox13String, textBox14String, textBox15String, textBox16String;

        MccDaq.DigitalPortType PortNum;
        MccDaq.DigitalPortDirection Direction;
        clsDigitalIO DioProps = new clsDigitalIO();

        //   StreamWriter sw = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "PulseWidthTimer.txt");
        //   #TODO can't write to Program Files without Admin privilege. Write to AppData or MyDocs instead

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            testNotes();
            //    textBoxCollection = new String[] { textBox1String, textBox2String, textBox3String };

            textBoxLowClampInt = int.Parse(txtBoxLowClamp.Text.Replace(".", ""));
            textBoxHiClampInt = int.Parse(txtBoxHiClamp.Text.Replace(".", ""));
            textBox4Int = int.Parse(maskedTextBox4.Text.Replace(".", ""));

            textBox1String = maskedTextBox1.Text;
            textBox2String = maskedTextBox2.Text;
            textBox3String = maskedTextBox3.Text;
            textBox4String = maskedTextBox4.Text;
            textBox5String = maskedTextBox5.Text;
            textBox6String = maskedTextBox6.Text;
            textBox7String = maskedTextBox7.Text;
            textBox8String = maskedTextBox8.Text;
            textBox9String = maskedTextBox9.Text;
            textBox10String = maskedTextBox10.Text;
            textBox11String = maskedTextBox11.Text;
            textBox12String = maskedTextBox12.Text;
            textBox13String = maskedTextBox13.Text;
            textBox14String = maskedTextBox14.Text;
            textBox15String = maskedTextBox15.Text;
            textBox16String = maskedTextBox16.Text;


            if (Regex.IsMatch(maskedTextBox1.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltageAndUpdatePin(0, textBox1String);
                hScrollBar1.Value = SetScrollBar(textBox1String);
            }

            if (Regex.IsMatch(maskedTextBox2.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltageAndUpdatePin(1, textBox2String);
                hScrollBar2.Value = SetScrollBar(textBox2String);
            }

            if (Regex.IsMatch(maskedTextBox3.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltageAndUpdatePin(2, textBox3String);
                hScrollBar3.Value = SetScrollBar(textBox3String);
            }

            if (Regex.IsMatch(maskedTextBox4.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                if ((textBoxLowClampInt <= textBox4Int) && (textBoxHiClampInt >= textBox4Int))
                {
                    sendVoltage(3, float.Parse(textBox4String));
                    hScrollBar4.Value = SetScrollBar(textBox4String);
                }
                else if (textBoxLowClampInt >= textBox4Int)
                {
                    sendVoltage(3, float.Parse(txtBoxLowClamp.Text));
                    hScrollBar4.Value = SetScrollBar(txtBoxLowClamp.Text);
                }
                else if (textBoxHiClampInt <= textBox4Int)
                {
                    sendVoltage(3, float.Parse(txtBoxHiClamp.Text));
                    hScrollBar4.Value = SetScrollBar(txtBoxHiClamp.Text);
                }
            }

            //maskedTextBox5 begin
            if (linked == 1)
            {
                sendVoltage(4, float.Parse(textBox4String));
                hScrollBar5.Value = SetScrollBar(textBox4String);
                maskedTextBox5.Text = textBox4String;
            }

            if (linked == 2)
            {
                decimal toDec = decimal.Parse(textBox4String);
                decimal invertedVal = 5 - toDec;

                sendVoltage(4, float.Parse(invertedVal.ToString()));
                hScrollBar5.Value = SetScrollBar(invertedVal.ToString());
                maskedTextBox5.Text = invertedVal.ToString();
            }

            if (linked == 0)
            {
                if (Regex.IsMatch(maskedTextBox5.Text, @"[0-5]\.[0-9][0-9][0-9]"))
                {
                    sendVoltage(4, float.Parse(textBox5String));
                    hScrollBar5.Value = SetScrollBar(textBox5String);
                }
            }

            //maskedTextBox5 end


            //maskedTextBox6 begin

            if (linkedThrottle == 1)
            {
                sendVoltage(5, float.Parse(textBox4String));
                hScrollBar6.Value = SetScrollBar(textBox4String);
                maskedTextBox6.Text = textBox4String;
            }

            if (linkedThrottle == 0)
            {
                if (Regex.IsMatch(maskedTextBox6.Text, @"[0-5]\.[0-9][0-9][0-9]"))
                {
                    sendVoltage(5, float.Parse(textBox6String));
                    hScrollBar6.Value = SetScrollBar(textBox6String);
                }
            }

            //maskedTextBox6 end


            //maskedTextBox7 begin
            if (linkedPairTwo == 1)
            {
                sendVoltage(6, float.Parse(textBox6String));
                hScrollBar7.Value = SetScrollBar(textBox6String);
                maskedTextBox7.Text = textBox6String;
            }

            if (linkedPairTwo == 2)
            {
                decimal toDec = decimal.Parse(textBox6String);
                decimal invertedVal = 5 - toDec;

                sendVoltage(6, float.Parse(invertedVal.ToString()));
                hScrollBar7.Value = SetScrollBar(invertedVal.ToString());
                maskedTextBox7.Text = invertedVal.ToString();
            }

            if (linkedPairTwo == 0)
            {
                if (Regex.IsMatch(maskedTextBox7.Text, @"[0-5]\.[0-9][0-9][0-9]"))
                {
                    sendVoltage(6, float.Parse(textBox7String));
                    hScrollBar7.Value = SetScrollBar(textBox7String);
                }
            }

            //maskedTextBox7 end

            if (Regex.IsMatch(maskedTextBox8.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(7, float.Parse(textBox8String));
                hScrollBar8.Value = SetScrollBar(textBox8String);
            }

            if (Regex.IsMatch(maskedTextBox9.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(8, float.Parse(textBox9String));
                hScrollBar9.Value = SetScrollBar(textBox9String);
            }

            if (Regex.IsMatch(maskedTextBox10.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(9, float.Parse(textBox10String));
                hScrollBar10.Value = SetScrollBar(textBox10String);
            }

            if (Regex.IsMatch(maskedTextBox11.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(10, float.Parse(textBox11String));
                hScrollBar11.Value = SetScrollBar(textBox11String);
            }

            if (Regex.IsMatch(maskedTextBox12.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(11, float.Parse(textBox12String));
                hScrollBar12.Value = SetScrollBar(textBox12String);
            }

            if (Regex.IsMatch(maskedTextBox13.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(12, float.Parse(textBox13String));
                hScrollBar13.Value = SetScrollBar(textBox13String);
            }

            if (Regex.IsMatch(maskedTextBox14.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(13, float.Parse(textBox14String));
                hScrollBar14.Value = SetScrollBar(textBox14String);
            }

            if (Regex.IsMatch(maskedTextBox15.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(14, float.Parse(textBox15String));
                hScrollBar15.Value = SetScrollBar(textBox15String);
            }

            if (Regex.IsMatch(maskedTextBox16.Text, @"[0-5]\.[0-9][0-9][0-9]"))
            {
                sendVoltage(15, float.Parse(textBox16String));
                hScrollBar16.Value = SetScrollBar(textBox16String);
            }



            /////////////////////////////// DIG //////////////////////////////////

            if (Regex.IsMatch(maskedTextBoxDIG0.Text, @"\d{1,3}"))
            {
                if (threadRunning == false)
                {
                    //   sendDIGVal(0, float.Parse(maskedTextBoxDIG0.Text));
                    //     TxDIG(int.Parse(maskedTextBoxDIG0.Text));
                    int pulseWidth = int.Parse(maskedTextBoxDIG0.Text);
                    Thread a = new Thread(() => TxDIGTest(pulseWidth));
                    a.Start();
                    hScrollBarDIG0.Value = SetScrollBar(maskedTextBoxDIG0.Text);
                }
            }

        }

        public void sendVoltageToClampedPin()
        {//not used yet
            if ((int.Parse(txtBoxLowClamp.Text.Replace(".", "")) <= int.Parse(maskedTextBox4.Text.Replace(".", ""))) && (int.Parse(txtBoxHiClamp.Text.Replace(".", "")) >= int.Parse(maskedTextBox4.Text.Replace(".", ""))))
            {
                sendVoltage(3, float.Parse(maskedTextBox4.Text));
                hScrollBar4.Value = SetScrollBar(maskedTextBox4.Text);
            }
            else if (int.Parse(txtBoxLowClamp.Text.Replace(".", "")) >= int.Parse(maskedTextBox4.Text.Replace(".", "")))
            {
                sendVoltage(3, float.Parse(txtBoxLowClamp.Text));
                hScrollBar4.Value = SetScrollBar(txtBoxLowClamp.Text);
            }
            else if (int.Parse(txtBoxHiClamp.Text.Replace(".", "")) <= int.Parse(maskedTextBox4.Text.Replace(".", "")))
            {
                sendVoltage(3, float.Parse(txtBoxHiClamp.Text));
                hScrollBar4.Value = SetScrollBar(txtBoxHiClamp.Text);
            }
        }

        private Boolean isWithinRange()
        {//not user yet
            if ((int.Parse(txtBoxLowClamp.Text.Replace(".", "")) <= int.Parse(maskedTextBox4.Text.Replace(".", ""))) && (int.Parse(txtBoxHiClamp.Text.Replace(".", "")) >= int.Parse(maskedTextBox4.Text.Replace(".", ""))))
            {

            }
            return true;
        }

        public void sendVoltageAndUpdatePin(int pinNum, string boxNum)
        {
            sendVoltage(pinNum, float.Parse(boxNum));
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
            if (((float)(hScrollBar4.Value) >= textBoxLowClampInt) && ((float)(hScrollBar4.Value) <= textBoxHiClampInt))
            {
                maskedTextBox4.Text = a;
            }
            else if ((float)(hScrollBar4.Value) <= textBoxLowClampInt)
            {
                maskedTextBox4.Text = txtBoxLowClamp.Text;
            }
            else if ((float)(hScrollBar4.Value) >= textBoxHiClampInt)
            {
                maskedTextBox4.Text = txtBoxHiClamp.Text;
            }
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

        private void hScrollBarDIG0_Scroll(object sender, ScrollEventArgs e)
        {
            // string a = ((float)(hScrollBarDIG0.Value).ToString("0.000");
            maskedTextBoxDIG0.Text = hScrollBarDIG0.Value.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //    Properties.Settings.Default.Notes1 = "";
            //Properties.Settings.Default.Save();
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

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 1)
            {
                linkedThrottle = 1;
                comboBox1.SelectedIndex = 2;
                comboBox2.SelectedIndex = 2;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                textBox3.Text = "Throttle Pedal";
                textBox4.Text = "Pedal Tracking";
                textBox5.Text = "Throttle Servo";
                textBox6.Text = "Servo Tracking";
            }

            if (comboBox3.SelectedIndex == 0)
            {
                linkedThrottle = 0;
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
            }
        }

        public void sendDIGVal(int pinName, float num)
        {
            PortType = clsDigitalIO.PORTOUT;
            NumPorts = DioProps.FindPortsOfType(DaqBoard, PortType, out ProgAbility,
                out PortNum,
                out NumBits, out FirstBit);

            Direction = MccDaq.DigitalPortDirection.DigitalOut;
            DaqBoard.DConfigPort(PortNum, Direction);

            DaqBoard.DOut(PortNum, (ushort)num);
        }



        private static System.Timers.Timer aTimer;

    /*    public void TxDIG(int pulseWidth)
        {
            threadRunning = true;
            DateTime curr = DateTime.Now;

            PortType = clsDigitalIO.PORTOUT;
            NumPorts = DioProps.FindPortsOfType(DaqBoard, PortType, out ProgAbility,
                out PortNum,
                out NumBits, out FirstBit);

            Direction = MccDaq.DigitalPortDirection.DigitalOut;

            DaqBoard.DConfigPort(PortNum, Direction);

            using (sw)
            {

                while ((maskedTextBoxDIG0.Text != "0"))
                {
                    var sleepPeriod = 255 - int.Parse(maskedTextBoxDIG0.Text);
                    DaqBoard.DOut(PortNum, (ushort)0);
                    Thread.Sleep(sleepPeriod);
                    DaqBoard.DOut(PortNum, (ushort)1);
                    Thread.Sleep(sleepPeriod);
                    //        sw.WriteLine("sleepPeriod: " + sleepPeriod + "       Timer:    " + DateTime.Now.ToString("HH:mm:ss.fff"));
                    //#TODO Failing as soon as we change value because its opening up another thread I think. Closing one and opening another? 
                }
            }
            threadRunning = false;
        }*/

        public void TxDIGTest(int pulseWidth)
        {
            threadRunning = true;
            PortType = clsDigitalIO.PORTOUT;
            NumPorts = DioProps.FindPortsOfType(DaqBoard, PortType, out ProgAbility,
                out PortNum,
                out NumBits, out FirstBit);

            Direction = MccDaq.DigitalPortDirection.DigitalOut;

            DaqBoard.DConfigPort(PortNum, Direction);
            var swt = new Stopwatch();

            int sleepPeriod = 0;
            string sleepPeriodText = null;

            while (maskedTextBoxDIG0.Text != "0")
            {
                if ((sleepPeriodText == null) || (sleepPeriodText != maskedTextBoxDIG0.Text))
                {
                    sleepPeriod = 255 - int.Parse(maskedTextBoxDIG0.Text);
                }
                DaqBoard.DOut(PortNum, (ushort)0);
                swExtension.fastSleep(swt, sleepPeriod);
                DaqBoard.DOut(PortNum, (ushort)1);
                swExtension.fastSleep(swt, sleepPeriod);


            }
            threadRunning = false;
        }


        private void maskedTextBoxDIG0_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void saveSettingsBtn_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Notes0 = textBox0.Text;
            Properties.Settings.Default.Notes1 = textBox1.Text;
            Properties.Settings.Default.Notes2 = textBox2.Text;
            Properties.Settings.Default.Notes3 = textBox3.Text;
            Properties.Settings.Default.Notes4 = textBox4.Text;
            Properties.Settings.Default.Notes5 = textBox5.Text;
            Properties.Settings.Default.Notes6 = textBox6.Text;
            Properties.Settings.Default.Notes7 = textBox7.Text;
            Properties.Settings.Default.Notes8 = textBox8.Text;
            Properties.Settings.Default.Notes9 = textBox9.Text;
            Properties.Settings.Default.Notes10 = textBox10.Text;
            Properties.Settings.Default.Notes11 = textBox11.Text;
            Properties.Settings.Default.Notes12 = textBox12.Text;
            Properties.Settings.Default.Notes13 = textBox13.Text;
            Properties.Settings.Default.Notes14 = textBox14.Text;
            Properties.Settings.Default.Notes15 = textBox15.Text;

            Properties.Settings.Default.Save();
        }

        private void testNotes()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            bool a = config.HasFile;
            var b = config.FilePath;
            textBox1.Text = b;
            //var Note1settings = Properties.Settings.Default.Notes1;
            //var c = config.AppSettings;
            try
            {
                textBox0.Text = Properties.Settings.Default.Notes0;
                textBox1.Text = Properties.Settings.Default.Notes1;
                textBox2.Text = Properties.Settings.Default.Notes2;
                textBox3.Text = Properties.Settings.Default.Notes3;
                textBox4.Text = Properties.Settings.Default.Notes4;
                textBox5.Text = Properties.Settings.Default.Notes5;
                textBox6.Text = Properties.Settings.Default.Notes6;
                textBox7.Text = Properties.Settings.Default.Notes7;
                textBox8.Text = Properties.Settings.Default.Notes8;
                textBox9.Text = Properties.Settings.Default.Notes9;
                textBox10.Text = Properties.Settings.Default.Notes10;
                textBox11.Text = Properties.Settings.Default.Notes11;
                textBox12.Text = Properties.Settings.Default.Notes12;
                textBox13.Text = Properties.Settings.Default.Notes13;
                textBox14.Text = Properties.Settings.Default.Notes14;
                textBox15.Text = Properties.Settings.Default.Notes15;
            }
            catch (Exception e)
            {
                //all good
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


    public static class swExtension
    {
        public static void fastSleep(Stopwatch swt, int ms)
        {
            if (swt.IsRunning)
            {
                swt.Restart();
            }
            else
            {
                swt.Start();
            }

            while (swt.ElapsedMilliseconds < ms)
            {
                Thread.Sleep(0); // relinquish thread. Stops the thread taking all the processor
            }
        }
    }
}