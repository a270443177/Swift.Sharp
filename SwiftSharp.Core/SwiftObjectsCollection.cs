using SwiftSharp.Core.Rest;
// ---------------------------------------------------------------------------
// <copyright file="ObjectsCollection.cs" company="">
//     Copyright (c) Israel 2013. All rights reserved.
//     Author: alex
//     Date: 9-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Json;

    /// <summary>
    /// Collection of swift objects
    /// </summary>
    public class SwiftObjectsCollection : List<SwiftObject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SwiftObjectsCollection"/> class.
        /// </summary>
        public SwiftObjectsCollection()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwiftObjectsCollection"/> class.
        /// </summary>
        /// <param name="coll">The coll.</param>
        public SwiftObjectsCollection(IEnumerable<SwiftObject> coll)
            : base(coll)
        {
        }
    }

    /// <summary>
    /// <see cref="WebResponse"/> parser that build <see cref="SwiftObjectsCollectionParser"/>
    /// </summary>
    public class SwiftObjectsCollectionParser : IWebResponseParser<SwiftObjectsCollection>
    {
        private SwiftObjectsCollection swiftObjectsCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="SwiftObjectsCollectionParser"/> class.
        /// </summary>
        public SwiftObjectsCollectionParser()
        {
            swiftObjectsCollection = null;
        }

        public SwiftObjectsCollection Data
        {
            get 
            {
                return swiftObjectsCollection;
            }
        }

        /// <summary>
        /// Builds from web response.
        /// </summary>
        /// <param name="webResponseDetails"><see cref="WebResponseDetails" /> object</param>
        /// <exception cref="System.ArgumentNullException">webResponseDetails;Incorrect web response</exception>
        public void BuildFromWebResponse(IWebResponseDetails webResponseDetails)
        {
            if (webResponseDetails == null)
            {
                throw new ArgumentNullException("webResponseDetails", "Incorrect web response");
            }

            swiftObjectsCollection = new SwiftObjectsCollection();

            //
            // Body
            if (string.IsNullOrEmpty(webResponseDetails.Body) == false)
            {
                List<SwiftObject> tmpObjects = new List<SwiftObject>();
                using (MemoryStream mStream = new MemoryStream(System.Text.Encoding.Default.GetBytes(webResponseDetails.Body)))
                {
                    DataContractJsonSerializer deSerializer = new DataContractJsonSerializer(tmpObjects.GetType());
                    try
                    {
                        tmpObjects = deSerializer.ReadObject(mStream) as List<SwiftObject>;
                        swiftObjectsCollection.AddRange(tmpObjects);
                    }
                    catch (FormatException exp_format)
                    {
                        System.Diagnostics.Trace.WriteLine("[ContainerCollectionParser::BuildFromWebResponse] Could not desterilize data. Raw data: " + webResponseDetails.Body + "\n\nException: " + exp_format.ToString());
                        throw;
                    }
                }
            }
            else
            {
                // No objects ?
            }

        }
    }
}
