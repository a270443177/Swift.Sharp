// ---------------------------------------------------------------------------
// <copyright file="AccountDetails.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 3-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Core
{
    using System;
    using SwiftSharp.Core.Rest;

    /// <summary>
    /// Very simple and fast-to-get account details
    /// </summary>
    public class AccountDetails
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
        /// Gets or sets the objects count.
        /// </summary>
        /// <value>
        /// The objects count.
        /// </value>
        public int ObjectsCount
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Web response parser that create <see cref="AccountDetails"/> object
    /// </summary>
    internal class AccountDetailsParser : IWebResponseParser<AccountDetails>
    {
        /// <summary>
        /// The header name that define 'bytes used'
        /// </summary>
        private const string HEADER_BYTES_USED = "X-Account-Bytes-Used";

        /// <summary>
        /// The header name that defined 'headers count'
        /// </summary>
        private const string HEADER_CONTAINERS_COUNT = "X-Account-Container-Count";

        /// <summary>
        /// The header name that define 'objects count'
        /// </summary>
        private const string HEADER_OBJECT_COUNT = "X-Account-Object-Count";


        private AccountDetails details;

        /// <summary>
        /// Builds from web response.
        /// </summary>
        /// <param name="webResponseDetails">The details of SWIFT server response</param>
        /// <example>
        ///     HTTP/1.1 204 No Content
        ///     Content-Length: 0
        ///     Accept-Ranges: bytes
        ///     X-Timestamp: 1370188777.21130
        ///     X-Account-Bytes-Used: 0
        ///     X-Account-Container-Count: 1
        ///     Content-Type: text/plain; charset=utf-8
        ///     X-Account-Object-Count: 0
        ///     Date: Mon, 03 Jun 2013 08:34:03 GMT
        /// </example>
        public void BuildFromWebResponse(IWebResponseDetails webResponseDetails)
        {
            if ((webResponseDetails == null) || (webResponseDetails.Headers == null))
            {
                throw new ArgumentException("Incorrect web-response");
            }

            details = new AccountDetails();

            int iData = 0;
            string strData = string.Empty;

            //
            // BytesUsed
            strData = webResponseDetails.Headers[HEADER_BYTES_USED];
            if (string.IsNullOrEmpty(strData) == false)
            {
                if (int.TryParse(strData, out iData))
                {
                    details.BytesUsed = iData;
                }
                else
                {
                    throw new FormatException("Header parameter 'BytesCount' could not be converted to integer value. Raw data: " + strData);
                }
            }
            else
            {
                details.BytesUsed = 0;
            }

            //
            // Containers count
            strData = webResponseDetails.Headers[HEADER_CONTAINERS_COUNT];
            if (string.IsNullOrEmpty(strData) == false)
            {
                if (int.TryParse(strData, out iData))
                {
                    details.ContainerCount = iData;
                }
                else
                {
                    throw new FormatException("Header parameter 'Container count' could not be converted to integer value. Raw data: " + strData);
                }
            }
            else
            {
                details.ContainerCount = 0;
            }

            //
            // Object count
            strData = webResponseDetails.Headers[HEADER_OBJECT_COUNT];
            if (string.IsNullOrEmpty(strData) == false)
            {
                if (int.TryParse(strData, out iData))
                {
                    details.ObjectsCount = iData;
                }
                else
                {
                    throw new FormatException("Header parameter 'Object count' could not be converted to integer value. Raw data: " + strData);
                }
            }
            else
            {
                details.ObjectsCount = 0;
            }
        }

        /// <summary>
        /// Gets the <see cref="AccountDetails"/> object created from 'web response'
        /// </summary>
        /// <value>
        /// The <see cref="AccountDetails"/> object
        /// </value>
        public AccountDetails Data
        {
            get 
            {
                return details;
            }
        }
    }
}
