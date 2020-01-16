using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver.Controls
{
    public class ProgressBar : Bases.IntValueControlBase
    {
        public ProgressBar(NextionDisplay display, byte pageId, string id, byte index) : base(display, pageId, id, index)
        {
        }
    }
}
