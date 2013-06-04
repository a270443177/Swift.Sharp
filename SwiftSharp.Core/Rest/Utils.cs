// ---------------------------------------------------------------------------
// <copyright file="Utils.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 3-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Core.Rest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Various utilities/helpers for REST client 
    /// </summary>
    internal static class Utils
    {
        /// <summary>
        /// To the query string.
        /// </summary>
        /// <param name="queryParameters">The query parameters.</param>
        /// <returns></returns>
        internal static string ToQueryString(this Dictionary<string,string> queryParameters)
        {
            if (queryParameters.Count == 0)
            {
                return string.Empty;
            }

            string queryString = (from item in queryParameters
                               select string.Format("{0}={1}", item.Key, item.Value))
                               .Aggregate((x, y) => { return x + "&" + y; });

            return queryString;
        }
    }
}
