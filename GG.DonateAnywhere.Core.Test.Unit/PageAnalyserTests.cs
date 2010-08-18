using System;
using GG.DonateAnywhere.Core.Http;
using GG.DonateAnywhere.Core.PageAnalysis;
using NUnit.Framework;

namespace GG.DonateAnywhere.Core.Test.Unit
{
    [TestFixture]
    public class PageAnalyserTests
    {
        [Test]
        public void Something()
        {
            var pageAn = new PageAnalyser(new DirectHttpRequestTransport(), new SimpleKeywordRankingTextAnalyser());

            var report = pageAn.Analyse(new Uri("http://www.guardian.co.uk/environment/2010/jul/28/global-temperatures-2010-record"));
        }
    }
}
