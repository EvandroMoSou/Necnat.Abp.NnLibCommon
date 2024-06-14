//using Necnat.Abp.NnLibCommon.Dtos;
//using Necnat.Abp.NnLibCommon.Entities;
//using Necnat.Abp.NnLibCommon.Exceptions;
//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using Volo.Abp.Application.Dtos;
//using Volo.Abp.Application.Services;
//using Volo.Abp.Domain.Entities;
//using Volo.Abp.Domain.Repositories;

//namespace Necnat.Abp.NnLibCommon.Services
//{
//    public abstract class SyncLegacyAppService<TEntity, TEntityDto, TKey, TSearchInput, TLegacyEntity, TLegacyKey>
//        : SyncLegacyAppService<TEntity, TEntityDto, TKey, PagedAndSortedResultRequestDto, TEntityDto, TSearchInput, TLegacyEntity, TLegacyKey>
//        where TEntity : class, IEntity<TKey>, IEntityWithLegacy<TLegacyKey>
//        where TEntityDto : IEntityDto<TKey>
//        where TSearchInput : OptionalPagedAndSortedResultRequestDto
//        where TLegacyEntity : class, IEntity<TLegacyKey>
//        where TLegacyKey : struct
//    {
//        protected SyncLegacyAppService(IRepository<TEntity, TKey> repository, IRepository<TLegacyEntity, TLegacyKey> legadoRepository) : base(repository, legadoRepository)
//        {

//        }
//    }

//    public abstract class SyncLegacyAppService<TEntity, TEntityDto, TKey, TCreateInput, TSearchInput, TLegacyEntity, TLegacyKey>
//        : SyncLegacyAppService<TEntity, TEntityDto, TKey, PagedAndSortedResultRequestDto, TCreateInput, TSearchInput, TLegacyEntity, TLegacyKey>
//        where TEntity : class, IEntity<TKey>, IEntityWithLegacy<TLegacyKey>
//        where TEntityDto : IEntityDto<TKey>
//        where TSearchInput : OptionalPagedAndSortedResultRequestDto
//        where TLegacyEntity : class, IEntity<TLegacyKey>
//        where TLegacyKey : struct
//    {
//        protected SyncLegacyAppService(IRepository<TEntity, TKey> repository, IRepository<TLegacyEntity, TLegacyKey> legadoRepository) : base(repository, legadoRepository)
//        {

//        }
//    }

//    public abstract class SyncLegacyAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TSearchInput, TLegacyEntity, TLegacyKey>
//       : CrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TSearchInput>
//       where TEntity : class, IEntity<TKey>, IEntityWithLegacy<TLegacyKey>
//       where TEntityDto : IEntityDto<TKey>
//       where TSearchInput : OptionalPagedAndSortedResultRequestDto, TGetListInput
//       where TLegacyEntity : class, IEntity<TLegacyKey>
//       where TLegacyKey : struct
//    {
//        protected IRepository<TLegacyEntity, TLegacyKey> LegacyRepository { get; }

//        protected SyncLegacyAppService(IRepository<TEntity, TKey> repository, IRepository<TLegacyEntity, TLegacyKey> legadoRepository) : base(repository)
//        {
//            LegacyRepository = legadoRepository;
//        }

//        public override async Task<TEntityDto> CreateAsync(TCreateInput input)
//        {
//            await CheckCreatePolicyAsync();

//            var entity = await MapToEntityAsync(input);

//            if (entity.LegacyId != null || !entity.LegacyId!.Equals(default(TLegacyEntity)))
//                throw new SyncException($"The field LegacyId must be null or default.");

//            TryToSetTenantId(entity);

//            var eLegacy = ObjectMapper.Map<TEntity, TLegacyEntity>(entity);
//            eLegacy = await LegacyRepository.InsertAsync(eLegacy);

//            entity.LegacyId = eLegacy.Id;
//            await Repository.InsertAsync(entity);

//            return await MapToGetOutputDtoAsync(entity);
//        }

