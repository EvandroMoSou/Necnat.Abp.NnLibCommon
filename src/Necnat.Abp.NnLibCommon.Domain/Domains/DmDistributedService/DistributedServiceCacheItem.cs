using System;
using System.Collections.Generic;

namespace Necnat.Abp.NnLibCommon.Domains
{
    [Serializable]
    public class DistributedServiceCacheItem
    {
        public List<DistributedService> List { get; set; } = new List<DistributedService>();

        public DistributedServiceCacheItem()
        {

        }

        public DistributedServiceCacheItem(List<DistributedService> list)
        {
            List = list;
        }
    }
}
