using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using GG.DonateAnywhere.Core.Http;
using HtmlAgilityPack;

namespace GG.DonateAnywhere.Core.PageAnalysis
{
    public class PageAnalyser : IPageAnalyser
    {
        private readonly IDirectHttpRequestTransport _httpRequestTransport;
        private readonly IKeywordRankingStrategy _keywordRankingStrategy;

        public PageAnalyser(IDirectHttpRequestTransport httpRequestTransport, IKeywordRankingStrategy keywordRankingStrategy)
        {
            _httpRequestTransport = httpRequestTransport;
            _keywordRankingStrategy = keywordRankingStrategy;
        }

        public PageReport Analyse(Uri uri)
        {
            var html = _httpRequestTransport.FetchUri(uri);
            var rawText = ExtractPlainTextFromHtml(html);
            var highlightedWords = ExtractImportantWordsFromHtml(html);

            var ranking = _keywordRankingStrategy.RankKeywords(rawText);
            AdjustKeywordDensityAccordingToSignificantUrlWords(uri, ranking);
            AdjustKeywordDensityAccordingToSignificantDocumentContent(ranking, highlightedWords);

            var orderedRanking = ranking.OrderBy(x => x.Value).Reverse().ToDictionary(x => x.Key, x => x.Value);

            return new PageReport { KeywordDensity = orderedRanking };
        }


        private static IEnumerable<string> ExtractImportantWordsFromHtml(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var upRankedwords = new List<string>();

            var titleNode = doc.DocumentNode.SelectNodes("//title");
            if (titleNode != null)
            {
                foreach (var word in titleNode.Select(node => node.InnerText.Split(' ').Take(3)).SelectMany(words => words.Where(word => !upRankedwords.Contains(word))))
                {
                    upRankedwords.Add(word);
                }
            }

            var h1Node = doc.DocumentNode.SelectNodes("//h1");
            if(h1Node != null)
            {
                foreach (var word in h1Node.Select(node => node.InnerText.Split(' ')).SelectMany(words => words.Where(word => !upRankedwords.Contains(word))))
                {
                    upRankedwords.Add(word);
                }
            }

            return upRankedwords;
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

        private static void AdjustKeywordDensityAccordingToSignificantUrlWords(Uri uri, IDictionary<string, decimal> ranking)
        {
            var parts = uri.Segments[uri.Segments.Length-1].Split('_', '-', '/', ')', '(');

            foreach (var extraSignificantKeyword in parts.Select(urlPart => urlPart.ToLower()).Where(ranking.ContainsKey))
            {
                ranking[extraSignificantKeyword] += 30;
            }
        }

        private static void AdjustKeywordDensityAccordingToSignificantDocumentContent(IDictionary<string, decimal> ranking, IEnumerable<string> highlightedWords)
        {
            foreach (var extraSignificantKeyword in highlightedWords.Select(word => word.ToLower()).Where(ranking.ContainsKey))
            {
                ranking[extraSignificantKeyword] += 20;
            }
        }
    }
}
