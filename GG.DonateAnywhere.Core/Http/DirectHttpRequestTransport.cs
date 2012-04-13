using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Caching;

namespace GG.DonateAnywhere.Core.Http
{
    public class DirectHttpRequestTransport : IDirectHttpRequestTransport
    {
        public string FetchUri(Uri uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri.ToString());

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseStream = response.GetResponseStream();

                if(responseStream == null)
                {
                    throw new Exception("Response stream is null.");
                }

                using (var reader = new StreamReader(responseStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }

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

    public class Cache
    {
        private readonly MemoryCache _appCache;

        public Cache()
        {
            _appCache = new MemoryCache("contentCache");
        }

        public T CachedCallTo<T>(string key, Func<T> function)
        {
            var name = function.Method.DeclaringType.FullName + "_" + function.Method.Name;
            name += ":" + key;

            if(!_appCache.Contains(name))
            {
                _appCache.Add(name, function(),
                              new CacheItemPolicy
                                  {
                                      AbsoluteExpiration = DateTime.Now.AddMinutes(10), 
                                      RemovedCallback = x => Debug.WriteLine("Dropped " + name + " from cache")
                                  });
            }

            return (T)_appCache[name];
        }
    }
}