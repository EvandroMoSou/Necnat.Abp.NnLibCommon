namespace Necnat.Abp.NnLibCommon.Domains
{
    public class DistributedServiceModel
    {
        public string ApplicationName { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
