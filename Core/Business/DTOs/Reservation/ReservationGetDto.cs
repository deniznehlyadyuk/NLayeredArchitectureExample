using System;
using Core.Business.DTOs.Abstract;
using Core.Business.DTOs.Person;

namespace Core.Business.DTOs.Reservation
{
    public class ReservationGetDto : ReservationCreateDto, IEntityGetDto
    {
        public Guid Id { get; set; }
        public PersonCreateDto DoctorInfo { get; set; }
        public bool IsCanceled { get; set; }
    }
}