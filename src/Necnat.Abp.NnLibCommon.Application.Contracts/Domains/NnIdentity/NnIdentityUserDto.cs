using Necnat.Abp.NnLibCommon.Dtos;
using Volo.Abp.Identity;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    public class NnIdentityUserDto : IdentityUserDto, IDistributedServiceDto
    {
        public string? DistributedAppName { get; set; }
    }
}
