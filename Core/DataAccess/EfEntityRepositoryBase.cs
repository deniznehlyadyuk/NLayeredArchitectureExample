using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : BaseEntity, new()
        where TContext : DbContext
    {
        protected readonly TContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public EfEntityRepositoryBase(TContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity input)
        {
            await DbSet.AddAsync(input);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity input)
        {
            DbSet.Update(input);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity input)
        {
            DbSet.Remove(input);
            await Context.SaveChangesAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<ICollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return await DbSet.Where(predicate).ToListAsync();
            }

            return await DbSet.ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AnyAsync(predicate);
        }
    }
}