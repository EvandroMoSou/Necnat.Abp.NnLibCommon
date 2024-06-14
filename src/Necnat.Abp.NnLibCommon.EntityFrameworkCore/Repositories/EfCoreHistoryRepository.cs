using EntityFrameworkCore.Triggered;
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
    public class EfCoreHistoryRepository<TDbContext, TEntityHistory, TKey, TEntity> : EfCoreRepository<TDbContext, TEntityHistory, TKey>, IHistoryRepository<TEntityHistory, TKey, TEntity>
        where TDbContext : IEfCoreDbContext
        where TEntityHistory : class, IEntityHistory<TKey>
        where TEntity : class, IGetSetEntity<TKey>
    {
        public EfCoreHistoryRepository(IDbContextProvider<TDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<TEntity?> FindEntityAsync(DateTime changeTimeDesc, TKey baseId)
        {
            var dbSet = await GetDbSetAsync();
            var history = await dbSet.Where(x => x.BaseId!.Equals(baseId) && x.ChangeTime <= changeTimeDesc).OrderByDescending(x => x.ChangeTime).FirstOrDefaultAsync();
            if (history == null || history.ChangeType == (int)ChangeType.Deleted)
                return null;

            var entity = JsonUtil.CloneTo<TEntityHistory, TEntity>(history);
            entity.Id = history.BaseId;

            return entity;
        }

        public async Task<List<TEntity>> GetListEntityAsync(DateTime currentTimeDesc, List<TKey> lBaseId)
        {
            var l = new List<TEntity>();
            foreach (var iBaseId in lBaseId)
            {
                var entity = await FindEntityAsync(currentTimeDesc, iBaseId);
                if (entity != null)
                    l.Add(entity);
            }

            return l;
        }
    }
}
