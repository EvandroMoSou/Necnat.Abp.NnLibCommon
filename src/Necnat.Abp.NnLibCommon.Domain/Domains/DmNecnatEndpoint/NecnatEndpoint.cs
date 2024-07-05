using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Necnat.Abp.NnLibCommon.Domains
{

    public class NecnatEndpoint : AuditedAggregateRoot<Guid>
    {
        public string DisplayName { get; set; } = string.Empty;
        public string Endpoint { get; set; } = string.Empty;
        public string PermissionsGroupName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsAuthorization { get; set; }
        public bool IsAuthServer { get; set; }
        public bool IsBilling { get; set; }
    }
}