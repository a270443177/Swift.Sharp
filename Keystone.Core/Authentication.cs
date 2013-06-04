// ---------------------------------------------------------------------------
// <copyright file="Authentication.cs" company="">
//     Copyright (c) Israel 2013. All rights reserved.
//     Author: alex
//     Date: 4-6-2013
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
        /// <param name="tenantName">The tenant</param>
        internal Authentication(string username, string password, string tenantName)
        {
            this.PasswordCredentials = new Credentials();
            this.PasswordCredentials.Username = username;
            this.PasswordCredentials.Password = password;
            this.TenantName = tenantName;
        }

        /// <summary>
        /// Gets or sets the password credentials.
        /// </summary>
        /// <value>
        /// The password credentials.
        /// </value>
        [DataMember(Name = "passwordCredentials")]
        public Credentials PasswordCredentials
        {
            get;
            set;
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
    }
}
