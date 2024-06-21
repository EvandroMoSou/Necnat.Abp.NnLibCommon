using Blazorise.Components;
using Microsoft.AspNetCore.Components;
using Necnat.Abp.NnLibCommon.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Necnat.Abp.NnLibCommon.Blazor.Components
{
    public abstract class ObservableAutocompleteCmptBase<TAppService, TEntityDto, TKey, TSearchInput> : SelectedEntityDtoObservableCmptBase<TAppService, TEntityDto, TKey, TSearchInput>
        where TAppService : ICrudAppService<TEntityDto, TKey, TSearchInput>
        where TEntityDto : IEntityDto<TKey>
        where TKey : struct
        where TSearchInput : PagedAndSortedResultRequestDto, IIdListResultRequestDto<TKey>, IOptionalResultRequestDto, new()
    {
        public override async Task OnAfterAddAsync()
        {
            await base.OnAfterAddAsync();
            await _autocomplete!.Clear();
        }

        #region Autocomplete

        protected Autocomplete<TEntityDto, TEntityDto>? _autocomplete;

        [Parameter]
        public int Qty { get; set; } = 5;

        [Parameter]
        public int SearchValueMinLength { get; set; } = 3;

        public virtual TSearchInput GetSearchInput(string searchValue)
        {
            return new TSearchInput();
        }

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
                        var pagedResultDto = await AppService.GetListAsync(searchInput);
                        Data = pagedResultDto.Items.ToList();
                    }
                    else
                        Data = new List<TEntityDto>();
                }
            }
        }

        #endregion
    }
}
