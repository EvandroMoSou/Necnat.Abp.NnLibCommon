using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Necnat.Abp.NnLibCommon.Dtos;
using Necnat.Abp.NnLibCommon.Localization;
using Necnat.Abp.NnLibCommon.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Necnat.Abp.NnLibCommon.Services;

public abstract class NecnatAppService<TEntity, TEntityDto, TKey, TRepository, TValidator>
    : NecnatAppService<TEntity, TEntityDto, TKey, OptionalPagedAndSortedResultRequestDto, TRepository, TValidator>
    where TEntity : class, IEntity<TKey>
    where TEntityDto : class, IEntityDto<TKey>
    where TRepository : IRepository<TEntity, TKey>
    where TValidator : IValidator<TEntityDto>
{
    protected NecnatAppService(
        ICurrentUser currentUser,
        IStringLocalizer<NnLibCommonResource> necnatLocalizer,
        TRepository repository) : base(currentUser, necnatLocalizer, repository)
    {

    }
}

public abstract class NecnatAppService<TEntity, TEntityDto, TKey, TGetListInput, TRepository, TValidator>
    : NecnatAppService<TEntity, TEntityDto, TKey, TGetListInput, TEntityDto, TRepository, TValidator>
    where TEntity : class, IEntity<TKey>
    where TEntityDto : class, IEntityDto<TKey>
    where TGetListInput : OptionalPagedAndSortedResultRequestDto
    where TRepository : IRepository<TEntity, TKey>
    where TValidator : IValidator<TEntityDto, TGetListInput>
{
    protected NecnatAppService(
        ICurrentUser currentUser,
        IStringLocalizer<NnLibCommonResource> necnatLocalizer,
        TRepository repository) : base(currentUser, necnatLocalizer, repository)
    {

    }
}

public abstract class NecnatAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TRepository, TValidator>
    : NecnatAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput, TRepository, TValidator>
    where TEntity : class, IEntity<TKey>
    where TGetListInput : OptionalPagedAndSortedResultRequestDto
    where TCreateInput : class, IEntityDto<TKey>
    where TRepository : IRepository<TEntity, TKey>
    where TValidator : IValidator<TCreateInput, TGetListInput>
{
    protected NecnatAppService(
        ICurrentUser currentUser,
        IStringLocalizer<NnLibCommonResource> necnatLocalizer,
        TRepository repository) : base(currentUser, necnatLocalizer, repository)
    {

    }
}

public abstract class NecnatAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput, TRepository, TValidator>
    : NecnatAppService<TEntity, TEntityDto, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput, TRepository, TValidator>
    where TEntity : class, IEntity<TKey>
    where TGetListInput : OptionalPagedAndSortedResultRequestDto
    where TCreateInput : class, IEntityDto<TKey>
    where TUpdateInput : class, IEntityDto<TKey>    
    where TRepository : IRepository<TEntity, TKey>
    where TValidator : IValidator<TCreateInput, TUpdateInput, TGetListInput>
{
    protected NecnatAppService(
        ICurrentUser currentUser,
        IStringLocalizer<NnLibCommonResource> necnatLocalizer,
        TRepository repository) : base(currentUser, necnatLocalizer, repository)
    {

    }

    protected override Task<TEntityDto> MapToGetListOutputDtoAsync(TEntity entity)
    {
        return MapToGetOutputDtoAsync(entity);
    }

    protected override TEntityDto MapToGetListOutputDto(TEntity entity)
    {
        return MapToGetOutputDto(entity);
    }
}

