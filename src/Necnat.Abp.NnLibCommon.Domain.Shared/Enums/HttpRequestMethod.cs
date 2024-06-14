using System.ComponentModel;

namespace Necnat.Abp.NnLibCommon.Enums
{
    public enum HttpRequestMethod
    {
        [Description("GET")]
        Get = 1,

        [Description("POST")]
        Post = 2,

        [Description("PUT")]
        Put = 3,

        [Description("DELETE")]
        Delete = 4,

        [Description("CONNECT")]
        Connect = 5,

        [Description("HEAD")]
        Head = 6,

        [Description("OPTIONS")]
        Options = 7,

        [Description("TRACE")]
        Trace = 8
    }
}
