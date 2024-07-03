using Necnat.Abp.NnLibCommon.Domains;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Necnat.Abp.NnLibCommon.Blazor.Services
{
    public class NecnatEndpointService : INecnatEndpointService
    {
        protected readonly INecnatEndpointAppService _appService;

        public IReadOnlyList<NecnatEndpointDto>? NecnatEndpointList { get; set; }

        public NecnatEndpointService(
            INecnatEndpointAppService appService)
        {
            _appService = appService;
        }

        public async Task<IReadOnlyList<NecnatEndpointDto>> GetListAsync()
        {
            if (NecnatEndpointList == null)
                NecnatEndpointList = (await _appService.GetListAsync(new NecnatEndpointResultRequestDto { IsPaged = false, IsActive = true })).Items;

            return NecnatEndpointList;
        }

        public async Task<NecnatEndpointDto> GetByPermissionsGroupNameAsync(string permissionsGroupName)
        {
            var list = await GetListAsync();
            return list.Where(x => x.PermissionsGroupName == permissionsGroupName).First();
        }
    }
}
