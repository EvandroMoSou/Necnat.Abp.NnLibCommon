using Necnat.Abp.NnLibCommon.Dtos;
using System;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    public class NnIdentityRoleResultRequestDto : IdListOptionalPagedAndSortedResultRequestDto<Guid>
    {
        public string? NameContains { get; set; }
    }
}
