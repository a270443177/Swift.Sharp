namespace SwiftSharp.Core.XUnit
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading;

    public class KeystoneData
    {
        internal static Uri keystoneServer = new Uri("http://10.0.0.195:35357/v2.0/tokens");
        internal const string SWIFT_SERIVCE_NAME = "swift";
        internal static string keystoneUser = "alex";
        internal static string keystoneUserPassword = "123456";
        internal static string keystoneTenant = "test";

        internal KeystoneData()
        {
            //
            // Test that keystone server is exist and kicking
            HttpWebRequest request = WebRequest.Create(KeystoneData.keystoneServer) as HttpWebRequest;
            try
            {
                request.GetResponse();
            }
            catch (WebException exp_web)
            {
                if (exp_web.Status == WebExceptionStatus.Timeout)
                {
                    System.Diagnostics.Trace.WriteLine("[SwiftAccountDetailsXunit::ctor] Keystone server at address: " + KeystoneData.keystoneServer.ToString() + " is not responding");
                    throw exp_web;
                }
                else
                {
                    // any other response is good
                }
            }
        }

        internal static Tuple<Uri, string> GetKeystoneToken()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            Keystone.Core.KeystoneClient keystone = new Keystone.Core.KeystoneClient();
            var tsk = keystone.GetToken(keystoneServer, keystoneUser, keystoneUserPassword, keystoneTenant, tokenSource.Token);
            Keystone.Core.KeystoneResponse response = tsk.Result;

            System.Diagnostics.Trace.WriteLine("Swift endpoint: ");

            var zz = response.Access.ServiceCatalog.Where(c => c.Name.Equals(SWIFT_SERIVCE_NAME)).FirstOrDefault().EndpointsCollection.Where(e => !string.IsNullOrEmpty(e.PublicUrl)).Select(x => x);

            //zz.AsParallel().ForAll(e => System.Diagnostics.Trace.WriteLine(e.PublicUrl));

            //return response.Access.Token.Id;
            return new Tuple<Uri, string>(new Uri(zz.FirstOrDefault().PublicUrl), response.Access.Token.Id);
        }

    }
}
