using System;
using Microsoft.SPOT;
using JernejK.NextionNET.Driver;
using System.Threading;

namespace Demo.TestControls
{
    class TestGaugeTimerVariable
    {
        public static void Test(NextionDisplay display)
        {
            display.PageId = DisplayConfiguration.Page3.Id;
            display.TouchMode = TouchMode.Component;

            int[] pointerColors = { (int)Color.Black, (int)Color.Green, (int)Color.Red, (int)Color.Yellow, (int)Color.Blue };
            byte nextColorIndex = 0;

            display.TouchEvent += (sender, args) =>
                {
                    var ctrl = args.ResolveControl();
                    if (ctrl == null)
                        return;

                    if (ctrl == DisplayConfiguration.Page3.btnFast)
                    {
                        //Fast timer
                        DisplayConfiguration.Page3.tm0.Timeout = 100;
                    }
                    else if (ctrl == DisplayConfiguration.Page3.btnSlow)
                    {
                        DisplayConfiguration.Page3.tm0.Timeout = 400;
                    }
                    else if (ctrl == DisplayConfiguration.Page3.btnColor)
                    {
                        //Reading or writing to Nextion display require new thread.
                        ThreadHelper.Run(() =>
                            {
                                DisplayConfiguration.Page3.gauge.ForegroundColor = pointerColors[nextColorIndex];
                                DisplayConfiguration.Page3.gauge.Refresh();
                                nextColorIndex++;
                                if (nextColorIndex >= pointerColors.Length)
                                    nextColorIndex = 0;
                            });
                    }
                    else if (ctrl == DisplayConfiguration.Page3.btnThick)
                    {
                        //Reading or writing to Nextion display require new thread.
                        ThreadHelper.Run(() =>
                            {
                                byte tickness = DisplayConfiguration.Page3.gauge.Thickness;
                                tickness++;
                                if (tickness >= 5)
                                    tickness = 1;

                                DisplayConfiguration.Page3.gauge.Thickness = tickness;
                                DisplayConfiguration.Page3.gauge.Refresh();
                            });
                    }
                    else if (ctrl == DisplayConfiguration.Page3.btnStart)
                    {
                        DisplayConfiguration.Page3.va0.Val = 0;
                        DisplayConfiguration.Page3.tm0.Enabled = true;
                    }
                };

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
