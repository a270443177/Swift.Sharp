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
    public class SwiftAccountDetailsXunit : KeystoneData
    {
        private CancellationTokenSource tokenSource;

        private Swift swiftclient;

        private Tuple<Uri, string> swiftConnectionData;

        public SwiftAccountDetailsXunit() : base ()
        {
        }

        [Fact(DisplayName = "[AccountDetails] Swift should fail on wrong endpoint")]
        public void Should_throw_on_wrong_endpoint()
        {
            tokenSource = new CancellationTokenSource();
            swiftConnectionData = GetKeystoneToken();

            //Uri endpoint, string token, string tenant
            Uri wrongUri = new Uri("http://1.1.1.1");
            swiftclient = new Swift(wrongUri, swiftConnectionData.Item2, KeystoneData.keystoneTenant);

            Assert.Throws(typeof(AggregateException), () => {
                var tsk = swiftclient.GetAccountDetails(tokenSource.Token);
                tsk.Wait();
            });
        }

        [Fact(DisplayName = "[AccountDetails] Swift should fail on wrong token")]
        public void Should_fail_on_wrong_token()
        {
            tokenSource = new CancellationTokenSource();
            swiftConnectionData = KeystoneData.GetKeystoneToken();

            //Uri endpoint, string token, string tenant
            swiftclient = new Swift(swiftConnectionData.Item1, "$$wrong_token", KeystoneData.keystoneTenant);

            Assert.Throws(typeof(AggregateException), () =>
            {
                var tsk = swiftclient.GetAccountDetails(tokenSource.Token);
                tsk.Wait();
            });

            try
            {
                var tsk = swiftclient.GetAccountDetails(tokenSource.Token);
                tsk.Wait();
            }
            catch (AggregateException exp_agr)
            {
                AggregateException exp2 = exp_agr.Flatten();

                //
                // Find System.UnauthorizedAccessException in inner exceptions
                bool found = false;

                foreach (var exp in exp2.InnerExceptions)
                {
                    if (exp.GetType().Equals(typeof(System.UnauthorizedAccessException)))
                    {
                        found = true;
                    }
                }

                Assert.True(found);
            }
        }

        [Fact(DisplayName = "[AccountDetails] Correctly bring AccountDetails object")]
        public void Work_normally()
        {
            swiftConnectionData = KeystoneData.GetKeystoneToken();
            tokenSource = new CancellationTokenSource();

            swiftclient = new Swift(swiftConnectionData.Item1, swiftConnectionData.Item2, KeystoneData.keystoneTenant);

            AccountDetails accountDetails = null;

            Assert.DoesNotThrow(() => {
                var tsk = swiftclient.GetAccountDetails(tokenSource.Token);
                accountDetails = tsk.Result;
                Assert.NotNull(accountDetails);
            });
        }
    }
}
