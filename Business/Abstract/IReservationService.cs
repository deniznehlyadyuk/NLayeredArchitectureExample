using Business.Utils;
using Core.Business.DTOs.Reservation;

namespace Business.Abstract
{
    public interface IReservationService : ICrudEntityService<ReservationGetDto, ReservationCreateDto, ReservationUpdateDto>
    {
        
    }
}