using Application.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infra.Repository
{
    public abstract class ARepository<TEntity> : IARepository<TEntity> where TEntity : class
    {
        protected DbContext _context;
        protected DbSet<TEntity> _dbSet;

        public ARepository(DbContext context)
        {
            _context = context;
            if (context != null)
            {
                _dbSet = _context.Set<TEntity>();
            }
        }

        public DbContext DbContext { get { return _context; } }

        public void Commit() => _context.SaveChanges();
        public void DeleteRange(params TEntity[] pObjects)
        {
            foreach (var pObject in pObjects)
            {
                Delete(pObject);
            }
        }
        public TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAllByFilter(Expression<Func<TEntity, bool>> pFilter = null)
        {
            return _dbSet.Where(pFilter).ToList();
        }

        public TEntity Find(Expression<Func<TEntity, bool>> keys = null)
        {
            return _dbSet.FirstOrDefault(keys);
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public virtual IQueryable<TEntity> SelectQuery(string query)
        {
            return _context.Set<TEntity>().FromSqlRaw(query).AsQueryable();
        }
        public virtual bool Any(Expression<Func<TEntity, bool>> pFilter = null)
        {
            return _dbSet.Any(pFilter);
        }
        public virtual void Insert(TEntity entity)
        {
            if (entity.GetType().GetProperty("CreatedDate") != null)
            {
                entity.GetType().GetProperty("CreatedDate").SetValue(entity, DateTime.Now);
            }

            _context.Add(entity);
            _context.Entry(entity).State = EntityState.Added;
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                if (entity.GetType().GetProperty("CreatedDate") != null)
                {
                    entity.GetType().GetProperty("CreatedDate").SetValue(entity, DateTime.Now);
                }
            }
            InsertGraphRange(entities);
        }

        public virtual void InsertGraphRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public IQueryable<TEntity> Queryable()
        {
            return _dbSet;
        }

        public async Task<List<TEntity>> ToListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public IQueryable<TEntity> QueryableDetach()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }

        public async Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> pFilter = null)
        {
            return await _dbSet.Where(pFilter).ToListAsync();
        }

        public IIncludableQueryable<TEntity, object> Include(Expression<Func<TEntity, object>> entity)
        {
            return _dbSet.Include(entity);
        }
        public void BulkInsert(IEnumerable<TEntity> entities)
        {
            _context.BulkInsert(entities);
        }

        public void BulkUpdate(IEnumerable<TEntity> entities)
        {
            _context.BulkUpdate(entities);
        }

        public void BulkMerge(IEnumerable<TEntity> entities)
        {
            _context.BulkMerge(entities);
        }

        public void BulkSyncronize(IEnumerable<TEntity> entities)
        {
            _context.BulkSynchronize(entities);
        }

        public IQueryable<TEntity> GetAll()
        {
            var x = _context.Set<TEntity>();
            return _dbSet;
        }
    }
}
