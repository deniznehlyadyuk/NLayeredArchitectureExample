using System;
using System.Threading.Tasks;
using Core.DataAccess;
using Domain;

namespace DataAccess.Abstract
{
    public interface IReservationRepository : IEntityRepository<Reservation>
    {
        Task<Reservation> GetWithInclude(Guid id);
    }
}