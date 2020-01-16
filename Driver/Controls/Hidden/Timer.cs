using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver.Controls.Hidden
{
    public class Timer : Bases.ControlBase
    {
        public Timer(NextionDisplay display, byte pageId, string id, byte index) : base(display, pageId, id, index)
        {

        }

        /// <summary>
        /// Get or set timer enabled
        /// </summary>
        public bool Enabled
        {
            get { return Display.GetAttributeValueInt(Id, "en") == 1; }
            set { Display.SetAttributeValue(Id, "en", value ? 1 : 0); }
        }

        /// <summary>
        /// Timer timeout in ms
        /// </summary>
        public int Timeout
        {
            get { return Display.GetAttributeValueInt(Id, "tim"); }
            set { Display.SetAttributeValue(Id, "tim", value); }
        }
    }
}
