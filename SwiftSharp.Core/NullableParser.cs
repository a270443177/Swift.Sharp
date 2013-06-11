using SwiftSharp.Core.Rest;
// ---------------------------------------------------------------------------
// <copyright file="NullableParser.cs" company="">
//     Copyright (c) Israel 2013. All rights reserved.
//     Author: alex
//     Date: 9-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Core
{
    /// <summary>
    /// Web response for request that does not have a web response
    /// </summary>
    public class NullableParser : IWebResponseParser<string>
    {
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public string Data
        {
            get 
            {
                return null;
            }
        }

        /// <summary>
        /// Builds from web response.
        /// </summary>
        /// <param name="webResponseDetails"><see cref="WebResponseDetails" /> object</param>
        public void BuildFromWebResponse(IWebResponseDetails webResponseDetails)
        {
            // Nothing to do
        }
    }
}
