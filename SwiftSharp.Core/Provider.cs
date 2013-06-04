// ---------------------------------------------------------------------------
// <copyright file="Class1.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 28-5-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Core
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Provider object for actual SWIFT
    /// </summary>
    public class Provider   // : ProviderBase, IProvider
    {
        //private Uri serverUri;
        //private string username;
        //private string token;

        ///// <summary>
        ///// Gets the account data (number of containers, objects, etc...)
        ///// </summary>
        ///// <param name="swiftEndpoint">The swift endpoint.</param>
        ///// <param name="username">The username.</param>
        ///// <param name="token">The token.</param>
        ///// <param name="cancellationToken">The cancellation token.</param>
        ///// <returns><see cref="AccountRequestData"/> object</returns>
        //public Task<AccountRequestData> GetAccountData(Uri swiftEndpoint, string username, string token, CancellationToken cancellationToken)
        //{
        //    this.username = username;
        //    this.token = token;
        //    this.serverUri = swiftEndpoint;

        //    return Task<AccountRequestData>.Factory.StartNew(() =>
        //    {
                
        //        HttpWebRequest request = BuildHeaders(username, token, swiftEndpoint);
        //        request.Method = "HEAD";
                

        //        cancellationToken.ThrowIfCancellationRequested();

        //        var tskResponse = request.GetResponseAsync();

        //        cancellationToken.ThrowIfCancellationRequested();

        //        var tskContinue = tskResponse.ContinueWith<AccountRequestData>(tsk1 =>
        //        {
        //            WebResponse response = tsk1.Result;
        //            return new AccountRequestData(swiftEndpoint, response.Headers);

        //        }
        //        , cancellationToken
        //        , TaskContinuationOptions.OnlyOnRanToCompletion
        //        , TaskScheduler.Current);

        //        tskResponse.ContinueWith(tsk2 => {
        //            System.Diagnostics.Trace.WriteLine("[SwiftSharp.Core::Provider] Error occurred while trying to read response from server: " + tsk2.Exception.ToString());
        //            throw tsk2.Exception;
        //        }
        //        , cancellationToken
        //        , TaskContinuationOptions.OnlyOnFaulted
        //        , TaskScheduler.Current);

        //        return tskContinue.Result;

        //    }, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Current);
        //}
    }
}
