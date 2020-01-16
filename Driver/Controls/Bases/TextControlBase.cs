using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver.Controls.Bases
{
    public abstract class TextControlBase : ForeBackColorSurfaceControlBase
    {
        public TextControlBase(NextionDisplay display, byte pageId, string id, byte index) : base(display, pageId, id, index)
        {

        }

        /// <summary>
        /// Font Id
        /// </summary>
        public byte FontId
        {
            get { return (byte)Display.GetAttributeValueInt(Id, "font"); }
            set { Display.SetAttributeValue(Id, "font", value); }
        }

        /// <summary>
        /// Text horizontal alignment
        /// </summary>
        public HorizontalAlignment HorizontalAlignment
        {
            get { return (HorizontalAlignment)Display.GetAttributeValueInt(Id, "xcen"); }
            set { Display.SetAttributeValue(Id, "xcen", (int)value); }
        }

        /// <summary>
        /// Text vertical alignment
        /// </summary>
        public VerticalAlignment VerticalAlignment
        {
            get { return (VerticalAlignment)Display.GetAttributeValueInt(Id, "ycen"); }
            set { Display.SetAttributeValue(Id, "ycen", (int)value); }
        }
    }
}
