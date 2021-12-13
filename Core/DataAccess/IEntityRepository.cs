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
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<ICollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
    }
}