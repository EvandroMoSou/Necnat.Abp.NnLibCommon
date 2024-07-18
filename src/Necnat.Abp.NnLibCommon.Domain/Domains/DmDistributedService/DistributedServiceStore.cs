using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public class DistributedServiceStore : IDistributedServiceStore, ITransientDependency
    {
        protected const string _key = "0a4662bc-812b-4b1b-9c77-fb3af66720db";

        protected readonly IDistributedServiceRepository _repository;
        protected readonly IDistributedCache<DistributedServiceCacheItem> _cache;

        public DistributedServiceStore(
            IDistributedServiceRepository repository,
            IDistributedCache<DistributedServiceCacheItem> cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<List<DistributedServiceModel>> GetListAsync(string? applicationName = null, string? tag = null, bool isActive = true)
        {
            var cache = await GetCacheItemAsync();

            return cache.List
                .Where(x => (string.IsNullOrWhiteSpace(applicationName) || x.ApplicationName == applicationName)
                    && (string.IsNullOrWhiteSpace(tag) || x.Tag.StartsWith(tag))
                    && x.IsActive == isActive)
                .Select(x => new DistributedServiceModel
                {
                    ApplicationName = x.ApplicationName,
                    Tag = x.Tag,
                    Url = x.Url,
                    IsActive = x.IsActive
                }).ToList();
        }

        protected virtual async Task<DistributedServiceCacheItem> GetCacheItemAsync()
        {
            return (await _cache.GetOrAddAsync(
                _key,
                async () => await GetFromDatabaseAsync(),
                () => new DistributedCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.Now.AddHours(1) }
            ))!;
        }

        protected virtual async Task<DistributedServiceCacheItem> GetFromDatabaseAsync()
        {
            var e = await _repository.GetListAsync();
            return new DistributedServiceCacheItem(e);
        }
    }
}
