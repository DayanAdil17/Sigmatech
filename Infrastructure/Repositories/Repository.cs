using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Sigmatech.Exceptions.Global;
using Sigmatech.Helpers.Wrapper;
using Sigmatech.Infrastructure.Context;
using Sigmatech.Interfaces.Entities;
using Sigmatech.Interfaces.Repositories.IRepository;

namespace Sigmatech.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbSet<T> _dbSet;
        private DataContext _context;

        public Repository(DataContext context)
        {
            _dbSet = context.Set<T>();
            _context = context;
        }
        public T Add(T entity)
        {
            ((IBaseEntity)entity).createdAt = DateTime.Now;
            ((IBaseEntity)entity).createdBy = "admin";
            ((IBaseEntity)entity).updatedAt = DateTime.Now;
            ((IBaseEntity)entity).updatedBy = "admin";
            ((IBaseEntity)entity).deletedBy = "admin";

            _dbSet.Add(entity);

            return entity;
        }

        public List<T> AddBulk(List<T> entities)
        {
            var entityCollection = new List<T>();

            foreach (var entity in entities)
            {
                ((IBaseEntity)entity).createdAt = DateTime.Now;

                ((IBaseEntity)entity).createdBy = "admin";
                
                ((IBaseEntity)entity).updatedAt = DateTime.Now;
                
                ((IBaseEntity)entity).updatedBy = "admin";

                entityCollection.Add(entity);
            }
            _dbSet.AddRange(entityCollection);

            return entityCollection;
        }

        public async Task<IEnumerable<T>> Finds(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> db = predicate == null ? _dbSet.AsNoTracking() : _dbSet.AsNoTracking().Where(predicate);
            return await db.Where(x => ((IBaseEntity)x).deletedAt == null).ToListAsync();
        }
        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate, IEnumerable<string> includes)
        {
            IQueryable<T> db = predicate == null ? _dbSet : _dbSet.Where(predicate);
            foreach (var includeItem in includes)
            {
                db = db.Include(includeItem);
            }
            return await db.Where(predicate).AsNoTracking().ToListAsync();
        }

        public virtual async Task<PaginationResponse<IEnumerable<T>>> FindPagination(
            Expression<Func<T, bool>> predicate,
            int page,
            int limit,
            string sort,
            string sortType,
            IEnumerable<string> includes)
        {
            var sortedBy = sort == null || sort.Equals("") ? "id" : sort;
            var orderBy = sortType == null || sortType.Equals("") ? "asc" : sortType;
            var propertyInfo = typeof(T).GetProperty(sortedBy, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
            if (propertyInfo == null) throw new NotFoundGlobalException(sort, "PROPERTY", "property", sort);

            IQueryable<T> db = predicate == null ? _dbSet : _dbSet.Where(predicate);

            if (includes.Count() > 0)
            { 
                foreach (var includeItem in includes)
                {
                    db = db.Include(includeItem);
                }
            }

            IEnumerable<T> query = orderBy.ToLower().Equals("asc") ? db.AsEnumerable().OrderBy(x => propertyInfo.GetValue(x)) : db.AsEnumerable().OrderByDescending(x => propertyInfo.GetValue(x));

            if (typeof(IBaseEntity).IsAssignableFrom(typeof(T)))
            {
                query = query.Where(x => ((IBaseEntity)x).deletedBy == null);
            }

            query = query.Skip((page < 1 ? 1 : page - 1) * limit).Take(limit < 1 ? 1 : limit);

            var count = predicate == null ? _dbSet : _dbSet.Where(predicate);
            if (typeof(IBaseEntity).IsAssignableFrom(typeof(T)))
            {
                count = count.Where(x => ((IBaseEntity)x).deletedBy == null);
            }
            var data = query.ToList();
            var total = await count.CountAsync();

            return new PaginationResponse<IEnumerable<T>>(page, limit, total, data);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public T Update(T entity)
        {
            ((IBaseEntity)entity).updatedAt = DateTime.Now;

            ((IBaseEntity)entity).updatedBy = "admin";

            _dbSet.Update(entity);

            return entity;
        }

        public async Task<T> FindSingleOrDefault(Expression<Func<T, bool>> predicate)
        {   
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }

        public async Task<T> FindSingleOrDefault(Expression<Func<T, bool>> predicate, IEnumerable<string> includes)
        {   
            IQueryable<T> db = predicate == null ? _dbSet : _dbSet.Where(predicate);
            foreach (var includeItem in includes)
            {
                db = db.Include(includeItem);
            }
            return await db.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> Any(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public bool HardDelete(Expression<Func<T, bool>> predicate)
        {
            var listEntity = _dbSet.Where(predicate).ToList();

            Parallel.ForEach(listEntity, entity =>
            {
                ((IBaseEntity)entity).deletedAt = DateTime.Now;

                ((IBaseEntity)entity).deletedBy = "admin";
            });
            
            _dbSet.UpdateRange(listEntity);

            _dbSet.RemoveRange(listEntity);

            return true;
        }

        public bool SoftDelete(Expression<Func<T, bool>> predicate)
        {
            var listEntity = _dbSet.Where(predicate).ToList();

            Parallel.ForEach(listEntity, entity =>
            {
                ((IBaseEntity)entity).deletedAt = DateTime.Now;

                ((IBaseEntity)entity).deletedBy = "admin";
            });

            _dbSet.UpdateRange(listEntity);

            _dbSet.RemoveRange(listEntity);

            return true;
        }
    }
}