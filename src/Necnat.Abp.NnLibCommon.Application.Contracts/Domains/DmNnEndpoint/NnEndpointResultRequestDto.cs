using Necnat.Abp.NnLibCommon.Dtos;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public class NnEndpointResultRequestDto : OptionalPagedAndSortedResultRequestDto
    {
        public string? DisplayNameContains { get; set; }
        public string? TagContains { get; set; }
        public string? UrlUriContains { get; set; }
        public bool? IsActive { get; set; }
    }
}
