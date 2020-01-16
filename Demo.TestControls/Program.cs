using System;
using Microsoft.SPOT;
using JernejK.NextionNET.Driver;
using JernejK.NextionNET.Driver.Controls;
using System.Threading;
using System.Diagnostics;

namespace Demo.TestControls
{
    public class Program
    {
        private static void TestTextBox(TextBox ctrl)
        {
            ctrl.Text = "Test123";
            Debug.Print(ctrl.Text);
            //Use " in text
            ctrl.TextEscaped = "This is \"Test\".";
            Debug.Print(ctrl.Text);

            ctrl.BackgroundColor = (int)Color.Red;
            ctrl.Refresh();
            ctrl.ForegroundColor = (int)Color.Green;
            ctrl.Refresh();
            ctrl.FontId = (byte)DisplayConfiguration.Fonts.Sanserif_16_B;
            ctrl.Refresh();
            ctrl.HorizontalAlignment = HorizontalAlignment.Right;
            ctrl.Refresh();
            ctrl.VerticalAlignment = VerticalAlignment.Top;
            ctrl.Refresh();

            ctrl.Text = "Line 1\r\nLine 2";

            /*
            int tmp = ctrl.X;
            ctrl.X = tmp + 5;
            ctrl.Refresh();
            ctrl.X = tmp;
            ctrl.Refresh();

            tmp = ctrl.Y;
            ctrl.Y = tmp + 5;
            ctrl.Refresh();
            ctrl.Y = tmp;
            ctrl.Refresh();

            ctrl.Height = 5;
            ctrl.Refresh();
            Debug.Print(ctrl.Height.ToString());
             * */
        }

        private static ManualResetEvent TestButtonResetEvent = new ManualResetEvent(false);
        private static void TestButton(NextionDisplay display, Button ctrl)
        {
            ctrl.Text = "Test123";
            Debug.Print(ctrl.Text);
            ctrl.TextEscaped = "\"Test\"";
            ctrl.BackgroundColor = (int)Color.Red;
            ctrl.Refresh();
            ctrl.ForegroundColor = (int)Color.Green;
            ctrl.Refresh();
            ctrl.FontId = (byte)DisplayConfiguration.Fonts.Sanserif_16_B;
            ctrl.Refresh();
            ctrl.HorizontalAlignment = HorizontalAlignment.Right;
            ctrl.Refresh();
            ctrl.VerticalAlignment = VerticalAlignment.Bottom;
            ctrl.Refresh();

            Debug.Print("Press button");
            display.TouchEvent += TestButton_TouchEvent;
            TestButtonResetEvent.WaitOne();
            display.TouchEvent -= TestButton_TouchEvent;
            Debug.Print("Pressed");
        }

        static void TestButton_TouchEvent(object sender, TouchEventArgs args)
        {
            var ctrl = args.ResolveControl();
            if (ctrl != null && ctrl == DisplayConfiguration.Page0.b0)
                TestButtonResetEvent.Set();
        }

        private static void TestProgressBar(ProgressBar ctrl)
        {
            ctrl.BackgroundColor = (int)Color.Red;
            ctrl.Refresh();
            ctrl.ForegroundColor = (int)Color.Green;
            ctrl.Refresh();
            /*
            Debug.Print(ctrl.Height.ToString());
            int tmp = ctrl.X;
            ctrl.X = tmp + 5;
            ctrl.Refresh();
            ctrl.X = tmp;
            ctrl.Refresh();

            tmp = ctrl.Y;
            ctrl.Y = tmp + 5;
            ctrl.Refresh();
            ctrl.Y = tmp;
            ctrl.Refresh();
            */

            ctrl.Val = 80;
            Debug.Print(ctrl.Val.ToString());

            /*
            ctrl.Height = 20;
            ctrl.Refresh();
            Debug.Print(ctrl.Height.ToString());

            ctrl.Width = 50;
            ctrl.Refresh();
            Debug.Print(ctrl.Width.ToString());
             * */
        }

        private static void TestPictureBox(PictureBox ctrl)
        {
            /*
            int tmp = ctrl.X;
            ctrl.X = tmp + 5;
            ctrl.Refresh();
            ctrl.X = tmp;
            ctrl.Refresh();

            tmp = ctrl.Y;
            ctrl.Y = tmp + 5;
            ctrl.Refresh();
            ctrl.Y = tmp;
            ctrl.Refresh();
            */

            ctrl.PictureId = (byte)DisplayConfiguration.MyPictures.Smyle2;
            ctrl.PictureId = (byte)DisplayConfiguration.MyPictures.Smyle3;
            Debug.Print(ctrl.PictureId.ToString());
            
            /*
            ctrl.Height = 15;
            ctrl.Refresh();
            Debug.Print(ctrl.Height.ToString());

            ctrl.Width = 15;
            ctrl.Refresh();
            Debug.Print(ctrl.Width.ToString());
             * */
        }

