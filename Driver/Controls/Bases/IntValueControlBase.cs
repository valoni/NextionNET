using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver.Controls.Bases
{
    public abstract class IntValueControlBase : ForeBackColorSurfaceControlBase
    {
        public IntValueControlBase(NextionDisplay display, byte pageId, string id, byte index) : base(display, pageId, id, index)
        {

        }

        /// <summary>
        /// Value
        /// </summary>
        public int Val
        {
            get { return Display.GetAttributeValueInt(Id, "val"); }
            set { Display.SetAttributeValue(Id, "val", value); }
        }
    }
}
