// ---------------------------------------------------------------------------
// <copyright file="Role.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 30-5-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace Keystone.Core
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 'Role' entity of <see cref="User"/> object
    /// </summary>
    [DataContract]
    public class Role
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember(Name="name")]
        public string Name
        {
            get;
            set;
        }
    }
}
