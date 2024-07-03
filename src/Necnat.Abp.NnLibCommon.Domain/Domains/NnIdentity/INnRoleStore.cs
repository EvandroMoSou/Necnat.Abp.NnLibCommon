using System;
using System.Threading.Tasks;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    public interface INnRoleStore
    {
        Task<string> GetNameByIdAsync(Guid id);
    }
}
