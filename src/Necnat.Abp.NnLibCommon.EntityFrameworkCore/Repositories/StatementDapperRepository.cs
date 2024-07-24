using Dapper;
using Necnat.Abp.NnLibCommon.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Linq;

namespace Necnat.Abp.NnLibCommon.Repositories
{
    public abstract class StatementDapperRepository<TDbContext, TEntity, TKey> : DapperRepository<TDbContext>, IRepository<TEntity, TKey>
        where TDbContext : IEfCoreDbContext
        where TEntity : class, IGetSetEntity<TKey>
    {
        protected readonly IDbStatement _dbStatement;

        protected string _tableName;
        protected string _primaryKeyName;

        public IAsyncQueryableExecuter AsyncExecuter => throw new NotImplementedException();
        public bool? IsChangeTrackingEnabled => false;
        public bool IsPrimaryKeyMappingActive { get; set; } = false;

        public StatementDapperRepository(IDbContextProvider<TDbContext> dbContextProvider,
            IDbStatement dbStatement) : base(dbContextProvider)
        {
            _dbStatement = dbStatement;

            //_tableName
            var tableAttribute = typeof(TEntity).GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == "TableAttribute") as TableAttribute;
            if (tableAttribute == null)
                throw new NotImplementedException($"TableAttribute on {typeof(TEntity).Name}");
            _tableName = tableAttribute.Name;

            //_primaryKeyName
            PropertyInfo[] properties = typeof(TEntity).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var attribute = Attribute.GetCustomAttribute(property, typeof(KeyAttribute)) as KeyAttribute;
                if (attribute != null)
                {
                    var column = property.GetCustomAttribute<ColumnAttribute>()?.Name ?? property.Name;
                    _primaryKeyName = column;
                }
            }
            if (string.IsNullOrWhiteSpace(_primaryKeyName))
                throw new NotImplementedException($"KeyAttribute on {typeof(TEntity).Name}");
        }

        #region Delete

        public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual async Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var statment = _dbStatement.GenerateDeleteStatement(_tableName, _primaryKeyName);

            var lParameter = new Dictionary<string, object?>
            {
                { _dbStatement.ToParameter(_primaryKeyName), id }
            };

            var dbConnection = await GetDbConnectionAsync();
            await dbConnection.ExecuteAsync(
                statment,
                new DynamicParameters(lParameter),
                await GetDbTransactionAsync()
            );
        }

        public virtual async Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await DeleteAsync(entity.Id, autoSave, cancellationToken);
        }

        public virtual Task DeleteDirectAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual async Task DeleteManyAsync(IEnumerable<TKey> ids, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            foreach (var id in ids)
                await DeleteAsync(id, autoSave, cancellationToken);
        }

        public virtual async Task DeleteManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
                await DeleteAsync(entity, autoSave, cancellationToken);
        }

        #endregion

        #region Get

        public virtual Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TEntity?> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            var statment = _dbStatement.GenerateGetStatement(_tableName, _primaryKeyName);

            var lParameter = new Dictionary<string, object?>
            {
                { _dbStatement.ToParameter(_primaryKeyName), id }
            };

            var dbConnection = await GetDbConnectionAsync();
            return (await dbConnection.QueryAsync<TEntity>(statment, new DynamicParameters(lParameter), await GetDbTransactionAsync())).FirstOrDefault();
        }

        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            var statment = _dbStatement.GenerateGetStatement(_tableName, _primaryKeyName);

            var lParameter = new Dictionary<string, object?>
            {
                { _dbStatement.ToParameter(_primaryKeyName), id }
            };

            var dbConnection = await GetDbConnectionAsync();
            var entity = (await dbConnection.QueryAsync<TEntity>(statment, new DynamicParameters(lParameter), await GetDbTransactionAsync())).FirstOrDefault();

            if (entity == null)
                throw new EntityNotFoundException(typeof(TEntity), id);

            return entity;
        }

        #endregion

        public virtual async Task<long> GetCountAsync(CancellationToken cancellationToken = default)
        {
            var dbConnection = await GetDbConnectionAsync();
            return await dbConnection.ExecuteScalarAsync<long>(_dbStatement.GenerateCountStatement(_tableName), transaction: await GetDbTransactionAsync());
        }

        #region GetList

        public virtual Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<List<TEntity>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            var statment = _dbStatement.GenerateGetListStatement(_tableName);

            var dbConnection = await GetDbConnectionAsync();
            return (await dbConnection.QueryAsync<TEntity>(statment, transaction: await GetDbTransactionAsync())).ToList();
        }

        public virtual async Task<List<TEntity>> GetPagedListAsync(PagedResultRequest pagedResultRequest, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            if (!pagedResultRequest.IsPaged)
                return await GetListAsync(includeDetails, cancellationToken);

            return await GetPagedListAsync((int)pagedResultRequest.SkipCount, (int)pagedResultRequest.MaxResultCount, pagedResultRequest.Sorting, includeDetails, cancellationToken);
        }

        public virtual async Task<List<TEntity>> GetPagedListAsync(int skipCount, int maxResultCount, string? sorting = null, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sorting))
                sorting = _primaryKeyName + " DESC";

            var statment = _dbStatement.GenerateGetListStatement(_tableName);
            statment = _dbStatement.GeneratePagedStatement(statment, skipCount, maxResultCount, sorting);

            var dbConnection = await GetDbConnectionAsync();
            return (await dbConnection.QueryAsync<TEntity>(statment, transaction: await GetDbTransactionAsync())).ToList();
        }

        public virtual async Task<PagedResult<TEntity>> GetPagedListAsync(string fromStatment, PagedResultRequest pagedResultRequest, object? param = null, string selectStatment = "SELECT * ")
        {
            return await GetPagedListAsync(fromStatment, pagedResultRequest.SkipCount, pagedResultRequest.MaxResultCount, pagedResultRequest.Sorting, pagedResultRequest.IsPaged, param, selectStatment);
        }

        public virtual async Task<PagedResult<TEntity>> GetPagedListAsync(string fromStatment, long skipCount, long maxResultCount, string? sorting = null, bool isPaged = true, object? param = null, string selectStatment = "SELECT * ")
        {
            if (string.IsNullOrWhiteSpace(sorting))
                sorting = _primaryKeyName + " DESC";

            var statment = selectStatment + Environment.NewLine + fromStatment;
            if (isPaged)
                statment = _dbStatement.GeneratePagedStatement(statment, skipCount, maxResultCount, sorting);

            var dbConnection = await GetDbConnectionAsync();

            var pagedResult = new PagedResult<TEntity>();
            pagedResult.Items = (await dbConnection.QueryAsync<TEntity>(statment, param, transaction: await GetDbTransactionAsync())).ToList();

            if (isPaged)
                pagedResult.TotalCount = await dbConnection.ExecuteScalarAsync<long>("SELECT COUNT(1) " + fromStatment, param, transaction: await GetDbTransactionAsync());
            else
                pagedResult.TotalCount = pagedResult.Items.Count;

            return pagedResult;
        }

        #endregion

        public virtual Task<IQueryable<TEntity>> GetQueryableAsync()
        {
            throw new NotImplementedException();
        }

        #region Insert

        public virtual async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var lColumn = new List<string>();
            var lParameter = new Dictionary<string, object?>();

            entity = await OnBeforeInsertAsync(entity);
            var lMapping = MappingInsert(entity);

            foreach (var iMapping in lMapping)
            {
                lColumn.Add(iMapping.Key);
                lParameter.Add(_dbStatement.ToParameter(iMapping.Key), iMapping.Value);
            }

            if (lColumn.Count < 1)
                throw new ArgumentException(NnLibCommonErrorCodes.IncorrectMapping);

            var statment = await GetInsertStatementAsync(lColumn);

            var dynamicParameters = new DynamicParameters(lParameter);
            dynamicParameters.Add(
                "return_value",
                dbType:
                    typeof(TKey) == typeof(int) ? DbType.Int32 :
                    typeof(TKey) == typeof(long) ? DbType.Int64 :
                    typeof(TKey) == typeof(Guid) ? DbType.Guid :
                    typeof(TKey) == typeof(string) ? DbType.String
                    : throw new NotImplementedException(),
                direction: ParameterDirection.Output);

            var dbConnection = await GetDbConnectionAsync();
            await dbConnection.ExecuteAsync(statment, dynamicParameters, await GetDbTransactionAsync());

            entity.Id = dynamicParameters.Get<TKey>("return_value");
            return entity;
        }

        public virtual async Task InsertManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
                await InsertAsync(entity, autoSave, cancellationToken);
        }

        public virtual Dictionary<string, object?> MappingInsert(TEntity entity)
        {
            var dict = new Dictionary<string, object?>();

            var lProperty = typeof(TEntity)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => x.CanRead && x.CanWrite && (x.PropertyType == typeof(string) || !typeof(IEnumerable).IsAssignableFrom(x.PropertyType)));

            foreach (var iProperty in lProperty)
            {
                var column = iProperty.GetCustomAttribute<ColumnAttribute>()?.Name ?? iProperty.Name;
                if (!IsPrimaryKeyMappingActive && column == _primaryKeyName)
                    continue;

                object? value = iProperty.GetValue(entity, null);

                if (value != null && !string.IsNullOrEmpty(value.ToString()))
                    dict.Add(column, iProperty.GetValue(entity, null));
            }

            return dict;
        }

        public virtual Task<TEntity> OnBeforeInsertAsync(TEntity entity)
        {
            return Task.FromResult(entity);
        }

        protected virtual Task<string> GetInsertStatementAsync(List<string> lColumn)
        {
            return Task.FromResult(_dbStatement.GenerateInsertStatement(_tableName, _primaryKeyName, lColumn));
        }

        #endregion

        #region Update

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var dbEntity = await FindAsync(entity.Id);
            if (dbEntity == null)
                throw new ArgumentException(NnLibCommonErrorCodes.RecordNotFound);

            var lColumn = new List<string>();
            var lParameter = new Dictionary<string, object?>();

            entity = await OnBeforeUpdateAsync(dbEntity, entity);
            var lMapping = MappingUpdate(dbEntity, entity);

            if (lMapping.Count < 1)
                return entity;

            foreach (var iMapping in lMapping)
            {
                lColumn.Add(iMapping.Key);
                lParameter.Add(_dbStatement.ToParameter(iMapping.Key), iMapping.Value);
            }

            lParameter.Add($"{_dbStatement.ToParameter(_primaryKeyName)}", entity.Id);

            var statment = _dbStatement.GenerateUpdateStatement(_tableName, _primaryKeyName, lColumn);

            var dbConnection = await GetDbConnectionAsync();
            await dbConnection.ExecuteAsync(statment, new DynamicParameters(lParameter), await GetDbTransactionAsync());

            return entity;
        }

        public virtual async Task UpdateManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
                await UpdateAsync(entity, autoSave, cancellationToken);
        }

        public virtual Dictionary<string, object?> MappingUpdate(TEntity dbEntity, TEntity entity)
        {
            var dict = new Dictionary<string, object?>();

            var lProperty = typeof(TEntity)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => x.CanRead && x.CanWrite && (x.PropertyType == typeof(string) || !typeof(IEnumerable).IsAssignableFrom(x.PropertyType)));

            foreach (var iProperty in lProperty)
            {
                var column = iProperty.GetCustomAttribute<ColumnAttribute>()?.Name ?? iProperty.Name;
                if (!IsPrimaryKeyMappingActive && column == _primaryKeyName)
                    continue;

                object? dbValue = iProperty.GetValue(dbEntity, null);
                object? value = iProperty.GetValue(entity, null);

                if (dbValue == null && value == null)
                    continue;

                if (dbValue == null && value != null)
                    dict.Add(column, iProperty.GetValue(entity, null));
                else if (!Equals(dbValue, value))
                    dict.Add(column, iProperty.GetValue(entity, null));
            }

            return dict;
        }

        public virtual Task<TEntity> OnBeforeUpdateAsync(TEntity dbEntity, TEntity entity)
        {
            return Task.FromResult(entity);
        }

        #endregion

        #region WithDetails

        public virtual IQueryable<TEntity> WithDetails()
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<TEntity> WithDetails(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IQueryable<TEntity>> WithDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<IQueryable<TEntity>> WithDetailsAsync(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

