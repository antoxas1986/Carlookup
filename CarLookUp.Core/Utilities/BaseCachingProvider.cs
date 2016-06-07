using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace CarLookUp.Core.Utilities
{
    public class BaseCachingProvider
    {
        protected MemoryCache cache = new MemoryCache("CachingProvider");
        private static readonly object _padlock = new object();

        public BaseCachingProvider()
        {
        }

        protected virtual void AddItem(string key, object value, DateTimeOffset? expiration = null)
        {
            lock (_padlock)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                if (expiration != null)
                {
                    policy.AbsoluteExpiration = expiration.Value;
                }
                else
                {
                    policy.AbsoluteExpiration = DateTimeOffset.Now.AddHours(1);
                }
                cache.Add(key, value, policy);
            }
        }

        protected virtual object GetItem(string key)
        {
            lock (_padlock)
            {
                return cache[key];
            }
        }

        protected virtual void RemoveItem(string key)
        {
            lock (_padlock)
            {
                cache.Remove(key);
            }
        }
    }
}
