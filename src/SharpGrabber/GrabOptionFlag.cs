using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.SharpGrabber
{
    /// <summary>
    /// Flags for grab options
    /// </summary>
    [Flags]
    public enum GrabOptionFlag
    {
        /// <summary>
        /// No grab flags
        /// </summary>
        None = 0,

        /// <summary>
        /// Grabber may decipher URIs automatically where necessary.
        /// </summary>
        Decipher = 1,

        /// <summary>
        /// Grabber may grab related images.
        /// </summary>
        GrabImages = 2,
    }
}
