using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver.Controls.Bases
{
    public abstract class ForeBackColorSurfaceControlBase : SurfaceControlBase
    {
        /// <summary>
        /// Default contructor
        /// </summary>
        /// <param name="display"></param>
        /// <param name="pageId"></param>
        /// <param name="id"></param>
        public ForeBackColorSurfaceControlBase(NextionDisplay display, byte pageId, string id, byte index) : base(display, pageId, id, index)
        {

        }

        /// <summary>
        /// Backgroud color
        /// </summary>
        public int BackgroundColor
        {
            get { return Display.GetAttributeValueInt(Id, "bco"); }
            set { Display.SetAttributeValue(Id, "bco", value); }
        }

        /// <summary>
        /// Foreground color
        /// </summary>
        public int ForegroundColor
        {
            get { return Display.GetAttributeValueInt(Id, "pco"); }
            set { Display.SetAttributeValue(Id, "pco", value); }
        }
    }
}
