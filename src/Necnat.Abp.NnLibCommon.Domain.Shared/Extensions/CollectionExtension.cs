using System.Collections.Generic;

namespace Necnat.Abp.NnLibCommon.Extensions
{
    public static class CollectionExtension
    {
        public static bool AddIfNotIsNullOrWhiteSpace(this ICollection<string> source, string? item)
        {
            if (string.IsNullOrWhiteSpace(item))
                return false;

            source.Add(item!);
            return true;
        }
    }
}
