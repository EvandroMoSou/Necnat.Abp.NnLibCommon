using Volo.Abp.Domain.Entities;

namespace Necnat.Abp.NnLibCommon.Entities
{
    public interface IGetSetEntity<TKey> : IEntity<TKey>
    {
        new TKey Id { get; set; }
    }
}
