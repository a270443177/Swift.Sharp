// ---------------------------------------------------------------------------
// <copyright file="Token.cs" company="">
//     Copyright (c) Israel 2013. All rights reserved.
//     Author: alex
//     Date: 4-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace Keystone.Core
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 'Token' entity in <see cref="KeystoneResponse"/>
    /// </summary>
    [DataContract]
    public class Token
    {
        /// <summary>
        /// Gets or sets the issued at.
        /// </summary>
        /// <value>
        /// The issued at.
        /// </value>
        [DataMember(Name = "issued_at")]
        public string IssuedAt
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the expires.
        /// </summary>
        /// <value>
        /// The expires.
        /// </value>
        [DataMember(Name = "expires")]
        public string Expires
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
        /// Gets or sets the tenant.
        /// </summary>
        /// <value>
        /// The tenant.
        /// </value>
        [DataMember(Name = "tenant")]
        public Tenant Tenant
        {
            get;
            set;
        }
    }
}
