using System;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess;
using DataAccess.Abstract;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class DoctorRepository : EfEntityRepositoryBase<Doctor, ReservationContext>, IDoctorRepository
    {
        public DoctorRepository(ReservationContext context) : base(context)
        {
        }


        public async Task<Doctor> GetWithInclude(Guid id)
        {
            return await DbSet.Where(x => x.Id == id)
                .Include(x => x.Employee)
                .ThenInclude(x => x.Person)
                .Include(x => x.Employee.Address)
                .Include(x=>x.Reservations)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}