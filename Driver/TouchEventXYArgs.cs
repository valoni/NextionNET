using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver
{
    public class TouchEventXYArgs : EventArgs
    {
        /// <summary>
        /// Touch coordinate X
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Touch coordinate Y
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Press / Release
        /// </summary>
        public bool Press { get; set; }
        /// <summary>
        /// Is display in sleep mode
        /// </summary>
        public bool SleepMode { get; set; }
    }
}