        private static void TestSlider(Slider ctrl)
        {
            ctrl.BackgroundColor = (int)Color.Gray;
            ctrl.Refresh();
            ctrl.ForegroundColor = (int)Color.Blue;
            ctrl.Refresh();

            ctrl.CursorHeight = 10;
            ctrl.Refresh();
            ctrl.CursorWidth = 15;
            ctrl.Refresh();
            Debug.Print(ctrl.CursorHeight + " - " + ctrl.CursorWidth);

            ctrl.Val = 0;
            Debug.Print(ctrl.Val.ToString());

            ctrl.Val = 50;

            Debug.Print("Min/Max " + ctrl.MinValue + " / " + ctrl.MaxValue);

            ctrl.MinValue = 10;
            ctrl.Refresh();
            Debug.Print(ctrl.MinValue.ToString());
            Debug.Print(ctrl.Val.ToString());

            ctrl.MaxValue = 500;
            ctrl.Refresh();
            Debug.Print(ctrl.MaxValue.ToString());
            var tmp = ctrl.Val;
            ctrl.Val = 500;
            ctrl.Val = tmp;

            ctrl.Val = 100;
            ctrl.Val = 500;

            Debug.Print("-----------------------------");
            Debug.Print("Slider value change test start");
            for (int i = 0; i < 5; i++)
            {
                Debug.Print(ctrl.Val.ToString());
                Thread.Sleep(5000);
            }
            Debug.Print("Slider value change test end");
            Debug.Print("-----------------------------");
        }

        private static void TestWaveform(Waveform waveform)
        {
            waveform.BackgroundColor = (int)Color.Blue;
            Debug.Print(waveform.BackgroundColor.ToString());
            waveform.Refresh();
            waveform.BackgroundColor = (int)Color.Black;
            waveform.Refresh();

            waveform.GridColor = (int)Color.Red;
            Debug.Print("Grid: " + waveform.GridIntervalHorizontal + " - " + waveform.GridIntervalVertical);
            /*
            waveform.Refresh();
            waveform.GridIntervalHorizontal = 0;
            waveform.GridIntervalVertical = 0;
            waveform.Refresh();

            waveform.GridIntervalHorizontal = 30;
            waveform.GridIntervalVertical = 60;
            waveform.Refresh();
            */

            waveform.ForegroundColor0 = (int)Color.White;
            waveform.ForegroundColor1 = (int)Color.Blue;
            waveform.ForegroundColor2 = (int)Color.Green;
            waveform.ForegroundColor3 = (int)Color.Yellow;
            waveform.Refresh();

            Debug.Print("-----------------------------");
            Debug.Print("Waveform value add test start");

            int sleep = 100;

            for (double angle = 0; angle < 120; angle += 0.1)
            {
                byte value0 = (byte)(angle);

                byte value1 = ConvertToByte(System.Math.Sin(angle));
                byte value2 = ConvertToByte(System.Math.Cos(angle));

                waveform.Add(0, value0);
                waveform.Add(1, value1);
                waveform.Add(2, value2);

                Thread.Sleep(sleep);
            }

            Debug.Print("Waveform value add test end");
            Debug.Print("-----------------------------");
        }

        private static void TestNumberBox(NumberBox ctrl)
        {
            ctrl.BackgroundColor = (int)Color.Blue;
            Debug.Print(ctrl.BackgroundColor.ToString());
            ctrl.Refresh();
            ctrl.ForegroundColor = (int)Color.Red;
            Debug.Print(ctrl.ForegroundColor.ToString());
            ctrl.Refresh();
            ctrl.FontId = (byte)DisplayConfiguration.Fonts.Sanserif_16_B;
            Debug.Print(ctrl.FontId.ToString());
            ctrl.Refresh();
            ctrl.NumberOfDigits = 2;
            Debug.Print(ctrl.NumberOfDigits.ToString());

            ctrl.HorizontalAlignment = HorizontalAlignment.Center;
            ctrl.Refresh();
            ctrl.VerticalAlignment = VerticalAlignment.Center;
            ctrl.Refresh();

            for (int i = 0; i <= 10; i++)
            {
                ctrl.Value = i;
                Debug.Print(ctrl.Value.ToString());
                Thread.Sleep(500);
            }
        }

        private static byte ConvertToByte(double value)
        {
            return (byte)((value + 1) * (120 / 2));
        }

        public static void Main()
        {
            var display = new NextionDisplay(new System.IO.Ports.SerialPort("COM2", 9600));
            DisplayConfiguration.Init(display);

            bool wait = true;
            //Prevent program autostart
            while (wait)
            {
                //This test is supposed to run with debugger attached
                if (Debugger.IsAttached)
                    Debugger.Break();

                Thread.Sleep(500);
            }

            display.PageId = DisplayConfiguration.Page0.Id;
            TestTextBox(DisplayConfiguration.Page0.t0);
            TestButton(display, DisplayConfiguration.Page0.b0);
            TestProgressBar(DisplayConfiguration.Page0.j0);
            TestPictureBox(DisplayConfiguration.Page0.p0);
            TestSlider(DisplayConfiguration.Page0.h0);
            TestNumberBox(DisplayConfiguration.Page0.n0);

            display.PageId = DisplayConfiguration.Page1.Id;
            TestWaveform(DisplayConfiguration.Page1.s0);

            TestDualStateButtonAndDisableTouch.Test(display);
            TestAutoSleep.Run(display);
            TestGaugeTimerVariable.Test(display);
        }
    }
}
