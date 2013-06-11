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
            swiftObjectCollection.AsParallel().ForAll(obj => System.Diagnostics.Trace.WriteLine("Swift object name: " + obj.Name + " Hash: " + obj.MD5Hash + " length: " + obj.Length + " Endpoint: " + obj.Endpoint.ToString()));
        }

        [Theory(DisplayName = "[Objects] Correctly upload object")]
        [PropertyData("RandomFileAtAllContainers")]        //[PropertyData("FixedFileName")]
        public void Upload_object(string fileName, Container container)
        {
            swiftConnectionData = KeystoneData.GetKeystoneToken();
            tokenSource = new CancellationTokenSource();

            swiftclient = new Swift(swiftConnectionData.Item1, swiftConnectionData.Item2, KeystoneData.keystoneTenant);

            System.Diagnostics.Trace.WriteLine("File: " + fileName + " gonna be uploaded to container: " + container.Name);

            Assert.DoesNotThrow(() => {
                var tsk2 = swiftclient.UploadObject(fileName, container, tokenSource.Token);
                tsk2.Wait();
            });
        }

        [Fact(DisplayName = "[Objects] Delete all objects for all containers")]
        public void Delete_all_objects_for_all_containers()
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

            List<Tuple<Container, SwiftObjectsCollection>> listOfEverything = new List<Tuple<Container,SwiftObjectsCollection>>();

            containerCollection.ForEach(container => {

                Assert.DoesNotThrow(() => {
                    var tsk2 = swiftclient.GetObjects(container, tokenSource.Token);
                    listOfEverything.Add(new Tuple<Container, SwiftObjectsCollection>(container, tsk2.Result));
                });
            });

            Assert.True(listOfEverything.Count > 0);

            foreach (Tuple<Container, SwiftObjectsCollection> record in listOfEverything)
            {
                SwiftObjectsCollection originalCollection = record.Item2;
                SwiftObjectsCollection generatedCollection = null;

                originalCollection.ForEach(obj => {
                    
                    Assert.DoesNotThrow(()=>{

                        System.Diagnostics.Trace.WriteLine("[Objects] Going to delete object: " + obj.Name + " from container: " + record.Item1.Name);

                        var tsk3 = swiftclient.DeleteObject(record.Item1, obj, tokenSource.Token);
                        generatedCollection = tsk3.Result;
                    });

                    Assert.NotNull(generatedCollection);

                    Assert.False(originalCollection.Intersect(generatedCollection).Count() == originalCollection.Count);
                });
               
            }
        }

        [Fact(DisplayName = "[Objects] Delete unknown object from container should throw")]
        public void Delete_unknown_object_from_container_should_throw()
        {
            swiftConnectionData = KeystoneData.GetKeystoneToken();
            tokenSource = new CancellationTokenSource();

            swiftclient = new Swift(swiftConnectionData.Item1, swiftConnectionData.Item2, KeystoneData.keystoneTenant);

            ContainerCollection containerCollection = null;

            Assert.DoesNotThrow(() =>
            {
                var tsk = swiftclient.GetContainers(tokenSource.Token);
                containerCollection = tsk.Result;
            });

            Assert.NotNull(containerCollection);


            Container container = containerCollection.First();
            SwiftObject badObject = new SwiftObject() { Name = "##I_am_super_bad##"};

            Assert.Throws(typeof(AggregateException), () => {
                var tsk =  swiftclient.DeleteObject(container, badObject, tokenSource.Token);
                var coll = tsk.Result;
                Assert.NotNull(coll);
            });
        }

        [Fact(DisplayName = "[Objects] Delete unknown container should fail")]
        public void Delete_unknown_container_should_fail()
        {
            swiftConnectionData = KeystoneData.GetKeystoneToken();
            tokenSource = new CancellationTokenSource();

            swiftclient = new Swift(swiftConnectionData.Item1, swiftConnectionData.Item2, KeystoneData.keystoneTenant);

            ContainerCollection containersCollection = null;

            Assert.DoesNotThrow(()=>{
                var tsk = swiftclient.GetContainers(tokenSource.Token);
                containersCollection = tsk.Result;
            });

            Assert.NotNull(containersCollection);
            string validUri = containersCollection.First().Endpoint.ToString();
            validUri = validUri.Substring(0, validUri.LastIndexOf("/"));

            Container badContainer = new Container(){
                Endpoint = new Uri(validUri + "/" + "##I_am_super_bad##")
            };


            Assert.Throws(typeof(AggregateException), () => {
                var tsk = swiftclient.DeleteContainer(badContainer, tokenSource.Token);
                var coll = tsk.Result;
                Assert.Null(coll);
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

        public static IEnumerable<object[]> RandomFileAtAllContainers
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


                string randomFileName = Path.Combine(Environment.CurrentDirectory, Path.GetRandomFileName());

                FillFileContent(randomFileName);

                string randomFileWithSpace = Path.GetRandomFileName();
                randomFileWithSpace = randomFileWithSpace.Insert(2, " ");   //insert space
                randomFileWithSpace = Path.Combine(Environment.CurrentDirectory, randomFileWithSpace);

                FillFileContent(randomFileWithSpace);

                List<string> fileNameColl = new List<string>();

                fileNameColl.Add(randomFileName);
                fileNameColl.Add(randomFileWithSpace);

                return from container in containerCollection
                       from fileName in fileNameColl
                       select new object[] { fileName, container };
            }
        }

        private static void FillFileContent(string fileName)
        {
            Random rnd = new Random(System.DateTime.Now.Millisecond);

            byte[] fileRandomContent = new byte[rnd.Next(1, 20000)];
            rnd.NextBytes(fileRandomContent);

            using (FileStream fStream = File.Create(fileName))
            {
                using (BinaryWriter writer = new BinaryWriter(fStream))
                {
                    writer.Write(fileRandomContent);
                    writer.Flush();
                }
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
