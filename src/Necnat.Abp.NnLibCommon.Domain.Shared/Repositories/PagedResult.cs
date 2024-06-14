using System.Collections.Generic;

namespace Necnat.Abp.NnLibCommon.Repositories
{
    public class PagedResult<T>
    {
        public PagedResult()
        {
            TotalCount = 0;
        }

        public PagedResult(long totalCount, IReadOnlyList<T> items)
        {
            TotalCount = totalCount;
            Items = items;
        }

        public long TotalCount { get; set; }
        public IReadOnlyList<T> Items { get; set; } = new List<T>();
    }
}
