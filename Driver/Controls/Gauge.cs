using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver.Controls
{
    public class Gauge : Bases.IntValueControlBase
    {
        public Gauge(NextionDisplay display, byte pageId, string id, byte index) : base(display, pageId, id, index)
        {

        }

        /// <summary>
        /// Current picture id
        /// </summary>
        public int PictureId
        {
            get { return Display.GetAttributeValueInt(Id, "pic"); }
            set { Display.SetAttributeValue(Id, "pic", value); }
        }

        /// <summary>
        /// Pointer thickness
        /// </summary>
        public byte Thickness
        {
            get { return (byte)Display.GetAttributeValueInt(Id, "wid"); }
            set { Display.SetAttributeValue(Id, "wid", value); }
        }

    }
}
