// ---------------------------------------------------------------------------
// <copyright file="UserMetadata.cs" company="">
//     Copyright (c) Israel 2013. All rights reserved.
//     Author: alex
//     Date: 4-6-2013
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
    public class UserMetadata
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Serialization property")]
        [DataMember(Name = "roles")]
        public string[] Roles
        {
            get;
            set;
        }
    }
}
