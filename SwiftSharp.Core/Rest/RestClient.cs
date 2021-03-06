﻿// ---------------------------------------------------------------------------
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
    using System.Text;
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

            //
            // 'Content-Length' must be modified by 'HttpRequest' object
            if (requestData.HeaderParams.Keys.Any(p => p.Equals("Content-Length")))
            {
                //request.ContentLength = long.Parse(requestData.HeaderParams["Content-Length"]);
                requestData.HeaderParams.Remove("Content-Length");
            }


            requestData.HeaderParams.AsParallel().ForAll((pair) => {
                System.Diagnostics.Trace.WriteLine("[RestClient::Execute] Going to insert header: " + pair.Key + " with Value: " + pair.Value);
                request.Headers.Add(pair.Key, pair.Value); 
            });

            request.Method = requestData.Method;

            if (string.IsNullOrEmpty(requestData.Content) == false)
            {
                using (System.IO.StreamWriter writer =  new System.IO.StreamWriter(request.GetRequestStream()))
                {
                    try
                    {
                        writer.Write(requestData.Content);
                    }
                    catch (System.Exception exp_egn2)
                    {
                        System.Diagnostics.Trace.WriteLine(exp_egn2.ToString());
                    }
                }
            }

            cancellationToken.ThrowIfCancellationRequested();

            return Task.Factory.StartNew<TResponse>(() =>
            {
                WebResponse response = null;
                try
                {
                    response = request.GetResponse();
                }
                catch (WebException exp_web)
                {
                    string errorData = BuildError(exp_web, requestData);
                    System.Diagnostics.Trace.WriteLine(errorData);

                    if (exp_web.Status == WebExceptionStatus.ProtocolError)
                    {
                        throw new System.UnauthorizedAccessException(errorData, exp_web);
                    }
                    else
                    {
                        throw exp_web;
                    }
                }
                
                TResponse responseObject = new TResponse();
                responseObject.BuildFromWebResponse(new WebResponseDetails(response));
                return responseObject;
            }
            , cancellationToken
            , TaskCreationOptions.LongRunning
            , TaskScheduler.Current);
        }

        /// <summary>
        /// Builds the error information
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="originalRequest">The original request.</param>
        /// <returns>Error information as text</returns>
        protected string BuildError(WebException exception, TRequest originalRequest)
        {
            StringBuilder errorString = new StringBuilder();
            errorString.AppendLine("[RestClient::Execute] Exception occurred while trying to communicate with SWIFT server");
            errorString.AppendLine("Diagnostic data");
            errorString.AppendLine("\tEndpoint:\t" + originalRequest.Endpoint.ToString());
            errorString.AppendLine("\tMethod:\t" + originalRequest.Method);
            errorString.AppendLine("\tHeaders:");
            if (originalRequest.HeaderParams.Count > 0)
            {
                foreach (string key in originalRequest.HeaderParams.Keys)
                {
                    errorString.AppendFormat("\t\t{0}:\t{1}\n", key, originalRequest.HeaderParams[key]);
                }
            }
            else
            {
                errorString.AppendLine("\t\t<empty>");
            }

            errorString.AppendLine("\tQuery parameters:");
            if (originalRequest.QueryParams.Count > 0)
            {
                foreach (string key in originalRequest.QueryParams.Keys)
                {
                    errorString.AppendFormat("\t\t{0}:\t{1}\n", key, originalRequest.QueryParams[key]);
                }
            }
            else
            {
                errorString.AppendLine("\t\t<empty>");
            }
            errorString.AppendLine("\tContent:");
            if (string.IsNullOrEmpty(originalRequest.Content))
            {
                errorString.AppendLine("\t\t<empty>");
            }
            else
            {
                errorString.AppendLine(originalRequest.Content);
            }

            errorString.AppendLine("\tException:");
            errorString.AppendLine("\t\t" + exception.ToString());

            return errorString.ToString();
        }
    }
}
