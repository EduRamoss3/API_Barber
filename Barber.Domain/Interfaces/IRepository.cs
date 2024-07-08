
using System.Linq.Expressions;

namespace Barber.Domain.Interfaces
{
    public interface IRepository<T>
    {
        Task<bool> AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate);
        Task<bool> RemoveAsync(T entity);
        Task<bool> UpdateAsync(T entity);
    }
}
