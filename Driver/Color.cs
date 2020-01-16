using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver
{
    /// <summary>
    /// Nextion in 5R6G5B format. Transparent is not valid in all cases.
    /// </summary>
    public enum Color
    {
        /// <summary>
        /// Red
        /// </summary>
        Red = 63488,
        /// <summary>
        /// Blue
        /// </summary>
        Blue = 31,
        /// <summary>
        /// Gray
        /// </summary>
        Gray = 33840,
        /// <summary>
        /// Black
        /// </summary>
        Black = 0,
        /// <summary>
        /// White
        /// </summary>
        White = 65535,
        /// <summary>
        /// Green
        /// </summary>
        Green = 2016,
        /// <summary>
        /// Brown
        /// </summary>
        Brown = 48192,
        /// <summary>
        /// Yellow
        /// </summary>
        Yellow = 65504,
        /// <summary>
        /// Transparent
        /// </summary>
        Transparent = -1
    }
}
