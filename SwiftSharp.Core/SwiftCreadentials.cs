// ---------------------------------------------------------------------------
// <copyright file="SwiftCreadentials.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 3-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Core
{
    using System;

    /// <summary>
    /// 'Credentials' object for Swift server
    /// </summary>
    public class SwiftCreadentials
    {
        public SwiftCreadentials(Uri endpoint, string username, string token, string tenant)
        {
            this.Endpoint = endpoint;
            this.Tenant = tenant;
            this.Token = token;
            this.Username = username;
        }

        /// <summary>
        /// Gets or sets the endpoint.
        /// </summary>
        /// <value>
        /// The endpoint.
        /// </value>
        public Uri Endpoint
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token
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
        public string Tenant
        {
            get;
            set;
        }
    }
}
