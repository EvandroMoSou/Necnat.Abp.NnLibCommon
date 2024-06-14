//using Necnat.Abp.NnLibCommon.Dtos;
//using System.Threading.Tasks;
//using Volo.Abp.Application.Dtos;
//using Volo.Abp.Application.Services;
//using Volo.Abp.Domain.Entities;
//using Volo.Abp.Domain.Repositories;

//namespace Necnat.Abp.NnLibCommon.Services
//{
//    public abstract class SearchDapperAppService<TEntity, TEntityDto, TKey, TSearchInput, IEntityRepository>
//    : SearchDapperAppService<TEntity, TEntityDto, TKey, TEntityDto, TSearchInput, IEntityRepository>
//    where TEntity : class, IEntity<TKey>
//    where TEntityDto : IEntityDto<TKey>
//    where IEntityRepository : IRepository<TEntity, TKey>
//    where TSearchInput : OptionalPagedAndSortedResultRequestDto
//    {
//        protected SearchDapperAppService(IEntityRepository repository) : base(repository)
//        {
//        }
//    }

//    public abstract class SearchDapperAppService<TEntity, TEntityDto, TKey, TCreateUpdateInput, TSearchInput, IEntityRepository>
//        : CrudAppService<
//            TEntity,
//            TEntityDto,
//            TKey,
//            PagedAndSortedResultRequestDto,
//            TCreateUpdateInput>,
//        ISearchAppService<TEntityDto, TKey, TCreateUpdateInput, TSearchInput>
//        where TEntity : class, IEntity<TKey>
//        where TEntityDto : IEntityDto<TKey>
//        where IEntityRepository : IRepository<TEntity, TKey>
//        where TSearchInput : OptionalPagedAndSortedResultRequestDto
//    {
//        protected IEntityRepository TypedRepository { get; }

//        protected SearchDapperAppService(IEntityRepository repository) : base(repository)
//        {
//            TypedRepository = repository;
//        }

//        public override async Task<PagedResultDto<TEntityDto>> GetListAsync(PagedAndSortedResultRequestDto input)
//        {
//            var entityList = await Repository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? string.Empty);
//            return new PagedResultDto<TEntityDto>(await Repository.CountAsync(), await MapToGetListOutputDtosAsync(entityList));
//        }

//        public abstract Task<PagedResultDto<TEntityDto>> SearchAsync(TSearchInput input);

//        public override async Task<TEntityDto> CreateAsync(TCreateUpdateInput input)
//        {
//            await CheckCreatePolicyAsync();
//            await ValidationAsync(input);

//            var entity = await MapToEntityAsync(input);

//            TryToSetTenantId(entity);

//            await Repository.InsertAsync(entity, autoSave: true);

//            return await MapToGetOutputDtoAsync(entity);
//        }

//        public override async Task<TEntityDto> UpdateAsync(TKey id, TCreateUpdateInput input)
//        {
//            await CheckUpdatePolicyAsync();
//            await ValidationAsync(input);

//            var entity = await GetEntityByIdAsync(id);
//            //TODO: Check if input has id different than given id and normalize if it's default value, throw ex otherwise
//            await MapToEntityAsync(input, entity);
//            await Repository.UpdateAsync(entity, autoSave: true);

//            return await MapToGetOutputDtoAsync(entity);
//        }

//        protected virtual Task ValidationAsync(TCreateUpdateInput input)
//        {
//            return Task.CompletedTask;
//        }
//    }
//}
