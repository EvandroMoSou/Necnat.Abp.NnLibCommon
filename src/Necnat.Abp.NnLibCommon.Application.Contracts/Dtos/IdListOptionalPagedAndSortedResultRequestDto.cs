using System.Collections.Generic;

namespace Necnat.Abp.NnLibCommon.Dtos
{
    public class IdListOptionalPagedAndSortedResultRequestDto<TKey> : OptionalPagedAndSortedResultRequestDto, IIdListResultRequestDto<TKey>, IOptionalResultRequestDto
    {
        public List<TKey>? IdList { get; set; }
    }
}
