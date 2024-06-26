using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    public interface INnIdentityUserRoleRepository : IRepository<IdentityUserRole>
    {
        Task<List<IdentityUserRole>> GetListByUserIdAsync(Guid userId);
        Task UpdateUserIdAsync(Guid oldUserId, Guid userId);
    }
}
