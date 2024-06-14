using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    public interface INnIdentityRoleRepository : IRepository<IdentityRole, Guid>
    {
        Task<IdentityRole?> FindByNameAsync(string name);
        Task<List<Guid>> GetListIdByNameAsync(List<string> lName);
    }
}
