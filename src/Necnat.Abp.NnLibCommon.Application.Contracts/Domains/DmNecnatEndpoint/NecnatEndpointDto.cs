using Necnat.Abp.NnLibCommon.Dtos;
using System;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public class NecnatEndpointDto : ConcurrencyEntityDto<Guid>
    {
        public string? DisplayName { get; set; }
        public string? Endpoint { get; set; }
        public string? PermissionsGroupName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsAuthorization { get; set; }
        public bool? IsAuthServer { get; set; }
        public bool? IsBilling { get; set; }
    }
}
