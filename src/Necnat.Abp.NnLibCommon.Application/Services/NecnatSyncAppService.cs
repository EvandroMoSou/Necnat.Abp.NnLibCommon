//using Microsoft.Extensions.Localization;
//using Necnat.Abp.NnLibCommon.Dtos;
//using Necnat.Abp.NnLibCommon.Exceptions;
//using Necnat.Abp.NnLibCommon.Localization;
//using System.Linq;
//using System.Threading.Tasks;
//using Volo.Abp.Application.Dtos;
//using Volo.Abp.Domain.Entities;
//using Volo.Abp.Domain.Repositories;
//using Volo.Abp.ObjectMapping;
//using Volo.Abp.Users;

//namespace Necnat.Abp.NnLibCommon.Services
//{
//    public abstract class NecnatSyncAppService<TEntity, TEntityDto, TKey, TRepository, TSyncEntity, TSyncKey, TSyncRepository>
//        : NecnatSyncAppService<TEntity, TEntityDto, TKey, OptionalPagedAndSortedResultRequestDto, TRepository, TSyncEntity, TSyncKey, TSyncRepository>
//        where TEntity : class, IEntity<TKey>
//        where TEntityDto : class, IEntityDto<TKey>
//        where TRepository : IRepository<TEntity, TKey>
//        where TSyncEntity : class, IEntity<TSyncKey>
//        where TSyncRepository : IRepository<TSyncEntity, TSyncKey>
//    {
//        protected NecnatSyncAppService(
//            ICurrentUser currentUser,
//            IStringLocalizer<NnLibCommonResource> necnatLocalizer,
//            TRepository repository,
//            TSyncRepository syncRepository) : base(currentUser, necnatLocalizer, repository, syncRepository)
//        {

//        }
//    }

//    public abstract class NecnatSyncAppService<TEntity, TEntityDto, TKey, TGetListInput, TRepository, TSyncEntity, TSyncKey, TSyncRepository>
//        : NecnatSyncAppService<TEntity, TEntityDto, TKey, TGetListInput, TEntityDto, TRepository, TSyncEntity, TSyncKey, TSyncRepository>
//        where TEntity : class, IEntity<TKey>
//        where TEntityDto : class, IEntityDto<TKey>
//        where TGetListInput : OptionalPagedAndSortedResultRequestDto
//        where TRepository : IRepository<TEntity, TKey>
//        where TSyncEntity : class, IEntity<TSyncKey>
//        where TSyncRepository : IRepository<TSyncEntity, TSyncKey>
//    {
//        protected NecnatSyncAppService(
//            ICurrentUser currentUser,
//            IStringLocalizer<NnLibCommonResource> necnatLocalizer,
//            TRepository repository,
//            TSyncRepository syncRepository) : base(currentUser, necnatLocalizer, repository, syncRepository)
//        {

//        }
//    }

//    public abstract class NecnatSyncAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TRepository, TSyncEntity, TSyncKey, TSyncRepository>
//        : NecnatSyncAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput, TRepository, TSyncEntity, TSyncKey, TSyncRepository>
//        where TEntity : class, IEntity<TKey>
//        where TEntityDto : class, IEntityDto<TKey>
//        where TGetListInput : OptionalPagedAndSortedResultRequestDto
//        where TCreateInput : class, IEntityDto<TKey>
//        where TRepository : IRepository<TEntity, TKey>
//        where TSyncEntity : class, IEntity<TSyncKey>
//        where TSyncRepository : IRepository<TSyncEntity, TSyncKey>
//    {
//        protected NecnatSyncAppService(
//            ICurrentUser currentUser,
//            IStringLocalizer<NnLibCommonResource> necnatLocalizer,
//            TRepository repository,
//            TSyncRepository syncRepository) : base(currentUser, necnatLocalizer, repository, syncRepository)
//        {

//        }
//    }

//    public abstract class NecnatSyncAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput, TRepository, TSyncEntity, TSyncKey, TSyncRepository>
//        : NecnatSyncAppService<TEntity, TEntityDto, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput, TRepository, TSyncEntity, TSyncKey, TSyncRepository>
//        where TEntity : class, IEntity<TKey>
//        where TEntityDto : class, IEntityDto<TKey>
//        where TGetListInput : OptionalPagedAndSortedResultRequestDto
//        where TCreateInput : class, IEntityDto<TKey>
//        where TUpdateInput : class, IEntityDto<TKey>
//        where TRepository : IRepository<TEntity, TKey>
//        where TSyncEntity : class, IEntity<TSyncKey>
//        where TSyncRepository : IRepository<TSyncEntity, TSyncKey>
//    {
//        protected NecnatSyncAppService(
//            ICurrentUser currentUser,
//            IStringLocalizer<NnLibCommonResource> necnatLocalizer,
//            TRepository repository,
//            TSyncRepository syncRepository) : base(currentUser, necnatLocalizer, repository, syncRepository)
//        {

//        }
//    }

