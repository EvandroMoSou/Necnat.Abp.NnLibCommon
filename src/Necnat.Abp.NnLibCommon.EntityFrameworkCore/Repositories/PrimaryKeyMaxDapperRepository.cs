using Necnat.Abp.NnLibCommon.Entities;
using Volo.Abp.EntityFrameworkCore;

namespace Necnat.Abp.NnLibCommon.Repositories
{
    public abstract class PrimaryKeyMaxDapperRepository<TDbContext, TEntity, TKey> : PrimaryKeyValueDapperRepository<TDbContext, TEntity, TKey>
        where TDbContext : IEfCoreDbContext
        where TEntity : class, IGetSetEntity<TKey>
    {
        protected PrimaryKeyMaxDapperRepository(IDbContextProvider<TDbContext> dbContextProvider, IDbStatement dbStatement) : base(dbContextProvider, dbStatement, string.Empty)
        {
            _primaryKeyValue = $"(SELECT MAX({_primaryKeyName}) + 1 FROM {_tableName})";
        }
    }
}
