using System;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Necnat.Abp.NnLibCommon.Entities
{
    [Serializable]
    public abstract class ConcurrencyAuditedEntity : AuditedEntity, IHasConcurrencyStamp
    {
        [DisableAuditing]
        public virtual string ConcurrencyStamp { get; set; }

        protected ConcurrencyAuditedEntity()
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
        }
    }

    [Serializable]
    public abstract class ConcurrencyAuditedEntity<TKey> : AuditedEntity<TKey>, IHasConcurrencyStamp
    {
        [DisableAuditing]
        public virtual string ConcurrencyStamp { get; set; }

        protected ConcurrencyAuditedEntity()
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
        }

        protected ConcurrencyAuditedEntity(TKey id)
            : base(id)
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
        }
    }
}
