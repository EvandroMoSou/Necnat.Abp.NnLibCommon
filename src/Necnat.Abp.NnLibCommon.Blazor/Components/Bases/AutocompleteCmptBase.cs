using Blazorise;
using Blazorise.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Necnat.Abp.NnLibCommon.Blazor.Components
{
    public abstract class AutocompleteCmptBase<TAppService, TEntityDto, TKey, TSearchInput> : SelectedEntityDtoCmptBase<TAppService, TEntityDto, TKey, TSearchInput>
        where TAppService : ICrudAppService<TEntityDto, TKey, TSearchInput>
        where TEntityDto : IEntityDto<TKey>, new()
        where TKey : struct
        where TSearchInput : PagedAndSortedResultRequestDto, new()
    {
        [Parameter]
        public int Qty { get; set; } = 5;

        [Parameter]
        public int SearchValueMinLength { get; set; } = 3;

        [Parameter]
        public Action<ValidatorEventArgs> Validator { get; set; } = null!;

        public virtual TSearchInput GetSearchInput(string searchValue)
        {
            return new TSearchInput();
        }

        public virtual async Task OnHandleReadData(AutocompleteReadDataEventArgs autocompleteReadDataEventArgs)
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
}
