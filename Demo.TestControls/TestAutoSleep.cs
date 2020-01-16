using System;
using Microsoft.SPOT;
using JernejK.NextionNET.Driver;
using System.Threading;
using System.Diagnostics;

namespace Demo.TestControls
{
    class TestAutoSleep
    {
        public static void Run(NextionDisplay display)
        {
            display.SystemEvent += display_SystemEvent;
            DisableSleep(display);
            display.SleepAfterNoSerialDataSeconds = 3;
            Debug.Print("New sleep data value: " + display.SleepAfterNoSerialDataSeconds);
            //Ping display for 5 seconds
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(500);
                display.RefreshAll();
            }

            Debug.Print("Is display still on?");
            if (Debugger.IsAttached)
                Debugger.Break();

            Thread.Sleep(4000);

            Debug.Print("Is display sleeping?");
            if (Debugger.IsAttached)
                Debugger.Break();

            display.SetSleepMode(false);

            Debug.Print("Is display on again?");
            if (Debugger.IsAttached)
                Debugger.Break();

            //Disable sleep on serial and enable no touch
            display.SleepAfterNoSerialDataSeconds = 0;
            display.SleepAfterNoTouchSeconds = 3;
            Debug.Print("New sleep data value: " + display.SleepAfterNoSerialDataSeconds);
            Debug.Print("New sleep touch value: " + display.SleepAfterNoTouchSeconds);

            display.TouchInSleepWillWakeUp = true;

            Thread.Sleep(30000);

            if (Debugger.IsAttached)
                Debugger.Break();
            DisableSleep(display);

            Thread.Sleep(4000);

            Debug.Print("Is display still on?");
            if (Debugger.IsAttached)
                Debugger.Break();
        }

        static void display_SystemEvent(object sender, SystemEventType eventType, SystemEventParameter parameter)
        {
            if (eventType == SystemEventType.AutomaticSleepMode)
            {
                Debug.Print("==> Automatic sleep mode");
            }
            else if (eventType == SystemEventType.AutomaticWakeUp)
            {
                Debug.Print("==> Automatic wakeup");
            }
            else
            {
                Debug.Print("Unknown system event type: " + eventType);
            }
        }

        private static void DisableSleep(NextionDisplay display)
        {
            display.SetSleepMode(false);

            display.SleepAfterNoTouchSeconds = 0;
            display.SleepAfterNoSerialDataSeconds = 0;

            Debug.Print("New sleep data value: " + display.SleepAfterNoSerialDataSeconds);
            Debug.Print("New sleep touch value: " + display.SleepAfterNoTouchSeconds);
        }
    }
}
