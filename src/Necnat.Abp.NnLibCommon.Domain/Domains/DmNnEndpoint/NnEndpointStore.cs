using Microsoft.Extensions.Caching.Distributed;
using Necnat.Abp.NnLibCommon.Domains.DmNnEndpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public class NnEndpointStore : INnEndpointStore, ITransientDependency
    {
        protected const string _key = "0a4662bc-812b-4b1b-9c77-fb3af66720db";

        protected readonly INnEndpointRepository _repository;
        protected readonly IDistributedCache<NnEndpointCacheItem> _cache;

        public NnEndpointStore(
            INnEndpointRepository repository,
            IDistributedCache<NnEndpointCacheItem> cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public virtual async Task<List<NnEndpointModel>> GetListByTag(string tag, bool isActive = true)
        {
            var nnEndpointList = await GetCacheItemAsync();

            return nnEndpointList.NnEndpointList.Where(x => x.IsActive == isActive && x.Tag.StartsWith(tag)).Select(
                x => new NnEndpointModel
                {
                    DisplayName = x.DisplayName,
                    Tag = x.Tag,
                    UrlUri = x.UrlUri,
                    IsActive = x.IsActive
                }).ToList();
        }

        protected virtual async Task<NnEndpointCacheItem> GetCacheItemAsync()
        {
            return (await _cache.GetOrAddAsync(
                _key,
                async () => await GetFromDatabaseAsync(),
                () => new DistributedCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.Now.AddHours(1) }
            ))!;
        }

        protected virtual async Task<NnEndpointCacheItem> GetFromDatabaseAsync()
        {
            var e = await _repository.GetListAsync();
            return new NnEndpointCacheItem(e);
        }
    }
}
