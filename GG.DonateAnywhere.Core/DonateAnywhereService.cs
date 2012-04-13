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
            
            var searchResults = _searchProvider.Search(keywords.ToListOfStrings()
                                               .Take(KEYWORD_CAP_FOR_SEARCH_REQUEST))
                                               .Take(SEARCH_RESULT_CAP)
                                               .BoostResultsWhichContainExactKeyword(keywords)
                                               .RemoveResultsThatDontMention(keywords);

            var relatedResults = _searchProvider.Search(keywords.ToListOfStrings())
                                                .RemoveAnyItemsThatAreAlsoIn(searchResults)
                                                .BoostResultsWhichContainExactKeyword(keywords)
                                                .Take(SEARCH_RESULT_CAP);

            return new DonateAnywhereResult
                       {
                           Keywords = keywords,
                           Results = searchResults.Take(SEARCH_RESULT_CAP),
                           RelatedResults = relatedResults.Take(SEARCH_RESULT_CAP).ToList(),
                           RequestContext = donateAnywhereContext
                       };
        }
        
        private Keywords CalculateKeywordsForSearchCriteria(IDonateAnywhereRequestContext donateAnywhereContext)
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

            return new Keywords(keywords);
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