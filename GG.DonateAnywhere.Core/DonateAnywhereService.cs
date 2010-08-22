using System;
using System.Collections.Generic;
using System.Linq;
using GG.DonateAnywhere.Core.Http;
using GG.DonateAnywhere.Core.PageAnalysis;
using GG.DonateAnywhere.Core.Searching;

namespace GG.DonateAnywhere.Core
{
    public class DonateAnywhereService: IDonateAnywhereService
    {       
        private readonly IPageAnalyser _pageAnalyser;
        private readonly ISearchProvider _searchProvider;

        public DonateAnywhereService(ISearchProvider searchProvider)
            : this(new PageAnalyser(new DirectHttpRequestTransport(), new SimpleKeywordRankingStrategy()), searchProvider)
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
            var results = _searchProvider.Search(keywords);

            var donateAnywhereResult = new DonateAnywhereResult
                                           {
                                               Keywords = keywords,
                                               Results = results,
                                               RequestContext = donateAnywhereContext
                                           };

            return donateAnywhereResult;
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