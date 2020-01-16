using System;
using Microsoft.SPOT;
using System.IO.Ports;
using System.Collections;

namespace JernejK.NextionNET.DebugHelper
{
    public class NextionDebugHelper
    {
        private Driver.NextionDisplay _Display;
        private int LineHeight;
        private string[] Messages;
        private int CurrentMessageIndex = 0;

        private object LockObj = new object();

        #region Ctor

        /// <summary>
        /// Construct NextionDebuggerHelper on provided NextionDisplay
        /// </summary>
        /// <param name="display"></param>
        /// <param name="lineHeight">If line height is too small there will be no text on display!</param>
        public NextionDebugHelper(Driver.NextionDisplay display, int lineHeight)
        {
            if (_Display.Height <= 0 || _Display.Width <= 0)
                throw new Exception("Plese set display size");

            _Display = display;
            LineHeight = lineHeight;

            AutoRefresh = true;
            Init();
        }

        /// <summary>
        /// Construct NextionDebuggerHelper with NextionDisplay on provided port.
        /// </summary>
        /// <param name="serial"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="lineHeight">If line height is too small there will be no text on display!</param>
        public NextionDebugHelper(SerialPort serial, int width, int height, int lineHeight)
        {
            _Display = new Driver.NextionDisplay(serial, width, height);
            LineHeight = lineHeight;

            AutoRefresh = true;
            Init();
        }

        #endregion

        #region Properties

        public bool AutoRefresh { get; set; }

        /// <summary>
        /// Direct access to display
        /// </summary>
        public Driver.NextionDisplay Display
        {
            get { return _Display; }
        }

        /// <summary>
        /// Text color
        /// </summary>
        public int FontColor
        {
            get { return _Display.GUI.DefaultFontColor; }
            set
            {
                _Display.GUI.DefaultFontColor = value;
                Render();
            }
        }

        /// <summary>
        /// Background color
        /// </summary>
        public int BackgroundColor
        {
            get { return _Display.GUI.DefaultBackgroundColor; }
            set
            {
                _Display.GUI.DefaultBackgroundColor = value;
                Render();
            }
        }

        /// <summary>
        /// Font ID
        /// </summary>
        public byte FontId
        {
            get { return _Display.GUI.DefaultFontId; }
            set
            {
                _Display.GUI.DefaultFontId = value;
                Render();
            }
        }

        /// <summary>
        /// How many lines will be displayed
        /// </summary>
        public int NumberOfLines { get; private set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Add message
        /// </summary>
        /// <param name="message"></param>
        public void Add(string message)
        {
            lock (LockObj)
            {
                Messages[CurrentMessageIndex] = NextionNET.Driver.NextUtils.EscapeString(message);
                CurrentMessageIndex = (CurrentMessageIndex + 1) % NumberOfLines;
                if (AutoRefresh)
                {
                    Render();
                }
            }
        }

        /// <summary>
        /// Clear messages
        /// </summary>
        public void Clear()
        {
            lock (LockObj)
            {
                Messages = new string[NumberOfLines];

                if (AutoRefresh)
                {
                    Render();
                }
            }
        }

        /// <summary>
        /// Render screen
        /// </summary>
        public void Render()
        {
            lock (LockObj)
            {
                lock (_Display.SyncObject)
                {
                    _Display.GUI.Clear(_Display.GUI.DefaultBackgroundColor);
                    for (int i = 0; i < NumberOfLines; i++)
                    {
                        string message = Messages[(CurrentMessageIndex + NumberOfLines - i - 1) % NumberOfLines];
                        _Display.GUI.WriteText(message, 0, i * LineHeight, LineHeight);
                    }
                }
            }
        }

        #endregion

        #region Private

        private void Init()
        {
            _Display.SetCommandProcessorState(true);
            NumberOfLines = _Display.Height / LineHeight;
            Clear();

            if (AutoRefresh)
            {
                Render();
            }
        }

        #endregion
    }
}
