using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Mvc;

namespace Necnat.Abp.NnLibCommon.Controllers
{
    public class NecnatControllerWithoutUpdate<TAppService, TEntityDto, TKey, TGetListInput> : AbpControllerBase
    where TAppService : ICrudAppService<TEntityDto, TKey, TGetListInput>
    {
        protected TAppService AppService { get; }

        public NecnatControllerWithoutUpdate(TAppService appService)
        {
            AppService = appService;
        }

        [HttpPost]
        [Route("get-list")]
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

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(TKey id)
        {
            return AppService.DeleteAsync(id);
        }
    }
}
