using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Necnat.Abp.NnLibCommon;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class NnLibCommonInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<NnLibCommonInstallerModule>();
        });
    }
}
