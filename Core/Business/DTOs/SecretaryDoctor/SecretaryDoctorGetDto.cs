using System;
using Core.Business.DTOs.Abstract;
using Core.Business.DTOs.Doctor;
using Core.Business.DTOs.Secretary;

namespace Core.Business.DTOs.SecretaryDoctor
{
    public class SecretaryDoctorGetDto : IEntityGetDto
    {
        public Guid Id { get; set; }
        public DoctorGetDto DoctorInfo { get; set; }
        public SecretaryGetDto SecretaryInfo { get; set; }
    }
}