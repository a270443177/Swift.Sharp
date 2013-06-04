// ---------------------------------------------------------------------------
// <copyright file="Endpoints.cs" company="">
//     Copyright (c) Israel 2013. All rights reserved.
//     Author: alex
//     Date: 4-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace Keystone.Core
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 'Endpoint' entity for 
    /// </summary>
    [DataContract]
    public class Endpoints
    {
        /// <summary>
        /// Gets or sets the endpoints.
        /// </summary>
        /// <value>
        /// The endpoints.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Serialization property")] 
        [DataMember(Name = "endpoints")]
        public Endpoint[] EndpointsCollection
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the endpoint links.
        /// </summary>
        /// <value>
        /// The endpoint links.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Serialization property")] 
        [DataMember(Name = "endpoints_links")]
        public string[] EndpointLinks
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Serialization property")] 
        [DataMember(Name = "type")]
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember(Name = "name")]
        public string Name
        {
            get;
            set;
        }
    }
}
