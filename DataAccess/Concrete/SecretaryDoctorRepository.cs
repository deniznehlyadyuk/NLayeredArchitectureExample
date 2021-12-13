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
    public class SecretaryDoctorRepository : EfEntityRepositoryBase<SecretaryDoctor, ReservationContext>, ISecretaryDoctorRepository
    {
        public SecretaryDoctorRepository(ReservationContext context) : base(context)
        {
        }

        public async Task<SecretaryDoctor> GetWithInclude(Guid id)
        {
            return await DbSet.Where(x=>x.Id == id)
                .Include(x => x.Doctor)
                .ThenInclude(x => x.Employee)
                .ThenInclude(x => x.Person)
                .Include(x => x.Secretary)
                .ThenInclude(x => x.Employee)
                .ThenInclude(x => x.Person)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<SecretaryDoctor>> GetWithInclude(Expression<Func<SecretaryDoctor, bool>> predicate)
        {
            return await DbSet.Where(predicate)
                .Include(x => x.Doctor)
                .ThenInclude(x => x.Employee)
                .ThenInclude(x => x.Person)
                .Include(x => x.Secretary)
                .ThenInclude(x => x.Employee)
                .ThenInclude(x => x.Person)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}