using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DataAccess;
using Domain;

namespace DataAccess.Abstract
{
    public interface IReservationRepository : IEntityRepository<Reservation>
    {
        Task<Reservation> GetWithInclude(Guid id);
        Task<ICollection<Reservation>> GetWithInclude(Expression<Func<Reservation, bool>> predicate = null);
    }
}