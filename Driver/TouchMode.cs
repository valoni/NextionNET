using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver
{
    /// <summary>
    /// How to process touches
    /// </summary>
    public enum TouchMode
    {
        /// <summary>
        /// Fire on component touch
        /// </summary>
        Component = 0,
        /// <summary>
        /// Fire on display touch
        /// </summary>
        Coordinates = 1
    }
}
