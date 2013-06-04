// ---------------------------------------------------------------------------
// <copyright file="Metadata.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 30-5-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace Keystone.Core
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// 'Metadata' entity in <see cref="KeystoneResponse"/>
    /// </summary>
    [DataContract]
    public class Metadata
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is admin.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is admin; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "is_admin")]
        public bool IsAdmin
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        [DataMember(Name = "roles")]
        public string[] Roles
        {
            get;
            set;
        }
    }
}
