// ---------------------------------------------------------------------------
// <copyright file="IAuthToken.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 29-5-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Core.Authentication
{
    interface IAuthToken
    {
        /// <summary>
        /// Convert token to JSON string
        /// </summary>
        /// <returns>Token as JSON string</returns>
        string ToJson();
    }
}
