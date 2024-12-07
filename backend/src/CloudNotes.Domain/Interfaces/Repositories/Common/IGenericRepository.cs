using System.Linq.Expressions;
using CloudNotes.Domain.Entities.Common;

namespace CloudNotes.Domain.Interfaces.Repositories.Common;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(Guid id);
    Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> InsertAsync(TEntity request);
    Task<TEntity> UpdateAsync(TEntity request);
    Task<bool> DeleteAsync(TEntity entity);
    Task<bool> ExistsAsync(Guid id);
}
