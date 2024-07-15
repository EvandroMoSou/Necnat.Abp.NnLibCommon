using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public class NnEndpoint : AuditedAggregateRoot<Guid>
    {
        public string DisplayName { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public string UrlUri { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}