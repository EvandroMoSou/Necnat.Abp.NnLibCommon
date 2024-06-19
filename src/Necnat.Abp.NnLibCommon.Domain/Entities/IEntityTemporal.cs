using System;
using Volo.Abp.Domain.Entities;

namespace Necnat.Abp.NnLibCommon.Entities
{
    public interface IEntityTemporal<TKey> : IEntity<TKey>
    {
        DateTime PeriodStart { get; set; }
        DateTime? PeriodEnd { get; set; }
    }
}