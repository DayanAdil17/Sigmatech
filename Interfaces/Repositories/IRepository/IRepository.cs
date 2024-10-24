using System.Linq.Expressions;
using Sigmatech.Helpers.Wrapper;

namespace Sigmatech.Interfaces.Repositories.IRepository
{
    public interface IRepository<T> where T : class
    {
         
        public Task<IEnumerable<T>>GetAll();  

        public Task<PaginationResponse<IEnumerable<T>>>FindPagination(Expression<Func<T, bool>> 
        predicate,int page, int limit, string sort, string sortType, IEnumerable<string>includes);

        public Task<T>GetById(object id);

        public T Add(T entity);

        public bool HardDelete(Expression<Func<T, bool>> predicate);

        public bool SoftDelete(Expression<Func<T, bool>> predicate);

        public T Update(T entity);

        public Task<IEnumerable<T>>Finds(Expression<Func<T, bool>>predicate);
        public Task<IEnumerable<T>>Find(Expression<Func<T, bool>>predicate, IEnumerable<string> includes);

        public List<T>AddBulk(List<T>entities);

        public Task<T> FindSingleOrDefault(Expression<Func<T, bool>> predicate);

        public Task<T> FindSingleOrDefault(Expression<Func<T, bool>> predicate, IEnumerable<string>includes);

    }
}