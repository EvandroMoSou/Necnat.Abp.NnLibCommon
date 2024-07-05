using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Necnat.Abp.NnLibCommon.Controllers
{
    public class NecnatController<TAppService, TEntityDto, TKey, TGetListInput> : NecnatControllerWithoutUpdate<TAppService, TEntityDto, TKey, TGetListInput>
        where TAppService : ICrudAppService<TEntityDto, TKey, TGetListInput>
    {
        public NecnatController(TAppService appService) : base(appService)
        {
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<TEntityDto> UpdateAsync(TKey id, TEntityDto input)
        {
            return AppService.UpdateAsync(id, input);
        }
    }
}
