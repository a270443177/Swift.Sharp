// ---------------------------------------------------------------------------
// <copyright file="KeystoneResponse.cs" company="">
//     Copyright (c) Israel 2013. All rights reserved.
//     Author: alex
//     Date: 4-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace Keystone.Core
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Keystone response object
    /// Actual JSON response will be de-serialized to this object
    /// </summary>
    /// <example>
    //{
    //    "access": 
    //    {
    //        "token": 
    //        {
    //            "issued_at": "2013-05-30T10:56:21.446377", 
    //            "expires": "2013-05-31T10:56:21Z", 
    //            "id": "MIIFxAYJKoZIhvcNAQcCoIIFtTCCBbECAQExCTAHBgUrDgMCGjCCBJ0GCSqGSIb3DQEHAaCCBI4EggSKeyJhY2Nlc3MiOiB7InRva2VuIjogeyJpc3N1ZWRfYXQiOiAiMjAxMy0wNS0zMFQxMDo1NjoyMS40NDYzNzciLCAiZXhwaXJlcyI6ICIyMDEzLTA1LTMxVDEwOjU2OjIxWiIsICJpZCI6ICJwbGFjZWhvbGRlciIsICJ0ZW5hbnQiOiB7ImRlc2NyaXB0aW9uIjogIlRlc3QgdGVuYW50IiwgImVuYWJsZWQiOiB0cnVlLCAiaWQiOiAiMDk3NDg4ODFjZThmNDc5YmE3NTQ3N2I3ZjVhNDZkN2IiLCAibmFtZSI6ICJ0ZXN0In19LCAic2VydmljZUNhdGFsb2ciOiBbeyJlbmRwb2ludHMiOiBbeyJhZG1pblVSTCI6ICJodHRwOi8vMTAuMC4wLjE5NTo4ODg4L3YxIiwgInJlZ2lvbiI6ICJSZWdpb25PbmUiLCAiaW50ZXJuYWxVUkwiOiAiaHR0cDovLzEwLjAuMC4xOTU6ODg4OC92MS9BVVRIXzA5NzQ4ODgxY2U4ZjQ3OWJhNzU0NzdiN2Y1YTQ2ZDdiIiwgImlkIjogIjBkMDkzZGYwNTMxYTRhOTRhMGQ5ZDJmZjJkN2ZmYWNmIiwgInB1YmxpY1VSTCI6ICJodHRwOi8vMTAuMC4wLjE5NTo4ODg4L3YxL0FVVEhfMDk3NDg4ODFjZThmNDc5YmE3NTQ3N2I3ZjVhNDZkN2IifV0sICJlbmRwb2ludHNfbGlua3MiOiBbXSwgInR5cGUiOiAib2JqZWN0LXN0b3JlIiwgIm5hbWUiOiAic3dpZnQifSwgeyJlbmRwb2ludHMiOiBbeyJhZG1pblVSTCI6ICJodHRwOi8vMTAuMC4wLjE5NTozNTM1Ny92Mi4wIiwgInJlZ2lvbiI6ICJSZWdpb25PbmUiLCAiaW50ZXJuYWxVUkwiOiAiaHR0cDovLzEwLjAuMC4xOTU6NTAwMC92Mi4wIiwgImlkIjogIjEyYWM5MzUxYjU4YjQ2OGY5NTVkNzYwMjI4YTZkMGRkIiwgInB1YmxpY1VSTCI6ICJodHRwOi8vMTAuMC4wLjE5NTo1MDAwL3YyLjAifV0sICJlbmRwb2ludHNfbGlua3MiOiBbXSwgInR5cGUiOiAiaWRlbnRpdHkiLCAibmFtZSI6ICJrZXlzdG9uZSJ9XSwgInVzZXIiOiB7InVzZXJuYW1lIjogImFsZXgiLCAicm9sZXNfbGlua3MiOiBbXSwgImlkIjogImUzY2EyY2IxM2NhZDRiZTU5YTYxMzIwNGM1NGQ2ZjE2IiwgInJvbGVzIjogW3sibmFtZSI6ICJfbWVtYmVyXyJ9LCB7Im5hbWUiOiAiYWRtaW4ifV0sICJuYW1lIjogImFsZXgifSwgIm1ldGFkYXRhIjogeyJpc19hZG1pbiI6IDAsICJyb2xlcyI6IFsiOWZlMmZmOWVlNDM4NGIxODk0YTkwODc4ZDNlOTJiYWIiLCAiZDQ4NWU0OTVhNzBjNGQ2NjhkZjFiZmM1MWYyNGY4NGYiXX19fTGB-zCB-AIBATBcMFcxCzAJBgNVBAYTAlVTMQ4wDAYDVQQIEwVVbnNldDEOMAwGA1UEBxMFVW5zZXQxDjAMBgNVBAoTBVVuc2V0MRgwFgYDVQQDEw93d3cuZXhhbXBsZS5jb20CAQEwBwYFKw4DAhowDQYJKoZIhvcNAQEBBQAEgYA4rv8i0KQYeX7v3M7rVktVCxjNJEPJXmMnfRrJ-kD1AgjXNPLCdyIke-hXXnxmb3WiAoUO2xCghJlQx4i3pc54KuuirUq6CMR7ijoDpLzcU24L1Ok5+soEqf2OyCvoY63J+lLphHFg1hB9gXt5eYRbvkTZz6BIHPCz7AD6Xhh3hA==", 
    //            "tenant": 
    //            {
    //                "description": "Test tenant", 
    //                "enabled": true, 
    //                "id": "09748881ce8f479ba75477b7f5a46d7b", 
    //                "name": "test"
    //             }
    //         }, 
    //         "serviceCatalog": 
    //         [
    //            {
    //                "endpoints": 
    //                [
    //                    {
    //                        "adminURL": "http://10.0.0.195:8888/v1", 
    //                        "region": "RegionOne", 
    //                        "internalURL": "http://10.0.0.195:8888/v1/AUTH_09748881ce8f479ba75477b7f5a46d7b", 
    //                        "id": "0d093df0531a4a94a0d9d2ff2d7ffacf", 
    //                        "publicURL": "http://10.0.0.195:8888/v1/AUTH_09748881ce8f479ba75477b7f5a46d7b"
    //                     }
    //                 ], 
    //                 "endpoints_links": [], 
    //                 "type": "object-store", 
    //                 "name": "swift"
    //             }, 
    //             {
    //                "endpoints": 
    //                [
    //                    {
    //                        "adminURL": "http://10.0.0.195:35357/v2.0", 
    //                        "region": "RegionOne", 
    //                        "internalURL": "http://10.0.0.195:5000/v2.0", 
    //                        "id": "12ac9351b58b468f955d760228a6d0dd", 
    //                        "publicURL": "http://10.0.0.195:5000/v2.0"
    //                     }
    //                ], 
    //                "endpoints_links": [], 
    //                "type": "identity", 
    //                "name": "keystone"
    //              }
    //          ], 
    //          "user": 
    //          {
    //            "username": "alex", 
    //            "roles_links": [], 
    //            "id": "e3ca2cb13cad4be59a613204c54d6f16", 
    //            "roles": 
    //            [
    //                {"name": "_member_"}, 
    //                {"name": "admin"}
    //            ], 
    //            "name": "alex"
    //     }, 
    //     "metadata": 
    //     {
    //        "is_admin": 0, 
    //        "roles": 
    //        [
    //            "9fe2ff9ee4384b1894a90878d3e92bab", 
    //            "d485e495a70c4d668df1bfc51f24f84f"
    //         ]
    //      }
    //   }
    //}
    /// </example>
    [DataContract]
    public class KeystoneResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeystoneResponse"/> class.
        /// </summary>
        public KeystoneResponse()
        {
            this.Access = new Access();
        }

        /// <summary>
        /// Gets or sets the access.
        /// </summary>
        /// <value>
        /// The access.
        /// </value>
        [DataMember(Name = "access")]
        public Access Access
        {
            get;
            set;
        }
    }
}
