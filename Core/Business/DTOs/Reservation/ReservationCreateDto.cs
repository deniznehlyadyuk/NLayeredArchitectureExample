using System;
using Core.Business.DTOs.Person;

namespace Core.Business.DTOs.Reservation
{
    public class ReservationCreateDto : IDto
    {
        public Guid DoctorId { get; set; }
        public PersonCreateDto PatientInfo { get; set; }
        public DateTime ResDate { get; set; }
    }
}