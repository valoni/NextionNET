using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver.Controls
{
    public class Button : Bases.TextControlTextBase
    {
        public Button(NextionDisplay display, byte pageId, string id, byte index) : base(display, pageId, id, index)
        {
        }
    }
}
