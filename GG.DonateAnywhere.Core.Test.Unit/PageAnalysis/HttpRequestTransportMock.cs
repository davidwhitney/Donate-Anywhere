using System;
using GG.DonateAnywhere.Core.Http;

namespace GG.DonateAnywhere.Core.Test.Unit.PageAnalysis
{
    public class HttpRequestTransportMock : IDirectHttpRequestTransport
    {
        private readonly string _responseContents;

        public HttpRequestTransportMock(string responseContents)
        {
            _responseContents = responseContents;
        }

        public string FetchUri(Uri uri)
        {
            return _responseContents;
        }
    }
}