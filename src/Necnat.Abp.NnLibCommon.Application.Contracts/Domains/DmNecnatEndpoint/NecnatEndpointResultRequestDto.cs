using Necnat.Abp.NnLibCommon.Dtos;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public class NecnatEndpointResultRequestDto : OptionalPagedAndSortedResultRequestDto
    {
        public string? DisplayNameContains { get; set; }
        public string? EndpointContains { get; set; }
        public bool? IsActive { get; set; }

        //IsAuthorization
        public bool? IsAuthorization { get; set; }
        public string? PermissionsGroupNameContains { get; set; }

        //IsAuthServer
        public bool? IsAuthServer { get; set; }

        //IsBilling
        public bool? IsBilling { get; set; }

        //IsHierarchyComponent
        public bool? IsHierarchyComponent { get; set; }
        public short? HierarchyComponentTypeId { get; set; }
    }
}
