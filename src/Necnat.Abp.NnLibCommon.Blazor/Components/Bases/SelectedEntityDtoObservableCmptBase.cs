﻿using Microsoft.AspNetCore.Components;
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
    public abstract class SelectedEntityDtoObservableCmptBase<TAppService, TEntityDto, TKey, TSearchInput> : SelectedEntityDtoObservableCmptBase<TEntityDto, TKey>
        where TAppService : ICrudAppService<TEntityDto, TKey, TSearchInput>
        where TEntityDto : IEntityDto<TKey>
        where TKey : struct
        where TSearchInput : PagedAndSortedResultRequestDto, IIdListResultRequestDto<TKey>, IOptionalResultRequestDto, new()
    {
        [Inject] protected TAppService AppService { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            if (SelectedKeyList != null)
            {
                if (Data == null)
                    Data = (await AppService.GetListAsync(new TSearchInput { IdList = SelectedKeyList, IsPaged = false })).Items.ToList();

                SelectedEntityDtoList = new ObservableCollection<TEntityDto>(Data.Where(x => SelectedKeyList.Contains(x.Id)).ToList());
                await SelectedEntityDtoListChanged.InvokeAsync(SelectedEntityDtoList);
            }

            if (SelectedEntityDtoList != null)
            {
                if (Data == null)
                    Data = SelectedEntityDtoList.ToList();

                SelectedKeyList = SelectedEntityDtoList.Select(x => x.Id).ToList();
                await SelectedKeyListChanged.InvokeAsync(SelectedKeyList);
            }
            else
                SelectedEntityDtoList = new ObservableCollection<TEntityDto>();

            _isLoading = false;
        }
    }

    public abstract class SelectedEntityDtoObservableCmptBase<TEntityDto, TKey> : AbpComponentBase
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
        public int PageSize { get; set; } = 5;

        [Parameter]
        public List<TKey>? SelectedKeyList { get; set; }

        [Parameter]
        public EventCallback<List<TKey>?> SelectedKeyListChanged { get; set; }

        [Parameter]
        public ObservableCollection<TEntityDto>? SelectedEntityDtoList { get; set; }

        [Parameter]
        public EventCallback<ObservableCollection<TEntityDto>?> SelectedEntityDtoListChanged { get; set; }

        [Parameter]
        public Func<TEntityDto, Task<TKey>>? AddMethod { get; set; }

        [Parameter]
        public Func<TKey, Task>? RemoveMethod { get; set; }

        protected TEntityDto? _internalSelectedValue;
        protected bool _isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            if (SelectedKeyList != null && Data != null)
            {
                SelectedEntityDtoList = new ObservableCollection<TEntityDto>(Data.Where(x => SelectedKeyList.Contains(x.Id)).ToList());
                await SelectedEntityDtoListChanged.InvokeAsync(SelectedEntityDtoList);
            }

            if (SelectedEntityDtoList != null)
            {
                if (Data == null)
                    Data = SelectedEntityDtoList.ToList();

                SelectedKeyList = SelectedEntityDtoList.Select(x => x.Id).ToList();
                await SelectedKeyListChanged.InvokeAsync(SelectedKeyList);
            }
            else
                SelectedEntityDtoList = new ObservableCollection<TEntityDto>();

            _isLoading = false;
        }

        public virtual async Task ReinitializeAsync()
        {
            await OnInitializedAsync();
        }

        public virtual async Task AddAsync()
        {
            if (_internalSelectedValue != null)
            {
                if (AddMethod != null)
                    _internalSelectedValue.Id = await AddMethod.Invoke(_internalSelectedValue);

                await InvokeAsync(async () =>
                {
                    SelectedEntityDtoList!.Add(_internalSelectedValue);

                    SelectedKeyList = SelectedEntityDtoList.Select(x => x.Id).ToList();
                    await SelectedKeyListChanged.InvokeAsync(SelectedKeyList);
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
                SelectedEntityDtoList!.Remove(dto);

                SelectedKeyList = SelectedEntityDtoList.Select(x => x.Id).ToList();
                await SelectedKeyListChanged.InvokeAsync(SelectedKeyList);
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
