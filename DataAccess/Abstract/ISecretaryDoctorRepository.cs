using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DataAccess;
using Domain;

namespace DataAccess.Abstract
{
    public interface ISecretaryDoctorRepository : IEntityRepository<SecretaryDoctor>
    {
        Task<SecretaryDoctor> GetWithInclude(Guid id);
        Task<ICollection<SecretaryDoctor>> GetWithInclude(Expression<Func<SecretaryDoctor, bool>> predicate);
    }
}