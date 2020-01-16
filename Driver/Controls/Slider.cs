using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver.Controls
{
    public class Slider : Bases.IntValueControlBase
    {
        public Slider(NextionDisplay display, byte pageId, string id, byte index) : base(display, pageId, id, index)
        {

        }

        public int CursorWidth
        {
            get { return Display.GetAttributeValueInt(Id, "wid"); }
            set { Display.SetAttributeValue(Id, "wid", value); }
        }

        public int CursorHeight
        {
            get { return Display.GetAttributeValueInt(Id, "hig"); }
            set { Display.SetAttributeValue(Id, "hig", value); }
        }

        public int MinValue
        {
            get { return Display.GetAttributeValueInt(Id, "minval"); }
            set { Display.SetAttributeValue(Id, "minval", value); }
        }

        public int MaxValue
        {
            get { return Display.GetAttributeValueInt(Id, "maxval"); }
            set { Display.SetAttributeValue(Id, "maxval", value); }
        }
    }
}
