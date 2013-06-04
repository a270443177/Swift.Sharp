using SwiftSharp.Core.Rest;
// ---------------------------------------------------------------------------
// <copyright file="AccountFactory.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 3-6-2013
// </copyright>
// -----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftSharp.Core
{
    internal class AccountFactory
    {
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
