using System;
using System.Data;

namespace Necnat.Abp.NnLibCommon.Repositories
{
    public sealed class DbSession : IDisposable
    {
        public Guid Id { get; }
        public IDbConnection Connection { get; }
        public IDbTransaction? Transaction { get; set; }

        public DbSession(IDbConnectionFactory _dbConnectionFactory)
        {
            Id = Guid.NewGuid();
            Connection = _dbConnectionFactory.GetDbConnection();
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
