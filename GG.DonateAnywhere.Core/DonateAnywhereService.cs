using System;
using System.Collections.Generic;
using System.Linq;
using GG.DonateAnywhere.Core.Http;
using GG.DonateAnywhere.Core.PageAnalysis;
using GG.DonateAnywhere.Core.Sanitise;
using GG.DonateAnywhere.Core.Searching;

namespace GG.DonateAnywhere.Core
{
    public class DonateAnywhereService: IDonateAnywhereService
    {       
        private readonly IPageAnalyser _pageAnalyser;
        private readonly ISearchProvider _searchProvider;

        public DonateAnywhereService(ISearchProvider searchProvider)
            : this(new PageAnalyser(new DirectHttpRequestTransport(), new SimpleKeywordRankingStrategy(), new ContentCleaner()), searchProvider)
        {
        }

        public DonateAnywhereService(IPageAnalyser pageAnalyser, ISearchProvider searchProvider)
        {
            _pageAnalyser = pageAnalyser;
            _searchProvider = searchProvider;
        }

        public DonateAnywhereResult EvaluateRequest(IDonateAnywhereRequestContext donateAnywhereContext)
        {
            if(!donateAnywhereContext.EnoughInformationToBuildSuggestions)
            {
                throw new ArgumentException("Not enough information on the current context is available to generate suggestions.", "donateAnywhereContext");
            }

            var keywords = CalculateKeywordsForSearchCriteria(donateAnywhereContext);
            var top4KeywordsResults = _searchProvider.Search(keywords.Take(4).ToList()).Take(10).ToList();
            var orderedTopResults = SortTopResultsByKeywordRelevance(keywords, top4KeywordsResults);
            orderedTopResults = FilterResultsThatDontMention(keywords, orderedTopResults);

            var rawRelatedResults = _searchProvider.Search(keywords).ToList();
            var relatedDictionary = DeduplicateRelatedResults(rawRelatedResults, top4KeywordsResults);
            var relatedResults = SortTopResultsByKeywordRelevance(keywords, relatedDictionary.Values.ToList()).Take(10).ToList();


            var donateAnywhereResult = new DonateAnywhereResult
                                           {
                                               Keywords = keywords,
                                               Results = orderedTopResults.Take(10).ToList(),
                                               RelatedResults = relatedResults.Take(10).ToList(),
                                               RequestContext = donateAnywhereContext
                                           };

            return donateAnywhereResult;
        }

        private static IEnumerable<SearchResult> SortTopResultsByKeywordRelevance(IList<string> keywords, IEnumerable<SearchResult> top4KeywordsResults)
        {
            var topResults = new List<SearchResult>();
            var bottomResults = new List<SearchResult>();

            int boostedInsert = 0;

            foreach(var item in top4KeywordsResults)
            {
                if (item.Title.ToLower().Contains(keywords[0]) && item.Description.ToLower().Contains(keywords[0]))
                {
                    topResults.Insert(boostedInsert, item);
                    boostedInsert++;
                }
                else if (item.Title.ToLower().Contains(keywords[1]) && item.Description.ToLower().Contains(keywords[1]))
                {
                    topResults.Add(item);
                }
                else
                {
                    bottomResults.Add(item);
                }
            }

            topResults.AddRange(bottomResults);

            return topResults;
        }

        private static IEnumerable<SearchResult> FilterResultsThatDontMention(IList<string> keywords, IEnumerable<SearchResult> top4KeywordsResults)
        {
            return top4KeywordsResults.Where(
                        item => item.Title.ToLower().Contains(keywords[0]) 
                                || item.Title.ToLower().Contains(keywords[1]) 
                                || item.Title.ToLower().Contains(keywords[2]) 
                                || item.Title.ToLower().Contains(keywords[3]) 
                                || item.Description.ToLower().Contains(keywords[0]) 
                                || item.Description.ToLower().Contains(keywords[1])
                                || item.Description.ToLower().Contains(keywords[2]) 
                                || item.Description.ToLower().Contains(keywords[3])).ToList();
        }

        private static Dictionary<string, SearchResult> DeduplicateRelatedResults(IEnumerable<SearchResult> relatedResults, IEnumerable<SearchResult> top4KeywordsResults)
        {
            var relatedDictionary = relatedResults.ToDictionary(relatedItem => relatedItem.Title);
            foreach (var key in top4KeywordsResults.Select(item => item.Title).Where(relatedDictionary.ContainsKey))
            {
                relatedDictionary.Remove(key);
            }
            return relatedDictionary;
        }

        private List<string> CalculateKeywordsForSearchCriteria(IDonateAnywhereRequestContext donateAnywhereContext)
        {
            var keywords = new List<string>();

            if (donateAnywhereContext.UserSuppliedKeywords.Count > 0)
            {
                TakeTopTenUserSuggestedKeywords(donateAnywhereContext, keywords);
            }
            else if (donateAnywhereContext.RequiresFullPageAnalysis)
            {
                ExtractTopTenKeywordsFromPage(donateAnywhereContext, keywords);
            }

            return keywords;
        }

        private void ExtractTopTenKeywordsFromPage(IDonateAnywhereRequestContext donateAnywhereContext, List<string> keywords)
        {
            var report = _pageAnalyser.Analyse(donateAnywhereContext.UriToAnalyse);
            keywords.AddRange(report.KeywordDensity.Keys.Take(10));
        }

        private static void TakeTopTenUserSuggestedKeywords(IDonateAnywhereRequestContext donateAnywhereContext, List<string> keywords)
        {
            keywords.AddRange(donateAnywhereContext.UserSuppliedKeywords.Take(10));
        }
    }
}