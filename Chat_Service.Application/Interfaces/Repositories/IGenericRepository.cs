using Chat_Service.Domain.Common;
using System.Linq.Expressions;

namespace Chat_Service.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : Entity
    {
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        Task<T> FindAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> FindAndIncludeAsync(Expression<Func<T, bool>> expression, params string[] includeProperties);
        Task<IEnumerable<T>> GetAllAsync(params string[] includeProperties);
        Task<T> GetByIdAsync(string id);
        Task<int> RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task<int> CountAsync(Expression<Func<T, bool>> expression);
        Task<int> CountAsync();
        Task<bool> SaveAsync();
    }
}
