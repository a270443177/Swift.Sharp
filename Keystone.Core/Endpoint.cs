// ---------------------------------------------------------------------------
// <copyright file="Endpoint.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 30-5-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace Keystone.Core
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 'Endpoint' entity for <see cref="Endpoints"/>
    /// </summary>
    [DataContract]
    public class Endpoint
    {
        /// <summary>
        /// Gets or sets the admin URL.
        /// </summary>
        /// <value>
        /// The admin URL.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Serialization property")]
        [DataMember(Name = "adminURL")]
        public string AdminUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        [DataMember(Name = "region")]
        public string Region
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the internal URL.
        /// </summary>
        /// <value>
        /// The internal URL.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Serialization property")] 
        [DataMember(Name = "internalURL")]
        public string InternalUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        [DataMember(Name = "id")]
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the public URL.
        /// </summary>
        /// <value>
        /// The public URL.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Serialization property")] 
        [DataMember(Name = "publicURL")]
        public string PublicUrl
        {
            get;
            set;
        }
    }
}
