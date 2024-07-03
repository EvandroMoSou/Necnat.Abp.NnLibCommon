using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public class NecnatEndpointStore : INecnatEndpointStore, ITransientDependency
    {
        protected readonly INecnatEndpointRepository _repository;
        protected readonly IDistributedCache<NecnatEndpointCacheItem> _cache;

        public NecnatEndpointStore(
            INecnatEndpointRepository repository,
            IDistributedCache<NecnatEndpointCacheItem> cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public virtual async Task<string> GetEndpointByPermissionsGroupNameAsync(string permissionsGroupName)
        {
            return (await GetCacheItemAsync(permissionsGroupName)).Endpoint;
        }

        protected virtual async Task<NecnatEndpointCacheItem> GetCacheItemAsync(string permissionsGroupName)
        {
            return (await _cache.GetOrAddAsync(
                NecnatEndpointCacheItem.CalculateCacheKey(permissionsGroupName),
                async () => await GetFromDatabaseAsync(permissionsGroupName),
                () => new DistributedCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.Now.AddHours(1) }
            ))!;
        }

        private async Task<NecnatEndpointCacheItem> GetFromDatabaseAsync(string permissionsGroupName)
        {
            var e = await _repository.GetByPermissionsGroupNameAsync(permissionsGroupName);
            return new NecnatEndpointCacheItem(e.Endpoint);
        }
    }
}
