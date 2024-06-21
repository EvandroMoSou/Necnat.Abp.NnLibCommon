using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Necnat.Abp.NnLibCommon.Dtos
{
    public class IdListPagedAndSortedResultRequestDto<TKey> : PagedAndSortedResultRequestDto, IIdListResultRequestDto<TKey>
    {
        public List<TKey>? IdList { get; set; }
    }
}
