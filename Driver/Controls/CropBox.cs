using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver.Controls
{
    public class CropBox : Bases.SurfaceControlBase
    {
        public CropBox(NextionDisplay display, byte pageId, string id, byte index) : base(display, pageId, id, index)
        {
        }

        /// <summary>
        /// Current picture id
        /// </summary>
        public int PictureId
        {
            get { return Display.GetAttributeValueInt(Id, "picc"); }
            set { Display.SetAttributeValue(Id, "picc", value); }
        }
    }
}
