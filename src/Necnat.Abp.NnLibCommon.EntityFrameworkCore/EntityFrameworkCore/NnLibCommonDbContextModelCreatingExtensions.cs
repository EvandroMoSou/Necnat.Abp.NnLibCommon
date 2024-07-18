using Microsoft.EntityFrameworkCore;
using Necnat.Abp.NnLibCommon.Domains;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Necnat.Abp.NnLibCommon.EntityFrameworkCore;

public static class NnLibCommonDbContextModelCreatingExtensions
{
    public static void ConfigureNnLibCommon(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(NnLibCommonDbProperties.DbTablePrefix + "Questions", NnLibCommonDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */

        builder.Entity<DistributedService>(b =>
        {
            b.ToTable(NnLibCommonDbProperties.DbTablePrefix + "DistributedService",
                NnLibCommonDbProperties.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.ApplicationName).HasMaxLength(DistributedServiceConsts.MaxApplicationNameLength);
            b.Property(x => x.Tag).IsRequired().HasMaxLength(DistributedServiceConsts.MaxTagLength);
            b.Property(x => x.Url).IsRequired().HasMaxLength(DistributedServiceConsts.MaxUrlLength);
            b.Property(x => x.IsActive).IsRequired();
        });
    }
}
