using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Necnat.Abp.NnLibCommon.EntityFrameworkCore;

[ConnectionStringName(NnLibCommonDbProperties.ConnectionStringName)]
public class NnLibCommonDbContext : AbpDbContext<NnLibCommonDbContext>, INnLibCommonDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public NnLibCommonDbContext(DbContextOptions<NnLibCommonDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureNnLibCommon();
    }
}
