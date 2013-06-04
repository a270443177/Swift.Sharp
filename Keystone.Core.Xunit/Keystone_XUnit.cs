using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using Xunit.Sdk;
using Xunit.Extensions;
using System.Threading;

using System.IO;
using System.Runtime.Serialization.Json;
using Keystone.Core;

namespace Keystone.Core.Xunit
{
    public class Keystone_XUnit
    {
        private KeystoneClient keystone;

        private static Uri localhost = new Uri("http://localhost");
        private static Uri wrongHost = new Uri("http://10.0.0.197");
        private static Uri realHost = new Uri("http://10.0.0.195:35357/v2.0/tokens");
        private static string realUsername = "alex";
        private static string realPassword = "123456";
        private static string realTenant = "test";

        /// <summary>
        /// Initializes a new instance of the <see cref="Keystone_XUnit"/> class.
        /// xUnit test start-up code
        /// </summary>
        public Keystone_XUnit()
        {
            keystone = new KeystoneClient();

            // Allow xUnit to trace/debug output to 'Output' window
            System.Diagnostics.Debug.Listeners.Add(new System.Diagnostics.DefaultTraceListener());
        }

        [Fact(DisplayName="Test keystone de-serialization process based on file")]
        public void TestDesirialization()
        {
            StreamReader reader = File.OpenText("rawData.txt");
            string rawData = reader.ReadToEnd();

            DataContractJsonSerializer deSerializer = new DataContractJsonSerializer(typeof(KeystoneResponse));
            try
            {
                MemoryStream mStream = new MemoryStream(Encoding.Unicode.GetBytes(rawData));
                KeystoneResponse response = deSerializer.ReadObject(mStream) as KeystoneResponse;
                System.Diagnostics.Trace.WriteLine("here");
            }
            catch (Exception exp_gen)
            {
                System.Diagnostics.Trace.WriteLine("Exception: " + exp_gen.ToString());

            }
        }


        [Theory(DisplayName="Test connection to live server")]
        [PropertyData("ConnectionData")]
        public void ConnectToLocalCloud(Uri keystoneServer, string username, string password, string tenantName)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            System.Diagnostics.Trace.WriteLine("[Test started] !!!");

            Assert.DoesNotThrow(() =>
            {
                var tsk = keystone.GetToken(keystoneServer, username, password, tenantName, tokenSource.Token);
                KeystoneResponse response = tsk.Result;

                response.Access.ServiceCatalog.AsParallel().ForAll(x => System.Diagnostics.Trace.WriteLine("Endpoint name: " + x.Name));
            });
        }


        public static IEnumerable<object[]> ConnectionData
        {
            get
            {
                List<object[]> connData = new List<object[]>();

                //connData.Add(new object[] { realHost, realUsername, realPassword, realTenant });
                connData.Add(new object[] { realHost, realUsername, realPassword, string.Empty });

                return connData;
            }
        }
    }
}
