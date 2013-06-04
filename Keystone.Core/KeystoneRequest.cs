// ---------------------------------------------------------------------------
// <copyright file="KeystoneRequest.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 30-5-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace Keystone.Core
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Request object for Keystone authentication
    /// Will be converted to JSON string
    /// </summary>
    [DataContract]
    internal class KeystoneRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeystoneTokenRequest"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        internal KeystoneRequest(string username, string password, string tenantName)
        {
            this.auth = new Authentication(username, password, tenantName);
        }

        /// <summary>
        /// Gets or sets the auth.
        /// </summary>
        /// <value>
        /// The auth.
        /// </value>
        [DataMember(Name = "auth")]
        public Authentication auth
        {
            get;
            set;
        }
    }
}