//        public override async Task<TEntityDto> UpdateAsync(TKey id, TCreateInput input)
//        {
//            await CheckUpdatePolicyAsync();

//            var entity = await GetEntityByIdAsync(id);

//            await MapToEntityAsync(input, entity);

//            if (entity.LegacyId != null)
//            {
//                var eLegacy = await LegacyRepository.FindAsync((TLegacyKey)entity.LegacyId);
//                if (eLegacy == null)
//                    throw new SyncException($"The Legacy database don't have the code: {entity.LegacyId}");

//                eLegacy = ObjectMapper.Map(entity, eLegacy);
//                await LegacyRepository.UpdateAsync(eLegacy);
//            }

//            await Repository.UpdateAsync(entity);

//            return await MapToGetOutputDtoAsync(entity);
//        }

//        public override async Task DeleteAsync(TKey id)
//        {
//            await CheckDeletePolicyAsync();

//            var entity = await Repository.GetAsync(id);

//            if (entity.LegacyId != null)
//            {
//                var eLegacy = await LegacyRepository.FindAsync((TLegacyKey)entity.LegacyId);
//                if (eLegacy == null)
//                    throw new SyncException($"The Legacy database don't have the code: {entity.LegacyId}");

//                await LegacyRepository.DeleteAsync(eLegacy);
//            }

//            await DeleteAsync(id);
//        }

//        public virtual async Task SyncFromLegacyAsync()
//        {
//            await CheckSyncFromLegacyPolicyAsync();

//            var l = await Repository.GetListAsync();
//            var lLegacy = await LegacyRepository.GetListAsync();

//            foreach (var iLegacy in lLegacy)
//            {
//                var e = l.Where(x => x.LegacyId != null && x.LegacyId.Equals(iLegacy.Id)).FirstOrDefault();
//                if (e == null)
//                {
//                    e = ObjectMapper.Map<TLegacyEntity, TEntity>(iLegacy);
//                    await Repository.InsertAsync(e);
//                }
//                else
//                {
//                    e = ObjectMapper.Map(iLegacy, e);
//                    await Repository.UpdateAsync(e!);
//                }
//            }

//            foreach (var iE in l.Where(x => !lLegacy.Any(y => y.Id.Equals(x.LegacyId))))
//                await Repository.DeleteAsync(iE.Id);
//        }

//        public virtual async Task SyncToLegacyAsync()
//        {
//            await CheckSyncToLegacyPolicyAsync();

//            var l = await Repository.GetListAsync();
//            var lLegacy = await LegacyRepository.GetListAsync();

//            foreach (var iE in l)
//            {
//                var eLegacy = lLegacy.Where(x => x.Id.Equals(iE.LegacyId)).FirstOrDefault();
//                if (eLegacy == null)
//                {
//                    eLegacy = ObjectMapper.Map<TEntity, TLegacyEntity>(iE);
//                    await LegacyRepository.InsertAsync(eLegacy);
//                }
//                else
//                {
//                    eLegacy = ObjectMapper.Map(iE, eLegacy);
//                    await LegacyRepository.UpdateAsync(eLegacy!);
//                }
//            }

//            foreach (var iLegacy in lLegacy.Where(x => !l.Any(y => y.LegacyId != null && y.LegacyId.Equals(x.Id))))
//                await LegacyRepository.DeleteAsync(iLegacy.Id);
//        }

//        #region Permission

//        protected virtual string? SyncFromLegacyPolicyName { get; set; }
//        protected virtual async Task CheckSyncFromLegacyPolicyAsync()
//        {
//            await CheckPolicyAsync(SyncFromLegacyPolicyName);
//        }

//        protected virtual string? SyncToLegacyPolicyName { get; set; }
//        protected virtual async Task CheckSyncToLegacyPolicyAsync()
//        {
//            await CheckPolicyAsync(SyncToLegacyPolicyName);
//        }

//        #endregion
//    }
//}
