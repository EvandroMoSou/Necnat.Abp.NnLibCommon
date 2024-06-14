//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Necnat.Abp.NnLibCommon.Blazor.Components
//{
//    public abstract class SearchComponent<
//        TAppService,
//        TEntityDto,
//        TKey,
//        TSearchInput>
//        : SearchComponent<TAppService, TEntityDto, TKey, TEntityDto, TSearchInput>
//    where TAppService : ISearchAppService<
//        TEntityDto,
//        TKey,
//        TSearchInput>
//    where TSearchInput : OptionalPagedAndSortedResultRequestDto, new()
//    {

//    }

//    public abstract class SearchComponent<
//        TAppService,
//        TEntityDto,
//        TKey,
//        TCreateUpdateInput,
//        TSearchInput>
//        : SearchComponent<TAppService, TEntityDto, TKey, PagedAndSortedResultRequestDto, TCreateUpdateInput, TSearchInput>
//    where TAppService : ISearchAppService<
//        TEntityDto,
//        TKey,
//        TCreateUpdateInput,
//        TSearchInput>
//    where TSearchInput : OptionalPagedAndSortedResultRequestDto, new()
//    {

//    }

//    public abstract class SearchComponent<
//        TAppService,
//        TEntityDto,
//        TKey,
//        TGetListInput,
//        TCreateUpdateInput,
//        TSearchInput>
//        : AbpComponentBase
//    where TAppService : ISearchAppService<
//        TEntityDto,
//        TKey,
//        TGetListInput,
//        TCreateUpdateInput,
//        TSearchInput>
//    where TSearchInput : OptionalPagedAndSortedResultRequestDto, new()
//    {
//        [Inject] protected TAppService? AppService { get; set; }

//        [Parameter]
//        public int PageSize { get; set; } = LimitedResultRequestDto.DefaultMaxResultCount;

//        [Parameter]
//        public CustomDataGridSelectionMode SelectionMode { get; set; } = CustomDataGridSelectionMode.None;

//        [Parameter]
//        public TEntityDto? SelectedRow { get; set; }

//        [Parameter]
//        public EventCallback<TEntityDto?> SelectedRowChanged { get; set; }

//        [Parameter]
//        public List<TEntityDto>? SelectedRows { get; set; }

//        [Parameter]
//        public EventCallback<List<TEntityDto>?> SelectedRowsChanged { get; set; }

//        protected int CurrentPage = 1;
//        protected string? CurrentSorting;
//        public bool IsLoading = true;
//        protected bool IsLoadingAutomatic = true;
//        protected bool LoadOnInit = true;
//        protected int? TotalCount;
//        protected IReadOnlyList<TEntityDto> Entities = Array.Empty<TEntityDto>();

//        protected Validations? SearchValidationsRef;
//        public TSearchInput SearchInput = new TSearchInput();

//        protected override async Task OnInitializedAsync()
//        {
//            await base.OnInitializedAsync();
//            if (IsLoadingAutomatic)
//                IsLoading = false;
//        }

//        protected virtual async Task OnDataGridReadAsync(DataGridReadDataEventArgs<TEntityDto> e)
//        {
//            if (!LoadOnInit)
//            {
//                LoadOnInit = true;

//                Entities = new List<TEntityDto>();
//                TotalCount = 0;

//                await InvokeAsync(StateHasChanged);
//                return;
//            }

//            CurrentSorting = e.Columns
//                .Where(c => c.SortDirection != SortDirection.Default)
//                .Select(c => c.SortField + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
//                .JoinAsString(",");
//            CurrentPage = e.Page;

//            await GetEntitiesAsync();

//            await InvokeAsync(StateHasChanged);
//        }

//        protected virtual async Task GetEntitiesAsync()
//        {
//            try
//            {
//                await InvokeAsync(StateHasChanged);

//                await UpdateGetListInputAsync();
//                var result = await AppService!.SearchAsync(SearchInput);
//                Entities = result.Items;
//                TotalCount = (int?)result.TotalCount;

//                await InvokeAsync(StateHasChanged);
//            }
//            catch (Exception ex)
//            {
//                await HandleErrorAsync(ex);
//            }
//        }

//        protected virtual Task UpdateGetListInputAsync()
//        {
//            if (SearchInput is ISortedResultRequest sortedResultRequestInput)
//            {
//                sortedResultRequestInput.Sorting = CurrentSorting;
//            }

//            if (SearchInput is IPagedResultRequest pagedResultRequestInput)
//            {
//                pagedResultRequestInput.SkipCount = (CurrentPage - 1) * PageSize;
//            }

//            if (SearchInput is ILimitedResultRequest limitedResultRequestInput)
//            {
//                limitedResultRequestInput.MaxResultCount = PageSize;
//            }

//            return Task.CompletedTask;
//        }

//        protected virtual async Task SelectAllAsync()
//        {
//            if (SelectedRows != null && SelectedRows.Count > 0)
//            {
//                SelectedRows = new List<TEntityDto>();
//                await SelectedRowsChanged.InvokeAsync(SelectedRows);
//                return;
//            }

//            if (SelectionMode == CustomDataGridSelectionMode.Multiple)
//            {
//                SearchInput.IsPaged = false;

//                await GetEntitiesAsync();

//                if (SelectedRows != null && SelectedRows.Count > 0)
//                    SelectedRows = JsonUtil.RemakeList(Entities.ToList(), SelectedRows);

//                SelectedRows = Entities.ToList();
//            }

//            await SelectedRowsChanged.InvokeAsync(SelectedRows);
//            await InvokeAsync(StateHasChanged);
//        }

//        protected virtual async Task RemakeListAsync()
//        {
//            SelectedRows = JsonUtil.RemakeList(Entities.ToList(), SelectedRows!);

//            await SelectedRowsChanged.InvokeAsync(SelectedRows);
//            await InvokeAsync(StateHasChanged);
//        }

//        public virtual async Task RefreshAsync()
//        {
//            await GetEntitiesAsync();
//        }

//        public virtual async Task ClearAsync()
//        {
//            SearchInput = new TSearchInput();
//            await InvokeAsync(StateHasChanged);
//        }

//        public virtual TSearchInput GetSearchInputBag()
//        {
//            return SearchInput;
//        }
//    }
//}
