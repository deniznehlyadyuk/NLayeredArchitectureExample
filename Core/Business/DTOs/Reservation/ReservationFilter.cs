using System;

namespace Core.Business.DTOs.Reservation
{
    public class ReservationFilter : IDto
    {
        public Guid? PatientId { get; set; }
        public Guid? DoctorId { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsCanceled { get; set; }
    }
}