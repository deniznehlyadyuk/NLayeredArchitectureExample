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
        private IPatientRepository _patientRepository;
        private IReservationRepository _reservationRepository;
        private IEmployeeRepository _employeeRepository;
        private ISecretaryDoctorRepository _secretaryDoctorRepository;

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

        public IPatientRepository PatientRepository
        {
            get
            {
                if (_patientRepository == null)
                {
                    _patientRepository = new PatientRepository(_context);
                }

                return _patientRepository;
            }
        }

        public IReservationRepository ReservationRepository
        {
            get
            {
                if (_reservationRepository == null)
                {
                    _reservationRepository = new ReservationRepository(_context);
                }

                return _reservationRepository;
            }
        }

        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                if (_employeeRepository == null)
                {
                    _employeeRepository = new EmployeeRepository(_context);
                }

                return _employeeRepository;
            }
        }

        public ISecretaryDoctorRepository SecretaryDoctorRepository
        {
            get
            {
                if (_secretaryDoctorRepository == null)
                {
                    _secretaryDoctorRepository = new SecretaryDoctorRepository(_context);
                }

                return _secretaryDoctorRepository;
            }
        }
    }
}