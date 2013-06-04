// ---------------------------------------------------------------------------
// <copyright file="ProviderBase.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 28-5-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Runtime.Serialization.Json;
    using System.IO;

    /// <summary>
    /// Provide base functions for <see cref="Provider"/> object
    /// </summary>
    public abstract class ProviderBase
    {
        /// <summary>
        /// Add authentication headers
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="token">The token (from Keystone).</param>
        /// <param name="serverUri">The server URI.</param>
        /// <returns><c>HttpWebRequest</c> object</returns>
        /// <exception cref="System.ArgumentNullException">serverUri;Server address must be provided</exception>
        internal HttpWebRequest BuildHeaders(string username, string token, Uri serverUri)
        {
            if (serverUri == null)
            {
                throw new ArgumentNullException("serverUri", "Server address must be provided");
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverUri);
            request.Headers.Add("X-Auth-User: " + username);
            //request.Headers.Add("X-Auth-Key: " + token);
            request.Headers.Add("X-Auth-Token: " + token);

            System.Diagnostics.Trace.WriteLine("[SwiftSharp.Core::Provider] Trying to connect to SWIFT server: [" + serverUri.ToString() + "] with username: [" + username + "] and token [" + token + "]");

            return request;
        }
    }
}
