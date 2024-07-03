using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.PermissionManagement;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    public class NnRoleStore : INnRoleStore, ITransientDependency
    {
        readonly INnIdentityRoleRepository _nnIdentityRoleRepository;
        readonly IPermissionGrantRepository _permissionGrantRepository;        
        readonly IDistributedCache<RoleNameCacheItem> _roleNameCache;
        readonly IDistributedCache<RolePermissionListCacheItem> _rolePermissionListCache;

        public NnRoleStore(
            INnIdentityRoleRepository nnIdentityRoleRepository,
            IPermissionGrantRepository permissionGrantRepository,
            IDistributedCache<RoleNameCacheItem> roleNameCache,
            IDistributedCache<RolePermissionListCacheItem> rolePermissionListCache)
        {
            _nnIdentityRoleRepository = nnIdentityRoleRepository;
            _permissionGrantRepository = permissionGrantRepository;
            _roleNameCache = roleNameCache;
            _rolePermissionListCache = rolePermissionListCache;
        }

        public async Task<string> GetNameByIdAsync(Guid id)
        {
            return (await GetRoleNameCacheItemAsync(id)).RoleName;
        }

        public async Task<RoleNameCacheItem> GetRoleNameCacheItemAsync(Guid id)
        {
            return (await _roleNameCache.GetOrAddAsync(
                id.ToString(), //Cache key
                async () => await GetRoleNameFromDatabaseAsync(id),
                () => new DistributedCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.Now.AddHours(6) }
            ))!;
        }

        private async Task<RoleNameCacheItem> GetRoleNameFromDatabaseAsync(Guid id)
        {
            return new RoleNameCacheItem { RoleName = await _nnIdentityRoleRepository.FindNameByIdAsync(id) };
        }

        public async Task<List<string>> GetPermissionListByIdAsync(Guid id)
        {
            return (await GetPermissionListCacheItemAsync(id)).PermissionList;
        }

        public async Task<RolePermissionListCacheItem> GetPermissionListCacheItemAsync(Guid id)
        {
            return (await _rolePermissionListCache.GetOrAddAsync(
                id.ToString(), //Cache key
                async () => await GetPermissionListFromDatabaseAsync(id),
                () => new DistributedCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.Now.AddHours(6) }
            ))!;
        }

        private async Task<RolePermissionListCacheItem> GetPermissionListFromDatabaseAsync(Guid id)
        {
            return new RolePermissionListCacheItem { PermissionList = (await _permissionGrantRepository.GetListAsync("R", await GetNameByIdAsync(id))).Select(x => x.Name).ToList() };
        }
    }
}
