using Blazorise;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Localization;
using Necnat.Abp.NnLibCommon.Blazor.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.Authorization;
using Volo.Abp.Localization;

namespace Necnat.Abp.NnLibCommon.Blazor.Pages;

public abstract class NecnatCuPageBase<
        TAppService,
        TEntityDto,
        TKey>
    : NecnatCuPageBase<
        TAppService,
        TEntityDto,
        TKey,
        PagedAndSortedResultRequestDto>
    where TAppService : ICrudAppService<
        TEntityDto,
        TKey>
    where TEntityDto : class, IEntityDto<TKey>, new()
{
}

public abstract class NecnatCuPageBase<
        TAppService,
        TEntityDto,
        TKey,
        TGetListInput>
    : NecnatCuPageBase<
        TAppService,
        TEntityDto,
        TKey,
        TGetListInput,
        TEntityDto>
    where TAppService : ICrudAppService<
        TEntityDto,
        TKey,
        TGetListInput>
    where TEntityDto : class, IEntityDto<TKey>, new()
    where TGetListInput : new()
{
}

public abstract class NecnatCuPageBase<
        TAppService,
        TEntityDto,
        TKey,
        TGetListInput,
        TCreateUpdateInput>
    : NecnatCuPageBase<
        TAppService,
        TEntityDto,
        TEntityDto,
        TKey,
        TGetListInput,
        TCreateUpdateInput>
    where TAppService : ICrudAppService<
        TEntityDto,
        TKey,
        TGetListInput,
        TCreateUpdateInput>
    where TEntityDto : IEntityDto<TKey>
    where TCreateUpdateInput : class, new()
    where TGetListInput : new()
{
}

public abstract class NecnatCuPageBase<
        TAppService,
        TGetOutputDto,
        TGetListOutputDto,
        TKey,
        TGetListInput,
        TCreateUpdateInput>
    : NecnatCuPageBase<
        TAppService,
        TGetOutputDto,
        TGetListOutputDto,
        TKey,
        TGetListInput,
        TCreateUpdateInput,
        TGetListOutputDto,
        TCreateUpdateInput>
    where TAppService : ICrudAppService<
        TGetOutputDto,
        TGetListOutputDto,
        TKey,
        TGetListInput,
        TCreateUpdateInput,
        TCreateUpdateInput>
    where TGetOutputDto : IEntityDto<TKey>
    where TGetListOutputDto : IEntityDto<TKey>
    where TCreateUpdateInput : class, new()
    where TGetListInput : new()
{
}

