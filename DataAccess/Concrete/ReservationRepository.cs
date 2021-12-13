using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<ICollection<Reservation>> GetWithInclude(Expression<Func<Reservation, bool>> predicate = null)
        {
            IQueryable<Reservation> queryable = DbSet.AsQueryable();
            
            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }

            return await queryable
                .Include(x => x.Doctor)
                .ThenInclude(x => x.Employee)
                .ThenInclude(x => x.Person)
                .Include(x => x.Patient)
                .ThenInclude(x => x.Person)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}