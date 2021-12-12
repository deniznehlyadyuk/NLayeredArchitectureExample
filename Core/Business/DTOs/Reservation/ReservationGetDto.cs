using System;
using Core.Business.DTOs.Person;

namespace Core.Business.DTOs.Reservation
{
    public class ReservationGetDto : IEntityGetDto
    {
        public Guid Id { get; set; }
        public PersonCreateDto DoctorInfo { get; set; }
        public PersonCreateDto PatientInfo { get; set; }
        public DateTime ResDate { get; set; }
        public bool IsCanceled { get; set; }
    }
}