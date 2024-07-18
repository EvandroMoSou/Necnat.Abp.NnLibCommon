using Necnat.Abp.NnLibCommon.Dtos;
using System;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public class DistributedServiceDto : ConcurrencyEntityDto<Guid>
    {
        public string? ApplicationName { get; set; }
        public string? Tag { get; set; }
        public string? Url { get; set; }
        public bool? IsActive { get; set; }
    }
}
