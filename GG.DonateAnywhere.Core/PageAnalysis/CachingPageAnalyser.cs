using System;
using GG.DonateAnywhere.Core.Http;

namespace GG.DonateAnywhere.Core.PageAnalysis
{
    public class CachingPageAnalyser : IPageAnalyser
    {
        private readonly IPageAnalyser _inner;
        private readonly Cache _cache;

        public CachingPageAnalyser(IPageAnalyser inner, Cache cache)
        {
            _inner = inner;
            _cache = cache;
        }

        public PageReport Analyse(Uri uri)
        {
            return _cache.CachedCallTo(uri.ToString(), () => _inner.Analyse(uri));
        }
    }
}