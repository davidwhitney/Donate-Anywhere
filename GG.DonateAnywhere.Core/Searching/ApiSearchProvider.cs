using System.Collections.Generic;
using System.Linq;
using JustGiving.Api.Sdk;

namespace GG.DonateAnywhere.Core.Searching
{
    public class ApiSearchProvider: ISearchProvider
    {
        public IList<SearchResult> Search(List<string> keywords)
        {
            var clientConfig = new ClientConfiguration("https://api.staging.justgiving.com/", "decbf1d2", 1);
            var client = new JustGivingClient(clientConfig);

            var all = client.Search.CharitySearch(string.Join(" + ", keywords));

            var cleaner = new HtmlCleaner.HtmlCleaner();

            var results = all.Results.Take(20).ToDictionary(charitySearchResult => charitySearchResult.CharityId,
                                                           charitySearchResult => new SearchResult
                                                                                      {
                                                                                          Description = cleaner.RemoveHtml(charitySearchResult.Description),
                                                                                          Title = cleaner.RemoveHtml(charitySearchResult.Name),
                                                                                          Url = "url here"
                                                                                      });

            return results.Values.ToList();

        }
    }
}