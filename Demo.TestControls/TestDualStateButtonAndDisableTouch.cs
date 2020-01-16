using System;
using Microsoft.SPOT;
using JernejK.NextionNET.Driver;
using System.Threading;

namespace Demo.TestControls
{
    class TestDualStateButtonAndDisableTouch
    {
        public static void Test(NextionDisplay display)
        {
            display.PageId = DisplayConfiguration.Page2.Id;
            DisplayConfiguration.Page2.btnTest.ChangeTouchEnabled(false);

            DisplayConfiguration.Page2.btnDualState.State = true;
            Debug.Print(DisplayConfiguration.Page2.btnDualState.State.ToString());
            DisplayConfiguration.Page2.btnDualState.State = false;
            Debug.Print(DisplayConfiguration.Page2.btnDualState.State.ToString());

            display.TouchMode = TouchMode.Component;
            var wait = new ManualResetEvent(false);
            int counter = 0;

            display.TouchEvent += (sender, args) =>
                {
                    var ctrl = args.ResolveControl();
                    if (ctrl == null)
                        return;

                    if (ctrl == DisplayConfiguration.Page2.btnContinue)
                    {
                        wait.Set();
                    }
                    else if (ctrl == DisplayConfiguration.Page2.btnDualState)
                    {
                        ThreadHelper.Run(() =>
                            {
                                //Display dont provide new value in event so we need to read it
                                bool state = DisplayConfiguration.Page2.btnDualState.State;
                                Debug.Print("Dual state changed. New state: " + state);
                                DisplayConfiguration.Page2.btnTest.ChangeTouchEnabled(state);
                            });
                    }
                    else if (ctrl == DisplayConfiguration.Page2.btnTest)
                    {
                        DisplayConfiguration.Page2.tbCounter.Text = (++counter).ToString();
                    }
                };

            wait.WaitOne();
        }

        static void display_TouchEvent(object sender, TouchEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
