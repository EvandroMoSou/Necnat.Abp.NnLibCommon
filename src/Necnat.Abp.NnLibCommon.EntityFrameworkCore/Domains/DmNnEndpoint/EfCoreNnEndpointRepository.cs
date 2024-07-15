using Necnat.Abp.NnLibCommon.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public class EfCoreNnEndpointRepository : EfCoreRepository<INnLibCommonDbContext, NnEndpoint, Guid>, INnEndpointRepository
    {
        public EfCoreNnEndpointRepository(IDbContextProvider<INnLibCommonDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
    }
}
