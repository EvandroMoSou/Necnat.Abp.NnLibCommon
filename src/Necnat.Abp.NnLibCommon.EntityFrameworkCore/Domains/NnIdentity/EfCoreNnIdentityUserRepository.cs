using Necnat.Abp.NnLibCommon.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    public class EfCoreNnIdentityUserRepository : EfCoreRepository<INnLibCommonDbContext, IdentityUser, Guid>, INnIdentityUserRepository
    {
        public EfCoreNnIdentityUserRepository(IDbContextProvider<INnLibCommonDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
    }
}
