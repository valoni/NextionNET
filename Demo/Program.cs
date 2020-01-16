using System;
using Microsoft.SPOT;
using System.Threading;

namespace JernejK.NextionNET.Demo
{
    public class Program
    {
        public enum Page
        {
            Unknown,
            Ready,
            DateTime,
            Draw5,
        }
        private static Driver.NextionDisplay Display;
        private static AutoResetEvent SemaphoreXY = new AutoResetEvent(false);
        private static Driver.TouchEventXYArgs LastTouchXY = null;
        private static string DateFormat = "HH:mm:ss";
        private static bool SleepMode;
        private static Page CurrentPage;
        private static Random Rand = new Random();


        public static void Main()
        {
            //Temporary change boudrate and create port
            Display = new Driver.NextionDisplay(Driver.NextionDisplay.TemporaryChangeBaudRate("COM2", 9600, 115200), 240, 320);
            //Display = new Driver.NextionDisplay(new System.IO.Ports.SerialPort("COM2", 9600), 240, 320);
            DisplayConfiguration.Init(Display);

            Display.TouchEvent += Display_TouchEvent;
            Display.TouchXYEvent += Display_TouchXYEvent;

            //First page is opend by default after display run, but I will init it anyway
            Display.PageId = DisplayConfiguration.Page0.Id;

            //Simulate progress
            for (int i = 0; i <= 100; i += 20)
            {
                DisplayConfiguration.Page0.ProgressBar.Val = i;
                Thread.Sleep(500);
                if (DisplayConfiguration.Page0.ProgressBar.Val != i)
                    throw new Exception("Progress bar update failed");
            }

            //Test colors
            Display.GUI.Clear(Driver.Color.Red);
            Thread.Sleep(200);
            Display.GUI.Clear(Driver.Color.Green);
            Thread.Sleep(200);
            Display.GUI.Clear(Driver.Color.Blue);
            Thread.Sleep(200);

            //Test Write text
            Display.GUI.WriteText("Testing text", 0, 0, 240, 320, (byte)DisplayConfiguration.Fonts.Sanserif_16_B, (int)Driver.Color.Yellow, (int)Driver.Color.Blue, Driver.HorizontalAlignment.Center, Driver.VerticalAlignment.Center);
            Thread.Sleep(1000);
            //Test write with default values
            Display.GUI.DefaultFontId = (int)DisplayConfiguration.Fonts.Sanserif_40;
            Display.GUI.DefaultBackgroundColor = (int)Driver.Color.Transparent;
            Display.GUI.WriteText("OK", 0, 100, 45);
            Thread.Sleep(1000);

            //Go to next page by page name and dim display
            Display.SetPage(DisplayConfiguration.Page1.Name);
            //Check if page was changed
            if (Display.PageId != DisplayConfiguration.Page1.Id)
                throw new Exception("Page change failed");

            CurrentPage = Page.Ready;
            Display.Backlight = 30;
            //Change touch mode to allow click anywhere
            Display.TouchMode = Driver.TouchMode.Coordinates;
            SemaphoreXY.WaitOne();

            //Restore full backlight, touch mode and go to next page
            Display.Backlight = 100;
            Display.TouchMode = Driver.TouchMode.Component;
            Display.PageId = DisplayConfiguration.Page2.Id;
            CurrentPage = Page.DateTime;

            while (true)
            {
                if (CurrentPage == Page.DateTime)
                {
                    DisplayConfiguration.Page2.MainTextbox.Text = DateTime.Now.ToString(DateFormat);
                    Thread.Sleep(1000);
                }
            }
        }

        static object threadLock = new object();
        static DateTime SleepAllowWakeup;
        static DateTime Draw5AllowNextTouch = DateTime.MinValue;
        static byte Draw5Count = 0;
        static Driver.Color[] CricleColorPalete = new[] { Driver.Color.Black, Driver.Color.Blue, Driver.Color.Brown, Driver.Color.Gray, Driver.Color.Green, Driver.Color.Red, Driver.Color.Yellow };
        static int Draw5LastX, Draw5LastY;

