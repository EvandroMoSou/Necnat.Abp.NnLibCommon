using Microsoft.Extensions.Localization;
using Necnat.Abp.NnLibCommon.Localization;
using Necnat.Abp.NnLibCommon.Permissions;
using Necnat.Abp.NnLibCommon.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Users;

namespace Necnat.Abp.NnLibCommon.Domains.DmNecnatEndpoint
{
    public class NnEndpointAppService :
        NecnatAppService<
            NnEndpoint,
            NnEndpointDto,
            Guid,
            NnEndpointResultRequestDto,
            INnEndpointRepository>,
        INnEndpointAppService
    {
        public NnEndpointAppService(
            ICurrentUser currentUser,
            IStringLocalizer<NnLibCommonResource> necnatLocalizer,
            INnEndpointRepository repository) : base(currentUser, necnatLocalizer, repository)
        {
            GetPolicyName = NnLibCommonPermissions.PrmNecnatEndpoint.Default;
            GetListPolicyName = NnLibCommonPermissions.PrmNecnatEndpoint.Default;
            CreatePolicyName = NnLibCommonPermissions.PrmNecnatEndpoint.Create;
            UpdatePolicyName = NnLibCommonPermissions.PrmNecnatEndpoint.Update;
            DeletePolicyName = NnLibCommonPermissions.PrmNecnatEndpoint.Delete;
        }

        protected override async Task<IQueryable<NnEndpoint>> CreateFilteredQueryAsync(NnEndpointResultRequestDto input)
        {
            ThrowIfIsNotNull(NnEndpointValidator.Validate(input, _necnatLocalizer));

            var q = await ReadOnlyRepository.GetQueryableAsync();

            if (!string.IsNullOrWhiteSpace(input.DisplayNameContains))
                q = q.Where(x => x.DisplayName.Contains(input.DisplayNameContains));

            if (!string.IsNullOrWhiteSpace(input.TagContains))
                q = q.Where(x => x.Tag.Contains(input.TagContains));

            if (!string.IsNullOrWhiteSpace(input.UrlUriContains))
                q = q.Where(x => x.UrlUri.Contains(input.UrlUriContains));

            if (input.IsActive != null)
                q = q.Where(x => x.IsActive == input.IsActive);

            return q;
        }

        protected override Task<NnEndpointDto> CheckCreateInputAsync(NnEndpointDto input)
        {
            ThrowIfIsNotNull(NnEndpointValidator.Validate(input, _necnatLocalizer));
            return Task.FromResult(input);
        }

        protected override Task<NnEndpointDto> CheckUpdateInputAsync(NnEndpointDto input)
        {
            ThrowIfIsNotNull(NnEndpointValidator.Validate(input, _necnatLocalizer));
            return Task.FromResult(input);
        }
    }
}
