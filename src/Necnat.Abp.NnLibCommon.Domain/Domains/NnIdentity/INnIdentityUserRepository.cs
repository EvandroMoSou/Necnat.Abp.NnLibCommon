using System;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    public interface INnIdentityUserRepository : IRepository<IdentityUser, Guid>
    {

    }
}
