using System.Data;

namespace Necnat.Abp.NnLibCommon.Repositories
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetDbConnection();
    }
}
