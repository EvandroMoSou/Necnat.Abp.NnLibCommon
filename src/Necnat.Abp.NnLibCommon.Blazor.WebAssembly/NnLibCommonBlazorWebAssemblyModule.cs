using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace Necnat.Abp.NnLibCommon.Blazor.WebAssembly;

[DependsOn(
    typeof(NnLibCommonBlazorModule),
    typeof(NnLibCommonHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
    )]
public class NnLibCommonBlazorWebAssemblyModule : AbpModule
{

}
