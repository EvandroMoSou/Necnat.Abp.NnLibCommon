using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Necnat.Abp.NnLibCommon.MongoDB;

[ConnectionStringName(NnLibCommonDbProperties.ConnectionStringName)]
public class NnLibCommonMongoDbContext : AbpMongoDbContext, INnLibCommonMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureNnLibCommon();
    }
}
