// ---------------------------------------------------------------------------
// <copyright file="User.cs" company="Walletex Microelectronics LTD">
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
        public string Username
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
        [DataMember(Name = "roles")]
        public Role[] Roles
        {
            get;
            set;
        }
    }
}
