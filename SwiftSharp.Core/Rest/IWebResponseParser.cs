// ---------------------------------------------------------------------------
// <copyright file="IWebResponseParser.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 3-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Core.Rest
{
    using System.Net;

    /// <summary>
    /// Interface definition for generic parser
    /// </summary>
    internal interface IWebResponseParser
    {
        /// <summary>
        /// Builds from web response.
        /// </summary>
        /// <param name="webResponseDetails"><see cref="WebResponseDetails"/> object</param>
        /// <returns>Data object build from <c>WebResponse</c></returns>
        void BuildFromWebResponse(IWebResponseDetails webResponseDetails);
    }

    /// <summary>
    /// Interface definition for generic parser
    /// </summary>
    /// <typeparam name="TDataObject">The type of the data object.</typeparam>
    internal interface IWebResponseParser<TDataObject> : IWebResponseParser
    {
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        TDataObject Data { get; }
    }
}
