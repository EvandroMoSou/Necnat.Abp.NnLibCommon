namespace Necnat.Abp.NnLibCommon.Domains.DmNnEndpoint
{
    public class NnEndpointModel
    {
        public string DisplayName { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public string UrlUri { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
