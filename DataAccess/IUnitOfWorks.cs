using System.Threading.Tasks;
using Core.DataAccess;
using Core.Domain;

namespace DataAccess
{
    public interface IUnitOfWorks
    {
        IEntityRepository<TEntity> GenerateRepository<TEntity>() where TEntity : BaseEntity, new();
        Task BeginTransactionAsync();
        Task RollbackTransactionAsync();
        Task CommitTransactionAsync();
    }
}