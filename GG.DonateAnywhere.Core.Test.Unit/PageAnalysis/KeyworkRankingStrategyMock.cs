using System.Collections.Generic;
using GG.DonateAnywhere.Core.PageAnalysis;

namespace GG.DonateAnywhere.Core.Test.Unit.PageAnalysis
{
    public class KeyworkRankingStrategyMock: IKeywordRankingStrategy
    {
        public string LastRankingRequestPlainText { get; set; }

        public IDictionary<string, decimal> RankKeywords(string plainText)
        {
            LastRankingRequestPlainText = plainText;
            return new Dictionary<string, decimal>();
        }
    }
}