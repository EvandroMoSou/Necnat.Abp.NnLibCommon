using System.ComponentModel;

namespace Necnat.Abp.NnLibCommon.Enums
{
    public enum SqlCommandType
    {
        [Description("INSERT")]
        Insert = 1,

        [Description("UPDATE")]
        Update = 2,

        [Description("DELETE")]
        Delete = 3
    }
}
