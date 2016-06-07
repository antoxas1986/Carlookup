﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLookUp.Core.Utilities
{
    public class GlobalCachingProvider : BaseCachingProvider
    {
        public static GlobalCachingProvider Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        public virtual new void AddItem(string key, object value, DateTimeOffset? expiration = null)
        {
            base.AddItem(key, value, expiration);
        }

        public virtual new object GetItem(string key)
        {
            return base.GetItem(key);
        }

        public virtual new void RemoveItem(string key)
        {
            base.RemoveItem(key);
        }

        private class Nested
        {
            internal static readonly GlobalCachingProvider instance = new GlobalCachingProvider();

            static Nested()
            {
            }
        }
    }
}
