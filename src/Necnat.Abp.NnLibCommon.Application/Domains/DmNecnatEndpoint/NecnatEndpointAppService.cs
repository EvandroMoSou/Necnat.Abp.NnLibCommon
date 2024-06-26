﻿using Microsoft.Extensions.Localization;
using Necnat.Abp.NnLibCommon.Localization;
using Necnat.Abp.NnLibCommon.Permissions;
using Necnat.Abp.NnLibCommon.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Users;

namespace Necnat.Abp.NnLibCommon.Domains.DmNecnatEndpoint
{
    public class NecnatEndpointAppService :
        NecnatAppService<
            NecnatEndpoint,
            NecnatEndpointDto,
            Guid,
            NecnatEndpointResultRequestDto,
            INecnatEndpointRepository>,
        INecnatEndpointAppService
    {
        public NecnatEndpointAppService(
            ICurrentUser currentUser,
            IStringLocalizer<NnLibCommonResource> necnatLocalizer,
            INecnatEndpointRepository repository) : base(currentUser, necnatLocalizer, repository)
        {
            GetPolicyName = NnLibCommonPermissions.PrmNecnatEndpoint.Default;
            GetListPolicyName = NnLibCommonPermissions.PrmNecnatEndpoint.Default;
            CreatePolicyName = NnLibCommonPermissions.PrmNecnatEndpoint.Create;
            UpdatePolicyName = NnLibCommonPermissions.PrmNecnatEndpoint.Update;
            DeletePolicyName = NnLibCommonPermissions.PrmNecnatEndpoint.Delete;
        }

        protected override async Task<IQueryable<NecnatEndpoint>> CreateFilteredQueryAsync(NecnatEndpointResultRequestDto input)
        {
            ThrowIfIsNotNull(NecnatEndpointValidator.Validate(input, _necnatLocalizer));

            var q = await ReadOnlyRepository.GetQueryableAsync();

            if (!string.IsNullOrWhiteSpace(input.DisplayNameContains))
                q = q.Where(x => x.DisplayName.Contains(input.DisplayNameContains));

            if (!string.IsNullOrWhiteSpace(input.EndpointContains))
                q = q.Where(x => x.Endpoint.Contains(input.EndpointContains));

            if (input.IsActive != null)
                q = q.Where(x => x.IsActive == input.IsActive);

            if (input.IsAuthz != null)
                q = q.Where(x => x.IsAuthz == input.IsAuthz);

            if (input.IsBilling != null)
                q = q.Where(x => x.IsBilling == input.IsBilling);

            if (input.IsUser != null)
                q = q.Where(x => x.IsUser == input.IsUser);

            return q;
        }

        protected override Task<NecnatEndpointDto> CheckCreateInputAsync(NecnatEndpointDto input)
        {
            ThrowIfIsNotNull(NecnatEndpointValidator.Validate(input, _necnatLocalizer));
            return Task.FromResult(input);
        }

        protected override Task<NecnatEndpointDto> CheckUpdateInputAsync(NecnatEndpointDto input)
        {
            ThrowIfIsNotNull(NecnatEndpointValidator.Validate(input, _necnatLocalizer));
            return Task.FromResult(input);
        }
    }
}
