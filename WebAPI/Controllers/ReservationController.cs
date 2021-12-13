using System;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Business.DTOs.Reservation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Abstract;

namespace WebAPI.Controllers
{
    public class ReservationController : EntityController<ReservationGetDto, ReservationCreateDto, ReservationUpdateDto>
    {
        private readonly IReservationService _service;
        public ReservationController(IReservationService service) : base(service)
        {
            _service = service;
        }

        [HttpGet("Doctor/{id:guid}")]
        public async Task<IActionResult> GetListByDoctorId(Guid id)
        {
            var result = await _service.GetListByDoctorId(id);
            return Ok(result);
        }
    }
}