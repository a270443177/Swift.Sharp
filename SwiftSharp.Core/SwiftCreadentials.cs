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
        /// <summary>
        /// Initializes a new instance of the <see cref="SwiftCreadentials"/> class.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="token">The token.</param>
        /// <param name="tenant">The tenant.</param>
        public SwiftCreadentials(Uri endpoint, string token, string tenant)
        {
            this.Endpoint = endpoint;
            this.Tenant = tenant;
            this.Token = token;
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
