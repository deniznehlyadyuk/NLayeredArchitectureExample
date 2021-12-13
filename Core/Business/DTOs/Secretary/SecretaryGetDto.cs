using System;
using Core.Business.DTOs.Abstract;

namespace Core.Business.DTOs.Secretary
{
    public class SecretaryGetDto : SecretaryCreateDto, IEmployeeEntityGetDto
    {
        public Guid Id { get; set; }
    }
}