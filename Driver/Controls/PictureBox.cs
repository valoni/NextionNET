using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver.Controls
{
    public class PictureBox : Bases.SurfaceControlBase
    {
        public PictureBox(NextionDisplay display, byte pageId, string id, byte index) : base(display, pageId, id, index)
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
    }
}
