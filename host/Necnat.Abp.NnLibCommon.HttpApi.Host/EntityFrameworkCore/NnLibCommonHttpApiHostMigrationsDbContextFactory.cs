using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Necnat.Abp.NnLibCommon.EntityFrameworkCore;

public class NnLibCommonHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<NnLibCommonHttpApiHostMigrationsDbContext>
{
    public NnLibCommonHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<NnLibCommonHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("NnLibCommon"));

        return new NnLibCommonHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
