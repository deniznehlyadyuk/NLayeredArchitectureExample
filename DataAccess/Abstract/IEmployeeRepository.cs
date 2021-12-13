using System;
using System.Threading.Tasks;
using Core.DataAccess;
using Domain;

namespace DataAccess.Abstract
{
    public interface IEmployeeRepository : IEntityRepository<Employee>
    {
        Task<Employee> GetWithInclude(Guid id);
    }
}