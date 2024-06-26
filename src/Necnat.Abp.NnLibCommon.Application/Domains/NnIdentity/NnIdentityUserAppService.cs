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
    public class NnIdentityUserAppService : NecnatAppService<IdentityUser, IdentityUserDto, Guid, NnIdentityUserResultRequestDto, INnIdentityUserRepository>, INnIdentityUserAppService
    {
        public NnIdentityUserAppService(
            ICurrentUser currentUser,
            IStringLocalizer<NnLibCommonResource> necnatLocalizer,
            INnIdentityUserRepository repository) : base(currentUser, necnatLocalizer, repository)
        {
            GetPolicyName = IdentityPermissions.Users.Default;
            GetListPolicyName = IdentityPermissions.Users.Default;
            CreatePolicyName = IdentityPermissions.Users.Create;
            UpdatePolicyName = IdentityPermissions.Users.Update;
            DeletePolicyName = IdentityPermissions.Users.Delete;
        }

        protected override async Task<IQueryable<IdentityUser>> CreateFilteredQueryAsync(NnIdentityUserResultRequestDto input)
        {
            var q = await ReadOnlyRepository.GetQueryableAsync();

            if (input.IdList != null)
                q = q.Where(x => input.IdList.Contains(x.Id));

            if (!string.IsNullOrWhiteSpace(input.NameContains))
                q = q.Where(x => x.Name.Contains(input.NameContains));

            if (!string.IsNullOrWhiteSpace(input.NameOrUserNameContains))
                q = q.Where(x => x.Name.Contains(input.NameOrUserNameContains) || x.UserName.Contains(input.NameOrUserNameContains));

            if (!string.IsNullOrWhiteSpace(input.UserNameContains))
                q = q.Where(x => x.UserName.Contains(input.UserNameContains));

            return q;
        }

        public async Task<IdentityUserDto> GetMyAsync(Guid id)
        {
            ThrowIfIsNotMy(id);
            return await base.GetAsync(id);
        }

        [RemoteService(false)]
        public override Task<IdentityUserDto> CreateAsync(IdentityUserDto input)
        {
            //return base.CreateAsync(input);
            throw new NotImplementedException();
        }

        [RemoteService(false)]
        public override Task<IdentityUserDto> UpdateAsync(Guid id, IdentityUserDto input)
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
