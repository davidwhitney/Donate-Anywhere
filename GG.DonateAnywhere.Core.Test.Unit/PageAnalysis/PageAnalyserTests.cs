using System;
using System.IO;
using GG.DonateAnywhere.Core.PageAnalysis;
using GG.DonateAnywhere.Core.Sanitise;
using NUnit.Framework;

namespace GG.DonateAnywhere.Core.Test.Unit.PageAnalysis
{
    [TestFixture]
    public class PageAnalyserTests
    {
        [Test]
        public void Analyse_HtmlElementNamesOutnumberPlainText_FrequentHtmlElementsNotPresentInTheResultingRanking()
        {
            var mockRankingStrategy = new KeyworkRankingStrategyMock();
            var transportMock = new HttpRequestTransportMock(File.ReadAllText("TestData/AscendingNumbersInHtml.txt"));
            var pageAnalyer = new PageAnalyser(transportMock, mockRankingStrategy, new ContentCleaner());

            pageAnalyer.Analyse(new Uri("http://some.com/url"));

            Assert.IsFalse(mockRankingStrategy.LastRankingRequestPlainText.Contains(" br "));
        }

        [Test]
        public void Analyse_DocumentContainsJavascript_JavascriptDataNotPresentInRanking()
        {
            var mockRankingStrategy = new KeyworkRankingStrategyMock();
            var transportMock = new HttpRequestTransportMock(File.ReadAllText("TestData/AscendingNumbersInHtml.txt"));
            var pageAnalyer = new PageAnalyser(transportMock, mockRankingStrategy, new ContentCleaner());

            pageAnalyer.Analyse(new Uri("http://some.com/url"));

            Assert.IsFalse(mockRankingStrategy.LastRankingRequestPlainText.Contains("TestableJavascriptVariableString"));
        }

        [Test]
        public void Analyse_DocumentContainsStyleSheet_StyleSheetDataNotPresentInRanking()
        {
            var mockRankingStrategy = new KeyworkRankingStrategyMock();
            var transportMock = new HttpRequestTransportMock(File.ReadAllText("TestData/AscendingNumbersInHtml.txt"));
            var pageAnalyer = new PageAnalyser(transportMock, mockRankingStrategy, new ContentCleaner());

            pageAnalyer.Analyse(new Uri("http://some.com/url"));

            Assert.IsFalse(mockRankingStrategy.LastRankingRequestPlainText.Contains("testableCssClassName"));
        }
    }
}
