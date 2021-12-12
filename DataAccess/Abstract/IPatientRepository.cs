using System.Threading.Tasks;
using Core.DataAccess;
using Domain;

namespace DataAccess.Abstract
{
    public interface IPatientRepository : IEntityRepository<Patient>
    {
        Task<Patient> GetByIdentityNumber(string identityNumber);
    }
}