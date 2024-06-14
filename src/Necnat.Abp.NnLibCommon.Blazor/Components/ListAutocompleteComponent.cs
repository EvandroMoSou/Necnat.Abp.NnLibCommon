using Blazorise;
using Blazorise.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Components;

namespace Necnat.Abp.NnLibCommon.Blazor.Components
{
    public abstract class ListAutocompleteComponent<TAppService, TEntityDto, TKey, TSearchInput> : AbpComponentBase
        where TAppService : ICrudAppService<TEntityDto, TKey, TSearchInput>
        where TEntityDto : IEntityDto<TKey>
        where TKey : struct
        where TSearchInput : PagedAndSortedResultRequestDto, new()
    {
        [Inject] protected TAppService? AppService { get; set; }

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
        protected Autocomplete<TEntityDto, TEntityDto>? _autocomplete;

        protected override void OnParametersSet()
        {
            if (SelectedValueList == null)
                SelectedValueList = new ObservableCollection<TEntityDto>();

            if (IsLoadingAutomatic)
                IsLoading = false;
        }

        public virtual async Task AddAsync()
        {
            if (_selectedValue != null)
            {
                if (AddMethod != null)
                    _selectedValue.Id = await AddMethod.Invoke(_selectedValue);

                SelectedValueList!.Add(_selectedValue);
                await SelectedValueListChanged.InvokeAsync(SelectedValueList);

                _selectedValue = default;
                await _autocomplete!.Clear();
            }
        }

        public virtual async Task RemoveAsync(TEntityDto dto)
        {
            if (RemoveMethod != null)
                await RemoveMethod.Invoke(dto.Id);

            SelectedValueList!.Remove(dto);
            await SelectedValueListChanged.InvokeAsync(SelectedValueList);
        }

        #region Autocomplete

        [Parameter]
        public int Qty { get; set; } = 5;

        [Parameter]
        public int SearchValueMinLength { get; set; } = 3;

        protected IEnumerable<TEntityDto>? _readData;
        protected TEntityDto? _selectedValue;

        protected virtual async Task OnHandleReadDataAsync(AutocompleteReadDataEventArgs autocompleteReadDataEventArgs)
        {
            if (!autocompleteReadDataEventArgs.CancellationToken.IsCancellationRequested)
            {
                if (!autocompleteReadDataEventArgs.CancellationToken.IsCancellationRequested)
                {
                    if (autocompleteReadDataEventArgs.SearchValue.Length >= SearchValueMinLength)
                    {
                        var searchInput = GetSearchInput(autocompleteReadDataEventArgs.SearchValue);
                        searchInput.MaxResultCount = Qty;
                        var pagedResultDto = await AppService!.GetListAsync(searchInput);
                        _readData = pagedResultDto.Items;
                    }
                    else
                        _readData = new List<TEntityDto>();
                }
            }
        }

        public virtual TSearchInput GetSearchInput(string searchValue)
        {
            return new TSearchInput();
        }

        #endregion
    }
}
