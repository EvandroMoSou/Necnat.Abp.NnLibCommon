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
    public class DistributedServiceAppService :
        NecnatAppService<
            DistributedService,
            DistributedServiceDto,
            Guid,
            DistributedServiceResultRequestDto,
            IDistributedServiceRepository>,
        IDistributedServiceDtoAppService
    {
        public DistributedServiceAppService(
            ICurrentUser currentUser,
            IStringLocalizer<NnLibCommonResource> necnatLocalizer,
            IDistributedServiceRepository repository) : base(currentUser, necnatLocalizer, repository)
        {
            GetPolicyName = NnLibCommonPermissions.PrmDistributedService.Default;
            GetListPolicyName = NnLibCommonPermissions.PrmDistributedService.Default;
            CreatePolicyName = NnLibCommonPermissions.PrmDistributedService.Create;
            UpdatePolicyName = NnLibCommonPermissions.PrmDistributedService.Update;
            DeletePolicyName = NnLibCommonPermissions.PrmDistributedService.Delete;
        }

        protected override async Task<IQueryable<DistributedService>> CreateFilteredQueryAsync(DistributedServiceResultRequestDto input)
        {
            ThrowIfIsNotNull(DistributedServiceValidator.Validate(input, _necnatLocalizer));

            var q = await ReadOnlyRepository.GetQueryableAsync();

            if (!string.IsNullOrWhiteSpace(input.ApplicationNameContains))
                q = q.Where(x => x.ApplicationName.Contains(input.ApplicationNameContains));

            if (!string.IsNullOrWhiteSpace(input.TagContains))
                q = q.Where(x => x.Tag.Contains(input.TagContains));

            if (!string.IsNullOrWhiteSpace(input.UrlContains))
                q = q.Where(x => x.Url.Contains(input.UrlContains));

            if (input.IsActive != null)
                q = q.Where(x => x.IsActive == input.IsActive);

            return q;
        }

        protected override Task<DistributedServiceDto> CheckCreateInputAsync(DistributedServiceDto input)
        {
            ThrowIfIsNotNull(DistributedServiceValidator.Validate(input, _necnatLocalizer));
            return Task.FromResult(input);
        }

        protected override Task<DistributedServiceDto> CheckUpdateInputAsync(DistributedServiceDto input)
        {
            ThrowIfIsNotNull(DistributedServiceValidator.Validate(input, _necnatLocalizer));
            return Task.FromResult(input);
        }
    }
}
