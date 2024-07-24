using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Necnat.Abp.NnLibCommon.Domains.DmDistributedService;
using Necnat.Abp.NnLibCommon.Extensions;
using Necnat.Abp.NnLibCommon.Localization;
using Necnat.Abp.NnLibCommon.Services;
using Necnat.Abp.NnLibCommon.Validators;
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
    public class NnIdentityUserAppService : NecnatAppService<IdentityUser, NnIdentityUserDto, Guid, NnIdentityUserResultRequestDto, INnIdentityUserRepository, NullValidator<NnIdentityUserDto, NnIdentityUserResultRequestDto>>, INnIdentityUserAppService
    {
        protected readonly IConfiguration _configuration;
        protected readonly IDistributedServiceStore _distributedServiceStore;
        protected readonly IHttpClientFactory _httpClientFactory;

        protected string _applicationName;
        protected const string _controllerbase = "nn-lib-common/nn-identity-user";

        public NnIdentityUserAppService(
            ICurrentUser currentUser,
            IStringLocalizer<NnLibCommonResource> necnatLocalizer,
            INnIdentityUserRepository repository,

            IConfiguration configuration,
            IDistributedServiceStore distributedServiceStore,
            IHttpClientFactory httpClientFactory) : base(currentUser, necnatLocalizer, repository)
        {
            _configuration = configuration;
            _distributedServiceStore = distributedServiceStore;
            _httpClientFactory = httpClientFactory;

            _applicationName = _configuration["DistributedService:ApplicationName"]!;

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

        public override async Task<NnIdentityUserDto> GetAsync(Guid id)
        {
            var distributedServiceList = await _distributedServiceStore.GetListAsync(tag: NnLibCommonDistributedServiceConsts.NnIdentityUserTag);
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
                                return (await httpResponseMessage.Content.ReadAsStringAsync()).DeserializeCaseInsensitive<NnIdentityUserDto>()!;
                        }
                        catch { }
                    }
                }
            }

            throw new EntityNotFoundException(typeof(IdentityUserDto), id);
        }

        public virtual async Task<NnIdentityUserDto> GetMyAsync(Guid id)
        {
            ThrowIfIsNotMy(id);

            var distributedServiceList = await _distributedServiceStore.GetListAsync(tag: NnLibCommonDistributedServiceConsts.NnIdentityUserTag);
            foreach (var iDistributedService in distributedServiceList)
            {
                if (iDistributedService.ApplicationName == _applicationName)
                {
                    try
                    {
                        return await GetAsync(id);
                    }
                    catch { }
                }
                else
                {
                    using (HttpClient client = _httpClientFactory.CreateClient(NnLibCommonDistributedServiceConsts.HttpClientName))
                    {
                        try
                        {
                            var httpResponseMessage = await client.GetAsync($"{iDistributedService.Url}/api/{_controllerbase}/my");
                            if (httpResponseMessage.IsSuccessStatusCode)
                                return (await httpResponseMessage.Content.ReadAsStringAsync()).DeserializeCaseInsensitive<NnIdentityUserDto>()!;
                        }
                        catch { }
                    }
                }
            }

            throw new EntityNotFoundException(typeof(IdentityUserDto), id);
        }

        public override async Task<PagedResultDto<NnIdentityUserDto>> GetListAsync(NnIdentityUserResultRequestDto input)
        {
            var l = new List<PagedResultDto<NnIdentityUserDto>>();

            var distributedServiceList = await _distributedServiceStore.GetListAsync(tag: NnLibCommonDistributedServiceConsts.NnIdentityUserTag);
            foreach (var iDistributedService in distributedServiceList)
            {
                if (iDistributedService.ApplicationName == _applicationName)
                {
                    try
                    {
                        var result = await base.GetListAsync(input);
                        var items = result.Items.ToList();
                        items.ForEach(x => x.DistributedAppName = _applicationName);
                        l.Add(new PagedResultDto<NnIdentityUserDto>(result.TotalCount, items));
                    }
                    catch { }
                }
                else
                {
                    using (HttpClient client = _httpClientFactory.CreateClient(NnLibCommonDistributedServiceConsts.HttpClientName))
                    {
                        try
                        {
                            var httpResponseMessage = await client.PostAsJsonAsync($"{iDistributedService.Url}/api/nn-lib-common/nn-identity-user/get-list", input);
                            if (httpResponseMessage.IsSuccessStatusCode)
                                l.Add((await httpResponseMessage.Content.ReadAsStringAsync()).DeserializeCaseInsensitive<PagedResultDto<NnIdentityUserDto>>()!);
                        }
                        catch { }
                    }
                }
            }

            return new PagedResultDto<NnIdentityUserDto>(l.Sum(x => x.TotalCount), l.SelectMany(x => x.Items).ToList());
        }

        [RemoteService(false)]
        public override Task<NnIdentityUserDto> CreateAsync(NnIdentityUserDto input)
        {
            //return base.CreateAsync(input);
            throw new NotImplementedException();
        }

        [RemoteService(false)]
        public override Task<NnIdentityUserDto> UpdateAsync(Guid id, NnIdentityUserDto input)
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
