using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using GG.DonateAnywhere.Core.Http;
using HtmlAgilityPack;

namespace GG.DonateAnywhere.Core.PageAnalysis
{
    public interface IPageAnalyser
    {
        PageReport Analyse(Uri uri);
    }

    public class PageAnalyser : IPageAnalyser
    {
        private readonly IDirectHttpRequestTransport _httpRequestTransport;
        private readonly SimpleKeywordRankingTextAnalyser _simpleKeywordRankingTextAnalyser;

        public PageAnalyser(IDirectHttpRequestTransport httpRequestTransport, SimpleKeywordRankingTextAnalyser simpleKeywordRankingTextAnalyser)
        {
            _httpRequestTransport = httpRequestTransport;
            _simpleKeywordRankingTextAnalyser = simpleKeywordRankingTextAnalyser;
        }

        public PageReport Analyse(Uri uri)
        {
            var html = _httpRequestTransport.FetchUri(uri);
            var rawText = ExtractPlainTextFromHtml(html);

            var ranking = _simpleKeywordRankingTextAnalyser.RankKeywords(rawText);
            AdjustKeywordDensityAccordingToSignificantUrlWords(uri, ranking);

            return new PageReport {KeywordDensity = ranking};
        }

        private static string ExtractPlainTextFromHtml(string html)
        {
            html = Regex.Replace(html, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            html = Regex.Replace(html, "<style.*?</style>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var rawText = doc.DocumentNode.InnerText;
            rawText = HttpUtility.HtmlDecode(rawText);

            return rawText;
        }

        private static void AdjustKeywordDensityAccordingToSignificantUrlWords(Uri uri, IDictionary<string, decimal> ranking)
        {
            var parts = uri.Segments[uri.Segments.Length-1].Split('_', '-', '/', ')', '(');

            foreach (var extraSignificantKeyword in parts.Select(urlPart => urlPart.ToLower()).Where(extraSignificantKeyword => ranking.ContainsKey(extraSignificantKeyword)))
            {
                ranking[extraSignificantKeyword] += 20;
            }

            ranking = ranking.OrderBy(x => x.Value).Reverse().ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
