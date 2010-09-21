using System;
using GG.DonateAnywhere.Core.PageAnalysis;
using GG.DonateAnywhere.Core.Sanitise;
using NUnit.Framework;

namespace GG.DonateAnywhere.Core.Test.Unit.PageAnalysis
{
    [TestFixture]
    public class SimpleKeywordRankingTextAnalyserTests
    {
        const string OneToFour = "one two two three three three four four four four";

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void RankKeywords_NullEmptyOrWhitespaceSourceDataProvided_ReturnsEmptyRankingCollection(string sourceData)
        {
            var keywordRankingTextAnalyser = new SimpleKeywordRankingStrategy();
            var keywordRanking = keywordRankingTextAnalyser.RankKeywords(sourceData);

            Assert.AreEqual(0, keywordRanking.Count);
        }

        [Test]
        public void RankKeywords_OneToFourCountProvided_RankedInOrder()
        {
            var keywordRankingTextAnalyser = new SimpleKeywordRankingStrategy();

            var keywordRanking = keywordRankingTextAnalyser.RankKeywords(OneToFour);

            var enumer = keywordRanking.GetEnumerator(); enumer.MoveNext();
            Assert.AreEqual("four", enumer.Current.Key); enumer.MoveNext();
            Assert.AreEqual("three", enumer.Current.Key); enumer.MoveNext();
            Assert.AreEqual("two", enumer.Current.Key); enumer.MoveNext();
            Assert.AreEqual("one", enumer.Current.Key); enumer.MoveNext();
        }

        [Test]
        public void RankKeywords_OneToFourCountProvidedAndBlackListProvidedWhichContainsFour_RankedInOrder()
        {
            var blacklist = new ExcludedWordsRepositoryMock {"four"};
            var keywordRankingTextAnalyser = new SimpleKeywordRankingStrategy(new ContentCleaner(blacklist));

            var keywordRanking = keywordRankingTextAnalyser.RankKeywords(OneToFour);

            var enumer = keywordRanking.GetEnumerator(); enumer.MoveNext();
            Assert.AreEqual("three", enumer.Current.Key); enumer.MoveNext();
            Assert.AreEqual("two", enumer.Current.Key); enumer.MoveNext();
            Assert.AreEqual("one", enumer.Current.Key); enumer.MoveNext();
        }

        [Test]
        public void RankKeywords_OneToFourCountProvided_RankedWithCorrectCount()
        {
            var keywordRankingTextAnalyser = new SimpleKeywordRankingStrategy();

            var keywordRanking = keywordRankingTextAnalyser.RankKeywords(OneToFour);

            var enumer = keywordRanking.GetEnumerator(); enumer.MoveNext();
            Assert.AreEqual("four", enumer.Current.Key); 
            Assert.AreEqual(4, enumer.Current.Value); enumer.MoveNext();
            Assert.AreEqual("three", enumer.Current.Key); 
            Assert.AreEqual(3, enumer.Current.Value); enumer.MoveNext();
            Assert.AreEqual("two", enumer.Current.Key); 
            Assert.AreEqual(2, enumer.Current.Value); enumer.MoveNext();
            Assert.AreEqual("one", enumer.Current.Key); 
            Assert.AreEqual(1, enumer.Current.Value); 
        }
    }
}
