using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.DataAccess
{
    public interface IEntityRepository<TEntity>
        where TEntity : BaseEntity, new()
    {
        Task AddAsync(TEntity input);
        Task UpdateAsync(TEntity input);
        Task DeleteAsync(TEntity input);
        Task DeleteByIdAsync(Guid id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<ICollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<TEntity> GetWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, 
            params Expression<Func<TEntity, object>>[] includes);
        Task<ICollection<TEntity>> GetListWithIncludeAsync(Expression<Func<TEntity, bool>> predicate = null,
            params Expression<Func<TEntity, object>>[] includes);
    }
}