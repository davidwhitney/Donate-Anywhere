using System;
using System.Collections.Generic;
using System.Linq;
using GG.DonateAnywhere.Core.PageAnalysis;
using GG.DonateAnywhere.Core.Searching;

namespace GG.DonateAnywhere.Core
{
    public class DonateAnywhereService: IDonateAnywhereService
    {       
        private readonly IPageAnalyser _pageAnalyser;
        private readonly ISearchProvider _searchProvider;

        private const int SEARCH_RESULT_CAP = 10;
        private const int KEYWORD_CAP = 10;
        private const int KEYWORD_CAP_FOR_SEARCH_REQUEST = 4;
        
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
            var searchResults = _searchProvider.Search(keywords.Take(KEYWORD_CAP_FOR_SEARCH_REQUEST).ToList())
                                               .Take(SEARCH_RESULT_CAP)
                                               .ToList();

            var adjustedResults = BoostResultsWhichContainExactKeyword(keywords, searchResults);
            adjustedResults = FilterResultsThatDontMention(keywords, adjustedResults);

            var rawRelatedResults = _searchProvider.Search(keywords).ToList();
            var relatedDictionary = DeduplicateRelatedResults(rawRelatedResults, searchResults);
            var relatedResults = BoostResultsWhichContainExactKeyword(keywords, relatedDictionary.Values.ToList()).Take(SEARCH_RESULT_CAP).ToList();


            var donateAnywhereResult = new DonateAnywhereResult
                                           {
                                               Keywords = keywords,
                                               Results = adjustedResults.Take(SEARCH_RESULT_CAP).ToList(),
                                               RelatedResults = relatedResults.Take(SEARCH_RESULT_CAP).ToList(),
                                               RequestContext = donateAnywhereContext
                                           };

            return donateAnywhereResult;
        }

        private static IEnumerable<SearchResult> BoostResultsWhichContainExactKeyword(IList<string> keywords, IEnumerable<SearchResult> top4KeywordsResults)
        {
            var topResults = new List<SearchResult>();
            var bottomResults = new List<SearchResult>();
            
            var boostedInsert = 0;

            if(keywords.Count < 1)
            {
                keywords.Insert(0, string.Empty);
            }
            if (keywords.Count < 2)
            {
                keywords.Insert(1, string.Empty);
            }
            if (keywords.Count < 3)
            {
                keywords.Insert(2, string.Empty);
            }
            if (keywords.Count < 4)
            {
                keywords.Insert(3, string.Empty);
            }

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
            keywords.AddRange(report.KeywordDensity.Keys.Take(KEYWORD_CAP));
        }

        private static void TakeTopTenUserSuggestedKeywords(IDonateAnywhereRequestContext donateAnywhereContext, List<string> keywords)
        {
            keywords.AddRange(donateAnywhereContext.UserSuppliedKeywords.Take(KEYWORD_CAP));
        }
    }
}