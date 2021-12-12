using System;
using System.Threading.Tasks;
using Core.DataAccess;
using Domain;

namespace DataAccess.Abstract
{
    public interface IDoctorRepository : IEntityRepository<Doctor>
    {
        Task<Doctor> GetWithInclude(Guid id);
    }
}