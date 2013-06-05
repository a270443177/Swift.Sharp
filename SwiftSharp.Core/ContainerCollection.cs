// ---------------------------------------------------------------------------
// <copyright file="ContainerCollection.cs" company="">
//     Copyright (c) Israel 2013. All rights reserved.
//     Author: alex
//     Date: 4-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Core
{
    using SwiftSharp.Core.Rest;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Swift containers collection object
    /// </summary>
    public class ContainerCollection
    {
        /// <summary>
        /// Gets or sets the bytes used.
        /// </summary>
        /// <value>
        /// The bytes used.
        /// </value>
        public int BytesUsed
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the container count.
        /// </summary>
        /// <value>
        /// The container count.
        /// </value>
        public int ContainerCount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the names of containers
        /// </summary>
        /// <value>
        /// The names of containers
        /// </value>
        public List<string> Names
        {
            get;
            set;
        }
    }

    /// <summary>
    /// <see cref="WebResponse"/> parser that build <see cref="ContainerCollection"/>
    /// </summary>
    internal class ContainerCollectionParser : IWebResponseParser<ContainerCollection>
    {
        /// <summary>
        /// The header name that define 'bytes used'
        /// </summary>
        internal const string HEADER_BYTES_USED = "X-Account-Bytes-Used";

        /// <summary>
        /// The header name that defined 'headers count'
        /// </summary>
        internal const string HEADER_CONTAINERS_COUNT = "X-Account-Container-Count";


        private ContainerCollection data = null;

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public ContainerCollection Data
        {
            get 
            {
                return data;
            }
        }

        /// <summary>
        /// Builds from web response.
        /// </summary>
        /// <param name="webResponseDetails"><see cref="WebResponseDetails" /> object</param>
        /// <example>
        /// Headers:
        /// 
        /// HTTP/1.1 200 OK
        /// Content-Length: 8
        /// Accept-Ranges: bytes
        /// X-Timestamp: 1370188777.21130
        /// X-Account-Bytes-Used: 0
        /// X-Account-Container-Count: 1
        /// Content-Type: text/plain; charset=utf-8
        /// X-Account-Object-Count: 0
        /// Date: Wed, 05 Jun 2013 11:53:50 GMT
        ///
        /// Body:
        /// cont123
        /// </example>
        public void BuildFromWebResponse(IWebResponseDetails webResponseDetails)
        {
            if ((webResponseDetails == null) || (webResponseDetails.Headers == null))
            {
                throw new ArgumentException("Incorrect web-response");
            }

            data = new ContainerCollection();

            int iData = 0;
            string strData = string.Empty;

            //
            // BytesUsed
            if (webResponseDetails.Headers.ContainsKey(HEADER_BYTES_USED))
            {
                strData = webResponseDetails.Headers[HEADER_BYTES_USED];
                if (string.IsNullOrEmpty(strData) == false)
                {
                    if (int.TryParse(strData, out iData))
                    {
                        data.BytesUsed = iData;
                    }
                    else
                    {
                        throw new FormatException("Header parameter 'BytesCount' could not be converted to integer value. Raw data: " + strData);
                    }
                }
                else
                {
                    data.BytesUsed = 0;
                }
            }
            else
            {
                data.BytesUsed = 0;
            }

            //
            // Containers count
            if (webResponseDetails.Headers.ContainsKey(HEADER_CONTAINERS_COUNT))
            {
                strData = webResponseDetails.Headers[HEADER_CONTAINERS_COUNT];
                if (string.IsNullOrEmpty(strData) == false)
                {
                    if (int.TryParse(strData, out iData))
                    {
                        data.ContainerCount = iData;
                    }
                    else
                    {
                        throw new FormatException("Header parameter 'Container count' could not be converted to integer value. Raw data: " + strData);
                    }
                }
                else
                {
                    data.ContainerCount = 0;
                }
            }
            else
            {
                data.ContainerCount = 0;
            }

            //
            // Body
            data.Names = new List<string>();
            if (string.IsNullOrEmpty(webResponseDetails.Body) == false)
            {
                string[] names = webResponseDetails.Body.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                data.Names.AddRange(names);
            }
        }
    }
}
