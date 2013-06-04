// ---------------------------------------------------------------------------
// <copyright file="Token.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 29-5-2013
// </copyright>
// -----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftSharp.Core.Authentication
{
    public struct AuthToken : IAuthToken
    {
        private object rawToken;

        public AuthToken(object rawToken)
        {
            this.rawToken = rawToken;
        }

        public string ToJson()
        {
            return string.Empty;
        }
    }
}
