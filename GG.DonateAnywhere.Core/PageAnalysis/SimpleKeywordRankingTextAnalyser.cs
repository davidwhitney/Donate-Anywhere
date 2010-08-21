using System;
using System.Collections.Generic;
using System.Linq;

namespace GG.DonateAnywhere.Core.PageAnalysis
{
    public class SimpleKeywordRankingTextAnalyser
    {
        private readonly IExcludedWordsRepository _excludedWordsRepository;

        public SimpleKeywordRankingTextAnalyser()
            : this(new AssemblyResourceExcludedWordsRepository("GG.DonateAnywhere.Core.PageAnalysis.whitelist.txt"))
        {
        }

        public SimpleKeywordRankingTextAnalyser(IExcludedWordsRepository excludedWordsRepository)
        {
            _excludedWordsRepository = excludedWordsRepository;
        }

        public IDictionary<string, decimal> RankKeywords(string plainText)
        {
            plainText = ClenseSourceData(plainText);

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

        private string ClenseSourceData(string plainText)
        {
            plainText = plainText.ToLower().Trim();
            plainText = FilterCommonWords(plainText);
            return plainText;
        }

        private string FilterCommonWords(string source)
        {
            source = RemoveSpecialCharacters(source);
            var exclusionList = _excludedWordsRepository.RetrieveExcludedWords();
            var words = source.Split(' ').ToList();
            foreach(var whitelistedWord in exclusionList)
            {
                var word = whitelistedWord;
                words.RemoveAll(s => s == word);
            }
            words.RemoveAll(s => s.Length <= 3);

            return words.Aggregate("", (current, item) => current + (item + " ")).Trim();
        }

        private static string RemoveSpecialCharacters(string source)
        {
            source = source.Replace(Environment.NewLine, "");
            source = source.Replace(".", "");
            source = source.Replace(",", "");
            source = source.Replace(";", "");
            source = source.Replace("'", "");
            source = source.Replace("[", "");
            source = source.Replace("]", "");
            source = source.Replace("(", "");
            source = source.Replace(")", "");
            source = source.Replace("{", "");
            source = source.Replace("}", "");
            source = source.Replace("|", "");
            source = source.Replace("-", "");
            source = source.Replace("`", "");
            source = source.Replace("_", "");
            source = source.Replace("+", "");
            source = source.Replace("=", "");
            source = source.Replace("%", "");
            source = source.Replace("^", "");
            source = source.Replace("&", "");
            source = source.Replace("*", "");
            source = source.Replace("\\", "");
            source = source.Replace("/", "");
            source = source.Replace("?", "");
            source = source.Replace("~", "");
            source = source.Replace("#", "");
            source = source.Replace("!", "");
            source = source.Replace("\"", "");
            source = source.Replace("£", "");
            source = source.Replace("$", "");
            source = source.Replace(":", "");
            source = source.Replace("<", "");
            source = source.Replace(">", "");
            source = source.Replace("0", "");
            source = source.Replace("1", "");
            source = source.Replace("2", "");
            source = source.Replace("3", "");
            source = source.Replace("4", "");
            source = source.Replace("5", "");
            source = source.Replace("6", "");
            source = source.Replace("7", "");
            source = source.Replace("8", "");
            source = source.Replace("9", "");
            return source;
        }
    }
}
