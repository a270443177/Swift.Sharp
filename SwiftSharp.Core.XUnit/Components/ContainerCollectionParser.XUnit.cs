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
using SwiftSharp.Core.Rest;


namespace SwiftSharp.Core.XUnit.Components
{
    public class ContainerCollectionParserTester
    {
        ContainerCollectionParser containerCollectionParser;
        ContainerCollection containerCollection;


        public ContainerCollectionParserTester()
        {
            containerCollectionParser = new ContainerCollectionParser();
        }

        [Fact(DisplayName = "[ContainerCollectionParser] Should return null 'data' before parsing")]
        public void Should_return_null_on_init()
        {
            Assert.Null(containerCollectionParser.Data);
        }

        [Fact(DisplayName = "[ContainerCollectionParser] Should not throw on empty headers")]
        public void Should_not_thow_on_empty_headers()
        {
            IWebResponseDetails details = MockRepository.GenerateMock<IWebResponseDetails>(null);
            details.Expect(d => d.Headers).Return(new Dictionary<string, string>());
            details.Expect(d => d.Body).Return(" ");

            Assert.DoesNotThrow(() => {
                containerCollectionParser.BuildFromWebResponse(details);
            });

            Assert.NotNull(containerCollectionParser.Data.Names);
        }

        [Fact(DisplayName = "[ContainerCollectionParser] Should not throw on empty 'body'")]
        public void Should_not_throw_on_missing_body()
        {
            IWebResponseDetails details = MockRepository.GenerateMock<IWebResponseDetails>(null);
            details.Expect(d => d.Headers).Return(new Dictionary<string, string>());
            details.Expect(d => d.Body).Return(null);

            Assert.DoesNotThrow(() => {
                containerCollectionParser.BuildFromWebResponse(details);
            });
        }
    }
}
