using System;
using Volo.Abp.Domain.Entities;

namespace Necnat.Abp.NnLibCommon.Entities
{
    public interface IEntityTemporal<TKey> : IEntity<TKey>
    {
        public DateTime PeriodStart { get; set; }
        public DateTime? PeriodEnd { get; set; }
    }
}