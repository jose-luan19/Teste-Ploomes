using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Infra
{
    public interface IARepository<TEntity> where TEntity : class
    {
        DbContext DbContext { get; }
        IQueryable<TEntity> GetAll();
        void DeleteRange(params TEntity[] pObjects);
        TEntity GetById(object id);
        IEnumerable<TEntity> GetAllByFilter(Expression<Func<TEntity, bool>> pFilter = null);
        TEntity Find(Expression<Func<TEntity, bool>> keys = null);
        TEntity Find(params object[] keyValues);
        IQueryable<TEntity> SelectQuery(string query);
        bool Any(Expression<Func<TEntity, bool>> pFilter = null);
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void InsertGraphRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
        IQueryable<TEntity> Queryable();
        Task<List<TEntity>> ToListAsync();
        IQueryable<TEntity> QueryableDetach();
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> pFilter = null);
        IIncludableQueryable<TEntity, object> Include(Expression<Func<TEntity, object>> entity);
        void BulkInsert(IEnumerable<TEntity> entities);
        void BulkUpdate(IEnumerable<TEntity> entities);
        void BulkMerge(IEnumerable<TEntity> entities);
        void BulkSyncronize(IEnumerable<TEntity> entities);
        void Commit();
    }
}
