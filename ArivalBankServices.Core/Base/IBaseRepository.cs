using ArivalBankServices.Core.Base;
using System.Linq.Expressions;

namespace ArivalBankServices.Core.Base
{
    public interface IBaseRepository<T> where T : Entity
    {
        Task CreateAsync(T entity, CancellationToken cancellationToken);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter);
        Task<T> GetByQueryAsync(Expression<Func<T, bool>>? filter);
    }
}
