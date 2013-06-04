// ---------------------------------------------------------------------------
// <copyright file="User.cs" company="">
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
    /// User entity in <see cref="KeystoneResponse"/>
    /// </summary>
    [DataContract]
    public class User
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [DataMember(Name = "username")]
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the roles links.
        /// </summary>
        /// <value>
        /// The roles links.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Serialization property")] 
        [DataMember(Name = "roles_links")]
        public string[] RolesLinks
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        [DataMember(Name = "id")]
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember(Name = "name")]
        public string Name
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
        public Role[] Roles
        {
            get;
            set;
        }
    }
}
