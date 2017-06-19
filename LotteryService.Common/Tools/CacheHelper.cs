using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LotteryService.Common.Tools
{
    public class CacheHelper
    {
        /// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="CacheKey">键</param>
        public static T GetCache<T>(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return (T)objCache[CacheKey];
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public static void SetCache<T>(string CacheKey, T objObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject);
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public static void SetCache<T>(string CacheKey, T objObject, TimeSpan Timeout)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject, null, DateTime.MaxValue, Timeout, System.Web.Caching.CacheItemPriority.NotRemovable, null);
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public static void SetCache<T>(string CacheKey, T objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }

        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        public static void RemoveAllCache(string CacheKey)
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            _cache.Remove(CacheKey);
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                _cache.Remove(CacheEnum.Key.ToString());
            }
        }
 

        public static void AddCacheListItem<T>(string cacheKey, T item)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            var list = GetCache<IList<T>>(cacheKey);
            list.Add(item);
            objCache[cacheKey] = list;
        }

        public static void RemoveCacheListItem<T>(string cacheKey, T item)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            var list = GetCache<IList<T>>(cacheKey);
            list.Remove(item);
            objCache[cacheKey] = list;
        }
    }
}