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
            Assert.NotNull(containerCollection.Names);
        }

        [Fact(DisplayName = "[ContainerCollectionParser] Create new container and ensure that it listed")]
        public void Create_container_and_enshure_it()
        {
            swiftConnectionData = KeystoneData.GetKeystoneToken();
            tokenSource = new CancellationTokenSource();

            swiftclient = new Swift(swiftConnectionData.Item1, swiftConnectionData.Item2, KeystoneData.keystoneTenant);

            ContainerCollection containerCollection = null;
            string containerName = (new Guid()).ToString();

            Assert.DoesNotThrow(() =>
            {
                var tsk = swiftclient.CreateContainer(containerName, tokenSource.Token);
                containerCollection = tsk.Result;
            });

            Assert.NotNull(containerCollection);
            Assert.NotNull(containerCollection.Names);
            Assert.True(containerCollection.Names.Contains(containerName));
        }
    }
}
