using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver
{
    /// <summary>
    /// Backgroun fill type
    /// </summary>
    public enum BackgroundFill
    {
        /// <summary>
        /// Crop image
        /// </summary>
        CropImage = 0,
        /// <summary>
        /// Solid color
        /// </summary>
        SolidColor = 1,
        /// <summary>
        /// Image
        /// </summary>
        Image = 2,
    }
}
