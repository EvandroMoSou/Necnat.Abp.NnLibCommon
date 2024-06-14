using Volo.Abp.Reflection;

namespace Necnat.Abp.NnLibCommon.Permissions;

public class NnLibCommonPermissions
{
    public const string GroupName = "NnLibCommon";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(NnLibCommonPermissions));
    }
}
