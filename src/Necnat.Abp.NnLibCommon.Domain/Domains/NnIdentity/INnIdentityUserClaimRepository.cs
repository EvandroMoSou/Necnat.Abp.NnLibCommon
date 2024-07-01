using System;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    public interface INnIdentityUserClaimRepository : IRepository<IdentityUserClaim, Guid>
    {

    }
}
