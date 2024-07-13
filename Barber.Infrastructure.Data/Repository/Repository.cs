using Barber.Domain.Interfaces;
using Barber.Domain.Parameters;
using Barber.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Barber.Infrastructure.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
       
        public Repository(AppDbContext context)
        {
            _context = context; 
        }
        public async Task<bool> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync(GetParametersPagination parameters)
        {
            var list = await _context.Set<T>().AsNoTracking().Skip((parameters.PageNumber -1) * parameters.PageSize).Take(parameters.PageSize).ToListAsync();  
            if(list is null)
            {
                return Enumerable.Empty<T>();
            }
            return list;
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public  bool Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            return true;
        }

        public bool Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return true;
        }
    }
}
