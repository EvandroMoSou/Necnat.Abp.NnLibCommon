using Microsoft.EntityFrameworkCore;
using Necnat.Abp.NnLibCommon.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public class EfCoreNecnatEndpointRepository : EfCoreRepository<INnLibCommonDbContext, NecnatEndpoint, Guid>, INecnatEndpointRepository
    {
        public EfCoreNecnatEndpointRepository(IDbContextProvider<INnLibCommonDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<NecnatEndpoint> GetByPermissionsGroupNameAsync(string permissionsGroupName)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(x => x.PermissionsGroupName == permissionsGroupName).FirstOrDefaultAsync() ?? throw new EntityNotFoundException(typeof(NecnatEndpoint), permissionsGroupName);
        }
    }
}
