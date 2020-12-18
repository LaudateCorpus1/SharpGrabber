using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DotNetTools.SharpGrabber
{
    /// <summary>
    /// Describes options for a grab request.
    /// </summary>
    public class GrabOptions
    {
        #region Static Properties
        /// <summary>
        /// Default <see cref="GrabOptions"/>
        /// </summary>
        public static readonly GrabOptions Default = new GrabOptions();
        #endregion

        #region Properties
        /// <summary>
        /// Option flags
        /// </summary>
        public GrabOptionFlag Flags { get; set; } = GrabOptionFlag.None;

        /// <summary>
        /// The credentials to use to authenticate the client (if necessary)
        /// </summary>
        public ICredentials Credentials { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a default instance of <see cref="GrabOptions"/>.
        /// </summary>
        public GrabOptions()
        {
        }

        /// <summary>
        /// Creates an empty instance of <see cref="GrabOptions"/>.
        /// </summary>
        public GrabOptions(GrabOptionFlag flags) : this()
        {
            Flags = flags;
        }

        /// <summary>
        /// Creates an instance of <see cref="GrabOptions"/> with copied properties of <paramref name="options"/>.
        /// </summary>
        public GrabOptions(GrabOptions options)
        {
            Flags = options.Flags;
            Credentials = options.Credentials;
        }
        #endregion

        #region Chaining Methods
        /// <summary>
        /// Sets credentials.
        /// </summary>
        /// <returns>The same instance for chaining.</returns>
        public GrabOptions WithCredentials(ICredentials credentials)
        {
            Credentials = credentials;
            return this;
        }

        /// <summary>
        /// Sets credentials.
        /// </summary>
        /// <returns>The same instance for chaining.</returns>
        public GrabOptions WithCredentials(string userName, string password)
        {
            Credentials = new NetworkCredential(userName, password);
            return this;
        }
        #endregion
    }
}