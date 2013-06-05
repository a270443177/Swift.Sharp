// ---------------------------------------------------------------------------
// <copyright file="Container.cs" company="">
//     Copyright (c) Israel 2013. All rights reserved.
//     Author: alex
//     Date: 5-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Core
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Swift 'Container' object
    /// </summary>
    [DataContract]
    public class Container
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember(Name="name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the endpoint.
        /// </summary>
        /// <value>
        /// The endpoint.
        /// </value>
        public Uri Endpoint
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the bytes used.
        /// </summary>
        /// <value>
        /// The bytes used.
        /// </value>
        [DataMember(Name="bytes")]
        public int BytesUsed
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
        [DataMember(Name="count")]
        public int ObjectsCount
        {
            get;
            set;
        }
    }
}
