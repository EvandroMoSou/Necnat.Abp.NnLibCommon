using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Necnat.Abp.NnLibCommon;

[DependsOn(
    typeof(NnLibCommonDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class NnLibCommonApplicationContractsModule : AbpModule
{

}
