using Localization.Resources.AbpUi;
using Necnat.Abp.NnLibCommon.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Necnat.Abp.NnLibCommon;

[DependsOn(
    typeof(NnLibCommonApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class NnLibCommonHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(NnLibCommonHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<NnLibCommonResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
