using Necnat.Abp.NnLibCommon.Dtos;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public class NecnatEndpointResultRequestDto : OptionalPagedAndSortedResultRequestDto
    {
        public string? DisplayNameContains { get; set; }
        public string? EndpointContains { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsAuthorization { get; set; }
        public bool? IsBilling { get; set; }
        public bool? IsAuthServer { get; set; }
    }
}
