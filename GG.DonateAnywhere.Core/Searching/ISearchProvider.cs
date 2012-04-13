using System.Collections.Generic;

namespace GG.DonateAnywhere.Core.Searching
{
    public interface ISearchProvider
    {
        SearchResults Search(IEnumerable<string> keywords);
    }
}