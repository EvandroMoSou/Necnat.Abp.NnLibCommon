using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Necnat.Abp.NnLibCommon.EntityFrameworkCore;

public class NnLibCommonHttpApiHostMigrationsDbContext : AbpDbContext<NnLibCommonHttpApiHostMigrationsDbContext>
{
    public NnLibCommonHttpApiHostMigrationsDbContext(DbContextOptions<NnLibCommonHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureNnLibCommon();
    }
}
