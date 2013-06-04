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

namespace SwiftSharp.Core.XUnit
{
    public class Provider_Xunit
    {
        private CancellationTokenSource tokenSource;

        private Swift swiftclient;

        private static Uri localhost = new Uri("http://127.0.0.1:8080/auth/v1.0");
//        private static Uri realServer = new Uri("http://10.0.0.197:8888/v1/AUTH_09748881ce8f479ba75477b7f5a46d7b"); ///cont123?tenant_id=test"); //?format=json&tenant_id=test");
        
        private static string realUsername = "alex";
        private static string realPassword = "123456";
        private static string realTenant = "test";

        public Provider_Xunit()
        {
            // Allow xUnit to trace/debug output to 'Output' window
            System.Diagnostics.Debug.Listeners.Add(new System.Diagnostics.DefaultTraceListener());


//            provider = new Provider();

            tokenSource = new CancellationTokenSource();
        }

        [Theory(DisplayName = "Test connection to SWIFT server")]
        [PropertyData("ConnectionParameters")]
        public void TestConnection(string token, Uri swiftEndpoint)
        {
            swiftclient = new Swift(swiftEndpoint, token, realTenant);

            if (swiftEndpoint != null)
            {
                Assert.DoesNotThrow(() =>
                {
                    var tsk = swiftclient.GetAccountDetails(tokenSource.Token);
                    AccountDetails accountDetails = tsk.Result;
                    System.Diagnostics.Trace.WriteLine("Result is: " + accountDetails.BytesUsed);
                });
            }
            else
            {
                Assert.Throws(typeof(System.AggregateException), () => {
                    //var tsk = provider.GetAccountData(swiftEndpoint, username, password, tokenSource.Token);
                    //var rawData = tsk.Result;

                    //System.Diagnostics.Trace.WriteLine("Result is: " + rawData);
                });
            }
            
        }

        public static IEnumerable<Object[]> ConnectionParameters
        {
            get
            {
                List<Object[]> testData = new List<object[]>();

                //testData.Add(new object[] {string.Empty, string.Empty, null });

                //testData.Add(new object[] { "vasya", "vasya123", localhost });

                //testData.Add(new object[] { "vasya", "vasya123", realServer });

                //testData.Add(new object[] { realUsername, realPassword, localhost });

                Tuple<Uri, string> keystoneData = GetKeystoneToken();

                testData.Add(new object[] { realUsername, keystoneData.Item2, keystoneData.Item1 });

                return testData;
            }
        }


        private static Uri keystoneServer = new Uri("http://10.0.0.195:35357/v2.0/tokens");

        private static Tuple<Uri,string> GetKeystoneToken()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            Keystone.Core.Keystone keystone = new Keystone.Core.Keystone();
            var tsk = keystone.GetToken(keystoneServer, realUsername, realPassword, realTenant, tokenSource.Token);
            Keystone.Core.KeystoneResponse response = tsk.Result;

            System.Diagnostics.Trace.WriteLine("Swift endpoint: " );

            var zz = response.Access.ServiceCatalog.Where(c => c.Name.Equals("swift")).FirstOrDefault().EndpointsColl.Where(e => !string.IsNullOrEmpty(e.PublicUrl)).Select(x => x);

            //zz.AsParallel().ForAll(e => System.Diagnostics.Trace.WriteLine(e.PublicUrl));

            //return response.Access.Token.Id;
            return new Tuple<Uri, string>(new Uri(zz.FirstOrDefault().PublicUrl), response.Access.Token.Id);
        }
    }
}
