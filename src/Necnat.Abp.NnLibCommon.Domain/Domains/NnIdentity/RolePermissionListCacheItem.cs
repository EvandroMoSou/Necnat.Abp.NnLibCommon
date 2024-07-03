using System;
using System.Collections.Generic;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    [Serializable]
    public class RolePermissionListCacheItem
    {
        public List<string> PermissionList { get; set; } = new List<string>();
    }
}
