using Necnat.Abp.NnLibCommon.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Necnat.Abp.NnLibCommon.Repositories
{
    public interface IHistoryRepository<TEntityHistory, TKey, TEntity> : IRepository<TEntityHistory, TKey>
        where TEntityHistory : class, IEntityHistory<TKey>
        where TEntity : class, IGetSetEntity<TKey>
    {
        Task<TEntity?> FindEntityAsync(DateTime changeTimeDesc, TKey baseId);
        Task<List<TEntity>> GetListEntityAsync(DateTime currentTimeDesc, List<TKey> lBaseId);
    }
}
