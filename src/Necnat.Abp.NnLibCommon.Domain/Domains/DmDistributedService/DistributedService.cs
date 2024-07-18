using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public class DistributedService : AuditedAggregateRoot<Guid>
    {
        public string ApplicationName { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}