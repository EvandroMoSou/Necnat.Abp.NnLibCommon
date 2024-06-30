using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    public class RoleNameServiceCache : IRoleNameService, ITransientDependency
    {
        readonly INnIdentityRoleRepository _nnIdentityRoleRepository;
        readonly IDistributedCache<RoleNameCacheItem> _roleNameCache;

        public RoleNameServiceCache(
            INnIdentityRoleRepository nnIdentityRoleRepository,
            IDistributedCache<RoleNameCacheItem> roleNameCache)
        {
            _nnIdentityRoleRepository = nnIdentityRoleRepository;
            _roleNameCache = roleNameCache;
        }

        public async Task<string> GetByIdAsync(Guid id)
        {
            return (await GetCacheItemAsync(id)).RoleName;
        }

        public async Task<RoleNameCacheItem> GetCacheItemAsync(Guid id)
        {
            return (await _roleNameCache.GetOrAddAsync(
                id.ToString(), //Cache key
                async () => await GetFromDatabaseAsync(id),
                () => new DistributedCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.Now.AddHours(24) }
            ))!;
        }

        private async Task<RoleNameCacheItem> GetFromDatabaseAsync(Guid id)
        {
            return new RoleNameCacheItem { RoleName = await _nnIdentityRoleRepository.FindNameByIdAsync(id) };
        }
    }
}
