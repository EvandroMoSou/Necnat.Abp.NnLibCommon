using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public class NecnatEndpoint : AuditedAggregateRoot<Guid>
    {
        public string DisplayName { get; set; } = string.Empty;
        public string Endpoint { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        //IsAuthorization
        public bool IsAuthorization { get; set; }
        public string? PermissionsGroupName { get; set; }

        //IsAuthServer
        public bool IsAuthServer { get; set; }

        //IsBilling
        public bool IsBilling { get; set; }

        //IsHierarchyComponent
        public bool IsHierarchyComponent { get; set; }
        public short? HierarchyComponentTypeId { get; set; }        
    }
}