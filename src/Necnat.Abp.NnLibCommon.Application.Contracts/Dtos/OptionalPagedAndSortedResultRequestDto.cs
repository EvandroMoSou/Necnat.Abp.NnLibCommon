using Volo.Abp.Application.Dtos;

namespace Necnat.Abp.NnLibCommon.Dtos
{
    public class OptionalPagedAndSortedResultRequestDto : PagedAndSortedResultRequestDto
    {
        public bool IsPaged { get; set; } = true;
    }
}
