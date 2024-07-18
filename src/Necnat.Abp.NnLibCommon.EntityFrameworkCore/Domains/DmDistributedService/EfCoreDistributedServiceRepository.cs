using Necnat.Abp.NnLibCommon.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public class EfCoreDistributedServiceRepository : EfCoreRepository<INnLibCommonDbContext, DistributedService, Guid>, IDistributedServiceRepository
    {
        public EfCoreDistributedServiceRepository(IDbContextProvider<INnLibCommonDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
    }
}
