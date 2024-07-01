using Microsoft.AspNetCore.Components;
using Necnat.Abp.NnLibCommon.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Necnat.Abp.NnLibCommon.Blazor.Components
{
    public abstract class SelectCmptBase<TAppService, TEntityDto, TKey, TSearchInput> : SelectCmptBase<TEntityDto, TKey>
        where TAppService : ICrudAppService<TEntityDto, TKey, TSearchInput>
        where TEntityDto : IEntityDto<TKey>, new()
        where TKey : struct
        where TSearchInput : OptionalPagedAndSortedResultRequestDto, new()
    {
        [Inject] protected TAppService AppService { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            if (Data == null)
                Data = (await AppService.GetListAsync(new TSearchInput { IsPaged = false })).Items.ToList();

            if (IsAutoSelectFirst && Data != null && Data.Count > 0)
                await OnSelectedValueChangedAsync(Data.First().Id);

            _isLoading = false;
        }
    }

    public abstract class SelectCmptBase<TEntityDto, TKey> : SelectedEntityDtoCmptBase<TEntityDto, TKey>
        where TEntityDto : IEntityDto<TKey>, new()
        where TKey : struct
    {

        [Parameter]
        public bool IsAutoSelectFirst { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            if (IsAutoSelectFirst && Data != null && Data.Count > 0)
                await OnSelectedValueChangedAsync(Data.First().Id);

            _isLoading = false;
        }
    }
}
