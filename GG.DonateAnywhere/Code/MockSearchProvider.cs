using System.Collections.Generic;
using GG.DonateAnywhere.Core.Searching;

namespace GG.DonateAnywhere.Code
{
    public class MockSearchProvider: ISearchProvider
    {
        public IList<SearchResult> Search(List<string> keywords)
        {
            var results = new List<SearchResult>
                              {
                                  new SearchResult { Title = "Result 1", Description = "Mock description 1", Url = "http://tempuri.org/1" },
                                  new SearchResult { Title = "Result 2", Description = "Mock description 2", Url = "http://tempuri.org/2" },
                                  new SearchResult { Title = "Result 3", Description = "Mock description 3", Url = "http://tempuri.org/3" },
                                  new SearchResult { Title = "Result 4", Description = "Mock description 4", Url = "http://tempuri.org/4" }
                              };

            return results;

        }
    }
}