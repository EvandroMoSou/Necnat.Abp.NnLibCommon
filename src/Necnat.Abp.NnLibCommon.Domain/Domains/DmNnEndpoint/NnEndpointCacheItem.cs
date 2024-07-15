using System;
using System.Collections.Generic;

namespace Necnat.Abp.NnLibCommon.Domains
{
    [Serializable]
    public class NnEndpointCacheItem
    {
        public List<NnEndpoint> NnEndpointList { get; set; } = new List<NnEndpoint>();

        public NnEndpointCacheItem()
        {

        }

        public NnEndpointCacheItem(List<NnEndpoint> necnatEndpointList)
        {
            NnEndpointList = necnatEndpointList;
        }
    }
}
