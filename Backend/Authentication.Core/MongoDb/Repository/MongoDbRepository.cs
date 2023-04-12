using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Authentication.Core.MongoDb.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Authentication.Core.Models;

namespace Authentication.Core.MongoDb.Repository
{
    public class MongoDbRepository<TEntity> : IMongoDbRepository<TEntity>
         where TEntity : Entity
    {
        private readonly Lazy<IMongoCollection<TEntity>> _collection;
        private readonly IMongoDatabase _mongoDatabase;

        public MongoDbRepository(IMongoDbContext mongoDbContext)
        {
            var mongoDatabase = mongoDbContext.GetDatabase();
            if (mongoDatabase == null)
                throw new ArgumentNullException(nameof(mongoDatabase));

            _mongoDatabase = mongoDatabase;
            _collection = new Lazy<IMongoCollection<TEntity>>(CreateCollection);
        }

        #region Initialization
        /// <summary>
        /// Gets the name of the collection.
        /// </summary>
        /// <returns></returns>
        protected virtual string CollectionName()
        {
            return typeof(TEntity).Name;
        }

        /// <summary>
        /// Creates the collection.
        /// </summary>
        /// <returns></returns>
        protected virtual IMongoCollection<TEntity> CreateCollection()
        {
            var database = _mongoDatabase;

            string collectionName = CollectionName();
            var mongoCollection = CreateCollection(database, collectionName);

            EnsureIndexes(mongoCollection);

            return mongoCollection;
        }

        /// <summary>
        /// Creates the collection.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="collectionName">Name of the collection.</param>
        /// <returns></returns>
        protected virtual IMongoCollection<TEntity> CreateCollection(IMongoDatabase database, string collectionName)
        {
            var mongoCollection = database.GetCollection<TEntity>(collectionName);
            return mongoCollection;
        }

        /// <summary>
        /// Create indexes on the collection.
        /// </summary>
        /// <param name="mongoCollection">The mongo collection.</param>
        protected virtual void EnsureIndexes(IMongoCollection<TEntity> mongoCollection)
        {

        }
        #endregion


        public IMongoCollection<TEntity> Collection => _collection.Value;

        public virtual IMongoQueryable<TEntity> QueryCollection
        {
            get { return Collection.AsQueryable(); }
        }

        public virtual TEntity GetById(string id)
        {
            return Collection.Find(e => e.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Get entity by identifier async
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual Task<TEntity> GetByIdAsync(string id)
        {
            return Collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual TEntity Insert(TEntity entity)
        {
            BeforeInsert(entity);
            Collection.InsertOne(entity);
            return entity;
        }

        /// <summary>
        /// Async Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            BeforeInsert(entity);
            await Collection.InsertOneAsync(entity);
            return entity;
        }

        /// <summary>
        /// Async Insert many entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task InsertManyAsync(IEnumerable<TEntity> entities)
        {
            entities.ToList().ForEach(i => BeforeInsert(i));
            await Collection.InsertManyAsync(entities);
        }

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            entities.ToList().ForEach(i => BeforeInsert(i));
            Collection.InsertMany(entities);
        }

        /// <summary>
        /// Async Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task<IEnumerable<TEntity>> InsertAsync(IEnumerable<TEntity> entities)
        {
            entities.ToList().ForEach(i => BeforeInsert(i));
            await Collection.InsertManyAsync(entities);
            return entities;
        }


        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual TEntity Update(TEntity entity)
        {
            BeforeUpdate(entity);
            Collection.ReplaceOne(x => x.Id == entity.Id, entity, new ReplaceOptions() { IsUpsert = false });
            return entity;

        }

        /// <summary>
        /// Async Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            BeforeUpdate(entity);
            await Collection.ReplaceOneAsync(x => x.Id == entity.Id, entity, new ReplaceOptions() { IsUpsert = false });
            return entity;
        }

        /// <summary>
        /// Save entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual TEntity Save(TEntity entity)
        {
            BeforeUpdate(entity);
            Collection.ReplaceOne(x => x.Id == entity.Id, entity, new ReplaceOptions() { IsUpsert = true });
            return entity;
        }

        public virtual async Task<TEntity> SaveAsync(TEntity entity)
        {
            BeforeUpdate(entity);
            await Collection.ReplaceOneAsync(x => x.Id == entity.Id, entity, new ReplaceOptions() { IsUpsert = true });
            return entity;
        }

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Update(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                Update(entity);
            }
        }

