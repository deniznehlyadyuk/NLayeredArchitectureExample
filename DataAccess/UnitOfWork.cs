using System.Threading.Tasks;
using Core.DataAccess;
using Core.Domain;
using DataAccess.Abstract;
using DataAccess.Concrete;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWorks
    {
        private readonly ReservationContext _context;

        public UnitOfWork(ReservationContext context)
        {
            _context = context;
        }

        public IEntityRepository<TEntity> GenerateRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            return new EfEntityRepositoryBase<TEntity, ReservationContext>(_context);
        }

        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        private IDoctorRepository _doctorRepository;

        public IDoctorRepository DoctorRepository
        {
            get
            {
                if (_doctorRepository == null)
                {
                    _doctorRepository = new DoctorRepository(_context);
                }

                return _doctorRepository;
            }
        }
    }
}