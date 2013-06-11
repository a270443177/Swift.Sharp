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
using System.Reflection;
using System.IO;

namespace SwiftSharp.Core.XUnit
{
    public class SwiftObjectsXUnit : KeystoneData
    {
        private CancellationTokenSource tokenSource;

        private Swift swiftclient;

        private Tuple<Uri, string> swiftConnectionData;

        public SwiftObjectsXUnit() : base ()
        {
        }

        [Theory(DisplayName = "[Objects] Bring all objects for container")]
        [PropertyData("GetAllContainers")]
        public void Bring_all_objects(Container targetContainer)
        {
            swiftConnectionData = KeystoneData.GetKeystoneToken();
            tokenSource = new CancellationTokenSource();

            swiftclient = new Swift(swiftConnectionData.Item1, swiftConnectionData.Item2, KeystoneData.keystoneTenant);

            SwiftObjectsCollection swiftObjectCollection = null;

            Assert.DoesNotThrow(() => {
                var tsk = swiftclient.GetObjects(targetContainer, tokenSource.Token);
                swiftObjectCollection = tsk.Result;
            });

            //
            // Even if they are not objects the 'data' could not be null
            Assert.NotNull(swiftObjectCollection);

            System.Diagnostics.Trace.WriteLine("Container: " + targetContainer.Name + " has " + swiftObjectCollection.Count.ToString() + " objects inside it");
            swiftObjectCollection.AsParallel().ForAll(obj => System.Diagnostics.Trace.WriteLine("Swift object name: " + obj.Name + " Hash: " + obj.MD5Hash + " length: " + obj.Length));
        }

        [Theory(DisplayName = "[Objects] Correctly upload object")]
        [PropertyData("RandomFile")]        //[PropertyData("FixedFileName")]
        public void Upload_object(string fileName)
        {
            swiftConnectionData = KeystoneData.GetKeystoneToken();
            tokenSource = new CancellationTokenSource();

            swiftclient = new Swift(swiftConnectionData.Item1, swiftConnectionData.Item2, KeystoneData.keystoneTenant);

            Container container = null;

            Assert.DoesNotThrow(() => {
                container = GetFirstContainer.First()[0] as Container;
            });

            System.Diagnostics.Trace.WriteLine("File: " + fileName + " gonna be uploaded to container: " + container.Name);

            Assert.DoesNotThrow(() => {
                var tsk2 = swiftclient.UploadObject(fileName, container, tokenSource.Token);
                tsk2.Wait();
            });
        }

        public static IEnumerable<object[]> FixedFileName
        {
            get
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                using (Stream fileStream = asm.GetManifestResourceStream("SwiftSharp.Core.XUnit.FixedFile.txt"))
                {
                    StreamReader reader = new StreamReader(fileStream);
                    using (StreamWriter writer = File.CreateText(Path.Combine(Environment.CurrentDirectory, "FixedFile.txt")))
                    {
                        writer.Write(reader.ReadToEnd());
                        writer.Flush();

                        List<object[]> fileName = new List<object[]>();

                        fileName.Add(new object[] { Path.Combine(Environment.CurrentDirectory, "FixedFile.txt") });

                        return fileName;
                    }
                }
            }
        }
    
        public static IEnumerable<object[]> RandomFile
        {
            get
            {
                Random rnd = new Random(System.DateTime.Now.Millisecond);

                string randomFileName = Path.Combine(Environment.CurrentDirectory, Path.GetRandomFileName());
                
                byte [] fileRandomContent = new byte[rnd.Next(1, 20000)];
                rnd.NextBytes(fileRandomContent);

                using (FileStream fStream = File.Create(randomFileName))
                {
                    using (BinaryWriter writer = new BinaryWriter(fStream))
                    {
                        writer.Write(fileRandomContent);
                        writer.Flush();
                    }
                }

                List<object[]> fileNames = new List<object[]>();

                fileNames.Add(new object[] { randomFileName });

                return fileNames;
            }
        }

        public static IEnumerable<object[]> GetFirstContainer
        {
            get
            {
                Tuple<Uri, string>  swiftConnectionData = KeystoneData.GetKeystoneToken();
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                Swift swiftclient = new Swift(swiftConnectionData.Item1, swiftConnectionData.Item2, KeystoneData.keystoneTenant);

                ContainerCollection containerCollection = null;

                Assert.DoesNotThrow(() =>
                {
                    var tsk = swiftclient.GetContainers(tokenSource.Token);
                    containerCollection = tsk.Result;
                });

                //
                // Not empty
                Assert.True(containerCollection.Any());

                List<object[]> firstContainer = new List<object[]>();

                firstContainer.Add(new object[] { containerCollection.First() });

                return firstContainer;
            }
        }

        public static IEnumerable<object[]> GetAllContainers
        {
            get
            {
                Tuple<Uri, string> swiftConnectionData = KeystoneData.GetKeystoneToken();
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                Swift swiftclient = new Swift(swiftConnectionData.Item1, swiftConnectionData.Item2, KeystoneData.keystoneTenant);

                ContainerCollection containerCollection = null;

                Assert.DoesNotThrow(() =>
                {
                    var tsk = swiftclient.GetContainers(tokenSource.Token);
                    containerCollection = tsk.Result;
                });

                //
                // Not empty
                Assert.True(containerCollection.Any());

                return from container in containerCollection
                         select new object[] { container };
            }
        }
    }
}
