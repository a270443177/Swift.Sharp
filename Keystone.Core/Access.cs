// ---------------------------------------------------------------------------
// <copyright file="Access.cs" company="Walletex Microelectronics LTD">
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
    /// 'Access' entity for <see cref="KeystoneResponse"/>
    /// </summary>
    [DataContract]
    public class Access
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        [DataMember(Name = "token")]
        public Token Token
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the service catalog.
        /// </summary>
        /// <value>
        /// The service catalog.
        /// </value>
        [DataMember(Name = "serviceCatalog")]
        public Endpoints[] ServiceCatalog
        {
            get;
            set;
        }

        [DataMember(Name = "user")]
        public User User
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        /// <value>
        /// The metadata.
        /// </value>
        [DataMember(Name = "metadata")]
        public Metadata Metadata
        {
            get;
            set;
        }
    }
}
