using Necnat.Abp.NnLibCommon.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Necnat.Abp.NnLibCommon.Permissions;

public class NnLibCommonPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(NnLibCommonPermissions.GroupName, L("Permission:NnLibCommon"));

        var pgDistributedService = myGroup.AddPermission(NnLibCommonPermissions.PrmDistributedService.Default, L("Permission:DistributedService:Default"));
        pgDistributedService.AddChild(NnLibCommonPermissions.PrmDistributedService.Create, L("Permission:DistributedService:Create"));
        pgDistributedService.AddChild(NnLibCommonPermissions.PrmDistributedService.Update, L("Permission:DistributedService:Update"));
        pgDistributedService.AddChild(NnLibCommonPermissions.PrmDistributedService.Delete, L("Permission:DistributedService:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<NnLibCommonResource>(name);
    }
}
