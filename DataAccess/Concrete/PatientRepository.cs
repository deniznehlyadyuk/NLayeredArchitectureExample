using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess;
using DataAccess.Abstract;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class PatientRepository : EfEntityRepositoryBase<Patient, ReservationContext>, IPatientRepository
    {
        public PatientRepository(ReservationContext context) : base(context)
        {
        }

        public async Task<Patient> GetByIdentityNumber(string identityNumber)
        {
            return await DbSet.Include(x => x.Person)
                .Where(x => x.Person.IdentityNumber == identityNumber)
                .FirstOrDefaultAsync();
        }
    }
}