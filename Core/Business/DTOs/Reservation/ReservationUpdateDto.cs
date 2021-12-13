using System;
using Core.Business.DTOs.Abstract;
using Core.Business.DTOs.Person;

namespace Core.Business.DTOs.Reservation
{
    public class ReservationUpdateDto : IDto
    {
        public Guid DoctorId { get; set; }
        public PersonUpdateDto PatientInfo { get; set; }
        public DateTime ResDate { get; set; }
        public bool IsCanceled { get; set; }
    }
}