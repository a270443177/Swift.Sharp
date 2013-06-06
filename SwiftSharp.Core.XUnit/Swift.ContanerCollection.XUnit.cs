using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using Xunit.Sdk;
using Xunit.Extensions;
using System.Threading;
using System.Net;
using SwiftSharp.Core;

using Rhino.Mocks;
using SwiftSharp;
using SwiftSharp.Core.Rest;


namespace SwiftSharp.Core.XUnit
{
    public class SwiftContainerCollectionXunit : KeystoneData
    {
        private CancellationTokenSource tokenSource;

        private Swift swiftclient;

        private Tuple<Uri, string> swiftConnectionData;

        public SwiftContainerCollectionXunit() : base()
        {
        }

        [Fact(DisplayName = "[ContainerCollection] Correctly bring ContainerCollection data")]
        public void Work_normally()
        {
            swiftConnectionData = KeystoneData.GetKeystoneToken();
            tokenSource = new CancellationTokenSource();

            swiftclient = new Swift(swiftConnectionData.Item1, swiftConnectionData.Item2, KeystoneData.keystoneTenant);

            ContainerCollection containerCollection = null;

            Assert.DoesNotThrow(() => {
                var tsk = swiftclient.GetContainers(tokenSource.Token);
                containerCollection = tsk.Result;
            });

            Assert.NotNull(containerCollection);

            System.Diagnostics.Trace.WriteLine("Found: " + containerCollection.Count.ToString() + " containers");
            containerCollection.AsParallel().ForAll(cont => System.Diagnostics.Trace.WriteLine("Container name: " + cont.Name + " Endpoint: " + cont.Endpoint.ToString()));
        }

        [Fact(DisplayName = "[ContainerCollection] Create new container and ensure that it listed")]
        public void Create_container_and_enshure_it()
        {
            swiftConnectionData = KeystoneData.GetKeystoneToken();
            tokenSource = new CancellationTokenSource();

            swiftclient = new Swift(swiftConnectionData.Item1, swiftConnectionData.Item2, KeystoneData.keystoneTenant);

            ContainerCollection containerCollection = null;
            string containerName = Guid.NewGuid().ToString();

            Assert.DoesNotThrow(() =>
            {
                var tsk = swiftclient.CreateContainer(containerName, tokenSource.Token);
                containerCollection = tsk.Result;
            });

            Assert.NotNull(containerCollection);
            Assert.True(containerCollection.Any(c => c.Name.Equals(containerName)));
        }

        [Fact(DisplayName = "[ContainerCollection] Delete existing container")]
        public void Delete_existing_container()
        {
            swiftConnectionData = KeystoneData.GetKeystoneToken();
            tokenSource = new CancellationTokenSource();

            swiftclient = new Swift(swiftConnectionData.Item1, swiftConnectionData.Item2, KeystoneData.keystoneTenant);

            ContainerCollection containerCollection = null;
            string containerName = Guid.NewGuid().ToString();

            //
            // Create container
            Assert.DoesNotThrow(() =>
            {
                var tsk = swiftclient.CreateContainer(containerName, tokenSource.Token);
                containerCollection = tsk.Result;
            });

            Assert.NotNull(containerCollection);
            Assert.True(containerCollection.Any(c => c.Name.Equals(containerName)));

            ContainerCollection containerCollection2 = null;
            Container container = containerCollection.First(c => c.Name.Equals(containerName));

            //
            // Delete this container
            Assert.DoesNotThrow(() => {
                var tsk1 = swiftclient.DeleteContainer(container, tokenSource.Token);
                containerCollection2 = tsk1.Result;
            });

            //
            // Is not exist in new list
            Assert.False(containerCollection2.Any(c => c.Name.Equals(containerName)));
        }

        [Fact(DisplayName = "[ContainerCollection] Delete container by name")]
        public void Delete_container_by_name()
        {
            swiftConnectionData = KeystoneData.GetKeystoneToken();
            tokenSource = new CancellationTokenSource();

            swiftclient = new Swift(swiftConnectionData.Item1, swiftConnectionData.Item2, KeystoneData.keystoneTenant);

            ContainerCollection containerCollection = null;
            string containerName = Guid.NewGuid().ToString();

            //
            // Create container
            Assert.DoesNotThrow(() =>
            {
                var tsk = swiftclient.CreateContainer(containerName, tokenSource.Token);
                containerCollection = tsk.Result;
            });

            Assert.NotNull(containerCollection);
            Assert.True(containerCollection.Any(c => c.Name.Equals(containerName)));

            ContainerCollection containerCollection2 = null;
            Container container = containerCollection.First(c => c.Name.Equals(containerName));

            Assert.DoesNotThrow(() => {
                var tsk1 = swiftclient.DeleteContainer(containerName, tokenSource.Token);
                containerCollection2 = tsk1.Result;
            });

            //
            // Is not exist in new list
            Assert.False(containerCollection2.Any(c => c.Name.Equals(containerName)));
        }

        [Fact(DisplayName = "[ContainerCollection] Should throw in case container with name does not exist")]
        public void Should_throw_on_container_that_does_not_exist()
        {
            byte[] bRndName = new byte[10];
            new Random().NextBytes(bRndName);

            string containerName = System.Text.Encoding.Default.GetString(bRndName);

            swiftConnectionData = KeystoneData.GetKeystoneToken();
            tokenSource = new CancellationTokenSource();

            swiftclient = new Swift(swiftConnectionData.Item1, swiftConnectionData.Item2, KeystoneData.keystoneTenant);

            ContainerCollection containerCollection = null;
            Assert.DoesNotThrow(() => {
                var tsk = swiftclient.GetContainers(tokenSource.Token);
                containerCollection = tsk.Result;
            });
            

            //
            // Check that it does not exist
            Assert.False(containerCollection.Any(c => c.Name.Equals(containerName)));

            //
            // Should throw
            Assert.Throws(typeof(ArgumentNullException), ()=>{
                var tsk2 = swiftclient.DeleteContainer(containerName, tokenSource.Token);
            });
        }
    }
}
