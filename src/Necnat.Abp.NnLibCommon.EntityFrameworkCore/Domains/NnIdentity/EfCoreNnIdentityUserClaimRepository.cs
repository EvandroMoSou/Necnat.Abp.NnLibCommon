using Necnat.Abp.NnLibCommon.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    public class EfCoreNnIdentityUserClaimRepository : EfCoreRepository<NnLibCommonDbContext, IdentityUserClaim, Guid>, INnIdentityUserClaimRepository
    {
        public EfCoreNnIdentityUserClaimRepository(IDbContextProvider<NnLibCommonDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

    }
}
