using Volo.Abp.Reflection;

namespace Necnat.Abp.NnLibCommon.Permissions;

public class NnLibCommonPermissions
{
    public const string GroupName = "NnLibCommon";

    public static class PrmNecnatEndpoint
    {
        public const string Default = GroupName + ".NecnatEndpoint";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(NnLibCommonPermissions));
    }
}
