using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver.Controls.Bases
{
    public abstract class SurfaceControlBase : ControlBase
    {
        public SurfaceControlBase(NextionDisplay display, byte pageId, string id, byte index) : base(display, pageId, id, index)
        {

        }

        /*
        /// <summary>
        /// Position X
        /// </summary>
        public int X
        {
            get { return Display.GetAttributeValueInt(Id, "x"); }
            set { Display.SetAttributeValue(Id, "x", value); }
        }

        /// <summary>
        /// Position Y
        /// </summary>
        public int Y
        {
            get { return Display.GetAttributeValueInt(Id, "y"); }
            set { Display.SetAttributeValue(Id, "y", value); }
        }

        /// <summary>
        /// Control width
        /// </summary>
        public int Width
        {
            get { return Display.GetAttributeValueInt(Id, "w"); }
            set { Display.SetAttributeValue(Id, "w", value); }
        }

        /// <summary>
        /// Control height
        /// </summary>
        public int Height
        {
            get { return Display.GetAttributeValueInt(Id, "h"); }
            set { Display.SetAttributeValue(Id, "h", value); }
        }
        */

        /// <summary>
        /// Manualy refresh control
        /// </summary>
        public void Refresh()
        {
            Display.RefreshComponent(Id);
        }

        /// <summary>
        /// Enable or disable touch of this control
        /// </summary>
        /// <param name="enabled"></param>
        public void ChangeTouchEnabled(bool enabled)
        {
            ((INextionDisplayPrivate)Display).SendCommand("tsw " + Id + "," + (enabled ? "1" : "0"), false);
        }
    }
}
