using Necnat.Abp.NnLibCommon.Dtos;
using System;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    public class NnIdentityUserResultRequestDto : IdListOptionalPagedAndSortedResultRequestDto<Guid>
    {
        public string? NameContains { get; set; }
        public string? NameOrUserNameContains { get; set; }
        public string? UserNameContains { get; set; }
    }
}
