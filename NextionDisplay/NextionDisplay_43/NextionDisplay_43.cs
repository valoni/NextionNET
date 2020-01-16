using System;
using Microsoft.SPOT;

using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using GTI = Gadgeteer.SocketInterfaces;

using Driver = global::JernejK.NextionNET.Driver;

namespace Gadgeteer.Modules.JernejK
{
    /// <summary>
    /// A NextionDisplay module for Microsoft .NET Gadgeteer
    /// </summary>
    public class NextionDisplay : GTM.Module, Driver.INextionDisplay
    {
        Driver.NextionDisplay Display;

        /// <summary></summary>
        /// <param name="socketNumber">The socket that this module is plugged in to.</param>
        public NextionDisplay(int socketNumber)
        {
            Socket socket = Socket.GetSocket(socketNumber, true, this, "U");
            socket.EnsureTypeIsSupported('U', this);

            Display = new Driver.NextionDisplay(new System.IO.Ports.SerialPort(socket.SerialPortName, 9600));
        }

        /// <summary>
        /// Get or set display backlight % (0-100)
        /// </summary>
        public byte Backlight
        {
            get
            {
                return Display.Backlight;
            }
            set
            {
                Display.Backlight = value;
            }
        }

        /// <summary>
        /// Defined control collection
        /// </summary>
        public Driver.ControlCollection Controls
        {
            get { return Display.Controls; }
        }

        /// <summary>
        /// Direct access to GUI
        /// </summary>
        public Driver.NextionDisplay.GUIDesign GUI
        {
            get { return Display.GUI; }
        }

        /// <summary>
        /// Display height (0 - undefined)
        /// </summary>
        public int Height
        {
            get { return Display.Height; }
        }

        /// <summary>
        /// Current page id
        /// </summary>
        public byte PageId
        {
            get
            {
                return Display.PageId;
            }
            set
            {
                Display.PageId = value;
            }
        }

        /// <summary>
        /// Refresh selected control
        /// </summary>
        /// <param name="id"></param>
        public void RefreshComponent(string id)
        {
            Display.RefreshComponent(id);
        }

        /// <summary>
        /// Set current page by page name
        /// </summary>
        /// <param name="pageName"></param>
        public void SetPage(string pageName)
        {
            Display.SetPage(pageName);
        }

        /// <summary>
        /// Enable / disable display sleep mode
        /// </summary>
        /// <param name="sleepMode"></param>
        public void SetSleepMode(bool sleepMode)
        {
            Display.SetSleepMode(sleepMode);
        }

        /// <summary>
        /// Internal synhornization object
        /// </summary>
        public object SyncObject
        {
            get { return Display.SyncObject; }
        }

        /// <summary>
        /// On touch component event
        /// </summary>
        public event Driver.NextionDisplay.TouchEventDelegate TouchEvent
        {
            add { Display.TouchEvent += value; }
            remove { Display.TouchEvent -= value; }
        }

        /// <summary>
        /// Current touch mode
        /// </summary>
        public Driver.TouchMode TouchMode
        {
            get
            {
                return Display.TouchMode;
            }
            set
            {
                Display.TouchMode = value;
            }
        }

        /// <summary>
        /// On touch XY event
        /// </summary>
        public event Driver.NextionDisplay.TouchEventXYDelegate TouchXYEvent
        {
            add { Display.TouchXYEvent += value; }
            remove { Display.TouchXYEvent -= value; }
        }

        /// <summary>
        /// Display width (0 - undefined)
        /// </summary>
        public int Width
        {
            get { return Display.Width; }
        }

        #region IDisposable implementation

        public void Dispose()
        {
            try
            {
                if (Display != null)
                {
                    Display.Dispose();
                    Display = null;
                }
            }
            catch { }
        }

        #endregion
    }
}
