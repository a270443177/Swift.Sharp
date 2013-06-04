// ---------------------------------------------------------------------------
// <copyright file="WebResponseDetails.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 4-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Core.Rest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;

    /// <summary>
    /// Data object to hold interesting parts of SWIFT server <see cref="WebResponse"/>
    /// </summary>
    public class WebResponseDetails : IWebResponseDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebResponseDetails"/> class.
        /// </summary>
        /// <param name="webResponse">The web response.</param>
        public WebResponseDetails(WebResponse webResponse)
        {
            if (webResponse == null)
            {
                throw new ArgumentNullException("webResponse", "Server response could not be null");
            }

            //
            // Copy headers
            this.Headers = new Dictionary<string,string>();
            if ((webResponse.Headers != null) && (webResponse.Headers.Count > 0))
            {
                foreach (string key in webResponse.Headers.AllKeys)
                    this.Headers.Add(key, webResponse.Headers[key]);
            }

            //
            // Copy body (raw)
            try
            {
                StreamReader reader = new StreamReader(webResponse.GetResponseStream(), System.Text.Encoding.Unicode);
                this.Body = reader.ReadToEnd();
            }
            catch (ArgumentNullException exp_null)
            {
                //
                // No response stream ?
                System.Diagnostics.Trace.WriteLine("WebResponse does not have any response stream");
                this.Body = string.Empty;
            }
        }


        /// <summary>
        /// Gets or sets the headers.
        /// </summary>
        /// <value>
        /// The headers.
        /// </value>
        public Dictionary<string, string> Headers
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the body (raw)
        /// </summary>
        /// <value>
        /// The web response body (raw)
        /// </value>
        public string Body
        {
            get;
            private set;
        }
    }
}
