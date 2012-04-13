using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using GG.DonateAnywhere.Core.Http;
using GG.DonateAnywhere.Core.Sanitise;
using HtmlAgilityPack;

namespace GG.DonateAnywhere.Core.PageAnalysis
{
    public class PageAnalyser : IPageAnalyser
    {
        private readonly IDirectHttpRequestTransport _httpRequestTransport;
        private readonly IKeywordRankingStrategy _keywordRankingStrategy;
        private readonly ContentCleaner _contentCleaner;

        public PageAnalyser(IDirectHttpRequestTransport httpRequestTransport, IKeywordRankingStrategy keywordRankingStrategy, ContentCleaner contentCleaner)
        {
            _httpRequestTransport = httpRequestTransport;
            _keywordRankingStrategy = keywordRankingStrategy;
            _contentCleaner = contentCleaner;
        }

        public PageReport Analyse(Uri uri)
        {
            string html;
            using (new DebugTimer("HTTP fetch"))
            {
                html = _httpRequestTransport.FetchUri(uri);
            }

            var rawText = ExtractPlainTextFromHtml(html);
            
            var ranking = _keywordRankingStrategy.RankKeywords(rawText);

            var emphasisedWords = ExtractImportantWordsFromHtml(html).ToList();
            var importantUriWords = ExtractKeywordsFromUri(uri);

            Uprank(ranking, importantUriWords, 30);
            Uprank(ranking, emphasisedWords, 20);
            Uprank(ranking, emphasisedWords.Take(1), 100);

            var orderedRanking = ranking.OrderBy(x => x.Value).Reverse().ToDictionary(x => x.Key, x => x.Value);

            return new PageReport { KeywordDensity = orderedRanking };
        }


        private IEnumerable<string> ExtractImportantWordsFromHtml(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var upRankedwords = new List<string>();

            var titleNode = doc.DocumentNode.SelectNodes("//title");
            AddCleanedWordsFromNode(titleNode, upRankedwords);

            var h1Node = doc.DocumentNode.SelectNodes("//h1");
            AddCleanedWordsFromNode(h1Node, upRankedwords);

            return upRankedwords;
        }

        private void AddCleanedWordsFromNode(IEnumerable<HtmlNode> containingNode, ICollection<string> upRankedwords)
        {
            if (containingNode == null)
            {
                return;
            }

            foreach (var word in from word in containingNode.Select(node => node.InnerText.Split(' ').Take(3))
                                                            .SelectMany(words => words.Where(word => !upRankedwords.Contains(word)))
                                                            .Where(word => !string.IsNullOrWhiteSpace(word)) 
                                 let cleanWord = _contentCleaner.ClenseSourceData(word) 
                                 where !string.IsNullOrWhiteSpace(cleanWord) 
                                 select word)
            {
                upRankedwords.Add(_contentCleaner.ClenseSourceData(word));
            }
        }

        private static string ExtractPlainTextFromHtml(string html)
        {
            html = Regex.Replace(html, "<script.*?</script>", " ", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            html = Regex.Replace(html, "<style.*?</style>", " ", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var rawText = doc.DocumentNode.InnerText;
            rawText = HttpUtility.HtmlDecode(rawText);

            return rawText;
        }

        private static IEnumerable<string> ExtractKeywordsFromUri(Uri uri)
        {
            var words = uri.Segments[uri.Segments.Length-1].Split('_', '-', '/', ')', '(').ToList();
            for (var index = 0; index < words.Count; index++)
            {
                words[index] = words[index].Replace(".html", "");
                words[index] = words[index].Replace(".htm", "");
                words[index] = words[index].Replace(".php", "");
                words[index] = words[index].Replace(".jsp", "");
                words[index] = words[index].Replace(".asp", "");
                words[index] = words[index].Replace(".aspx", "");
            }

            return words;
        }

        public static void Uprank(IDictionary<string, decimal> ranking, IEnumerable<string> words, int modifier)
        {
            foreach (var extraSignificantKeyword in words.Select(word => word.ToLower()).Where(ranking.ContainsKey))
            {
                ranking[extraSignificantKeyword] += modifier;
            }
        }

    }
}
