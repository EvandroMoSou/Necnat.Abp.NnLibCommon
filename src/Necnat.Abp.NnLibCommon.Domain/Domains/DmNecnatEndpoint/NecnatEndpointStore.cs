using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public virtual async Task<string?> FindAuthServerEndpointAsync(bool isActive = true)
        {
            return (await GetCacheItemAsync()).NecnatEndpointList.Where(x => x.IsActive == isActive && x.IsAuthServer).FirstOrDefault()?.Endpoint;
        }

        public virtual async Task<List<KeyValuePair<string, string>>> GetListAuthorizationEndpointAsync(bool isActive = true)
        {
            return (await GetCacheItemAsync()).NecnatEndpointList.Where(x => x.IsActive == isActive && x.IsAuthorization)
                .Select(x => new KeyValuePair<string, string>(x.PermissionsGroupName!, x.Endpoint)).ToList();
        }

        public virtual async Task<List<string>> GetListBillingEndpointAsync(bool isActive = true)
        {
            return (await GetCacheItemAsync()).NecnatEndpointList.Where(x => x.IsActive == isActive && x.IsBilling)
                .Select(x => x.Endpoint).ToList();
        }

        public virtual async Task<List<KeyValuePair<short, string>>> GetListHierarchyComponentEndpointAsync(bool isActive = true)
        {
            return (await GetCacheItemAsync()).NecnatEndpointList.Where(x => x.IsActive == isActive && x.IsHierarchyComponent)
                .Select(x => new KeyValuePair<short, string>((short)x.HierarchyComponentTypeId!, x.Endpoint)).ToList();
        }

        protected virtual async Task<NecnatEndpointCacheItem> GetCacheItemAsync()
        {
            return (await _cache.GetOrAddAsync(
                _key,
                async () => await GetFromDatabaseAsync(),
                () => new DistributedCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.Now.AddHours(1) }
            ))!;
        }

        protected virtual async Task<NecnatEndpointCacheItem> GetFromDatabaseAsync()
        {
            var e = await _repository.GetListAsync();
            return new NecnatEndpointCacheItem(e);
        }
    }
}
