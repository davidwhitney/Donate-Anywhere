using System.Collections.Generic;

namespace GG.DonateAnywhere.Core.PageAnalysis
{
    public interface IKeywordRankingStrategy
    {
        IDictionary<string, decimal> RankKeywords(string plainText);
    }
}