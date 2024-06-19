using Necnat.Abp.NnLibCommon.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Necnat.Abp.NnLibCommon.Repositories
{
    public interface ITemporalRepository<TEntityTemporal, TKey, TEntity> : IRepository<TEntityTemporal>
        where TEntityTemporal : class, IEntityTemporal<TKey>
        where TEntity : class, IGetSetEntity<TKey>
    {
        Task<TEntity?> FindByIdAsync(DateTime period, TKey id);
        Task<List<TEntity>> GetListByIdListAsync(DateTime period, List<TKey> idList);
    }
}