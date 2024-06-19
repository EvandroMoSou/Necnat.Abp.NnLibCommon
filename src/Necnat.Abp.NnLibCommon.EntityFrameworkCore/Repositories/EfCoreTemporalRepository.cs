using Microsoft.EntityFrameworkCore;
using Necnat.Abp.NnLibCommon.Entities;
using Necnat.Abp.NnLibCommon.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Necnat.Abp.NnLibCommon.Repositories
{
    public class EfCoreTemporalRepository<TDbContext, TEntityTemporal, TKey, TEntity> : EfCoreRepository<TDbContext, TEntityTemporal>, ITemporalRepository<TEntityTemporal, TKey, TEntity>
        where TDbContext : IEfCoreDbContext
        where TEntityTemporal : class, IEntityTemporal<TKey>
        where TEntity : class, IGetSetEntity<TKey>
    {
        public EfCoreTemporalRepository(IDbContextProvider<TDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<TEntity?> FindByIdAsync(DateTime period, TKey id)
        {
            var dbSet = await GetDbSetAsync();
            var temporal = await dbSet.Where(x => x.Id!.Equals(id) && x.PeriodStart <= period && (x.PeriodEnd == null || x.PeriodEnd > period)).OrderBy(x => x.PeriodEnd).FirstOrDefaultAsync();
            return MapToEntity(temporal);
        }

        public async Task<List<TEntity>> GetListByIdListAsync(DateTime period, List<TKey> idList)
        {
            var dbSet = await GetDbSetAsync();
            var temporalList = await dbSet.Where(x => idList.Contains(x.Id) && x.PeriodStart <= period && (x.PeriodEnd == null || x.PeriodEnd > period)).OrderBy(x => x.PeriodEnd).ToListAsync();
            return MapToEntity(temporalList);
        }

        protected virtual TEntity? MapToEntity(TEntityTemporal? temporal)
        {
            if (temporal == null)
                return null;

            return JsonUtil.CloneTo<TEntityTemporal, TEntity>(temporal);
        }

        protected virtual List<TEntity> MapToEntity(List<TEntityTemporal> temporalList)
        {
            return JsonUtil.CloneTo<List<TEntityTemporal>, List<TEntity>>(temporalList);
        }
    }
}
