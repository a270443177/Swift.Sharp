using SwiftSharp.Core.Rest;
// ---------------------------------------------------------------------------
// <copyright file="Swift.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 3-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// SWIFT client
    /// Provide all functions to work with SWIFT server
    /// </summary>
    public class Swift
    {
        private SwiftCreadentials creadentials;

        public Swift(Uri endpoint, string username, string token, string tenant) : this (new SwiftCreadentials(endpoint, username, token, tenant))
        {

        }

        public Swift(SwiftCreadentials creadentials)
        {
            this.creadentials = creadentials;
        }

        public Task<AccountDetails> GetAccountDetails(CancellationToken cancellationToken)
        {
            GenericDataProvider request = AccountFactory.BuildRequest(this.creadentials, HttpMethod.Get);

            RestClient<GenericDataProvider, AccountDetailsParser> client = new RestClient<GenericDataProvider, AccountDetailsParser>();
            var tsk = client.Execute(request, cancellationToken);

            var tskResult = tsk.ContinueWith<AccountDetails>(tsk1 => {
                return tsk1.Result.Data as AccountDetails;
            }
            , cancellationToken
            , TaskContinuationOptions.OnlyOnRanToCompletion
            , TaskScheduler.Default);

            return tskResult;
        }
    }
}
