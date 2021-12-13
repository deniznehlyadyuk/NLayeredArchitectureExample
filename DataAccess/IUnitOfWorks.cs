using System.Threading.Tasks;
using Core.DataAccess;
using Core.Domain;
using DataAccess.Abstract;

namespace DataAccess
{
    public interface IUnitOfWorks
    {
        IEntityRepository<TEntity> GenerateRepository<TEntity>() where TEntity : BaseEntity, new();
        Task BeginTransactionAsync();
        Task RollbackTransactionAsync();
        Task CommitTransactionAsync();
        IDoctorRepository DoctorRepository { get; }
        IPatientRepository PatientRepository { get; }
        IReservationRepository ReservationRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        ISecretaryDoctorRepository SecretaryDoctorRepository { get; }
    }
}