using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudStructures;
using LotteryService.Common.Extensions;
using LotteryService.Common.Tools;
using StackExchange.Redis;

namespace LotteryService.Common
{
    public sealed class RedisHelper
    {
        #region RedisHelper 

        private static readonly string ConnectString = Utils.GetConfigValuesByKey("RedisConnection");

        private static Lazy<ConnectionMultiplexer> _lazyConnection;
        private static readonly object multiplexerLock = new object();
        private static readonly IDatabase Cache;


        static RedisHelper()
        {
            var conn = CreateManager.Value;
            Cache = conn.GetDatabase(); //获取实例
        }

        private static Lazy<ConnectionMultiplexer> GetManager(string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = GetDefaultConnectionString();
            }

            return new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(connectionString));
        }

        private static Lazy<ConnectionMultiplexer> CreateManager
        {
            get
            {
                if (_lazyConnection == null)
                {
                    lock (multiplexerLock)
                    {
                        if (_lazyConnection != null) return _lazyConnection;

                        _lazyConnection = GetManager();
                        return _lazyConnection;
                    }
                }

                return _lazyConnection;
            }
        }

        public static string GetDefaultConnectionString()
        {
            return ConnectString;
        }

        #region Key

        public static bool KeyExists(string key)
        {
            var bResult = Cache.KeyExists(key);
            return bResult;
        }

        public static bool SetExpire(string key, DateTime datetime)
        {
            return Cache.KeyExpire(key, datetime);
        }

        public static bool SetExpire(string key, int timeout = 0)
        {
            return Cache.KeyExpire(key, DateTime.Now.AddSeconds(timeout));
        }

        public static bool Set<T>(string key, T t, int timeout = 0)
        {
            bool bResult = Cache.StringSet(key, t.ToJsonString());
            if (timeout > 0)
            {
                Cache.KeyExpire(key, DateTime.Now.AddSeconds(timeout));
            }
            return bResult;
        }


        public static bool SetString(string key, string value, int timeout = 0)
        {
            bool bResult = Cache.StringSet(key, value);
            if (timeout > 0)
            {
                Cache.KeyExpire(key, DateTime.Now.AddSeconds(timeout));
            }
            return bResult;
        }

        public static bool KeyDelete(string key)
        {
            return Cache.KeyDelete(key);
        }

        public static bool KeyRename(string oldKey, string newKey)
        {
            return Cache.KeyRename(oldKey, newKey);
        }

        #endregion

        #region Hash
        public static bool IsExist(string hashId, string key)
        {
            return Cache.HashExists(hashId, key);
        }

        public static bool SetHash<T>(string hashId, string key, T t)
        {
            return Cache.HashSet(hashId, key, t.ToJsonString());
        }

        public static bool Remove(string hashId, string key)
        {
            return Cache.HashDelete(hashId, key);
        }

        public static long StringIncrement(string hashId, string key, long value = 1)
        {
            return Cache.HashIncrement(hashId, key, value);
        }

        public static T Get<T>(string hashId, string key)
        {
            string value = Cache.HashGet(hashId, key);
            return value.ToObject<T>();
        }

        public static long GetHashCount(string hashId)
        {
            var length = Cache.HashLength(hashId);
            return length;
        }

        public static string Get(string hashId, string key)
        {
            string value = Cache.HashGet(hashId, key).ToString();
            return value;
        }
        public static List<T> GetAll<T>(string hashId)
        {
            var result = new List<T>();
            var list = Cache.HashGetAll(hashId).ToList();
            if (list.Count > 0)
            {
                list.ForEach(x =>
                {
                    var value = x.Value.ToString().ToObject<T>();
                    result.Add(value);
                });
            }
            return result;
        }

        public static List<string> GetAllFields(string hashId)
        {
            var result = new List<string>();
            var list = Cache.HashKeys(hashId).ToList();
            if (list.Count > 0)
            {
                list.ForEach(x =>
                {
                    var value = x;
                    result.Add(value);
                });
            }
            return result;
        }
        #endregion

        #region Lists

        //public static bool SetList<T>(string listId, List<T> list)
        //{
        //    var value = list.ToJsonString();
        //    return Cache.SetAdd(listId, value);
        //}

        public static long AddList<T>(string listId, T t)
        {
            var value = t.ToJsonString();
            return Cache.ListLeftPush(listId, value);
        }

        public static long RemoveList<T>(string listId, T t)
        {
            var value = t.ToJsonString();
            return Cache.ListRemove(listId, value);
        }

        public static List<T> GetList<T>(string listId, long start = 0, long stop = -1)
        {
            var result = new List<T>();
            var list = Cache.ListRange(listId, start, stop).ToList();
            if (list.Count > 0)
            {
                list.ForEach(x =>
                {
                    var value = x.ToString().ToObject<T>();
                    result.Add(value);
                });
            }
            return result;
        }
        #endregion

        #region Strings

        public static string Get(string key)
        {
            string value = Cache.StringGet(key);
            return value;
        }

        public static double StringIncrement(string key, double value)
        {
            return Cache.StringIncrement(key, value);
        }

        public static long StringAppend(string key, string value)
        {
            return Cache.StringAppend(value, value, CommandFlags.None);
        }
        #endregion

        #endregion

        #region  obsoleting

        //public static bool Set<T>(string key, T t, int timeout = 0)
        //{
        //    var redis = new RedisString<T>(RedisServer.Default, key);
        //    if (timeout > 0)
        //    {
        //        redis.Expire(DateTime.Now.AddSeconds(timeout));
        //    }

        //    return  redis.Set(t).Result;
        //}

        //public static long AddList<T>(string key, params T[] items)
        //{
        //    var list = new RedisList<T>(RedisServer.Default, key);
        //    var result = list.LeftPush(items);
        //    return result.Result;

        //}

        //public static IList<T> GetAllList<T>(string key)
        //{
        //    var list = new RedisList<T>(RedisServer.Default, key);
        //    return list.Range().Result;
        //}

        //public static long AddSet<T>(string key,params T[] items)
        //{
        //    var set = new RedisSet<T>(RedisServer.Default,key);
        //    var result = set.Add(items);
        //    return result.Result;
        //}

        //public static ICollection<T> GetSetMembers<T>(string key)
        //{
        //    var set = new RedisSet<T>(RedisServer.Default, key);
        //    return set.Members().Result;
        //}

        //public static void AddHash<T>(string hashId, string key, T value)
        //{
        //    var hash = new RedisHash<string>(RedisServer.Default, hashId);
        //    hash.Set(key, value);
        //}

        //public static ICollection<T> GetAllList<T>(string hashId, string key)
        //{
        //    var hash = new RedisHash<string>(RedisServer.Default, hashId);
        //    return hash.Get<T>(key).Result;
        //}

        //public static void AddHash<T>(string hashId, string key, params T[] values)
        //{
        //    var hash = new RedisHash<string>(RedisServer.Default, hashId);
        //    foreach (var value in values)
        //    {
        //        hash.Set(key, value);
        //    }
        //}

        #endregion
    }

    //public static class RedisServer
    //{
    //    private static readonly string ConnectString = Utils.GetConfigValuesByKey("RedisConnection");
    //    public static readonly RedisSettings Default = new RedisSettings(ConnectString);
    //}
}