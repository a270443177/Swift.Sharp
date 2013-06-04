// ---------------------------------------------------------------------------
// <copyright file="IWebResponseDetails.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 4-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Core.Rest
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface definition for <see cref="WebResponseDetails"/>
    /// </summary>
    public interface IWebResponseDetails
    {
        /// <summary>
        /// Gets the body (raw)
        /// </summary>
        /// <value>
        /// The web response body (raw)
        /// </value>
        string Body { get; }

        /// <summary>
        /// Gets or sets the headers.
        /// </summary>
        /// <value>
        /// The headers.
        /// </value>
        Dictionary<string, string> Headers { get; }
    }
}
