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
    }
}