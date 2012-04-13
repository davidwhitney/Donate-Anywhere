using System;

namespace GG.DonateAnywhere.Core.Http
{
    public class CachingHttpGetter : IDirectHttpRequestTransport
    {
        private readonly IDirectHttpRequestTransport _inner;
        private readonly Cache _cache;

        public CachingHttpGetter(IDirectHttpRequestTransport inner, Cache cache)
        {
            _inner = inner;
            _cache = cache;
        }

        public string FetchUri(Uri uri)
        {
            return _cache.CachedCallTo(uri.ToString(), () => _inner.FetchUri(uri));
        }
    }
}