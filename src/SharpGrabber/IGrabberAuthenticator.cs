using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTools.SharpGrabber
{
    /// <summary>
    /// Authenticates <see cref="IGrabber"/> client.
    /// </summary>
    public interface IGrabberAuthenticator
    {
        /// <summary>
        /// Authenticates with the specified credentials.
        /// </summary>
        Task AuthenticateAsync(ICredentials credentials);

        /// <summary>
        /// Applies the credentials to the specified <see cref="HttpClient"/>
        /// </summary>
        void TouchHttpClient(HttpClient client);
    }
}
