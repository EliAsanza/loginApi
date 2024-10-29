using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SecureLogin2FA.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);


        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetByIdAsync(int id);

        Task<bool> Exists(Expression<Func<T, bool>> predicate);
    }
}
