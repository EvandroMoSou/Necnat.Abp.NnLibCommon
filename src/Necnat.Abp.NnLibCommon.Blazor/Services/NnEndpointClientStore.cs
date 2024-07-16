using Necnat.Abp.NnLibCommon.Domains;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Necnat.Abp.NnLibCommon.Blazor.Services
{
    public class NnEndpointClientStore : INnEndpointStore
    {
        protected readonly INnEndpointAppService _appService;

        public List<NnEndpointModel>? NnEndpointList { get; set; }

        public NnEndpointClientStore(
            INnEndpointAppService appService)
        {
            _appService = appService;
        }

        public async Task<List<NnEndpointModel>> GetListAsync(bool isActive = true)
        {
            if (NnEndpointList == null)
                NnEndpointList = (await _appService.GetListAsync(new NnEndpointResultRequestDto { IsPaged = false, IsActive = true })).Items.Where(x => x.IsActive == isActive).Select(
                    x => new NnEndpointModel
                    {
                        DisplayName = x.DisplayName ?? string.Empty,
                        Tag = x.Tag ?? string.Empty,
                        UrlUri = x.UrlUri ?? string.Empty,
                        IsActive = true
                    }).ToList();

            return NnEndpointList;
        }

        public async Task<List<NnEndpointModel>> GetListByTagAsync(string tag, bool isActive = true)
        {
            if (NnEndpointList == null)
                NnEndpointList = (await _appService.GetListAsync(new NnEndpointResultRequestDto { IsPaged = false, IsActive = true })).Items
                    .Where(x => x.IsActive == isActive && (x.Tag != null && x.Tag.StartsWith(tag))).Select(
                    x => new NnEndpointModel
                    {
                        DisplayName = x.DisplayName ?? string.Empty,
                        Tag = x.Tag ?? string.Empty,
                        UrlUri = x.UrlUri ?? string.Empty,
                        IsActive = true
                    }).ToList();

            return NnEndpointList;
        }
    }
}
