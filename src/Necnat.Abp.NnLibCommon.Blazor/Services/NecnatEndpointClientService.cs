using Necnat.Abp.NnLibCommon.Domains;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Necnat.Abp.NnLibCommon.Blazor.Services
{
    public class NecnatEndpointClientService : INecnatEndpointClientService
    {
        protected readonly INecnatEndpointAppService _appService;

        public List<NecnatEndpointDto>? NecnatEndpointList { get; set; }

        public NecnatEndpointClientService(
            INecnatEndpointAppService appService)
        {
            _appService = appService;
        }

        public async Task<List<NecnatEndpointDto>> GetListAsync()
        {
            if (NecnatEndpointList == null)
                NecnatEndpointList = (await _appService.GetListAsync(new NecnatEndpointResultRequestDto { IsPaged = false, IsActive = true })).Items.ToList();

            return NecnatEndpointList;
        }
    }
}
