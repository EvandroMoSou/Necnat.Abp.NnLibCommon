using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    public interface INnRoleStore
    {
        Task<string> GetNameByIdAsync(Guid id);
        Task<List<string>> GetPermissionListByIdAsync(Guid id);
    }
}
