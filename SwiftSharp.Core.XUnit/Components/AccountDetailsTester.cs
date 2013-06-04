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
        public void EmptyResponse()
        {
            Assert.Throws(typeof(ArgumentException), () => {
                accountDetailsParser.BuildFromWebResponse(null);
            });
        }

        [Fact(DisplayName = "AccountDetailsParser should not fail on empty headers")]
        public void DoesNotHaveHeaders()
        {
            IWebResponseDetails details = MockRepository.GenerateMock<IWebResponseDetails>(null);
            details.Expect(d => d.Headers).Return(null);

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
    }
}
