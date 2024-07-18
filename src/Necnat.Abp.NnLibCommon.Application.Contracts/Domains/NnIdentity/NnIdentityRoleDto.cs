using Necnat.Abp.NnLibCommon.Dtos;
using Volo.Abp.Identity;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    public class NnIdentityRoleDto : IdentityRoleDto, IDistributedServiceDto
    {
        public string? DistributedAppName { get; set; }
    }
}
