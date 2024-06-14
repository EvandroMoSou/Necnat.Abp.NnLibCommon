using System;
using Volo.Abp.Domain.Entities;

namespace Necnat.Abp.NnLibCommon.Entities
{
    public interface IEntityHistory<TKey> : IEntity<TKey>
    {
        TKey BaseId { get; set; }
        int ChangeType { get; set; }
        DateTime ChangeTime { get; set; }
    }
}
