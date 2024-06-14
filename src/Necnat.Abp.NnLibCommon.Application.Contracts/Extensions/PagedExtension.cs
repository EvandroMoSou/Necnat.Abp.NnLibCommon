using Necnat.Abp.NnLibCommon.Dtos;
using Necnat.Abp.NnLibCommon.Repositories;
using Volo.Abp.Application.Dtos;

namespace Necnat.Abp.NnLibCommon.Extensions
{
    public static class PagedExtension
    {
        public static PagedResultDto<T> ToPagedResultDto<T>(this PagedResult<T> p)
        {
            return new PagedResultDto<T>(p.TotalCount, p.Items);
        }

        public static PagedResultRequest ToPagedResultRequest(this PagedAndSortedResultRequestDto p)
        {
            return new PagedResultRequest(p.SkipCount, p.MaxResultCount, p.Sorting);
        }

        public static PagedResultRequest ToPagedResultRequest(this OptionalPagedAndSortedResultRequestDto p)
        {
            return new PagedResultRequest(p.SkipCount, p.MaxResultCount, p.Sorting, p.IsPaged);
        }
    }
}
