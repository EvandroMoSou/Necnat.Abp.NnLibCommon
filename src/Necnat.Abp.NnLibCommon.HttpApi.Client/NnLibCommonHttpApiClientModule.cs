using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Necnat.Abp.NnLibCommon;

[DependsOn(
    typeof(NnLibCommonApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class NnLibCommonHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(NnLibCommonApplicationContractsModule).Assembly,
            NnLibCommonRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<NnLibCommonHttpApiClientModule>();
        });

    }
}
