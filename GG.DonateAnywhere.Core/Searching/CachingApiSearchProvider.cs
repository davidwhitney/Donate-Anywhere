using System.Collections.Generic;
using System.Linq;

namespace GG.DonateAnywhere.Core.Searching
{
    public class CachingApiSearchProvider : ISearchProvider
    {
        private readonly ISearchProvider _inner;
        private readonly Cache _cache;

        public CachingApiSearchProvider(ISearchProvider inner, Cache cache)
        {
            _inner = inner;
            _cache = cache;
        }

        public SearchResults Search(IEnumerable<string> keywords)
        {
            var kw = keywords.ToList();
            return _cache.CachedCallTo(string.Join("|", kw), () => _inner.Search(kw));
        }
    }
}