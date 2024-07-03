using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public class NecnatEndpointStore : INecnatEndpointStore, ITransientDependency
    {
        protected const string _key = "0a4662bc-812b-4b1b-9c77-fb3af66720db";

        protected readonly INecnatEndpointRepository _repository;
        protected readonly IDistributedCache<NecnatEndpointCacheItem> _cache;

        public NecnatEndpointStore(
            INecnatEndpointRepository repository,
            IDistributedCache<NecnatEndpointCacheItem> cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public virtual async Task<List<NecnatEndpoint>> GetListAsync()
        {
            return (await GetCacheItemAsync()).NecnatEndpointList;
        }

        protected virtual async Task<NecnatEndpointCacheItem> GetCacheItemAsync()
        {
            return (await _cache.GetOrAddAsync(
                _key,
                async () => await GetFromDatabaseAsync(),
                () => new DistributedCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.Now.AddHours(1) }
            ))!;
        }

        private async Task<NecnatEndpointCacheItem> GetFromDatabaseAsync()
        {
            var e = await _repository.GetListAsync();
            return new NecnatEndpointCacheItem(e);
        }
    }
}