public abstract class NecnatAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput, TRepository, TValidator>
    : CrudAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    where TEntity : class, IEntity<TKey>
    where TGetListInput : OptionalPagedAndSortedResultRequestDto
    where TCreateInput : class, IEntityDto<TKey>
    where TUpdateInput : class, IEntityDto<TKey>
    where TRepository : IRepository<TEntity, TKey>
    where TValidator : IValidator<TCreateInput, TUpdateInput, TGetListInput>
{
    protected readonly ICurrentUser _currentUser;
    protected readonly IStringLocalizer<NnLibCommonResource> _necnatLocalizer;
    protected new TRepository Repository { get; }

    protected NecnatAppService(
        ICurrentUser currentUser,
        IStringLocalizer<NnLibCommonResource> necnatLocalizer,
        TRepository repository) : base(repository)
    {
        _necnatLocalizer = necnatLocalizer;
        _currentUser = currentUser;

        Repository = repository;
    }

    #region Get

    public override async Task<TGetOutputDto> GetAsync(TKey id)
    {
        await CheckGetPolicyAsync();

        var entity = await GetEntityByIdAsync(id);
        entity = await AfterGetAsync(entity);

        return await MapToGetOutputDtoAsync(entity);
    }

    protected virtual Task<TEntity> AfterGetAsync(TEntity entity)
    {
        return Task.FromResult(entity);
    }

    #endregion

    #region GetList

    [HttpPost]
    public override async Task<PagedResultDto<TGetListOutputDto>> GetListAsync(TGetListInput input)
    {
        await CheckGetListPolicyAsync();
        ThrowIfIsNotNull(TValidator.Validate(input, new Dictionary<string, IStringLocalizer> { { ValidatorConsts.StringLocalizerNecnat, _necnatLocalizer }, { ValidatorConsts.StringLocalizerApp, L } }));

        input = await BeforeGetListAsync(input);

        var query = await CreateFilteredQueryAsync(input);
        var totalCount = await AsyncExecuter.CountAsync(query);

        var entities = new List<TEntity>();
        var entityDtos = new List<TGetListOutputDto>();

        if (totalCount > 0)
        {
            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            entities = await AsyncExecuter.ToListAsync(query);
            entities = await AfterGetListAsync(entities);

            entityDtos = await MapToGetListOutputDtosAsync(entities);
            entityDtos = await AfterGetListAsync(entityDtos);
        }

        return new PagedResultDto<TGetListOutputDto>(
            totalCount,
            entityDtos
        );
    }

    protected virtual Task<TGetListInput> BeforeGetListAsync(TGetListInput input)
    {
        return Task.FromResult(input);
    }

    protected virtual Task<List<TEntity>> AfterGetListAsync(List<TEntity> entityList)
    {
        return Task.FromResult(entityList);
    }

    protected virtual Task<List<TGetListOutputDto>> AfterGetListAsync(List<TGetListOutputDto> entityDtoList)
    {
        return Task.FromResult(entityDtoList);
    }

    #endregion

    #region Create

    public override async Task<TGetOutputDto> CreateAsync(TCreateInput input)
    {
        await CheckCreatePolicyAsync();
        input = await CheckCreateInputAsync(input);

        var entity = await MapToEntityAsync(input);
        entity = await CheckCreateMappedEntityAsync(entity, input);

        TryToSetTenantId(entity);

        entity = await Repository.InsertAsync(entity, autoSave: true);
        entity = await CheckCreateInsertedEntityAsync(entity, input);

        return await MapToGetOutputDtoAsync(entity);
    }

    protected virtual Task<TCreateInput> CheckCreateInputAsync(TCreateInput input)
    {
        ThrowIfIsNotNull(TValidator.ValidateCreate(input, new Dictionary<string, IStringLocalizer> { { ValidatorConsts.StringLocalizerNecnat, _necnatLocalizer }, { ValidatorConsts.StringLocalizerApp, L } }));
        return Task.FromResult(input);
    }

    protected virtual Task<TEntity> CheckCreateMappedEntityAsync(TEntity mappedEntity, TCreateInput? input = null)
    {
        return Task.FromResult(mappedEntity);
    }

    protected virtual Task<TEntity> CheckCreateInsertedEntityAsync(TEntity insertedEntity, TCreateInput? input = null)
    {
        return Task.FromResult(insertedEntity);
    }

    #endregion

    #region Update

    public override async Task<TGetOutputDto> UpdateAsync(TKey id, TUpdateInput input)
    {
        await CheckUpdatePolicyAsync();
        input = await CheckUpdateInputAsync(input);

        var entity = await GetEntityByIdAsync(id);
        entity = await CheckUpdateDbEntityAsync(entity, input);

        if (id == null || id.Equals(default(TKey)) || !id.Equals(input.Id))
            throw new UserFriendlyException("Id is not valid.");

        await MapToEntityAsync(input, entity);
        entity = await CheckUpdateMappedEntityAsync(entity, input);

        await Repository.UpdateAsync(entity, autoSave: true);
        entity = await CheckUpdateUpdatedEntityAsync(entity, input);

        return await MapToGetOutputDtoAsync(entity);
    }

    protected virtual Task<TUpdateInput> CheckUpdateInputAsync(TUpdateInput input)
    {
        ThrowIfIsNotNull(TValidator.ValidateUpdate(input, new Dictionary<string, IStringLocalizer> { { ValidatorConsts.StringLocalizerNecnat, _necnatLocalizer }, { ValidatorConsts.StringLocalizerApp, L } }));
        return Task.FromResult(input);
    }

    protected virtual Task<TEntity> CheckUpdateDbEntityAsync(TEntity dbEntity, TUpdateInput? input = null)
    {
        return Task.FromResult(dbEntity);
    }

    protected virtual Task<TEntity> CheckUpdateMappedEntityAsync(TEntity mappedEntity, TUpdateInput? input = null)
    {
        return Task.FromResult(mappedEntity);
    }

    protected virtual Task<TEntity> CheckUpdateUpdatedEntityAsync(TEntity updatedEntity, TUpdateInput? input = null)
    {
        return Task.FromResult(updatedEntity);
    }

    #endregion

    #region Delete

    protected override async Task DeleteByIdAsync(TKey id)
    {
        var entity = await GetEntityByIdAsync(id);
        entity = await CheckDeleteDbEntityAsync(entity);

        await Repository.DeleteAsync(entity);
    }

    protected virtual Task<TEntity> CheckDeleteDbEntityAsync(TEntity dbEntity)
    {
        return Task.FromResult(dbEntity);
    }

    #endregion

    protected void ThrowIfIsNotNull(List<string>? errors)
    {
        if (errors != null && errors.Count > 0)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Validation errors:");
            foreach (var error in errors)
            {
                sb.AppendLine(" - " + error);
                throw new UserFriendlyException(sb.ToString());
            }
        }
    }

    protected void ThrowIfIsNotMy(Guid? id)
    {
        if(id == null)
            throw new UserFriendlyException("Personal API.");

        if (_currentUser.Id != id.Value )
            throw new UserFriendlyException("Personal API.");
    }

    protected override IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query, TGetListInput input)
    {
        if (input.IsPaged)
            return base.ApplyPaging(query, input);

        return query;
    }
}
