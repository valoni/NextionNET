using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver.Controls.Bases
{
    /// <summary>
    /// Base class for controls
    /// </summary>
    public abstract class ControlBase
    {
        /// <summary>
        /// Access to display object
        /// </summary>
        protected NextionDisplay Display;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="display"></param>
        /// <param name="pageId"></param>
        /// <param name="id"></param>
        protected ControlBase(NextionDisplay display, byte pageId, string id, byte index)
        {
            Display = display;
            PageId = pageId;
            Id = id;
            Index = index;
        }

        /// <summary>
        /// Control id
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// Page id
        /// </summary>
        public byte PageId { get; private set; }
        /// <summary>
        /// Control index
        /// </summary>
        public byte Index { get; private set; }
    }

}
