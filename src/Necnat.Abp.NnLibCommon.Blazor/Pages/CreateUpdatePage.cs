//using AutoMapper.Internal.Mappers;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Components;
//using Necnat.Abp.NnLibCommon.Blazor.Helpers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Volo.Abp.Application.Dtos;
//using Volo.Abp.AspNetCore.Components;

//namespace Necnat.Abp.NnLibCommon.Blazor.Pages
//{
//    public abstract class CreateUpdatePage<
//        TAppService,
//        TEntityDto,
//        TKey,
//        TSearchInput>
//        : CreateUpdatePage<TAppService, TEntityDto, TKey, PagedAndSortedResultRequestDto, TEntityDto, TSearchInput>
//            where TAppService : ISearchAppService<
//                TEntityDto,
//                TKey,
//                TEntityDto,
//                TSearchInput>
//            where TEntityDto : class, new()
//    {

//    }

//    public abstract class CreateUpdatePage<
//        TAppService,
//        TEntityDto,
//        TKey,
//        TCreateUpdateInput,
//        TSearchInput>
//        : CreateUpdatePage<TAppService, TEntityDto, TKey, PagedAndSortedResultRequestDto, TCreateUpdateInput, TSearchInput>
//            where TAppService : ISearchAppService<
//                TEntityDto,
//                TKey,
//                TCreateUpdateInput,
//                TSearchInput>
//            where TCreateUpdateInput : class, new()
//    {

//    }

//    public abstract class CreateUpdatePage<
//        TAppService,
//        TEntityDto,
//        TKey,
//        TGetListInput,
//        TCreateUpdateInput,
//        TSearchInput>
//        : AbpComponentBase
//            where TAppService : ISearchAppService<
//                TEntityDto,
//                TKey,
//                TGetListInput,
//                TCreateUpdateInput,
//                TSearchInput>
//            where TCreateUpdateInput : class, new()
//    {
//        [Inject] protected TAppService? AppService { get; set; }
//        [Inject] protected IPageHistoryState? PageHistoryState { get; set; }
//        [Inject] protected NavigationManager? NavigationManager { get; set; }
//        [Inject] protected SweetAlertService? Swal { get; set; }

//        public bool IsLoading = true;
//        protected bool IsLoadingAutomatic = true;
//        protected TKey? EntityId;
//        protected TCreateUpdateInput? Entity;

//        protected string? CreatePolicyName { get; set; }
//        protected string? UpdatePolicyName { get; set; }

//        public bool HasCreatePermission { get; set; }
//        public bool HasUpdatePermission { get; set; }

//        protected async override Task OnInitializedAsync()
//        {
//            await SetPermissionsAsync();

//            var uri = NavigationManager!.ToAbsoluteUri(NavigationManager.Uri);
//            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("id", out var id))
//            {
//                if (typeof(TKey) == typeof(Guid))
//                    EntityId = (TKey)Convert.ChangeType(new Guid(id.ToString()!), typeof(TKey));
//                else
//                    EntityId = (TKey)Convert.ChangeType(id.ToString(), typeof(TKey));
//            }

//            if (EntityId != null && !EntityId.Equals(default(TKey)))
//            {
//                var dto = await AppService!.GetAsync(EntityId);
//                Entity = MapToCreateInput(dto);
//            }
//            else
//            {
//                Entity = InitializeEntity();
//            }

//            await InvokeAsync(StateHasChanged);
//            if (IsLoadingAutomatic)
//                IsLoading = false;
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
//        }

//        protected virtual async Task CheckPolicyAsync(string? policyName)
//        {
//            if (string.IsNullOrEmpty(policyName))
//            {
//                return;
//            }

//            await AuthorizationService.CheckAsync(policyName);
//        }

//        protected virtual async Task CheckCreatePolicyAsync()
//        {
//            await CheckPolicyAsync(CreatePolicyName);
//        }

//        protected virtual async Task CheckUpdatePolicyAsync()
//        {
//            await CheckPolicyAsync(UpdatePolicyName);
//        }

//        protected void ToBackPage()
//        {
//            if (PageHistoryState!.CanGoBack())
//                NavigationManager!.NavigateTo(PageHistoryState!.GetGoBackPage()?.Uri ?? "/");
//        }

//        protected virtual TCreateUpdateInput InitializeEntity()
//        {
//            return new TCreateUpdateInput();
//        }

//        protected virtual async Task CreateEntityAsync()
//        {
//            try
//            {
//                if (!IsValid())
//                {
//                    await Swal!.FireAsync(
//                      L["Invalid Object!"],
//                      L["There are still fields that need your attention."],
//                      SweetAlertIcon.Warning);

//                    return;
//                }

//                await OnCreatingEntityAsync();

//                await CheckCreatePolicyAsync();
//                await AppService!.CreateAsync(Entity!);

//                await OnCreatedEntityAsync();
//            }
//            catch (Exception ex)
//            {
//                await HandleErrorAsync(ex);
//            }
//        }

//        protected virtual Task OnCreatingEntityAsync()
//        {
//            return Task.CompletedTask;
//        }

//        protected virtual Task OnCreatedEntityAsync()
//        {
//            ToBackPage();
//            return Task.CompletedTask;
//        }

//        protected virtual async Task UpdateEntityAsync()
//        {
//            try
//            {
//                if (!IsValid())
//                {
//                    await Swal!.FireAsync(
//                      L["Invalid Object!"],
//                      L["There are still fields that need your attention."],
//                      SweetAlertIcon.Warning);

//                    return;
//                }

//                await OnUpdatingEntityAsync();

//                await CheckUpdatePolicyAsync();
//                await AppService!.UpdateAsync(EntityId!, Entity!);

//                await OnUpdatedEntityAsync();
//            }
//            catch (Exception ex)
//            {
//                await HandleErrorAsync(ex);
//            }
//        }

//        protected virtual Task OnUpdatingEntityAsync()
//        {
//            return Task.CompletedTask;
//        }

//        protected virtual Task OnUpdatedEntityAsync()
//        {
//            ToBackPage();
//            return Task.CompletedTask;
//        }

//        protected virtual bool IsValid()
//        {
//            return true;
//        }

//        protected virtual TCreateUpdateInput MapToCreateInput(TEntityDto createViewModel)
//        {
//            if (typeof(TCreateUpdateInput) == typeof(TEntityDto))
//            {
//                return createViewModel!.As<TCreateUpdateInput>();
//            }

//            return ObjectMapper.Map<TEntityDto, TCreateUpdateInput>(createViewModel);
//        }
//    }
//}
