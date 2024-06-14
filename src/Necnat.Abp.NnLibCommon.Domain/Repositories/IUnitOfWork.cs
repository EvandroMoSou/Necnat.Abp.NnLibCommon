using System;

namespace Necnat.Abp.NnLibCommon.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        bool HasActiveTransaction();
        void Commit();
        void Rollback();
    }
}
