using System;
using System.Collections.Generic;
using System.Linq;
using GG.DonateAnywhere.Core.Sanitise;

namespace GG.DonateAnywhere.Core.PageAnalysis
{
    public class SimpleKeywordRankingStrategy : IKeywordRankingStrategy
    {
        private readonly ContentCleaner _contentCleaner;

        public SimpleKeywordRankingStrategy()
            : this(new ContentCleaner(new AssemblyResourceExcludedWordsRepository("GG.DonateAnywhere.Core.PageAnalysis.blacklist.txt")))
        {
        }

        public SimpleKeywordRankingStrategy(ContentCleaner contentCleaner)
        {
            _contentCleaner = contentCleaner;
        }

        public IDictionary<string, decimal> RankKeywords(string plainText)
        {
            if(string.IsNullOrWhiteSpace(plainText))
            {
                return new Dictionary<string, decimal>();
            }

            plainText = _contentCleaner.ClenseSourceData(plainText);
            var ranking = new Dictionary<string, decimal>();

            foreach (var word in plainText.Split(' ').Where(word => !string.IsNullOrWhiteSpace(word)))
            {
                if (ranking.ContainsKey(word))
                {
                    ranking[word]++;
                }
                else
                {
                    ranking.Add(word, 1);
                }
            }
            
            return ranking.OrderBy(x => x.Value).Reverse().ToDictionary(x => x.Key, x => x.Value);
        }

    }
}
