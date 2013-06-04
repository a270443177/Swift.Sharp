// ---------------------------------------------------------------------------
// <copyright file="Credentials.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 30-5-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace Keystone.Core
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Authentication details
    /// </summary>
    [DataContract(Name = "passwordCredentials")]
    internal class Credentials
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [DataMember]
        public string username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [DataMember]
        public string password { get; set; }
    }
}
