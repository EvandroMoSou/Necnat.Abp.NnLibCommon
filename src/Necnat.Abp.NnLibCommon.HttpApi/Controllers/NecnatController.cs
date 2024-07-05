using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Mvc;

namespace Necnat.Abp.NnLibCommon.Controllers
{
    public class NecnatController<TAppService, TEntityDto, TKey, TGetListInput> : AbpControllerBase
        where TAppService : ICrudAppService<TEntityDto, TKey, TGetListInput>
    {

        protected TAppService AppService { get; }

        public NecnatController(TAppService appService)
        {
            AppService = appService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<TEntityDto>> GetListAsync(TGetListInput input)
        {
            return AppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<TEntityDto> GetAsync(TKey id)
        {
            return AppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<TEntityDto> CreateAsync(TEntityDto input)
        {
            return AppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<TEntityDto> UpdateAsync(TKey id, TEntityDto input)
        {
            return AppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(TKey id)
        {
            return AppService.DeleteAsync(id);
        }
    }
}
