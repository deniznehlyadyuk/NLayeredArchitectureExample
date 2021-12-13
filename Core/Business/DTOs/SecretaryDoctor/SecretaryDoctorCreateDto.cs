using System;
using Core.Business.DTOs.Abstract;

namespace Core.Business.DTOs.SecretaryDoctor
{
    public class SecretaryDoctorCreateDto : IDto
    {
        public Guid DoctorId { get; set; }
        public Guid SecretaryId { get; set; }
    }
}