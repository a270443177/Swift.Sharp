using SwiftSharp.Core.Rest;
// ---------------------------------------------------------------------------
// <copyright file="Swift.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 3-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Core
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// SWIFT client
    /// Provide all functions to work with SWIFT server
    /// </summary>
    public class Swift
    {
        private SwiftCreadentials credentials;

        /// <summary>
        /// Initializes a new instance of the <see cref="Swift"/> class.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="token">The token.</param>
        /// <param name="tenant">The tenant.</param>
        public Swift(Uri endpoint, string token, string tenant) : this (new SwiftCreadentials(endpoint, token, tenant))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Swift"/> class.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        public Swift(SwiftCreadentials credentials)
        {
            this.credentials = credentials;
        }

        /// <summary>
        /// Gets the account details.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Awaitable task with result as <seealso cref="AccountDetails"/></returns>
        public Task<AccountDetails> GetAccountDetails(CancellationToken cancellationToken)
        {
            GenericDataProvider request = new GenericDataProvider(this.credentials, HttpMethod.Head);
            request.QueryParams.Add("format", "json");

            RestClient<GenericDataProvider, AccountDetailsParser> client = new RestClient<GenericDataProvider, AccountDetailsParser>();
            var tsk = client.Execute(request, cancellationToken);

            return tsk.ContinueWith<AccountDetails>(tskOk => {
                return tskOk.Result.Data as AccountDetails;
            }
            , cancellationToken);
        }

        /// <summary>
        /// Gets the containers.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="ContainerCollection"/> object</returns>
        public Task<ContainerCollection> GetContainers(CancellationToken cancellationToken)
        {
            GenericDataProvider request = new GenericDataProvider(this.credentials, HttpMethod.Get);
            request.QueryParams.Add("format", "json");

            RestClient<GenericDataProvider, ContainerCollectionParser> client = new RestClient<GenericDataProvider, ContainerCollectionParser>();
            var tsk = client.Execute(request, cancellationToken);

            return tsk.ContinueWith<ContainerCollection>(tskOk =>
            {
                ContainerCollection coll = tskOk.Result.Data as ContainerCollection;
                foreach (Container container in coll)
                {
                    string endpoint = this.credentials.Endpoint.ToString() + "/" + container.Name;
                    container.Endpoint = new Uri(endpoint);
                }
                return coll; ;
            }
            , cancellationToken);
        }

        /// <summary>
        /// Creates the container.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<ContainerCollection> CreateContainer(string containerName, CancellationToken cancellationToken)
        {
            GenericDataProvider request = new GenericDataProvider(this.credentials, HttpMethod.Put);
            string containerUri = request.Endpoint.ToString();
            if (containerUri.EndsWith("/"))
            {
                containerUri += containerName;
            }
            else
            {
                containerUri += "/" + containerName;
            }
            
            request.Endpoint = new Uri(containerUri);

            RestClient<GenericDataProvider, ContainerCollectionParser> client = new RestClient<GenericDataProvider, ContainerCollectionParser>();
            var tsk = client.Execute(request, cancellationToken);

            return tsk.ContinueWith<ContainerCollection>(tskOk =>
            {
                return GetContainers(cancellationToken).Result;
            }
            , cancellationToken);
        }

        /// <summary>
        /// Deletes the container.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<ContainerCollection> DeleteContainer(string containerName, CancellationToken cancellationToken)
        {
            var tsk = GetContainers(cancellationToken);
            ContainerCollection containerCollection = tsk.Result;
            Container deleteContainer = null;


            foreach (Container container in containerCollection)
            {
                if (container.Name.Equals(containerName))
                {
                    deleteContainer = container;
                }
            }

            if (deleteContainer != null)
            {
                return DeleteContainer(deleteContainer, cancellationToken);
            }
            else
            {
                throw new ArgumentNullException("containerName", "Container is not found");
            }
        }

        /// <summary>
        /// Deletes the container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<ContainerCollection> DeleteContainer(Container container, CancellationToken cancellationToken)
        {
            GenericDataProvider request = new GenericDataProvider(this.credentials, HttpMethod.Delete);

            request.Endpoint = container.Endpoint;

            RestClient<GenericDataProvider, ContainerCollectionParser> client = new RestClient<GenericDataProvider, ContainerCollectionParser>();
            var tsk = client.Execute(request, cancellationToken);

            return tsk.ContinueWith<ContainerCollection>(tskOk =>
            {
                //return tskOk.Result.Data as ContainerCollection;

                return GetContainers(cancellationToken).Result;
            }
            , cancellationToken);
        }

        /// <summary>
        /// Gets the objects for container
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="SwiftObjectsCollection"/> object</returns>
        public Task<SwiftObjectsCollection> GetObjects(Container container, CancellationToken cancellationToken)
        {
            GenericDataProvider request = new GenericDataProvider(this.credentials, HttpMethod.Get);

            request.Endpoint = container.Endpoint;

            request.QueryParams.Add("format", "json");

            RestClient<GenericDataProvider, SwiftObjectsCollectionParser> client = new RestClient<GenericDataProvider, SwiftObjectsCollectionParser>();

            var tsk = client.Execute(request, cancellationToken);

            return tsk.ContinueWith<SwiftObjectsCollection>(tskOk => {
                return tskOk.Result.Data;
            });
        }

        /// <summary>
        /// Uploads the object/file to SWFIT server
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="container">The container.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Awaitable task to wait for uploading to finish</returns>
        public Task UploadObject(string fileName, Container container, CancellationToken cancellationToken)
        {
            GenericDataProvider request = new GenericDataProvider(this.credentials, HttpMethod.Put);

            string objectUri = container.Endpoint.ToString() + "/" + Uri.EscapeDataString(FileUtils.NormalizeFileName(fileName));

            request.Endpoint = new Uri(objectUri);

            // request.QueryParams.Add("object", Uri.EscapeDataString(System.IO.Path.GetFileName(fileName)));

            // request.HeaderParams.Add("object", System.IO.Path.GetFileName(fileName));

            //request.HeaderParams.Add("ETag", FileUtils.GenerateMD5Hash(fileName));

            //request.HeaderParams.Add("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(fileName));

            //request.HeaderParams.Add("Content-Encoding", "BASE64");

            // Does not need to insert 'Content-Length'
            // request.HeaderParams.Add("Content-Length", new System.IO.FileInfo(fileName).Length.ToString());

            request.Content = FileUtils.FileContent(fileName);

            RestClient<GenericDataProvider, NullableParser> client = new RestClient<GenericDataProvider, NullableParser>();

            return client.Execute(request, cancellationToken);
        }
    
        //public Task<SwiftObject> DownloadObject(Container container, SwiftObject targetObject, CancellationToken cancellationToken)
        //{
        //    GenericDataProvider request = new GenericDataProvider(this.credentials, HttpMethod.Get);

        //    request.QueryParams.Add("container", container.Name);
        //    request.QueryParams.Add("object", targetObject.Name);

        //    string objectUri = container.Endpoint.ToString() + "/" + Uri.UnescapeDataString  targetObject.Name;

        //    request.Endpoint = container.Endpoint;

        //}
    }
}
