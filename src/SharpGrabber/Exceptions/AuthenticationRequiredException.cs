using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.SharpGrabber.Exceptions
{
    /// <summary>
    /// Thrown when a grab operation requires client authentication.
    /// </summary>
    public class AuthenticationRequiredException : GrabException
    {
        public AuthenticationRequiredException()
        {
        }

        public AuthenticationRequiredException(string message) : base(message)
        {
        }

        public AuthenticationRequiredException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
