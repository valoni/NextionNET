using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver.Controls.Hidden
{
    public class Variable : Bases.ControlBase
    {
        public Variable(NextionDisplay display, byte pageId, string id, byte index) : base(display, pageId, id, index)
        {

        }

        /// <summary>
        /// Get or set variable value
        /// </summary>
        public int Val
        {
            get { return Display.GetAttributeValueInt(Id, "val"); }
            set { Display.SetAttributeValue(Id, "val", value); }
        }
    }
}
