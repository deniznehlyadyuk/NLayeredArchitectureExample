using System;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess;
using DataAccess.Abstract;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class EmployeeRepository : EfEntityRepositoryBase<Employee, ReservationContext>, IEmployeeRepository
    {
        public EmployeeRepository(ReservationContext context) : base(context)
        {
        }

        public async Task<Employee> GetWithInclude(Guid id)
        {
            return await DbSet.Where(x => x.Id == id)
                .Include(x => x.Person)
                .Include(x => x.Address)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}