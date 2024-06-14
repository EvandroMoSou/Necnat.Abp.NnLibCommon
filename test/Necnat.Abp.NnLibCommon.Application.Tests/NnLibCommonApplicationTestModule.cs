using Volo.Abp.Modularity;

namespace Necnat.Abp.NnLibCommon;

[DependsOn(
    typeof(NnLibCommonApplicationModule),
    typeof(NnLibCommonDomainTestModule)
    )]
public class NnLibCommonApplicationTestModule : AbpModule
{

}
