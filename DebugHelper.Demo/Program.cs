using System;
using Microsoft.SPOT;
using System.Threading;
using System.IO.Ports;

namespace JernejK.NextionNET.DebugHelper.Demo
{
    public class Program
    {
        public static void Main()
        {
            var messageDisplay = new DebugHelper.NextionDebugHelper(Driver.NextionDisplay.TemporaryChangeBaudRate("COM2", 9600, 115200), 240, 320, 18);
            //var messageDisplay = new DebugHelper.NextionDebugHelper(new SerialPort("COM2", 9600), 240, 320, 20);

            messageDisplay.BackgroundColor = (int) Driver.Color.Black;
            messageDisplay.FontColor = (int) Driver.Color.Green;

            TestAutoRefresh(messageDisplay);
            //TestManualRefresh(messageDisplay);
        }

        private static void TestAutoRefresh(NextionDebugHelper messageDisplay)
        {
            while (true)
            {
                messageDisplay.Add("---\"" + DateTime.Now.ToString() + "\"---");
                Thread.Sleep(1000);
            }
        }

        private static void TestManualRefresh(NextionDebugHelper messageDisplay)
        {
            messageDisplay.AutoRefresh = false;
            bool doRefresh = false;
            while (true)
            {
                messageDisplay.Add(DateTime.Now.ToString());
                if (doRefresh)
                {
                    messageDisplay.Render();
                    doRefresh = false;
                }
                else
                {
                    doRefresh = true;
                }
                Thread.Sleep(500);
            }
        }

    }
}
