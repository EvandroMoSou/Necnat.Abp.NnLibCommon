using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace Necnat.Abp.NnLibCommon.MongoDB;

[DependsOn(
    typeof(NnLibCommonDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class NnLibCommonMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<NnLibCommonMongoDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
        });
    }
}
