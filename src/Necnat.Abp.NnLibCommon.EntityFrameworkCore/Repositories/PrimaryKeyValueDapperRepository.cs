using Necnat.Abp.NnLibCommon.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace Necnat.Abp.NnLibCommon.Repositories
{
    public abstract class PrimaryKeyValueDapperRepository<TDbContext, TEntity, TKey> : StatementDapperRepository<TDbContext, TEntity, TKey>
        where TDbContext : IEfCoreDbContext
        where TEntity : class, IGetSetEntity<TKey>
    {
        protected string _primaryKeyValue;

        protected PrimaryKeyValueDapperRepository(IDbContextProvider<TDbContext> dbContextProvider, IDbStatement dbStatement, string primaryKeyValue) : base(dbContextProvider, dbStatement)
        {
            _primaryKeyValue = primaryKeyValue;
        }

        protected override async Task<string> GetInsertStatementAsync(List<string> lColumn)
        {
            return _dbStatement.GenerateInsertWithPrimaryKeyValueStatement(_tableName, _primaryKeyName, lColumn, _primaryKeyValue);
        }
    }
}
