using Blazorise;
using Microsoft.AspNetCore.Components;
using Necnat.Abp.NnLibCommon.Blazor.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Components;

namespace Necnat.Abp.NnLibCommon.Blazor.Components
{
    public abstract class ListWrapperCmptBase<TAppService, TEntityDto, TKey, TSearchInput> : AbpComponentBase
        where TAppService : ICrudAppService<TEntityDto, TKey, TSearchInput>
        where TEntityDto : IEntityDto<TKey>, new()
        where TKey : struct
        where TSearchInput : PagedAndSortedResultRequestDto, new()
    {
        [Inject] protected TAppService AppService { get; set; } = default!;

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public Action<bool>? DisabledChanged { get; set; }

        [Parameter]
        public Action<ValidatorEventArgs>? Validator { get; set; }

        [Parameter]
        public int PageSize { get; set; } = 5;

        [Parameter]
        public ObservableCollection<TEntityDto>? SelectedValueList { get; set; }

        [Parameter]
        public EventCallback<ObservableCollection<TEntityDto>?> SelectedValueListChanged { get; set; }

        [Parameter]
        public Func<TEntityDto, Task<TKey>>? AddMethod { get; set; }

        [Parameter]
        public Func<TKey, Task>? RemoveMethod { get; set; }

        public bool IsLoading;
        public bool IsLoadingAutomatic = true;
        protected TEntityDto? _entity;

        protected override void OnParametersSet()
        {
            if (SelectedValueList == null)
                SelectedValueList = new ObservableCollection<TEntityDto>();

            if (_entity == null)
                _entity = new TEntityDto();

            if (IsLoadingAutomatic)
                IsLoading = false;
        }

        public virtual async Task AddAsync()
        {
            if (_entity != null)
            {
                if (AddMethod != null)
                    _entity.Id = await AddMethod.Invoke(_entity);

                await InvokeAsync(async () =>
                {
                    SelectedValueList!.Add(_entity);
                    await SelectedValueListChanged.InvokeAsync(SelectedValueList);

                    _entity = new TEntityDto();
                });
            }
        }

        public virtual async Task RemoveAsync(TEntityDto dto)
        {
            if (RemoveMethod != null)
                await RemoveMethod.Invoke(dto.Id);

            await InvokeAsync(async () =>
            {
                SelectedValueList!.Remove(dto);
                await SelectedValueListChanged.InvokeAsync(SelectedValueList);
            });
        }
    }
}