public abstract class NecnatCuPageBase<
        TAppService,
        TGetOutputDto,
        TGetListOutputDto,
        TKey,
        TGetListInput,
        TCreateUpdateInput,
        TListViewModel,
        TCreateUpdateViewModel>
    : AbpComponentBase
    where TAppService : ICrudAppService<
        TGetOutputDto,
        TGetListOutputDto,
        TKey,
        TGetListInput,
        TCreateUpdateInput,
        TCreateUpdateInput>
    where TGetOutputDto : IEntityDto<TKey>
    where TGetListOutputDto : IEntityDto<TKey>
    where TCreateUpdateInput : class
    where TGetListInput : new()
    where TListViewModel : IEntityDto<TKey>
    where TCreateUpdateViewModel : class, new()
{
    [Inject] public IAbpEnumLocalizer AbpEnumLocalizer { get; set; } = default!;
    [Inject] protected TAppService AppService { get; set; } = default!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
    [Inject] protected IPageHistoryState PageHistoryState { get; set; } = default!;
    [Inject] protected IStringLocalizer<AbpUiResource> UiLocalizer { get; set; } = default!;

    protected TKey EntityId = default!;
    protected TCreateUpdateViewModel Entity;
    protected Validations? CreateValidationsRef;
    protected Validations? EditValidationsRef;
    protected List<BreadcrumbItem> BreadcrumbItems = new List<BreadcrumbItem>(2);

    protected string? CreatePolicyName { get; set; }
    protected string? UpdatePolicyName { get; set; }

    public bool HasCreatePermission { get; set; }
    public bool HasUpdatePermission { get; set; }

    protected NecnatCuPageBase()
    {
        Entity = new TCreateUpdateViewModel();
    }

    protected async override Task OnInitializedAsync()
    {
        await TrySetPermissionsAsync();

        var uri = NavigationManager!.ToAbsoluteUri(NavigationManager.Uri);
        if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("id", out var id))
        {
            if (typeof(TKey) == typeof(Guid))
                EntityId = (TKey)Convert.ChangeType(new Guid(id.ToString()!), typeof(TKey));
            else
                EntityId = (TKey)Convert.ChangeType(id.ToString(), typeof(TKey));
        }

        if (EntityId != null && !EntityId.Equals(default(TKey)))
        {
            var dto = await AppService.GetAsync(EntityId);
            Entity = MapToEntity(dto);
        }
        else
        {
            Entity = InitializeCreateUpdateViewModel();
        }

        await InvokeAsync(StateHasChanged);
    }

    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task TrySetPermissionsAsync()
    {
        if (IsDisposed)
        {
            return;
        }

        await SetPermissionsAsync();
    }

    protected virtual async Task SetPermissionsAsync()
    {
        if (CreatePolicyName != null)
        {
            HasCreatePermission = await AuthorizationService.IsGrantedAsync(CreatePolicyName);
        }

        if (UpdatePolicyName != null)
        {
            HasUpdatePermission = await AuthorizationService.IsGrantedAsync(UpdatePolicyName);
        }
    }

    protected virtual TCreateUpdateViewModel InitializeCreateUpdateViewModel()
    {
        return new TCreateUpdateViewModel();
    }

    protected virtual TCreateUpdateViewModel MapToEntity(TGetOutputDto entityDto)
    {
        return ObjectMapper.Map<TGetOutputDto, TCreateUpdateViewModel>(entityDto);
    }

    protected virtual TCreateUpdateInput MapToCreateUpdateInput(TCreateUpdateViewModel viewModel)
    {
        if (typeof(TCreateUpdateInput) == typeof(TCreateUpdateViewModel))
        {
            return viewModel.As<TCreateUpdateInput>();
        }

        return ObjectMapper.Map<TCreateUpdateViewModel, TCreateUpdateInput>(viewModel);
    }

    protected void GoBackPage()
    {
        if (PageHistoryState.CanGoBack())
            NavigationManager.NavigateTo(PageHistoryState.GetGoBackPage()?.Uri ?? "/");
    }

    protected virtual async Task SaveEntityAsync()
    {
        try
        {
            var validate = true;
            if (CreateValidationsRef != null)
            {
                validate = await CreateValidationsRef.ValidateAll();
            }
            if (validate)
            {
                await OnSavingEntityAsync();

                if (EntityId == null || EntityId.Equals(default))
                    await CheckCreatePolicyAsync();
                else
                    await CheckUpdatePolicyAsync();

                var createInput = MapToCreateUpdateInput(Entity);

                if (EntityId == null || EntityId.Equals(default))
                    await AppService.CreateAsync(createInput);
                else
                    await AppService.UpdateAsync(EntityId, createInput);

                await OnSavedEntityAsync();
            }
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected virtual Task OnSavingEntityAsync()
    {
        return Task.CompletedTask;
    }

    protected virtual async Task OnSavedEntityAsync()
    {
        await Notify.Success(L["SavedSuccessfully"]);
    }

    protected virtual async Task CheckCreatePolicyAsync()
    {
        await CheckPolicyAsync(CreatePolicyName);
    }

    protected virtual async Task CheckUpdatePolicyAsync()
    {
        await CheckPolicyAsync(UpdatePolicyName);
    }

    /// <summary>
    /// Calls IAuthorizationService.CheckAsync for the given <paramref name="policyName"/>.
    /// Throws <see cref="AbpAuthorizationException"/> if given policy was not granted for the current user.
    ///
    /// Does nothing if <paramref name="policyName"/> is null or empty.
    /// </summary>
    /// <param name="policyName">A policy name to check</param>
    protected virtual async Task CheckPolicyAsync(string? policyName)
    {
        if (string.IsNullOrEmpty(policyName))
        {
            return;
        }

        await AuthorizationService.CheckAsync(policyName);
    }

    protected virtual ValueTask SetBreadcrumbItemsAsync()
    {
        return ValueTask.CompletedTask;
    }

    protected virtual ValueTask SetToolbarItemsAsync()
    {
        return ValueTask.CompletedTask;
    }
}