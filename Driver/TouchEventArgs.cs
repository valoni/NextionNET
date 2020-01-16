using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver
{
    public class TouchEventArgs : EventArgs
    {
        private ControlCollection CtrlColl;
        public TouchEventArgs(ControlCollection ctrlColl)
        {
            CtrlColl = ctrlColl;
        }

        /// <summary>
        /// Touched Page Id
        /// </summary>
        public byte PageId { get; set; }
        /// <summary>
        /// Touched Control Index
        /// </summary>
        public byte ControlIndex { get; set; }
        /// <summary>
        /// Press / Released
        /// </summary>
        public bool Press { get; set; }
        /// <summary>
        /// Find control in control collection
        /// </summary>
        /// <returns></returns>
        public Controls.Bases.ControlBase ResolveControl()
        {
            return CtrlColl.GetControlOrDefault(PageId, ControlIndex);
        }
    }
}
