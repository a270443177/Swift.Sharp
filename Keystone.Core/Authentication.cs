// ---------------------------------------------------------------------------
// <copyright file="Authentication.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 30-5-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace Keystone.Core
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Authentication object
    /// </summary>
    [DataContract(Name = "auth")]
    internal class Authentication
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Authentication"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        internal Authentication(string username, string password, string tenantName)
        {
            this.passwordCredentials = new Credentials();
            this.passwordCredentials.username = username;
            this.passwordCredentials.password = password;
            this.TenantName = tenantName;
        }

        /// <summary>
        /// Gets or sets the name of the tenant.
        /// </summary>
        /// <value>
        /// The name of the tenant that user have access to
        /// </value>
        [DataMember(Name = "tenantName")]
        internal string TenantName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the password credentials.
        /// </summary>
        /// <value>
        /// The password credentials.
        /// </value>
        [DataMember(Name = "passwordCredentials")]
        public Credentials passwordCredentials
        {
            get;
            set;
        }
    }
}
