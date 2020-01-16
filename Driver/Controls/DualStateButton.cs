using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver.Controls
{
    public class DualStateButton : Bases.SurfaceControlBase
    {
        public DualStateButton(NextionDisplay display, byte pageId, string id, byte index) : base(display, pageId, id, index)
        {

        }

        /// <summary>
        /// Backgroud color state 0
        /// </summary>
        public int BackgroundColor0
        {
            get { return Display.GetAttributeValueInt(Id, "bco0"); }
            set { Display.SetAttributeValue(Id, "bco0", value); }
        }

        /// <summary>
        /// Backgroud color state 1
        /// </summary>
        public int BackgroundColor1
        {
            get { return Display.GetAttributeValueInt(Id, "bco1"); }
            set { Display.SetAttributeValue(Id, "bco1", value); }
        }

        /// <summary>
        /// Current state
        /// </summary>
        public bool State
        {
            get { return Display.GetAttributeValueInt(Id, "val") == 1; }
            set { Display.SetAttributeValue(Id, "val", value ? 1 : 0); }
        }

    }
}
