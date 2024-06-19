//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Necnat.Abp.NnLibCommon.Blazor.Pages
//{
//    public abstract class ListingPage<
//        TSearchComponent,
//        TAppService,
//        TGetOutputDto,
//        TKey,
//        TSearchInput>
//        : ListingPage<TSearchComponent, TAppService, TGetOutputDto, TKey, PagedAndSortedResultRequestDto, TGetOutputDto, TSearchInput>
//        where TSearchComponent : SearchComponent<TAppService,
//            TGetOutputDto,
//            TKey,
//            TSearchInput>, new()
//        where TAppService : ISearchAppService<
//            TGetOutputDto,
//            TKey,
//            TSearchInput>
//        where TSearchInput : OptionalPagedAndSortedResultRequestDto, new()
//        where TGetOutputDto : IEntityDto<TKey>
//    {

//    }

//    public abstract class ListingPage<
//        TSearchComponent,
//        TAppService,
//        TGetOutputDto,
//        TKey,
//        TCreateUpdateInput,
//        TSearchInput>
//        : ListingPage<TSearchComponent, TAppService, TGetOutputDto, TKey, PagedAndSortedResultRequestDto, TCreateUpdateInput, TSearchInput>
//        where TSearchComponent : SearchComponent<TAppService,
//            TGetOutputDto,
//            TKey,
//            TCreateUpdateInput,
//            TSearchInput>, new()
//        where TAppService : ISearchAppService<
//            TGetOutputDto,
//            TKey,
//            TCreateUpdateInput,
//            TSearchInput>
//        where TSearchInput : OptionalPagedAndSortedResultRequestDto, new()
//        where TGetOutputDto : IEntityDto<TKey>
//    {

//    }

//    public abstract class ListingPage<
//        TSearchComponent,
//        TAppService,
//        TGetOutputDto,
//        TKey,
//        TGetListInput,
//        TCreateUpdateInput,
//        TSearchInput>
//        : AbpComponentBase
//        where TSearchComponent : SearchComponent<TAppService,
//            TGetOutputDto,
//            TKey,
//            TGetListInput,
//            TCreateUpdateInput,
//            TSearchInput>, new()
//        where TAppService : ISearchAppService<
//            TGetOutputDto,
//            TKey,
//            TGetListInput,
//            TCreateUpdateInput,
//            TSearchInput>
//        where TSearchInput : OptionalPagedAndSortedResultRequestDto, new()
//        where TGetOutputDto : IEntityDto<TKey>
//    {
//        [Inject] protected TAppService? AppService { get; set; }
//        [Inject] protected NavigationManager? NavigationManager { get; set; }
//        [Inject] protected IPageHistoryState? PageHistoryState { get; set; }
//        [Inject] protected SweetAlertService? Swal { get; set; }

//        public bool IsLoading = true;
//        protected bool IsLoadingAutomatic = true;
//        protected TSearchComponent SearchComponent = new TSearchComponent();

//        protected string? CreatePolicyName { get; set; }
//        protected string? UpdatePolicyName { get; set; }
//        protected string? DeletePolicyName { get; set; }

//        public bool HasCreatePermission { get; set; }
//        public bool HasUpdatePermission { get; set; }
//        public bool HasDeletePermission { get; set; }

//        protected string ListingPageUri { get; set; } = "/";
//        protected string CreatePageUri { get; set; } = "/";
//        protected string UpdatePageUri { get; set; } = "/";

//        protected async override Task OnInitializedAsync()
//        {
//            await SetPermissionsAsync();
//            await InvokeAsync(StateHasChanged);
//            if (IsLoadingAutomatic)
//                IsLoading = false;
//        }

//        protected async override Task OnAfterRenderAsync(bool firstRender)
//        {
//            if (firstRender)
//            {
//                var goBackPage = PageHistoryState!.GetGoBackPage();
//                if (goBackPage != null && goBackPage.Uri == ListingPageUri)
//                {
//                    if (goBackPage.Data != null && goBackPage.Data is TSearchInput)
//                        SearchComponent.SearchInput = (TSearchInput)goBackPage.Data;

//                    PageHistoryState!.RemoveLastPage();
//                }
//                else
//                    PageHistoryState!.Clear();
//            }

//            await base.OnAfterRenderAsync(firstRender);
//        }

//        protected virtual async Task SetPermissionsAsync()
//        {
//            if (CreatePolicyName != null)
//            {
//                HasCreatePermission = await AuthorizationService.IsGrantedAsync(CreatePolicyName);
//            }

//            if (UpdatePolicyName != null)
//            {
//                HasUpdatePermission = await AuthorizationService.IsGrantedAsync(UpdatePolicyName);
//            }

//            if (DeletePolicyName != null)
//            {
//                HasDeletePermission = await AuthorizationService.IsGrantedAsync(DeletePolicyName);
//            }
//        }

//        protected virtual async Task CheckPolicyAsync(string? policyName)
//        {
//            if (string.IsNullOrEmpty(policyName))
//            {
//                return;
//            }

//            await AuthorizationService.CheckAsync(policyName);
//        }

//        protected virtual async Task CheckDeletePolicyAsync()
//        {
//            await CheckPolicyAsync(DeletePolicyName);
//        }

//        protected void ToCreatePage()
//        {
//            PageHistoryState!.AddPageToHistory(ListingPageUri, SearchComponent.GetSearchInputBag());
//            NavigationManager!.NavigateTo(CreatePageUri);
//        }

//        protected void ToUpdatePage(TGetOutputDto getOutputDto)
//        {
//            PageHistoryState!.AddPageToHistory(ListingPageUri, SearchComponent.GetSearchInputBag());
//            NavigationManager!.NavigateTo(UpdatePageUri + "?id=" + getOutputDto.Id);
//        }

//        protected virtual async Task ConfirmDeleteEntityAsync(TGetOutputDto entity)
//        {
//            var result = await Swal!.FireAsync(new SweetAlertOptions
//            {
//                Title = GetConfirmDeleteEntityTitle(entity),
//                Text = GetConfirmDeleteEntityText(entity),
//                Icon = SweetAlertIcon.Warning,
//                ReverseButtons = true,
//                ShowCancelButton = true,
//                ConfirmButtonText = L["Yes, delete it."],
//                CancelButtonText = L["No, cancel."]
//            });

//            if (!string.IsNullOrEmpty(result.Value))
//            {
//                await DeleteEntityAsync(entity);
//            }
//        }

//        protected virtual async Task DeleteEntityAsync(TGetOutputDto entity)
//        {
//            try
//            {
//                await CheckDeletePolicyAsync();
//                await OnDeletingEntityAsync();
//                await AppService.DeleteAsync(entity.Id);
//                await OnDeletedEntityAsync();
//            }
//            catch (Exception ex)
//            {
//                await HandleErrorAsync(ex);
//            }
//        }

//        protected virtual Task OnDeletingEntityAsync()
//        {
//            return Task.CompletedTask;
//        }

//        protected virtual async Task OnDeletedEntityAsync()
//        {
//            await SearchComponent.RefreshAsync();
//        }

//        protected virtual string GetConfirmDeleteEntityTitle(TGetOutputDto entity)
//        {
//            return L["Are you sure?"];
//        }

//        protected virtual string GetConfirmDeleteEntityText(TGetOutputDto entity)
//        {
//            return L["Are you sure you want to delete this record?"];
//        }
//    }
//}
