using Microsoft.AspNetCore.Components;
using Necnat.Abp.NnLibCommon.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Components;

namespace Necnat.Abp.NnLibCommon.Blazor.Components
{
    public abstract class SelectedEntityDtoListCmptBase<TAppService, TEntityDto, TKey, TSearchInput> : SelectedEntityDtoListCmptBase<TEntityDto, TKey>
        where TAppService : ICrudAppService<TEntityDto, TKey, TSearchInput>
        where TEntityDto : IEntityDto<TKey>
        where TKey : struct
        where TSearchInput : PagedAndSortedResultRequestDto, IIdListResultRequestDto<TKey>, new()
    {
        [Inject] protected TAppService AppService { get; set; } = default!;

        protected override async Task OnParametersSetAsync()
        {
            if (SelectedKeyList != null)
            {
                if (Data == null)
                    Data = (await AppService.GetListAsync(new TSearchInput { IdList = SelectedKeyList })).Items.ToList();

                SelectedEntityDtoList = Data.Where(x => SelectedKeyList.Contains(x.Id)).ToList();
                await SelectedEntityDtoListChanged.InvokeAsync(SelectedEntityDtoList);
            }

            if (SelectedEntityDtoList != null)
            {
                if (Data == null)
                    Data = SelectedEntityDtoList;

                SelectedKeyList = SelectedEntityDtoList.Select(x => x.Id).ToList();
                await SelectedKeyListChanged.InvokeAsync(SelectedKeyList);
            }

            await base.OnParametersSetAsync();
            _isLoading = false;
        }
    }

    public abstract class SelectedEntityDtoListCmptBase<TEntityDto, TKey> : AbpComponentBase
        where TEntityDto : IEntityDto<TKey>
        where TKey : struct
    {
        [Parameter]
        public List<TEntityDto>? Data { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public Action<bool>? DisabledChanged { get; set; }

        [Parameter]
        public List<TKey>? SelectedKeyList { get; set; }

        [Parameter]
        public EventCallback<List<TKey>?> SelectedKeyListChanged { get; set; }

        [Parameter]
        public List<TEntityDto>? SelectedEntityDtoList { get; set; }

        [Parameter]
        public EventCallback<List<TEntityDto>?> SelectedEntityDtoListChanged { get; set; }

        [Parameter]
        public Func<TEntityDto, Task<TKey>>? AddMethod { get; set; }

        [Parameter]
        public Func<TKey, Task>? RemoveMethod { get; set; }

        protected TEntityDto? _internalSelectedValue;
        public ObservableCollection<TEntityDto>? _internalSelectedValueList;
        protected bool _isLoading = true;

        protected override async Task OnParametersSetAsync()
        {
            if (SelectedKeyList != null && Data != null)
            {
                SelectedEntityDtoList = Data.Where(x => SelectedKeyList.Contains(x.Id)).ToList();
                await SelectedEntityDtoListChanged.InvokeAsync(SelectedEntityDtoList);
            }

            if (SelectedEntityDtoList != null)
            {
                if (Data == null)
                    Data = SelectedEntityDtoList;

                _internalSelectedValueList = new ObservableCollection<TEntityDto>(SelectedEntityDtoList);

                SelectedKeyList = SelectedEntityDtoList.Select(x => x.Id).ToList();
                await SelectedKeyListChanged.InvokeAsync(SelectedKeyList);
            }

            await base.OnParametersSetAsync();
            _isLoading = false;
        }

        public virtual async Task AddAsync()
        {
            if (_internalSelectedValue != null)
            {
                if (AddMethod != null)
                    _internalSelectedValue.Id = await AddMethod.Invoke(_internalSelectedValue);

                await InvokeAsync(async () =>
                {
                    _internalSelectedValueList!.Add(_internalSelectedValue);

                    SelectedKeyList = _internalSelectedValueList.Select(x => x.Id).ToList();
                    await SelectedKeyListChanged.InvokeAsync(SelectedKeyList);

                    SelectedEntityDtoList = _internalSelectedValueList.ToList();
                    await SelectedEntityDtoListChanged.InvokeAsync(SelectedEntityDtoList);

                    await OnAfterAddAsync();
                });
            }
        }

        public virtual async Task RemoveAsync(TEntityDto dto)
        {
            if (RemoveMethod != null)
                await RemoveMethod.Invoke(dto.Id);

            await InvokeAsync(async () =>
            {
                _internalSelectedValueList!.Remove(dto);

                SelectedKeyList = _internalSelectedValueList.Select(x => x.Id).ToList();
                await SelectedKeyListChanged.InvokeAsync(SelectedKeyList);

                SelectedEntityDtoList = _internalSelectedValueList.ToList();
                await SelectedEntityDtoListChanged.InvokeAsync(SelectedEntityDtoList);

                await OnAfterRemoveAsync();
            });
        }

        public virtual Task OnAfterAddAsync()
        {
            _internalSelectedValue = default;
            return Task.CompletedTask;
        }

        public virtual Task OnAfterRemoveAsync()
        {
            _internalSelectedValue = default;
            return Task.CompletedTask;
        }
    }
}
