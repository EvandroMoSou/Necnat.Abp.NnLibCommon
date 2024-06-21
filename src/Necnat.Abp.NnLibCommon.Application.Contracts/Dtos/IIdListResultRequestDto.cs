using System.Collections.Generic;

namespace Necnat.Abp.NnLibCommon.Dtos
{
    public interface IIdListResultRequestDto<TKey>
    {
        List<TKey>? IdList { get; set; }
    }
}
