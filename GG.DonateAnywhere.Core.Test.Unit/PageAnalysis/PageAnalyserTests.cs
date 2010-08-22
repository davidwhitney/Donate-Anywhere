using System;
using System.IO;
using GG.DonateAnywhere.Core.PageAnalysis;
using NUnit.Framework;

namespace GG.DonateAnywhere.Core.Test.Unit.PageAnalysis
{
    [TestFixture]
    public class PageAnalyserTests
    {
        [Test]
        public void Analyse_HtmlElementNamesOutnumberPlainText_FrequentHtmlElementsNotPresentInTheResultingRanking()
        {
            var transportMock = new HttpRequestTransportMock(File.ReadAllText("TestData/AscendingNumbersInHtml.txt"));
            var pageAnalyer = new PageAnalyser(transportMock, new SimpleKeywordRankingTextAnalyser());

            var report = pageAnalyer.Analyse(new Uri("http://some.com/url"));

            Assert.IsFalse(report.KeywordDensity.ContainsKey("br"));
        }

        [Test]
        public void Analyse_DocumentContainsJavascript_JavascriptDataNotPresentInRanking()
        {
            var transportMock = new HttpRequestTransportMock(File.ReadAllText("TestData/AscendingNumbersInHtml.txt"));
            var pageAnalyer = new PageAnalyser(transportMock, new SimpleKeywordRankingTextAnalyser());

            var report = pageAnalyer.Analyse(new Uri("http://some.com/url"));

            Assert.IsFalse(report.KeywordDensity.ContainsKey("TestableJavascriptVariableString"));
        }

        [Test]
        public void Analyse_DocumentContainsStyleSheet_StyleSheetDataNotPresentInRanking()
        {
            var transportMock = new HttpRequestTransportMock(File.ReadAllText("TestData/AscendingNumbersInHtml.txt"));
            var pageAnalyer = new PageAnalyser(transportMock, new SimpleKeywordRankingTextAnalyser());

            var report = pageAnalyer.Analyse(new Uri("http://some.com/url"));

            Assert.IsFalse(report.KeywordDensity.ContainsKey("testableCssClassName"));
        }

        [Test]
        public void Analyse_DocumentContainsNumbersOneToElevenInAscendingOrderAsTextMultipliedByTheirValue_TopRankedKeywordIsEleven()
        {
            var transportMock = new HttpRequestTransportMock(File.ReadAllText("TestData/AscendingNumbersInHtml.txt"));
            var pageAnalyer = new PageAnalyser(transportMock, new SimpleKeywordRankingTextAnalyser());

            var report = pageAnalyer.Analyse(new Uri("http://some.com/url"));
            
            var enumer = report.KeywordDensity.GetEnumerator();
            enumer.MoveNext();
            Assert.AreEqual("eleven", enumer.Current.Key);
        }

        [Test]
        public void Analyse_DocumentContainsNumbersOneToElevenInAscendingOrderAsTextMultipliedByTheirValue_WordsAreRankedInPredictedOrder()
        {
            var transportMock = new HttpRequestTransportMock(File.ReadAllText("TestData/AscendingNumbersInHtml.txt"));
            var pageAnalyer = new PageAnalyser(transportMock, new SimpleKeywordRankingTextAnalyser());

            var report = pageAnalyer.Analyse(new Uri("http://some.com/url"));
            
            var enumer = report.KeywordDensity.GetEnumerator();
            enumer.MoveNext();
            
            Assert.AreEqual("eleven", enumer.Current.Key); enumer.MoveNext();
            Assert.AreEqual("ten", enumer.Current.Key); enumer.MoveNext();
            Assert.AreEqual("nine", enumer.Current.Key); enumer.MoveNext();
            Assert.AreEqual("eight", enumer.Current.Key); enumer.MoveNext();
            Assert.AreEqual("seven", enumer.Current.Key); enumer.MoveNext();
            Assert.AreEqual("six", enumer.Current.Key); enumer.MoveNext();
            Assert.AreEqual("five", enumer.Current.Key); enumer.MoveNext();
            Assert.AreEqual("four", enumer.Current.Key); enumer.MoveNext();
            Assert.AreEqual("three", enumer.Current.Key); enumer.MoveNext();
            Assert.AreEqual("two", enumer.Current.Key);

            // One could be in any order with the other words that only occur one time.
        }

        [Test]
        public void Analyse_WhenDocumentContainsWordPresentInBlackList_BlackListedWordNotPresentInRanking()
        {
            var transportMock = new HttpRequestTransportMock(File.ReadAllText("TestData/AscendingNumbersInHtml.txt"));
            var excludedWordsRepositoryMock = new ExcludedWordsRepositoryMock {"eleven"};
            var keywordAnayser = new SimpleKeywordRankingTextAnalyser(excludedWordsRepositoryMock);
            var pageAnalyer = new PageAnalyser(transportMock, keywordAnayser);

            var report = pageAnalyer.Analyse(new Uri("http://some.com/url"));

            var enumer = report.KeywordDensity.GetEnumerator();
            enumer.MoveNext();

            Assert.AreEqual("ten", enumer.Current.Key); enumer.MoveNext();
        }

    }
}
