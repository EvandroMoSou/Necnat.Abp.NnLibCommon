using Necnat.Abp.NnLibCommon.Domains;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Necnat.Abp.NnLibCommon.Blazor.Services
{
    public class DistributedServiceClientStore : IDistributedServiceStore
    {
        protected readonly IDistributedServiceDtoAppService _appService;

        public List<DistributedServiceModel>? List { get; set; }

        public DistributedServiceClientStore(
            IDistributedServiceDtoAppService appService)
        {
            _appService = appService;
        }

        public async Task<List<DistributedServiceModel>> GetListAsync(string? applicationName = null, string? tag = null, bool isActive = true)
        {
            var l = (await _appService.GetListAsync(new DistributedServiceResultRequestDto { IsPaged = false })).Items;

            return l
                .Where(x => (string.IsNullOrWhiteSpace(applicationName) || x.ApplicationName == applicationName)
                    && (string.IsNullOrWhiteSpace(tag) || x.Tag.StartsWith(tag))
                    && x.IsActive == isActive)
                .Select(x => new DistributedServiceModel
                {
                    ApplicationName = x.ApplicationName!,
                    Tag = x.Tag!,
                    Url = x.Url!,
                    IsActive = (bool)x.IsActive!
                }).ToList();
        }
    }
}
