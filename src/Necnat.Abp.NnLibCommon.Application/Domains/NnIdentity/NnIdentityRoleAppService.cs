using Microsoft.Extensions.Localization;
using Necnat.Abp.NnLibCommon.Localization;
using Necnat.Abp.NnLibCommon.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    public class NnIdentityRoleAppService : NecnatAppService<IdentityRole, NnIdentityRoleDto, Guid, NnIdentityRoleResultRequestDto, INnIdentityRoleRepository>, INnIdentityRoleAppService
    {
        public NnIdentityRoleAppService(
            ICurrentUser currentUser,
            IStringLocalizer<NnLibCommonResource> necnatLocalizer,
            INnIdentityRoleRepository repository) : base(currentUser, necnatLocalizer, repository)
        {
            GetPolicyName = IdentityPermissions.Roles.Default;
            GetListPolicyName = IdentityPermissions.Roles.Default;
            CreatePolicyName = IdentityPermissions.Roles.Create;
            UpdatePolicyName = IdentityPermissions.Roles.Update;
            DeletePolicyName = IdentityPermissions.Roles.Delete;
        }

        protected override async Task<IQueryable<IdentityRole>> CreateFilteredQueryAsync(NnIdentityRoleResultRequestDto input)
        {
            var q = await ReadOnlyRepository.GetQueryableAsync();

            if (input.IdList != null)
                q = q.Where(x => input.IdList.Contains(x.Id));

            if (!string.IsNullOrWhiteSpace(input.NameContains))
                q = q.Where(x => x.Name.Contains(input.NameContains));

            return q;
        }

        [RemoteService(false)]
        public override Task<NnIdentityRoleDto> CreateAsync(NnIdentityRoleDto input)
        {
            //return base.CreateAsync(input);
            throw new NotImplementedException();
        }

        [RemoteService(false)]
        public override Task<NnIdentityRoleDto> UpdateAsync(Guid id, NnIdentityRoleDto input)
        {
            //return base.UpdateAsync(id, input);
            throw new NotImplementedException();
        }

        [RemoteService(false)]
        public override Task DeleteAsync(Guid id)
        {
            //return base.DeleteAsync(id);
            throw new NotImplementedException();
        }
    }
}
