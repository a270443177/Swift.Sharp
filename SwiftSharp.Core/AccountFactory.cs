using SwiftSharp.Core.Rest;
// ---------------------------------------------------------------------------
// <copyright file="AccountFactory.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 3-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Core
{
    using System;

    /// <summary>
    /// Factory object for creating <see cref="GenericDataProvider"/> object
    /// </summary>
    internal class AccountFactory
    {
        /// <summary>
        /// Builds the request object <seealso cref="GenericDataProvider"/>
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <param name="method">The method.</param>
        /// <returns><see cref="GenericDataProvider"/> object</returns>
        internal static GenericDataProvider BuildRequest(SwiftCreadentials credentials, HttpMethod method)
        {
            GenericDataProvider requestData = null;

            switch(method)
            {
                case HttpMethod.Get:
                    requestData = new GenericDataProvider(credentials, "GET");
                    break;
                case HttpMethod.Head:
                    requestData = new GenericDataProvider(credentials, "GET");
                    break;
                case HttpMethod.Delete:
                    throw new ApplicationException("Method 'DELETE' is not supported");
                    break;
                case HttpMethod.Put:
                    throw new ApplicationException("Method 'PUT' is not supported");
                    break;
                default:
                    throw new ApplicationException("Unknown method provided");
                        break;
            }

            return requestData;
        }
    }
}
