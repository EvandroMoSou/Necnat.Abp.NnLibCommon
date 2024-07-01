using Necnat.Abp.NnLibCommon.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public class EfCoreNecnatEndpointRepository : EfCoreRepository<INnLibCommonDbContext, NecnatEndpoint, Guid>, INecnatEndpointRepository
    {
        public EfCoreNecnatEndpointRepository(IDbContextProvider<INnLibCommonDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
    }
}
