using Blazorise;
using Blazorise.Components;
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
    public abstract class AutocompleteCmptBase<TAppService, TEntityDto, TKey, TSearchInput> : AbpComponentBase
        where TAppService : ICrudAppService<TEntityDto, TKey, TSearchInput>
        where TEntityDto : IEntityDto<TKey>
        where TKey : struct
        where TSearchInput : PagedAndSortedResultRequestDto, new()
    {
        [Inject] protected TAppService AppService { get; set; } = default!;

        [Parameter]
        public int Qty { get; set; } = 5;

        [Parameter]
        public int SearchValueMinLength { get; set; } = 3;

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public Action<bool>? DisabledChanged { get; set; }

        [Parameter]
        public Validations? Validations { get; set; }

        [Parameter]
        public Action<ValidatorEventArgs>? Validator { get; set; }

        [Parameter]
        public TEntityDto? SelectedValue { get; set; }

        [Parameter]
        public EventCallback<TEntityDto?> SelectedValueChanged { get; set; }

        public bool IsLoading = true;
        public bool IsLoadingAutomatic = true;
        protected IEnumerable<TEntityDto>? _readData;

        protected override async Task OnInitializedAsync()
        {
            if (SelectedValue != null)
            {
                var getOutputDto = await AppService.GetAsync(SelectedValue.Id);
                _readData = new List<TEntityDto> { getOutputDto };
                SelectedValue = _readData.First();
                await SelectedValueChanged.InvokeAsync(SelectedValue);

                if (Validations != null && Validations.ValidateOnLoad)
                    await Validations.ValidateAll();
            }

            await base.OnInitializedAsync();
            if (IsLoadingAutomatic)
                IsLoading = false;
        }

        protected virtual async Task OnHandleReadData(AutocompleteReadDataEventArgs autocompleteReadDataEventArgs)
        {
            if (!autocompleteReadDataEventArgs.CancellationToken.IsCancellationRequested)
            {
                if (!autocompleteReadDataEventArgs.CancellationToken.IsCancellationRequested)
                {
                    if (autocompleteReadDataEventArgs.SearchValue.Length >= SearchValueMinLength)
                    {
                        var searchInput = GetSearchInput(autocompleteReadDataEventArgs.SearchValue);
                        searchInput.MaxResultCount = Qty;
                        var pagedResultDto = await AppService.GetListAsync(searchInput);
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

        public virtual async Task ClearAsync()
        {
            IsLoading = true;
            await InvokeAsync(StateHasChanged);

            SelectedValue = default;
            await SelectedValueChanged.InvokeAsync(SelectedValue);
            await InvokeAsync(StateHasChanged);

            IsLoading = false;
            await InvokeAsync(StateHasChanged);
        }
    }
}
