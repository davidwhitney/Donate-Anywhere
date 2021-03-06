﻿using System.Collections.Generic;
using GG.DonateAnywhere.Core.Searching;

namespace DonateAnywhere.Web.Code
{
    public class MockSearchProvider: ISearchProvider
    {
        public SearchResults Search(IEnumerable<string> keywords)
        {
            return new SearchResults
                       {
                           new SearchResult { Title = "Result 1", Description = "Mock description 1", CharityId = "1" },
                           new SearchResult { Title = "Result 2", Description = "Mock description 2", CharityId = "2" },
                           new SearchResult { Title = "Result 3", Description = "Mock description 3", CharityId = "3" },
                           new SearchResult { Title = "Result 4", Description = "Mock description 4", CharityId = "4" }
                       };

        }
    }
}