using Necnat.Abp.NnLibCommon.Dtos;
using System;
using System.Collections.Generic;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    public class NnIdentityUserResultRequestDto : OptionalPagedAndSortedResultRequestDto
    {
        public List<Guid>? IdList { get; set; }
        public string? NameContains { get; set; }
        public string? NameOrUserNameContains { get; set; }
        public string? UserNameContains { get; set; }
    }
}
