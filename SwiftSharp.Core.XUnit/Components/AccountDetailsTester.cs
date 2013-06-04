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
    public class AccountDetailsTester
    {
        AccountDetailsParser accountDetailsParser;
        AccountDetails accountDetails;

        public AccountDetailsTester()
        {
            accountDetailsParser = new AccountDetailsParser();
        }

        [Fact(DisplayName = "AccountDetailsParser won't work with empty stream")]
        public void Should_throw_on_empty_response()
        {
            Assert.Throws(typeof(ArgumentException), () => {
                accountDetailsParser.BuildFromWebResponse(null);
            });
        }

        [Fact(DisplayName = "AccountDetailsParser should not fail on empty headers")]
        public void Should_not_throw_on_missing_headers()
        {
            IWebResponseDetails details = MockRepository.GenerateMock<IWebResponseDetails>(null);
            details.Expect(d => d.Headers).Return(new Dictionary<string,string>());

            Assert.DoesNotThrow(() =>
            {
                accountDetailsParser.BuildFromWebResponse(details);
            });

            Assert.NotNull(accountDetailsParser.Data);

            accountDetails = accountDetailsParser.Data;

            Assert.Equal(accountDetails.BytesUsed, 0);
            Assert.Equal(accountDetails.ContainerCount, 0);
            Assert.Equal(accountDetails.ObjectsCount, 0);
        }

        [Fact(DisplayName = "AccountDetailsParser should throw on incorrect headers")]
        public void Should_throw_on_incorrect_headers()
        {
            Dictionary<string, string> wrongHeaders = new Dictionary<string, string>();
            wrongHeaders.Add(AccountDetailsParser.HEADER_BYTES_USED, "wrong_header_value");

            IWebResponseDetails details = MockRepository.GenerateMock<IWebResponseDetails>(null);
            details.Expect(d => d.Headers).Return(wrongHeaders);

            Assert.Throws(typeof(FormatException),()=>{
                accountDetailsParser.BuildFromWebResponse(details);
            });

            Assert.NotNull(accountDetailsParser.Data);

            accountDetails = accountDetailsParser.Data;

            Assert.Equal(accountDetails.BytesUsed, 0);
        }

        [Fact(DisplayName = "AccountDetailsParser should return null data before parse")]
        public void Parser_before_build_should_return_null_data()
        {
            Assert.Null(accountDetailsParser.Data);
        }
    }
}
