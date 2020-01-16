using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver.Controls.Bases
{
    public abstract class TextControlTextBase : TextControlBase
    {
        public TextControlTextBase(NextionDisplay display, byte pageId, string id, byte index) : base(display, pageId, id, index)
        {

        }

        /// <summary>
        /// Text. Use \r\n for new line
        /// </summary>
        public string Text
        {
            get { return Display.GetAttributeValueString(Id, "txt"); }
            set { Display.SetAttributeValue(Id, "txt", value); }
        }

        /// <summary>
        /// Provided text is escaped before set. You can use " in text
        /// </summary>
        public string TextEscaped
        {
            get { return Text; }
            set { Text = NextUtils.EscapeString(value); }
        }
    }
}
