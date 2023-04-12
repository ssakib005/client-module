using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Core.MongoDb.Repository
{
    public interface IMongoDbRepository<TEntity> : IDisposable
    {
        IMongoQueryable<TEntity> QueryCollection { get; }

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        TEntity GetById(string id);

        /// <summary>
        /// Get async entity by identifier 
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        Task<TEntity> GetByIdAsync(string id);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Async Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Async Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        Task<IEnumerable<TEntity>> InsertAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Async Insert many entities
        /// </summary>
        /// <param name="entities">Entities</param>
        Task InsertManyAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Async Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Save entity
        /// </summary>
        /// <param name="entities">Entities</param>
        TEntity Save(TEntity entity);

        /// <summary>
        /// Async Save entity
        /// </summary>
        /// <param name="entities">Entities</param>
        Task<TEntity> SaveAsync(TEntity entity);

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Async Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(TEntity entity);

        /// Async Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        Task<TEntity> DeleteAsync(TEntity entity);

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Async Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Find the first entity using the specified <paramref name="criteria"/> expression.
        /// </summary>
        /// <param name="criteria">The criteria expression.</param>
        /// <returns>
        /// An instance of TEnity that matches the criteria if found, otherwise null.
        /// </returns>
        TEntity FindOne(Expression<Func<TEntity, bool>> criteria);

        /// <summary>
        /// Find the first entity using the specified <paramref name="criteria"/> expression.
        /// </summary>
        /// <param name="criteria">The criteria expression.</param>
        /// <returns>
        /// An instance of TEnity that matches the criteria if found, otherwise null.
        /// </returns>
        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> criteria);

        /// <summary>
        /// Find all entities using the specified <paramref name="criteria"/> expression.
        /// </summary>
        /// <param name="criteria">The criteria expression.</param>
        /// <returns></returns>
        List<TEntity> FindAll(Expression<Func<TEntity, bool>> criteria);

        /// <summary>
        /// Find all entities using the specified <paramref name="criteria"/> expression.
        /// </summary>
        /// <param name="criteria">The criteria expression.</param>
        /// <returns></returns>
        Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="sortDefinitio"></param>
        /// <param name="GetPageAsync"></param>
        /// <param name=""></param>
        /// <returns></returns>
        Task<long> GetIndex(Expression<Func<TEntity, bool>> criteria, SortDefinition<TEntity> sortDefinition);

        /// <summary>
        /// Get entities of a page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetPageAsync(int page, int pageSize);

        /// <summary>
        /// Get entities of a page using the specified <paramref name="criteria"/> expression.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetPageAsync(int page, int pageSize, Expression<Func<TEntity, bool>> criteria);

        /// <summary>
        /// Get entities of a page using the specified <paramref name="criteria"/> expression which are disctict according to  <paramref name="equality"/>
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="equality"></param>
        /// <returns></returns>
        List<TEntity> GetPageDistinct(int page, int pageSize, Expression<Func<TEntity, bool>> criteria, IEqualityComparer<TEntity> equality);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="equality"></param>
        /// <returns></returns>
        List<TEntity> GetAllDistinctItemsDescending(Expression<Func<TEntity, bool>> criteria, IEqualityComparer<TEntity> equality);

        /// <summary>
        /// Get entities count of distinct items in a collection
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="equality"></param>
        /// <returns></returns>
        int GetPageDistinctCount(Expression<Func<TEntity, bool>> criteria, IEqualityComparer<TEntity> equality);



        /// <summary>
        /// Get entities count of a collection
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync();

        /// <summary>
        /// Get entities count of a collection using the specified <paramref name="criteria"/> expression.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> criteria);

        Task<List<T>> AggregationResultsAsync<T>(IEnumerable<string> query);
        Task<T> AggregationResultAsync<T>(IEnumerable<string> query);


        /// <summary>
        /// Get entities of a page in decending order 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByDescExpression"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetDescendingSortedPageAsync(int page, int pageSize, Expression<Func<TEntity, object>> orderByDescExpression);

        /// <summary>
        ///  Get entities of a page in decending order with filtering
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByDescExpression"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetDescendingSortedPageAsync(int page, int pageSize, Expression<Func<TEntity, object>> orderByDescExpression, Expression<Func<TEntity, bool>> criteria);

        Task<List<TEntity>> GetDescendingThenDiscendingSortedPageAsync(int page, int pageSize, Expression<Func<TEntity, object>> orderByDescExpression, Expression<Func<TEntity, object>> thenOrderByDescExpression, Expression<Func<TEntity, bool>> criteria);

        Task<List<TEntity>> GetDescendingThenAscendingSortedPageAsync(int page, int pageSize, Expression<Func<TEntity, object>> orderByDescExpression, Expression<Func<TEntity, object>> thenOrderByAscExpression, Expression<Func<TEntity, bool>> criteria);

        /// <summary>
        /// Get entities of a page in ascending order 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByAscExpression"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetAscendingSortedPageAsync(int page, int pageSize, Expression<Func<TEntity, object>> orderByAscExpression);


        /// <summary>
        /// Get entities in ascending order 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByAscExpression"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetAscendingSortedDataAsync(Expression<Func<TEntity, object>> orderByAscExpression, Expression<Func<TEntity, bool>> criteria);

        /// <summary>
        /// Get entities in descending order 
        /// </summary>
        /// <param name="orderByAscExpression"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetDescendingSortedDataAsync(Expression<Func<TEntity, object>> orderByAscExpression, Expression<Func<TEntity, bool>> criteria);

        /// <summary>
        /// /// Get entities of a page in ascending order with filtering
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByAscExpression"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetAscendingSortedPageAsync(int page, int pageSize, Expression<Func<TEntity, object>> orderByAscExpression, Expression<Func<TEntity, bool>> criteria);
    }
}
