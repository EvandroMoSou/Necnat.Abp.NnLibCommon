using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Necnat.Abp.NnLibCommon;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(NnLibCommonDomainSharedModule)
)]
public class NnLibCommonDomainModule : AbpModule
{

}