//    public abstract class NecnatSyncAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput, TRepository, TSyncEntity, TSyncKey, TSyncRepository>
//        : NecnatAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput, TRepository>
//        where TEntity : class, IEntity<TKey>
//        where TGetListInput : OptionalPagedAndSortedResultRequestDto
//        where TCreateInput : class, IEntityDto<TKey>
//        where TUpdateInput : class, IEntityDto<TKey>
//        where TRepository : IRepository<TEntity, TKey>
//        where TSyncEntity : class, IEntity<TSyncKey>
//        where TSyncRepository : IRepository<TSyncEntity, TSyncKey>
//    {
//        protected TSyncRepository SyncRepository { get; }

//        protected NecnatSyncAppService(
//            ICurrentUser currentUser,
//            IStringLocalizer<NnLibCommonResource> necnatLocalizer,
//            TRepository repository,
//            TSyncRepository syncRepository) : base(currentUser, necnatLocalizer, repository)
//        {
//            SyncRepository = syncRepository;
//        }

//        protected override async Task<TEntity> CheckCreateInsertedEntityAsync(TEntity insertedEntity, TCreateInput? input = null)
//        {
//            var syncEntity = ObjectMapper.Map<TEntity, TSyncEntity>(insertedEntity);
//            await SyncToAsync(syncEntity);

//            return insertedEntity;
//        }

//        public override async Task<TGetOutputDto> UpdateAsync(TKey id, TCreateInput input)
//        {
//            await CheckUpdatePolicyAsync();

//            var entity = await GetEntityByIdAsync(id);

//            await MapToEntityAsync(input, entity);

//            if (entity.SyncId != null)
//            {
//                var eSync = await SyncRepository.FindAsync((TSyncKey)entity.SyncId);
//                if (eSync == null)
//                    throw new SyncException($"The Sync database don't have the code: {entity.SyncId}");

//                eSync = ObjectMapper.Map(entity, eSync);
//                await SyncRepository.UpdateAsync(eSync);
//            }

//            await Repository.UpdateAsync(entity);

//            return await MapToGetOutputDtoAsync(entity);
//        }

//        public override async Task DeleteAsync(TKey id)
//        {
//            await CheckDeletePolicyAsync();

//            var entity = await Repository.GetAsync(id);

//            if (entity.SyncId != null)
//            {
//                var eSync = await SyncRepository.FindAsync((TSyncKey)entity.SyncId);
//                if (eSync == null)
//                    throw new SyncException($"The Sync database don't have the code: {entity.SyncId}");

//                await SyncRepository.DeleteAsync(eSync);
//            }

//            await DeleteAsync(id);
//        }

//        public virtual async Task SyncFromSyncAsync()
//        {
//            await CheckSyncFromSyncPolicyAsync();

//            var l = await Repository.GetListAsync();
//            var lSync = await SyncRepository.GetListAsync();

//            foreach (var iSync in lSync)
//            {
//                var e = l.Where(x => x.SyncId != null && x.SyncId.Equals(iSync.Id)).FirstOrDefault();
//                if (e == null)
//                {
//                    e = ObjectMapper.Map<TSyncEntity, TEntity>(iSync);
//                    await Repository.InsertAsync(e);
//                }
//                else
//                {
//                    e = ObjectMapper.Map(iSync, e);
//                    await Repository.UpdateAsync(e!);
//                }
//            }

//            foreach (var iE in l.Where(x => !lSync.Any(y => y.Id.Equals(x.SyncId))))
//                await Repository.DeleteAsync(iE.Id);
//        }

//        public virtual async Task SyncToSyncAsync()
//        {
//            await CheckSyncToSyncPolicyAsync();

//            var l = await Repository.GetListAsync();
//            var lSync = await SyncRepository.GetListAsync();

//            foreach (var iE in l)
//            {
//                var eSync = lSync.Where(x => x.Id.Equals(iE.SyncId)).FirstOrDefault();
//                if (eSync == null)
//                {
//                    eSync = ObjectMapper.Map<TEntity, TSyncEntity>(iE);
//                    await SyncRepository.InsertAsync(eSync);
//                }
//                else
//                {
//                    eSync = ObjectMapper.Map(iE, eSync);
//                    await SyncRepository.UpdateAsync(eSync!);
//                }
//            }

//            foreach (var iSync in lSync.Where(x => !l.Any(y => y.SyncId != null && y.SyncId.Equals(x.Id))))
//                await SyncRepository.DeleteAsync(iSync.Id);
//        }

//        #region Permission

//        protected virtual string? SyncFromSyncPolicyName { get; set; }
//        protected virtual async Task CheckSyncFromSyncPolicyAsync()
//        {
//            await CheckPolicyAsync(SyncFromSyncPolicyName);
//        }

//        protected virtual string? SyncToSyncPolicyName { get; set; }
//        protected virtual async Task CheckSyncToSyncPolicyAsync()
//        {
//            await CheckPolicyAsync(SyncToSyncPolicyName);
//        }

//        #endregion
//    }
//}
