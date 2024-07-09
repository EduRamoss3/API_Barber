
using System.Linq.Expressions;

namespace Barber.Domain.Interfaces
{
    public interface IRepository<T>
    {
        Task<bool> AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate);
        bool Remove(T entity);
        bool Update(T entity);
    }
}
