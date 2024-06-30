using System;
using System.Threading.Tasks;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    public interface IRoleNameService
    {
        Task<string> GetByIdAsync(Guid id);
    }
}
