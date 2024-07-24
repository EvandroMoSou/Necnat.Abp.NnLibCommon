using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Necnat.Abp.NnLibCommon.Dtos;
using Necnat.Abp.NnLibCommon.Localization;
using Necnat.Abp.NnLibCommon.Validators;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Necnat.Abp.NnLibCommon.Services
{
    public abstract class NecnatSyncAppService<TEntity, TEntityDto, TKey, TRepository, TValidator, TSyncEntity, TSyncKey, TSyncRepository>
        : NecnatSyncAppService<TEntity, TEntityDto, TKey, OptionalPagedAndSortedResultRequestDto, TRepository, TValidator, TSyncEntity, TSyncKey, TSyncRepository>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : class, IEntityDto<TKey>
        where TRepository : IRepository<TEntity, TKey>
        where TValidator : IValidator<TEntityDto>
        where TSyncEntity : class, IEntity<TSyncKey>
        where TSyncRepository : IRepository<TSyncEntity, TSyncKey>
    {
        protected NecnatSyncAppService(
            ICurrentUser currentUser,
            IStringLocalizer<NnLibCommonResource> necnatLocalizer,
            TRepository repository,
            IConfiguration configuration,
            TSyncRepository syncRepository) : base(currentUser, necnatLocalizer, repository, configuration, syncRepository)
        {

        }
    }

    public abstract class NecnatSyncAppService<TEntity, TEntityDto, TKey, TGetListInput, TRepository, TValidator, TSyncEntity, TSyncKey, TSyncRepository>
        : NecnatSyncAppService<TEntity, TEntityDto, TKey, TGetListInput, TEntityDto, TRepository, TValidator, TSyncEntity, TSyncKey, TSyncRepository>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : class, IEntityDto<TKey>
        where TGetListInput : OptionalPagedAndSortedResultRequestDto
        where TRepository : IRepository<TEntity, TKey>
        where TValidator : IValidator<TEntityDto, TGetListInput>
        where TSyncEntity : class, IEntity<TSyncKey>
        where TSyncRepository : IRepository<TSyncEntity, TSyncKey>
    {
        protected NecnatSyncAppService(
            ICurrentUser currentUser,
            IStringLocalizer<NnLibCommonResource> necnatLocalizer,
            TRepository repository,
            IConfiguration configuration,
            TSyncRepository syncRepository) : base(currentUser, necnatLocalizer, repository, configuration, syncRepository)
        {

        }
    }

    public abstract class NecnatSyncAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TRepository, TValidator, TSyncEntity, TSyncKey, TSyncRepository>
        : NecnatSyncAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput, TRepository, TValidator, TSyncEntity, TSyncKey, TSyncRepository>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : class, IEntityDto<TKey>
        where TGetListInput : OptionalPagedAndSortedResultRequestDto
        where TCreateInput : class, IEntityDto<TKey>
        where TRepository : IRepository<TEntity, TKey>
        where TValidator : IValidator<TCreateInput, TGetListInput>
        where TSyncEntity : class, IEntity<TSyncKey>
        where TSyncRepository : IRepository<TSyncEntity, TSyncKey>

    {
        protected NecnatSyncAppService(
            ICurrentUser currentUser,
            IStringLocalizer<NnLibCommonResource> necnatLocalizer,
            TRepository repository,
            IConfiguration configuration,
            TSyncRepository syncRepository) : base(currentUser, necnatLocalizer, repository, configuration, syncRepository)
        {

        }
    }

    public abstract class NecnatSyncAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput, TRepository, TValidator, TSyncEntity, TSyncKey, TSyncRepository>
        : NecnatSyncAppService<TEntity, TEntityDto, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput, TRepository, TValidator, TSyncEntity, TSyncKey, TSyncRepository>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : class, IEntityDto<TKey>
        where TGetListInput : OptionalPagedAndSortedResultRequestDto
        where TCreateInput : class, IEntityDto<TKey>
        where TUpdateInput : class, IEntityDto<TKey>
        where TRepository : IRepository<TEntity, TKey>
        where TValidator : IValidator<TCreateInput, TUpdateInput, TGetListInput>
        where TSyncEntity : class, IEntity<TSyncKey>
        where TSyncRepository : IRepository<TSyncEntity, TSyncKey>
    {
        protected NecnatSyncAppService(
            ICurrentUser currentUser,
            IStringLocalizer<NnLibCommonResource> necnatLocalizer,
            TRepository repository,
            IConfiguration configuration,
            TSyncRepository syncRepository) : base(currentUser, necnatLocalizer, repository, configuration, syncRepository)
        {

        }
    }

    public abstract class NecnatSyncAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput, TRepository, TValidator, TSyncEntity, TSyncKey, TSyncRepository>
        : NecnatAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput, TRepository, TValidator>
        where TEntity : class, IEntity<TKey>
        where TGetListInput : OptionalPagedAndSortedResultRequestDto
        where TCreateInput : class, IEntityDto<TKey>
        where TUpdateInput : class, IEntityDto<TKey>
        where TRepository : IRepository<TEntity, TKey>
        where TValidator : IValidator<TCreateInput, TUpdateInput, TGetListInput>
        where TSyncEntity : class, IEntity<TSyncKey>
        where TSyncRepository : IRepository<TSyncEntity, TSyncKey>
    {
        protected TSyncRepository SyncRepository { get; }

        protected readonly bool syncFrom;
        protected readonly bool syncTo;

        protected NecnatSyncAppService(
            ICurrentUser currentUser,
            IStringLocalizer<NnLibCommonResource> necnatLocalizer,
            TRepository repository,

            IConfiguration configuration,
            TSyncRepository syncRepository) : base(currentUser, necnatLocalizer, repository)
        {
            SyncRepository = syncRepository;

            var syncFromSettings = configuration[$"Sync:{this.GetType().Name}:SyncFrom"];
            syncFrom = syncFromSettings == null ? true : bool.Parse(syncFromSettings);

            var syncToSettings = configuration[$"Sync:{this.GetType().Name}:SyncTo"];
            syncTo = syncToSettings == null ? true : bool.Parse(syncToSettings);
        }

        protected override async Task<TEntity> AfterGetAsync(TEntity entity)
        {
            await SyncToAsync(entity);
            return entity;
        }

        IEnumerable<string> entityList = new List<string>();
        protected override async Task<TGetListInput> BeforeGetListAsync(TGetListInput input)
        {
            if (syncFrom)
            {
                var lSyncEntity = await GetListSyncAsync(input);
                foreach (var iSyncEntity in lSyncEntity)
                    await SyncFromAsync(iSyncEntity);
            }

            await UnitOfWorkManager.Current!.SaveChangesAsync();

            return input;
        }

        protected override async Task<List<TEntity>> AfterGetListAsync(List<TEntity> entityList)
        {
            foreach (var iEntity in entityList)
                await SyncToAsync(iEntity);

            return entityList;
        }

        protected override async Task<TEntity> CheckCreateInsertedEntityAsync(TEntity insertedEntity, TCreateInput? input = null)
        {
            await SyncToAsync(insertedEntity);
            return insertedEntity;
        }

        protected override async Task<TEntity> CheckUpdateUpdatedEntityAsync(TEntity updatedEntity, TUpdateInput? input = null)
        {
            await SyncToAsync(updatedEntity);
            return updatedEntity;
        }

        protected override async Task<TEntity> CheckDeleteDbEntityAsync(TEntity dbEntity)
        {
            var dbSyncEntity = await FindSyncByEntityAsync(dbEntity);
            if (dbSyncEntity != null)
                await SyncRepository.DeleteAsync(dbSyncEntity);

            return dbEntity;
        }

        protected virtual async Task SyncFromAsync(TSyncEntity syncEntity)
        {
            if (!syncFrom)
                return;

            var dbEntity = await FindEntityBySyncAsync(syncEntity);
            if (dbEntity == null)
            {
                var entity = ObjectMapper.Map<TSyncEntity, TEntity>(syncEntity);
                await Repository.InsertAsync(entity);
            }
            else
            {
                if (!NeedSyncFrom(syncEntity, dbEntity))
                    return;

                var entity = ObjectMapper.Map(syncEntity, dbEntity);
                await Repository.UpdateAsync(entity);
            }
        }

        protected virtual async Task SyncToAsync(TEntity entity)
        {
            if (!syncTo)
                return;

            var dbSyncEntity = await FindSyncByEntityAsync(entity);
            if (dbSyncEntity == null)
            {
                var syncEntity = ObjectMapper.Map<TEntity, TSyncEntity>(entity);
                await SyncRepository.InsertAsync(syncEntity);
            }
            else
            {
                if (!NeedSyncTo(entity, dbSyncEntity))
                    return;

                var syncEntity = ObjectMapper.Map(entity, dbSyncEntity);
                await SyncRepository.UpdateAsync(syncEntity);
            }
        }

        protected abstract Task<TEntity?> FindEntityBySyncAsync(TSyncEntity entity);
        protected abstract Task<TSyncEntity?> FindSyncByEntityAsync(TEntity entity);
        protected abstract Task<List<TSyncEntity>> GetListSyncAsync(TGetListInput input);
        protected abstract bool NeedSyncFrom(TSyncEntity syncEntity, TEntity entity);
        protected abstract bool NeedSyncTo(TEntity entity, TSyncEntity syncEntity);
    }
}