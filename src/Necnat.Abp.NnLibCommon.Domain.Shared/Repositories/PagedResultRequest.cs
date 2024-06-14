namespace Necnat.Abp.NnLibCommon.Repositories
{
    public class PagedResultRequest
    {
        public long SkipCount { get; set; }
        public long MaxResultCount { get; set; }
        public string? Sorting { get; set; }
        public bool IsPaged { get; set; } = true;

        public PagedResultRequest()
        {
            SkipCount = 0;
            MaxResultCount = 10;
        }

        public PagedResultRequest(long skipCount, long maxResultCount, string? sorting = null, bool isPaged = true)
        {
            SkipCount = skipCount;
            MaxResultCount = maxResultCount;
            Sorting = sorting;
            IsPaged = isPaged;
        }
    }
}
