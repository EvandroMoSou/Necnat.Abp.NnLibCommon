using Volo.Abp.Application.Dtos;

namespace Necnat.Abp.NnLibCommon.Dtos
{
    public class OptionalPagedAndSortedResultRequestDto : PagedAndSortedResultRequestDto, IOptionalResultRequestDto
    {
        public bool IsPaged { get; set; } = true;
    }
}
