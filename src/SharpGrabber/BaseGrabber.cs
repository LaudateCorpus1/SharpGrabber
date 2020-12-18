using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetTools.SharpGrabber
{
    /// <summary>
    /// Simple abstract implementation for <see cref="IGrabber"/>.
    /// </summary>
    public abstract class BaseGrabber : IGrabber
    {
        #region Properties
        /// <inheritdoc />
        public abstract string Name { get; }

        /// <inheritdoc />
        public GrabOptions DefaultGrabOptions { get; } = new GrabOptions(GrabOptionFlag.Decipher | GrabOptionFlag.GrabImages);

        /// <inheritdoc />
        public WorkStatus Status { get; } = new WorkStatus();

        protected IGrabberAuthenticator Authenticator { get; set; }
        #endregion

        #region Internal Methods
        protected async Task<GrabResult> AuthenticateAndRetryGrabAsync(Uri uri, CancellationToken cancellationToken, GrabOptions options)
        {
            if (options.Credentials == null)
                throw new InvalidOperationException("Cannot authenticate because no credentials are specified.");

            var authenticator = NewAuthenticator();
            await authenticator.AuthenticateAsync(options.Credentials).ConfigureAwait(false);

            var newOptions = new GrabOptions(options).WithCredentials(null);
            return await GrabAsync(uri, cancellationToken, newOptions).ConfigureAwait(false);
        }
        #endregion

        #region Methods
        /// <inheritdoc />
        public abstract bool Supports(Uri uri);

        /// <inheritdoc />
        public Task<GrabResult> GrabAsync(Uri uri) => GrabAsync(uri, new CancellationToken());

        /// <inheritdoc />
        public Task<GrabResult> GrabAsync(Uri uri, CancellationToken cancellationToken)
        {
            Status.Update(null, "Initializing...", WorkStatusType.Initiating);
            return GrabAsync(uri, cancellationToken, DefaultGrabOptions);
        }

        /// <inheritdoc />
        public abstract Task<GrabResult> GrabAsync(Uri uri, CancellationToken cancellationToken, GrabOptions options);

        public virtual IGrabberAuthenticator NewAuthenticator()
            => throw new NotSupportedException($"{Name} grabber does not support authentication.");

        public virtual void UseAuthenticator(IGrabberAuthenticator authenticator)
        {
            Authenticator = authenticator;
        }
        #endregion
    }
}