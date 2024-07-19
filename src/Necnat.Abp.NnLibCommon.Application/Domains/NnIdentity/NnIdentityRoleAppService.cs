using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Necnat.Abp.NnLibCommon.Domains.DmDistributedService;
using Necnat.Abp.NnLibCommon.Localization;
using Necnat.Abp.NnLibCommon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    public class NnIdentityRoleAppService : NecnatAppService<IdentityRole, NnIdentityRoleDto, Guid, NnIdentityRoleResultRequestDto, INnIdentityRoleRepository>, INnIdentityRoleAppService
    {
        protected readonly IConfiguration _configuration;
        protected readonly IDistributedServiceStore _distributedServiceStore;
        protected readonly IHttpClientFactory _httpClientFactory;

        protected string _applicationName;
        protected const string _controllerbase = "nn-lib-common/nn-identity-role";

        public NnIdentityRoleAppService(
            ICurrentUser currentUser,
            IStringLocalizer<NnLibCommonResource> necnatLocalizer,
            INnIdentityRoleRepository repository,

            IConfiguration configuration,
            IDistributedServiceStore distributedServiceStore,
            IHttpClientFactory httpClientFactory) : base(currentUser, necnatLocalizer, repository)
        {
            _configuration = configuration;
            _distributedServiceStore = distributedServiceStore;
            _httpClientFactory = httpClientFactory;

            _applicationName = _configuration["DistributedService:ApplicationName"]!;

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

        public override async Task<NnIdentityRoleDto> GetAsync(Guid id)
        {
            ThrowIfIsNotMy(id);

            var distributedServiceList = await _distributedServiceStore.GetListAsync(tag: NnLibCommonDistributedServiceConsts.NnIdentityRoleTag);
            foreach (var iDistributedService in distributedServiceList)
            {
                if (iDistributedService.ApplicationName == _applicationName)
                {
                    try
                    {
                        var result = await base.GetAsync(id);
                        result.DistributedAppName = _applicationName;
                        return result;
                    }
                    catch { }
                }
                else
                {
                    using (HttpClient client = _httpClientFactory.CreateClient(NnLibCommonDistributedServiceConsts.HttpClientName))
                    {
                        try
                        {
                            var httpResponseMessage = await client.GetAsync($"{iDistributedService.Url}/api/{_controllerbase}/{id}");
                            if (httpResponseMessage.IsSuccessStatusCode)
                                return JsonSerializer.Deserialize<NnIdentityRoleDto>(await httpResponseMessage.Content.ReadAsStringAsync())!;
                        }
                        catch { }
                    }
                }
            }

            throw new EntityNotFoundException(typeof(IdentityRoleDto), id);
        }

        public override async Task<PagedResultDto<NnIdentityRoleDto>> GetListAsync(NnIdentityRoleResultRequestDto input)
        {
            var l = new List<PagedResultDto<NnIdentityRoleDto>>();

            var distributedServiceList = await _distributedServiceStore.GetListAsync(tag: NnLibCommonDistributedServiceConsts.NnIdentityRoleTag);
            foreach (var iDistributedService in distributedServiceList)
            {
                if (iDistributedService.ApplicationName == _applicationName)
                {
                    try
                    {
                        var result = await base.GetListAsync(input);
                        var items = result.Items.ToList();
                        items.ForEach(x => x.DistributedAppName = _applicationName);
                        l.Add(new PagedResultDto<NnIdentityRoleDto>(result.TotalCount, items));
                    }
                    catch { }
                }
                else
                {
                    using (HttpClient client = _httpClientFactory.CreateClient(NnLibCommonDistributedServiceConsts.HttpClientName))
                    {
                        try
                        {
                            var httpResponseMessage = await client.PostAsJsonAsync($"{iDistributedService.Url}/api/{_controllerbase}/get-list", input);
                            if (httpResponseMessage.IsSuccessStatusCode)
                                l.Add(JsonSerializer.Deserialize<PagedResultDto<NnIdentityRoleDto>>(await httpResponseMessage.Content.ReadAsStringAsync())!);
                        }
                        catch { }
                    }
                }
            }

            return new PagedResultDto<NnIdentityRoleDto>(l.Sum(x => x.TotalCount), l.SelectMany(x => x.Items).ToList());
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
