using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Necnat.Abp.NnLibCommon.EntityFrameworkCore;

[ConnectionStringName(NnLibCommonDbProperties.ConnectionStringName)]
public interface INnLibCommonDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
