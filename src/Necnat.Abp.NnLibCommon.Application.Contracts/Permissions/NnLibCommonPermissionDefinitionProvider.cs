using Necnat.Abp.NnLibCommon.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Necnat.Abp.NnLibCommon.Permissions;

public class NnLibCommonPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(NnLibCommonPermissions.GroupName, L("Permission:NnLibCommon"));

        var pgNecnatEndpoint = myGroup.AddPermission(NnLibCommonPermissions.PrmNecnatEndpoint.Default, L("Permission:NecnatEndpoint:Default"));
        pgNecnatEndpoint.AddChild(NnLibCommonPermissions.PrmNecnatEndpoint.Create, L("Permission:NecnatEndpoint:Create"));
        pgNecnatEndpoint.AddChild(NnLibCommonPermissions.PrmNecnatEndpoint.Update, L("Permission:NecnatEndpoint:Update"));
        pgNecnatEndpoint.AddChild(NnLibCommonPermissions.PrmNecnatEndpoint.Delete, L("Permission:NecnatEndpoint:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<NnLibCommonResource>(name);
    }
}
