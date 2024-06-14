using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Necnat.Abp.NnLibCommon.EntityFrameworkCore;

[DependsOn(
    typeof(NnLibCommonDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class NnLibCommonEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<NnLibCommonDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
        });
    }
}
