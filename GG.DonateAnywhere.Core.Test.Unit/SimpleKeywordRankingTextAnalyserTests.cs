using GG.DonateAnywhere.Core.PageAnalysis;
using NUnit.Framework;

namespace GG.DonateAnywhere.Core.Test.Unit
{
    [TestFixture]
    public class SimpleKeywordRankingTextAnalyserTests
    {
        [Test]
        public void Something()
        {
            var plainText = "This is a string with with multiple instances instances instances of keywords";
            var pa = new SimpleKeywordRankingTextAnalyser();

            var keywordRanking = pa.RankKeywords(plainText);

            Assert.AreEqual(9, keywordRanking.Count);
        }
    }
}