        static void Display_TouchXYEvent(object sender, Driver.TouchEventXYArgs args)
        {
            //Process on other thread otherwise you will create deadlock.
            //Thread pool would be usefull in this case
            var newThread = new Thread(new ThreadStart(() =>
                {
                    lock (threadLock)
                    {
                        LastTouchXY = args;
                        if (SleepMode)
                        {
                            if (SleepAllowWakeup < DateTime.Now)
                            {
                                Display.SetSleepMode(false);
                                SleepMode = false;
                            }
                        }
                        else if (CurrentPage == Page.Ready)
                        {
                            SemaphoreXY.Set();
                        }
                        else if (CurrentPage == Page.Draw5)
                        {
                            //Debuncer
                            if (Draw5AllowNextTouch > DateTime.Now)
                                return;

                            if (Draw5Count >= 5)
                            {
                                Display.TouchMode = Driver.TouchMode.Component;
                                Display.PageId = DisplayConfiguration.Page2.Id;
                                CurrentPage = Page.DateTime;
                            }
                            else
                            {
                                DisplayConfiguration.Page3.txtX.Text = LastTouchXY.X.ToString();
                                DisplayConfiguration.Page3.txtY.Text = LastTouchXY.Y.ToString();

                                var color = CricleColorPalete[Rand.Next(CricleColorPalete.Length - 1)];
                                int shapeId = Rand.Next(3);
                                if (shapeId == 0)
                                {
                                    Display.GUI.DrawCircle(LastTouchXY.X, LastTouchXY.Y, 5, (int)color);
                                }
                                else if (shapeId == 1)
                                {
                                    Display.GUI.DrawRectangle(LastTouchXY.X - 5, LastTouchXY.Y - 5, LastTouchXY.X + 5, LastTouchXY.Y + 5, (int)color);
                                }
                                else
                                {
                                    Display.GUI.DrawRectangleFilled(LastTouchXY.X - 5, LastTouchXY.Y - 5, LastTouchXY.X + 5, LastTouchXY.Y + 5, (int)color);
                                }

                                if (Draw5Count > 0)
                                {
                                    Display.GUI.DrawLine(Draw5LastX, Draw5LastY, LastTouchXY.X, LastTouchXY.Y, (int)Driver.Color.Black);
                                }

                                Draw5LastX = LastTouchXY.X;
                                Draw5LastY = LastTouchXY.Y;

                                Draw5AllowNextTouch = DateTime.Now.AddMilliseconds(500);
                                Draw5Count++;
                            }
                        }
                    }
                }));
            newThread.Start();
        }

        static void Display_TouchEvent(object sender, Driver.TouchEventArgs args)
        {
            var ctrl = args.ResolveControl();
            if (ctrl == DisplayConfiguration.Page2.btnDate)
            {
                DateFormat = "dd.MM.yyyy";
            }
            else if (ctrl == DisplayConfiguration.Page2.btnTime)
            {
                DateFormat = "HH:mm:ss";
            }
            else if (ctrl == DisplayConfiguration.Page2.btnSleep)
            {
                Display.SetSleepMode(true);
                SleepMode = true;
                SleepAllowWakeup = DateTime.Now.AddSeconds(1);
            }
            else if (ctrl == DisplayConfiguration.Page2.btnDraw5)
            {
                CurrentPage = Page.Draw5;
                Display.PageId = DisplayConfiguration.Page3.Id;

                int width = Display.Width;
                int height = Display.Height;
                //Draw border
                Display.GUI.DrawRectangle(0, 0, width - 1, height - 1, (int)Driver.Color.Red);
                Display.GUI.DrawRectangle(1, 1, width - 2, height - 2, (int)Driver.Color.Red);
                Display.GUI.DrawRectangle(2, 2, width - 3, height - 3, (int)Driver.Color.Red);


                Display.TouchMode = Driver.TouchMode.Coordinates;
                Draw5Count = 0;
                Draw5AllowNextTouch = DateTime.Now.AddSeconds(1);
            }
        }
    }
}
