using Necnat.Abp.NnLibCommon.Domains;
using System;
using System.Linq;

namespace Necnat.Abp.NnLibCommon.Extensions
{
    public static class NnEndpointExtension
    {
        public static bool HasParameter(this DistributedServiceModel nnEndpoint)
        {
            return nnEndpoint.Tag.Contains(":");
        }

        public static string GetParameter(this DistributedServiceModel nnEndpoint, int index)
        {
            return nnEndpoint.Tag.Split(":")[index];
        }

        public static bool IsUrl(this DistributedServiceModel nnEndpoint)
        {
            return nnEndpoint.Url.ToCharArray().Count(c => c == '/') < 3;
        }
    }
}