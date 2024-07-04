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

        public virtual async Task<string> GetNameByIdAsync(Guid id)
        {
            return (await GetRoleNameCacheItemAsync(id)).RoleName;
        }

        protected virtual async Task<RoleNameCacheItem> GetRoleNameCacheItemAsync(Guid id)
        {
            return (await _roleNameCache.GetOrAddAsync(
                id.ToString(), //Cache key
                async () => await GetRoleNameFromDatabaseAsync(id),
                () => new DistributedCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.Now.AddHours(6) }
            ))!;
        }

        protected virtual async Task<RoleNameCacheItem> GetRoleNameFromDatabaseAsync(Guid id)
        {
            return new RoleNameCacheItem { RoleName = await _nnIdentityRoleRepository.FindNameByIdAsync(id) };
        }

        public virtual async Task<List<string>> GetPermissionListByIdAsync(Guid id)
        {
            return (await GetPermissionListCacheItemAsync(id)).PermissionList;
        }

        protected virtual async Task<RolePermissionListCacheItem> GetPermissionListCacheItemAsync(Guid id)
        {
            return (await _rolePermissionListCache.GetOrAddAsync(
                id.ToString(), //Cache key
                async () => await GetPermissionListFromDatabaseAsync(id),
                () => new DistributedCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.Now.AddHours(6) }
            ))!;
        }

        protected virtual async Task<RolePermissionListCacheItem> GetPermissionListFromDatabaseAsync(Guid id)
        {
            return new RolePermissionListCacheItem { PermissionList = (await _permissionGrantRepository.GetListAsync("R", await GetNameByIdAsync(id))).Select(x => x.Name).ToList() };
        }

        public virtual async Task<bool> HasPermissionNameAsync(Guid id, string permissionName)
        {
            return (await GetPermissionListByIdAsync(id)).Contains(permissionName);
        }

        public virtual async Task<List<Guid>> FilterByPermissionNameAsync(List<Guid> idList, string permissionName)
        {
            var filteredList = new List<Guid>();

            foreach (var iId in idList)
                if((await GetPermissionListByIdAsync(iId)).Contains(permissionName))
                    filteredList.Add(iId);

            return filteredList;
        }
    }
}
