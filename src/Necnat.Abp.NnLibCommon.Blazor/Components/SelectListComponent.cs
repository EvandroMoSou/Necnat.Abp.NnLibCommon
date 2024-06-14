using Microsoft.AspNetCore.Components;
using Necnat.Abp.NnLibCommon.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Components;

namespace Necnat.Abp.NnLibCommon.Blazor.Components
{
    public abstract class SelectListComponent<TAppService, TEntityDto, TKey, TSearchInput> : SelectListComponent<TEntityDto, TKey>
        where TAppService : ICrudAppService<TEntityDto, TKey, TSearchInput>
        where TEntityDto : IEntityDto<TKey>
        where TKey : struct
        where TSearchInput : OptionalPagedAndSortedResultRequestDto, new()
    {
        [Inject] protected TAppService? AppService { get; set; }

        protected override async Task GetEntitiesAsync()
        {
            var pagedResultDto = await AppService!.GetListAsync(new TSearchInput { IsPaged = false });
            Data = pagedResultDto.Items.ToList();

            await base.GetEntitiesAsync();
        }
    }

    public abstract class SelectListComponent<TEntityDto, TKey> : AbpComponentBase
        where TEntityDto : IEntityDto<TKey>
        where TKey : struct
    {
        [Parameter]
        public List<TEntityDto>? Data { get; set; }

        [Parameter]
        public TEntityDto? SelectedValue { get; set; }

        [Parameter]
        public EventCallback<TEntityDto?> SelectedValueChanged { get; set; }

        [Parameter]
        public bool IsAutoSelectFirst { get; set; } = false;

        protected TKey? _internalSelectedValue;
        protected async Task OnSelectedValueChangedAsync(TKey? value)
        {
            _internalSelectedValue = value;

            SelectedValue = Data!.Where(x => x.Id.Equals(_internalSelectedValue)).FirstOrDefault();
            await SelectedValueChanged.InvokeAsync(SelectedValue);
        }

        public bool IsLoading = true;
        protected bool IsLoadingAutomatic = true;

        protected override async Task OnInitializedAsync()
        {
            if (Data == null)
                await GetEntitiesAsync();

            if (SelectedValue != null)
            {
                if (Data!.Any(x => x.Id.Equals(SelectedValue.Id)))
                    _internalSelectedValue = SelectedValue.Id;
                else
                    _internalSelectedValue = default;
            }
            else if (IsAutoSelectFirst && Data != null && Data.Count > 0)
            {
                _internalSelectedValue = Data.First().Id;
                await OnSelectedValueChangedAsync(_internalSelectedValue);
            }

            await base.OnInitializedAsync();

            if (IsLoadingAutomatic)
                IsLoading = false;
        }

        protected virtual async Task GetEntitiesAsync()
        {
            if (Data != null && Data.Count == 1)
            {
                _internalSelectedValue = Data.First().Id;
                await OnSelectedValueChangedAsync(_internalSelectedValue);
            }
        }

        public virtual async Task RefreshAsync()
        {
            await GetEntitiesAsync();

            if (SelectedValue != null && Data!.Any(x => x.Id.Equals(SelectedValue.Id)))
                return;

            if (IsAutoSelectFirst && Data != null && Data.Count == 1)
            {
                _internalSelectedValue = Data.First().Id;
                await OnSelectedValueChangedAsync(_internalSelectedValue);
            }
            else
            {
                _internalSelectedValue = default;
                await OnSelectedValueChangedAsync(_internalSelectedValue);
            }
        }
    }
}
