using System;
using System.Collections.Generic;

namespace Necnat.Abp.NnLibCommon.Domains
{
    [Serializable]
    public class NecnatEndpointCacheItem
    {
        public List<NecnatEndpoint> NecnatEndpointList { get; set; } = new List<NecnatEndpoint>();

        public NecnatEndpointCacheItem()
        {

        }

        public NecnatEndpointCacheItem(List<NecnatEndpoint> necnatEndpointList)
        {
            NecnatEndpointList = necnatEndpointList;
        }
    }
}
