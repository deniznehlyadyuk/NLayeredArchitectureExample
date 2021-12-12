using System;

namespace Core.Business.DTOs.Reservation
{
    public class ReservationGetDto : ReservationCreateDto, IEntityGetDto
    {
        public Guid Id { get; set; }
        public bool IsCanceled { get; set; }
    }
}