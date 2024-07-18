using Necnat.Abp.NnLibCommon.Dtos;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public class DistributedServiceResultRequestDto : OptionalPagedAndSortedResultRequestDto
    {
        public string? ApplicationNameContains { get; set; }
        public string? TagContains { get; set; }
        public string? UrlContains { get; set; }
        public bool? IsActive { get; set; }
    }
}
