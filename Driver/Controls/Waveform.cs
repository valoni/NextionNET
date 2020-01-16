using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver.Controls
{
    /// <summary>
    /// Waveform control
    /// </summary>
    public class Waveform : Bases.SurfaceControlBase
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="display"></param>
        /// <param name="pageId"></param>
        /// <param name="id"></param>
        /// <param name="index"></param>
        public Waveform(NextionDisplay display, byte pageId, string id, byte index) : base(display, pageId, id, index)
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
        /// Foreground color channel 0
        /// </summary>
        public int ForegroundColor0
        {
            get { return Display.GetAttributeValueInt(Id, "pco0"); }
            set { Display.SetAttributeValue(Id, "pco0", value); }
        }

        /// <summary>
        /// Foreground color channel 1
        /// </summary>
        public int ForegroundColor1
        {
            get { return Display.GetAttributeValueInt(Id, "pco1"); }
            set { Display.SetAttributeValue(Id, "pco1", value); }
        }

        /// <summary>
        /// Foreground color channel 2
        /// </summary>
        public int ForegroundColor2
        {
            get { return Display.GetAttributeValueInt(Id, "pco2"); }
            set { Display.SetAttributeValue(Id, "pco2", value); }
        }

        /// <summary>
        /// Foreground color channel 3
        /// </summary>
        public int ForegroundColor3
        {
            get { return Display.GetAttributeValueInt(Id, "pco3"); }
            set { Display.SetAttributeValue(Id, "pco3", value); }
        }

        /// <summary>
        /// Grid color
        /// </summary>
        public int GridColor
        {
            get { return Display.GetAttributeValueInt(Id, "gdc"); }
            set { Display.SetAttributeValue(Id, "gdc", value); }
        }

        /// <summary>
        /// Vertial grid width
        /// </summary>
        public byte GridIntervalVertical
        {
            get { return (byte)Display.GetAttributeValueInt(Id, "gdw"); }
            set { Display.SetAttributeValue(Id, "gdw", value); }
        }

        /// <summary>
        /// Horizontal grid width
        /// </summary>
        public byte GridIntervalHorizontal
        {
            get { return (byte)Display.GetAttributeValueInt(Id, "gdh"); }
            set { Display.SetAttributeValue(Id, "gdh", value); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="value"></param>
        public void Add(byte channel, byte value)
        {
            ((INextionDisplayPrivate)Display).SendCommand("add " + Index + "," + channel + "," + value, false);
        }
    }
}
