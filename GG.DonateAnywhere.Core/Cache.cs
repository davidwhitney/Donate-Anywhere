using System;
using System.Diagnostics;
using System.Runtime.Caching;

namespace GG.DonateAnywhere.Core
{
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