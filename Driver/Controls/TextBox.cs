using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver.Controls
{
    public class TextBox : Bases.TextControlTextBase
    {
        public TextBox(NextionDisplay display, byte pageId, string id, byte index) : base(display, pageId, id, index)
        {
        }
    }
}