        /// <summary>
        /// Async Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                await UpdateAsync(entity);
            }
            return entities;
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Delete(TEntity entity)
        {
            Collection.FindOneAndDelete(e => e.Id == entity.Id);
        }

        /// <summary>
        /// Async Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task<TEntity> DeleteAsync(TEntity entity)
        {
            await Collection.DeleteOneAsync(e => e.Id == entity.Id);
            return entity;
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                Collection.FindOneAndDeleteAsync(e => e.Id == entity.Id);
            }
        }

        /// <summary>
        /// Async Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                await DeleteAsync(entity);
            }
            return entities;
        }

        /// <summary>
        /// Find the first entity using the specified <paramref name="criteria" /> expression.
        /// </summary>
        /// <param name="criteria">The criteria expression.</param>
        /// <returns>
        /// An instance of TEnity that matches the criteria if found, otherwise null.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">criteria</exception>
        public TEntity FindOne(Expression<Func<TEntity, bool>> criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException(nameof(criteria));

            return Collection
                .Find(criteria)
                .FirstOrDefault();
        }

        /// <summary>
        /// Find the first entity using the specified <paramref name="criteria" /> expression.
        /// </summary>
        /// <param name="criteria">The criteria expression.</param>
        /// <returns>
        /// An instance of TEnity that matches the criteria if found, otherwise null.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">criteria</exception>
        public Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException(nameof(criteria));

            return Collection
                .Find(criteria)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Find all entities using the specified <paramref name="criteria" /> expression.
        /// </summary>
        /// <param name="criteria">The criteria expression.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">criteria</exception>
        public List<TEntity> FindAll(Expression<Func<TEntity, bool>> criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException(nameof(criteria));

            return Collection
                .AsQueryable()
                .Where(criteria)
                .ToList();
        }

        /// <summary>
        /// Find all entities using the specified <paramref name="criteria" /> expression.
        /// </summary>
        /// <param name="criteria">The criteria expression.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">criteria</exception>
        public Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return Collection
                .Find(criteria)
                .ToListAsync();
        }

        public async Task<long> GetIndex(Expression<Func<TEntity, bool>> criteria,SortDefinition<TEntity> sortDefinition)
        {
            return await Collection
                .Find(criteria)
                .Sort(sortDefinition).CountDocumentsAsync();
        }

        public async Task<List<TEntity>> GetDescendingSortedPageAsync(int page, int pageSize, Expression<Func<TEntity, object>> orderByDescExpression)
        {
            return await Collection
                .AsQueryable()
                .OrderByDescending(orderByDescExpression)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<TEntity>> GetDescendingSortedPageAsync(int page, int pageSize, Expression<Func<TEntity, object>> orderByDescExpression, Expression<Func<TEntity, bool>> criteria)
        {
            return await Collection
                .AsQueryable()
                .Where(criteria)
                .OrderByDescending(orderByDescExpression)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<TEntity>> GetDescendingThenDiscendingSortedPageAsync(int page, int pageSize, Expression<Func<TEntity, object>> orderByDescExpression, Expression<Func<TEntity, object>> thenOrderByDescExpression, Expression<Func<TEntity, bool>> criteria)
        {
            return await Collection
                .AsQueryable()
                .Where(criteria)
                .OrderByDescending(orderByDescExpression)
                .ThenByDescending(thenOrderByDescExpression)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<TEntity>> GetDescendingThenAscendingSortedPageAsync(int page, int pageSize, Expression<Func<TEntity, object>> orderByDescExpression, Expression<Func<TEntity, object>> thenOrderByAscExpression, Expression<Func<TEntity, bool>> criteria)
        {
            return await Collection
                .AsQueryable()
                .Where(criteria)
                .OrderByDescending(orderByDescExpression)
                .ThenBy(thenOrderByAscExpression)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<TEntity>> GetAscendingSortedPageAsync(int page, int pageSize, Expression<Func<TEntity, object>> orderByAscExpression)
        {
            return await Collection
                .AsQueryable()
                .OrderBy(orderByAscExpression)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }



        public async Task<List<TEntity>> GetAscendingSortedDataAsync( Expression<Func<TEntity, object>> orderByAscExpression, Expression<Func<TEntity, bool>> criteria)
        {
            return await Collection
                .AsQueryable()
                 .Where(criteria)
                .OrderBy(orderByAscExpression)
                .ToListAsync();
        }

        public async Task<List<TEntity>> GetDescendingSortedDataAsync(Expression<Func<TEntity, object>> orderByAscExpression, Expression<Func<TEntity, bool>> criteria)
        {
            return await Collection
                .AsQueryable()
                 .Where(criteria)
                .OrderByDescending(orderByAscExpression)
                .ToListAsync();
        }

        public async Task<List<TEntity>> GetAscendingSortedPageAsync(int page, int pageSize, Expression<Func<TEntity, object>> orderByAscExpression, Expression<Func<TEntity, bool>> criteria)
        {
            return await Collection
                .AsQueryable()
                .Where(criteria)
                .OrderBy(orderByAscExpression)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<TEntity>> GetPageAsync(int page, int pageSize)
        {
            return await Collection
                .AsQueryable()
                .OrderByDescending(x => x.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<TEntity>> GetPageAsync(int page, int pageSize, Expression<Func<TEntity, bool>> criteria)
        {
            return await Collection
                .AsQueryable()
                .Where(criteria)
                .OrderByDescending(x => x.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }



        public List<TEntity> GetPageDistinct(int page, int pageSize, Expression<Func<TEntity, bool>> criteria, IEqualityComparer<TEntity> equality)
        {
            return Collection
                .AsQueryable()
                .OrderByDescending(x => x.CreatedAt)
                .Where(criteria)
                .ToList().Distinct(equality)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();
        }

        public List<TEntity> GetAllDistinctItemsDescending(Expression<Func<TEntity, bool>> criteria, IEqualityComparer<TEntity> equality)
        {
            return Collection
                .AsQueryable()
                .OrderByDescending(x => x.CreatedAt)
                .Where(criteria)
                .ToList().Distinct(equality).ToList();
        }

        public int GetPageDistinctCount(Expression<Func<TEntity, bool>> criteria, IEqualityComparer<TEntity> equality)
        {
            return Collection
                .AsQueryable()
                .Where(criteria)
                .ToList()
                .Distinct(equality)
                .Count();
        }

        public async Task<int> GetCountAsync()
        {
            return await Collection
                .AsQueryable()
                .CountAsync();
        }

        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return await Collection
                .AsQueryable()
                .Where(criteria)
                .CountAsync();
        }

        public async Task<List<T>> AggregationResultsAsync<T>(IEnumerable<string> query)
        {
            var aggregateAsync = Collection.AggregateAsync(PipelineDefinition<TEntity, T>.Create(query));

            using (var cursor = await aggregateAsync)
            {
                return await cursor.ToListAsync();
            }
        }

        public async Task<T> AggregationResultAsync<T>(IEnumerable<string> query)
        {
            using (var cursor = Collection.Aggregate(PipelineDefinition<TEntity, T>.Create(query)))
            {
                return await cursor.FirstOrDefaultAsync();
            }
        }

        /// <summary>
		/// Called before an insert.
		/// </summary>
		/// <param name="entity">The entity.</param>
		protected virtual void BeforeInsert(TEntity entity)
        {
            BeforeUpdate(entity);

            entity.CreatedAt = DateTime.Now;
        }

        /// <summary>
        /// Called before an update.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected virtual void BeforeUpdate(TEntity entity)
        {
            if (entity.CreatedAt == DateTime.MinValue)
                entity.CreatedAt = DateTime.Now;

            entity.UpdatedAt = DateTime.Now;
        }


        /// <summary>
        /// Releases unmanaged and managed resources.
        /// </summary>
        void IDisposable.Dispose()
        {
        }
    }
}
