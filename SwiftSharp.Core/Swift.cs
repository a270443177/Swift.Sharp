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
        private SwiftCreadentials credentials;

        /// <summary>
        /// Initializes a new instance of the <see cref="Swift"/> class.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="token">The token.</param>
        /// <param name="tenant">The tenant.</param>
        public Swift(Uri endpoint, string token, string tenant) : this (new SwiftCreadentials(endpoint, token, tenant))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Swift"/> class.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        public Swift(SwiftCreadentials credentials)
        {
            this.credentials = credentials;
        }

        /// <summary>
        /// Gets the account details.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Awaitable task with result as <seealso cref="AccountDetails"/></returns>
        public Task<AccountDetails> GetAccountDetails(CancellationToken cancellationToken)
        {
            GenericDataProvider request = AccountFactory.BuildRequest(this.credentials, HttpMethod.Get);

            RestClient<GenericDataProvider, AccountDetailsParser> client = new RestClient<GenericDataProvider, AccountDetailsParser>();
            var tsk = client.Execute(request, cancellationToken);

            return tsk.ContinueWith<AccountDetails>(tskOk => {
                return tskOk.Result.Data as AccountDetails;
            }
            , cancellationToken);
        }
    }
}
