using Necnat.Abp.NnLibCommon.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Necnat.Abp.NnLibCommon.Permissions;

public class NnLibCommonPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(NnLibCommonPermissions.GroupName, L("Permission:NnLibCommon"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<NnLibCommonResource>(name);
    }
}
