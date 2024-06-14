using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Necnat.Abp.NnLibCommon;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(NnLibCommonHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class NnLibCommonConsoleApiClientModule : AbpModule
{

}
