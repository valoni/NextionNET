using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver.Controls
{
    public class NumberBox : Bases.TextControlBase
    {
        public NumberBox(NextionDisplay display, byte pageId, string id, byte index) : base(display, pageId, id, index)
        {

        }

        /// <summary>
        /// Number of digits to display. 0 - Automatic
        /// </summary>
        public int NumberOfDigits
        {
            get { return Display.GetAttributeValueInt(Id, "lenth"); }
            set { Display.SetAttributeValue(Id, "lenth", value); }
        }

        /// <summary>
        /// Value to display
        /// </summary>
        public int Value
        {
            get { return Display.GetAttributeValueInt(Id, "val"); }
            set { Display.SetAttributeValue(Id, "val", value); }
        }
    }
}
