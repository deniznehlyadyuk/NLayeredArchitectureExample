using System;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess;
using DataAccess.Abstract;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class ReservationRepository : EfEntityRepositoryBase<Reservation,ReservationContext>, IReservationRepository
    {
        public ReservationRepository(ReservationContext context) : base(context)
        {
        }

        public async Task<Reservation> GetWithInclude(Guid id)
        {
            return await DbSet.Where(x => x.Id == id)
                .Include(x => x.Doctor)
                .ThenInclude(x => x.Employee)
                .ThenInclude(x => x.Person)
                .Include(x => x.Patient)
                .ThenInclude(x => x.Person)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}