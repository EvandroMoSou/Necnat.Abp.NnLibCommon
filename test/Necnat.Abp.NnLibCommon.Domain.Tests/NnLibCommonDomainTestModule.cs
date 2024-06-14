using Volo.Abp.Modularity;

namespace Necnat.Abp.NnLibCommon;

[DependsOn(
    typeof(NnLibCommonDomainModule),
    typeof(NnLibCommonTestBaseModule)
)]
public class NnLibCommonDomainTestModule : AbpModule
{

}
