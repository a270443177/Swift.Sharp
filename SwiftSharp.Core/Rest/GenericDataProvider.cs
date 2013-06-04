// ---------------------------------------------------------------------------
// <copyright file="GenericDataProvider.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 3-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Core.Rest
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface declare minimum data for object to execute REST call
    /// </summary>
    internal class GenericDataProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericDataProvider"/> class.
        /// </summary>
        internal GenericDataProvider(SwiftCreadentials credentials, string method)
        {
            this.Endpoint = credentials.Endpoint;

            this.HeaderParams = new Dictionary<string, string>();
            this.HeaderParams.Add("X-Auth-Token", credentials.Token);
            
            this.Method = method;
            
            this.QueryParams = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets or sets the endpoint.
        /// </summary>
        /// <value>
        /// The endpoint.
        /// </value>
        internal Uri Endpoint {get;set;}

        /// <summary>
        /// Gets or sets the header parameters
        /// </summary>
        /// <value>
        /// The header parameters
        /// </value>
        internal Dictionary<string, string> HeaderParams { get; set; }

        /// <summary>
        /// Gets or sets the query parameters
        /// </summary>
        /// <value>
        /// The query parameters
        /// </value>
        internal Dictionary<string, string> QueryParams { get; set; }

        /// <summary>
        /// Gets or sets the method (GET, PUT, etc...)
        /// </summary>
        /// <value>
        /// The method.
        /// </value>
        internal string Method { get; set; }
    }
}
