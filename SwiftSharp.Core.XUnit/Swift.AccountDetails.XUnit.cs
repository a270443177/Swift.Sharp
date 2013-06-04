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
    public class SwiftAccountDetailsXunit
    {
        private CancellationTokenSource tokenSource;

        private Swift swiftclient;

        private static Uri keystoneServer = new Uri("http://10.0.0.195:35357/v2.0/tokens");
        private static string keystoneUser = "alex";
        private static string keystoneUserPassword = "123456";
        private static string keystoneTenant = "test";
        private Tuple<Uri, string> swiftConnectionData;

        private static Tuple<Uri, string> GetKeystoneToken()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            Keystone.Core.Keystone keystone = new Keystone.Core.Keystone();
            var tsk = keystone.GetToken(keystoneServer, keystoneUser, keystoneUserPassword, keystoneTenant, tokenSource.Token);
            Keystone.Core.KeystoneResponse response = tsk.Result;

            System.Diagnostics.Trace.WriteLine("Swift endpoint: ");

            var zz = response.Access.ServiceCatalog.Where(c => c.Name.Equals("swift")).FirstOrDefault().EndpointsColl.Where(e => !string.IsNullOrEmpty(e.PublicUrl)).Select(x => x);

            //zz.AsParallel().ForAll(e => System.Diagnostics.Trace.WriteLine(e.PublicUrl));

            //return response.Access.Token.Id;
            return new Tuple<Uri, string>(new Uri(zz.FirstOrDefault().PublicUrl), response.Access.Token.Id);
        }

        public SwiftAccountDetailsXunit()
        {
            //
            // Test that keystone server is exist and kicking
            HttpWebRequest request = WebRequest.Create(keystoneServer) as HttpWebRequest;
            try
            {
                request.GetResponse();
            }
            catch (WebException exp_web)
            {
                if (exp_web.Status == WebExceptionStatus.Timeout)
                {
                    System.Diagnostics.Trace.WriteLine("[SwiftAccountDetailsXunit::ctor] Keystone server at address: " + keystoneServer.ToString() + " is not responding");
                    throw exp_web;
                }
                else
                {
                    // any other response is good
                }
            }
        }

        [Fact(DisplayName = "Swift should fail on wrong endpoint")]
        public void Should_throw_on_wrong_endpoint()
        {
            tokenSource = new CancellationTokenSource();
            swiftConnectionData = GetKeystoneToken();

            //Uri endpoint, string token, string tenant
            Uri wrongUri = new Uri("http://1.1.1.1");
            swiftclient = new Swift(wrongUri, swiftConnectionData.Item2, keystoneTenant);

            Assert.Throws(typeof(AggregateException), () => {
                var tsk = swiftclient.GetAccountDetails(tokenSource.Token);
                tsk.Wait();
            });
        }

        [Fact(DisplayName = "Swift should fail on wrong token")]
        public void Should_fail_on_wrong_token()
        {
            tokenSource = new CancellationTokenSource();
            swiftConnectionData = GetKeystoneToken();

            //Uri endpoint, string token, string tenant
            swiftclient = new Swift(swiftConnectionData.Item1, "$$wrong_token", keystoneTenant);

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

        [Fact(DisplayName = "Correctly bring AccountDetails object")]
        public void Work_normally()
        {
            swiftConnectionData = GetKeystoneToken();
            tokenSource = new CancellationTokenSource();

            swiftclient = new Swift(swiftConnectionData.Item1, swiftConnectionData.Item2, keystoneTenant);

            AccountDetails accountDetails = null;

            Assert.DoesNotThrow(() => {
                var tsk = swiftclient.GetAccountDetails(tokenSource.Token);
                accountDetails = tsk.Result;
                Assert.NotNull(accountDetails);
            });
        }
    }
}
