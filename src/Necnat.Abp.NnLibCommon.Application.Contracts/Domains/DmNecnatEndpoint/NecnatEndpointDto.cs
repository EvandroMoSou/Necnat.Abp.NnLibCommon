using Necnat.Abp.NnLibCommon.Dtos;
using System;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public class NecnatEndpointDto : ConcurrencyEntityDto<Guid>
    {
        public string? DisplayName { get; set; }
        public string? Endpoint { get; set; }
        public bool? IsActive { get; set; }

        //IsAuthorization
        public bool? IsAuthorization { get; set; }
        public string? PermissionsGroupName { get; set; }

        //IsAuthServer
        public bool? IsAuthServer { get; set; }

        //IsBilling
        public bool? IsBilling { get; set; }

        //IsHierarchyComponent
        public bool? IsHierarchyComponent { get; set; }
        public short? HierarchyComponentTypeId { get; set; }
    }
}
