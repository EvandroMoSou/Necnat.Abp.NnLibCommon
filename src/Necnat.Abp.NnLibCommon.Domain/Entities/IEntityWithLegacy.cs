namespace Necnat.Abp.NnLibCommon.Entities
{
    public interface IEntityWithLegacy<TKey>
        where TKey : struct
    {
        TKey? LegacyId { get; set; }
    }
}
