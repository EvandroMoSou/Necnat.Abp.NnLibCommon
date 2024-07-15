using Necnat.Abp.NnLibCommon.Dtos;
using System;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public class NnEndpointDto : ConcurrencyEntityDto<Guid>
    {
        public string? DisplayName { get; set; }
        public string? Tag { get; set; }
        public string? UrlUri { get; set; }
        public bool? IsActive { get; set; }
    }
}
