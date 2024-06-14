using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace Necnat.Abp.NnLibCommon.Blazor.Server;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerThemingModule),
    typeof(NnLibCommonBlazorModule)
    )]
public class NnLibCommonBlazorServerModule : AbpModule
{

}
