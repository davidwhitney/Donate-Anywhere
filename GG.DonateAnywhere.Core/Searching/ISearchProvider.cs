using System.Collections.Generic;

namespace GG.DonateAnywhere.Core.Searching
{
    public interface ISearchProvider
    {
        IList<SearchResult> Search(List<string> keywords);
    }
}