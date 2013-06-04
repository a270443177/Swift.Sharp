// ---------------------------------------------------------------------------
// <copyright file="KeystoneClient.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 30-5-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace Keystone.Core
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Provide Keystone manipulation functions
    /// </summary>
    public class KeystoneClient
    {
        /// <summary>
        /// Gets the token from keystone server
        /// </summary>
        /// <param name="keystoneServer">The keystone server.</param>
        /// <param name="userName">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="tenantName">Name of the tenant.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Awaitable task with <c>result</c> of token</returns>
        public Task<KeystoneResponse> GetToken(Uri keystoneServer, string userName, string password, string tenantName, CancellationToken cancellationToken)
        {
            return Task<KeystoneResponse>.Factory.StartNew(() =>
            {
                if (keystoneServer.Segments.Count() < 2)
                {
                    throw new ApplicationException("Incorrect server address. Address must include at least '/v2.0/tokens' at end");
                }

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(keystoneServer);
                request.ContentType = "application/json";
                request.Method = "POST";

                KeystoneRequest keyrequest = new KeystoneRequest(userName, password, tenantName);

                cancellationToken.ThrowIfCancellationRequested();

                //
                // Serialize
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(KeystoneRequest));

                using (var stream = request.GetRequestStream())
                {
                    serializer.WriteObject(stream, keyrequest);
                }

                //// DEBUG
                using (MemoryStream mStream1 = new MemoryStream())
                {
                    serializer.WriteObject(mStream1, keyrequest);
                    mStream1.Seek(0, SeekOrigin.Begin);
                    StreamReader reader1 = new StreamReader(mStream1);
                    System.Diagnostics.Trace.WriteLine("[Keystone::GetToken][Send request] " + reader1.ReadToEnd());
                }
                ////

                cancellationToken.ThrowIfCancellationRequested();

                System.Diagnostics.Trace.WriteLine("[Keystone::GetToken] Going to request token from: Server: [" + keystoneServer.ToString() + "] with Username: [" + userName + "] Password: [" + password + "] for Tenant: [" + tenantName + "]");
                var tskResponse = request.GetResponseAsync();

                cancellationToken.ThrowIfCancellationRequested();

                var tsk1 = tskResponse.ContinueWith<KeystoneResponse>((tskOk) =>
                {
                    System.Diagnostics.Trace.WriteLine("[Keystone::GetToken] Connected to server: " + keystoneServer.ToString() + " with username: " + userName + " password: " + password);

                    WebResponse response = tskOk.Result;

                    DataContractJsonSerializer deSerializer = new DataContractJsonSerializer(typeof(KeystoneResponse));
                    try
                    {
                        //// DEBUG
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        string jsonString = reader.ReadToEnd();
                        System.Diagnostics.Trace.WriteLine("[Keystone::GetToken] RAW response: " + jsonString);
                        //// DEBUG

                        KeystoneResponse kResponse = null;

                        using (System.IO.MemoryStream mStream = new MemoryStream(System.Text.Encoding.Unicode.GetBytes(jsonString)))
                        {
                            kResponse = deSerializer.ReadObject(mStream) as KeystoneResponse;
                        }

                        return kResponse;
                    }
                    catch (Exception exp_gen)
                    {
                        throw;
                    }

                }
                , cancellationToken
                , TaskContinuationOptions.OnlyOnRanToCompletion
                , TaskScheduler.Default);

                tskResponse.ContinueWith(tskBad =>
                {
                    System.Diagnostics.Trace.WriteLine("[Keystone::GetToken] Exception occurred while connecting to server: [" + keystoneServer.ToString() + "] with username: [" + userName + "] password: [" + password + "]");

                    System.Diagnostics.Debug.WriteLine("[Keystone::GetToken] Throw exception: " + tskBad.Exception.ToString());

                    throw tskBad.Exception;
                }
                , TaskContinuationOptions.OnlyOnFaulted);

                return tsk1.Result;
            }
            , cancellationToken
            , TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning
            , TaskScheduler.Default);
        }
    }
}
