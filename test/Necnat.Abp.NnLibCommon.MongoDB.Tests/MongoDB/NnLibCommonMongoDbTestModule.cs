using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace Necnat.Abp.NnLibCommon.MongoDB;

[DependsOn(
    typeof(NnLibCommonApplicationTestModule),
    typeof(NnLibCommonMongoDbModule)
)]
public class NnLibCommonMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
