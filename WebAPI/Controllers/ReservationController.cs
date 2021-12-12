using Business.Abstract;
using Core.Business.DTOs.Reservation;
using WebAPI.Controllers.Abstract;

namespace WebAPI.Controllers
{
    public class ReservationController : EntityController<ReservationGetDto, ReservationCreateDto, ReservationUpdateDto>
    {
        public ReservationController(IReservationService service) : base(service)
        {
        }
    }
}