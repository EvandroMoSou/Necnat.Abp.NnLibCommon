using System;

namespace Necnat.Abp.NnLibCommon.Domains
{
    [Serializable]
    public class NecnatEndpointCacheItem
    {
        private const string CacheKeyFormat = "pgn:{0}";

        public string Endpoint { get; set; } = string.Empty;

        public NecnatEndpointCacheItem()
        {

        }

        public NecnatEndpointCacheItem(string endpoint)
        {
            Endpoint = endpoint;
        }

        public static string CalculateCacheKey(string code)
        {
            return string.Format(CacheKeyFormat, code);
        }
    }
}
