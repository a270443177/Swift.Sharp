// ---------------------------------------------------------------------------
// <copyright file="RestClient.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 3-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Core.Rest
{
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// REST client
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    internal class RestClient<TRequest, TResponse>
        where TRequest : GenericDataProvider
        where TResponse : IWebResponseParser, new()
    {
        /// <summary>
        /// Executes the specified request data.
        /// </summary>
        /// <param name="requestData">The request data.</param>
        /// <returns></returns>
        internal Task<TResponse> Execute(TRequest requestData, CancellationToken cancellationToken)
        {
            var queryString = requestData.QueryParams.ToQueryString();
            string rawUri = requestData.Endpoint.ToString();

            if (string.IsNullOrEmpty(queryString) == false)
            {
                if (string.IsNullOrEmpty(requestData.Endpoint.Query))
                {
                    // Build new query string
                    rawUri += "?" + queryString;
                }
                else
                {
                    // Append to existing string
                    rawUri += "&" + queryString;
                }
            }

            cancellationToken.ThrowIfCancellationRequested();

            HttpWebRequest request = WebRequest.Create(rawUri) as HttpWebRequest;

            requestData.HeaderParams.AsParallel().ForAll((pair) => { request.Headers.Add(pair.Key, pair.Value); });

            request.Method = requestData.Method;

            cancellationToken.ThrowIfCancellationRequested();

            return Task.Factory.StartNew<TResponse>(() =>
            {
                WebResponse response = request.GetResponse();
                
                TResponse responseObject = new TResponse();
                responseObject.BuildFromWebResponse(new WebResponseDetails(response));
                return responseObject;
            }
            , cancellationToken
            , TaskCreationOptions.LongRunning
            , TaskScheduler.Current);
        }
    }
}
