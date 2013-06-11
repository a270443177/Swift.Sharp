// ---------------------------------------------------------------------------
// <copyright file="SwiftObject.cs" company="">
//     Copyright (c) Israel 2013. All rights reserved.
//     Author: alex
//     Date: 9-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Core
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Swift 'object' object
    /// </summary>
    [DataContract]
    public class SwiftObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SwiftObject"/> class.
        /// </summary>
        public SwiftObject()
        {
            this.LocalFileName = string.Empty;
        }

        /// <summary>
        /// Gets or sets the name of the object
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
        /// Gets or sets the MD5 hash.
        /// </summary>
        /// <value>
        /// The MD5 hash.
        /// </value>
        [DataMember(Name = "hash")]
        public string MD5Hash
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the length (in bytes)
        /// </summary>
        /// <value>
        /// The length of object (in bytes)
        /// </value>
        [DataMember(Name="bytes")]
        public string Length
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        [DataMember(Name = "content_type")]
        public string ContentType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last modify.
        /// </summary>
        /// <value>
        /// The last modify.
        /// </value>
        [DataMember(Name = "last_modified")]
        public string LastModify
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the local file.
        /// </summary>
        /// <value>
        /// The name of the local file.
        /// </value>
        public string LocalFileName
        {
            get;
            set;
        }
    }
}
