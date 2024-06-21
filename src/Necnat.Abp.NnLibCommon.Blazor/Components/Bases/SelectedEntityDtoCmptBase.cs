using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Components;

namespace Necnat.Abp.NnLibCommon.Blazor.Components
{
    public abstract class SelectedEntityDtoCmptBase<TAppService, TEntityDto, TKey, TSearchInput> : SelectedEntityDtoCmptBase<TEntityDto, TKey>
        where TAppService : ICrudAppService<TEntityDto, TKey, TSearchInput>
        where TEntityDto : IEntityDto<TKey>, new()
        where TKey : struct
        where TSearchInput : PagedAndSortedResultRequestDto, new()
    {
        [Inject] protected TAppService AppService { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            if (SelectedKey != null)
            {
                if (Data == null)
                    Data = new List<TEntityDto> { await AppService.GetAsync((TKey)SelectedKey!) };

                SelectedEntityDto = Data.Where(x => x.Id.Equals(SelectedKey)).First();
                await SelectedEntityDtoChanged.InvokeAsync(SelectedEntityDto);
            }

            if (SelectedEntityDto != null)
            {
                if (Data == null)
                    Data = new List<TEntityDto> { SelectedEntityDto };

                SelectedKey = SelectedEntityDto.Id;
                await SelectedKeyChanged.InvokeAsync(SelectedKey);
            }

            _isLoading = false;
        }
    }

    public abstract class SelectedEntityDtoCmptBase<TEntityDto, TKey> : AbpComponentBase
        where TEntityDto : IEntityDto<TKey>, new()
        where TKey : struct
    {
        [Parameter]
        public List<TEntityDto>? Data { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public Action<bool>? DisabledChanged { get; set; }

        [Parameter]
        public TKey? SelectedKey { get; set; }

        [Parameter]
        public EventCallback<TKey?> SelectedKeyChanged { get; set; }

        [Parameter]
        public TEntityDto? SelectedEntityDto { get; set; }

        [Parameter]
        public EventCallback<TEntityDto?> SelectedEntityDtoChanged { get; set; }

        protected TEntityDto? _internalSelectedValue;
        protected async Task OnSelectedValueChangedAsync(TEntityDto? value)
        {
            _internalSelectedValue = value;

            if (_internalSelectedValue == null)
            {
                SelectedKey = null;
                SelectedEntityDto = default;
            }
            else
            {
                SelectedKey = _internalSelectedValue.Id;
                if (Data != null)
                    SelectedEntityDto = Data.Where(x => x.Id.Equals(_internalSelectedValue.Id)).FirstOrDefault();
            }

            await SelectedKeyChanged.InvokeAsync(SelectedKey);
            await SelectedEntityDtoChanged.InvokeAsync(SelectedEntityDto);
        }

        protected bool _isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            if (SelectedKey != null)
                await OnSelectedValueChangedAsync(new TEntityDto { Id = (TKey)SelectedKey });
            else if (SelectedEntityDto != null)
                await OnSelectedValueChangedAsync(SelectedEntityDto);

            _isLoading = false;
        }

        public virtual async Task ClearAsync()
        {
            SelectedKey = null;
            SelectedEntityDto = default;

            await SelectedKeyChanged.InvokeAsync(SelectedKey);
            await SelectedEntityDtoChanged.InvokeAsync(SelectedEntityDto);
        }
    }
}
