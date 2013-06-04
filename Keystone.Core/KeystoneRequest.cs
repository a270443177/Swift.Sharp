// ---------------------------------------------------------------------------
// <copyright file="KeystoneRequest.cs" company="">
//     Copyright (c) Israel 2013. All rights reserved.
//     Author: alex
//     Date: 4-6-2013
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
        /// Initializes a new instance of the <see cref="KeystoneRequest"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="tenantName">The tenant name</param>
        internal KeystoneRequest(string username, string password, string tenantName)
        {
            this.Authentication = new Authentication(username, password, tenantName);
        }

        /// <summary>
        /// Gets or sets the auth.
        /// </summary>
        /// <value>
        /// The auth.
        /// </value>
        [DataMember(Name = "auth")]
        public Authentication Authentication
        {
            get;
            set;
        }
    }
}
