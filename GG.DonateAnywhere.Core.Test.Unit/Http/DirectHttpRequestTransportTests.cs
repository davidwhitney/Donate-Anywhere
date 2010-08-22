using System;
using GG.DonateAnywhere.Core.Http;
using NUnit.Framework;

namespace GG.DonateAnywhere.Core.Test.Unit.Http
{
    [TestFixture]
    public class DirectHttpRequestTransportTests
    {
        [Test]
        [Ignore("This test sucks, I'm not sure how to test this.")]
        public void FetchUri_WhenSuppliedWithGoogle_CanGetGoogle()
        {
            var uri = new Uri("http://www.google.com");
            var http = new DirectHttpRequestTransport();

            var document = http.FetchUri(uri);

            Assert.That(document, Is.StringContaining("Google"));
        }
    }
}
