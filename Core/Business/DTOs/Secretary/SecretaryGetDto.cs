using System;

namespace Core.Business.DTOs.Secretary
{
    public class SecretaryGetDto : SecretaryCreateDto, IEntityGetDto
    {
        public Guid Id { get; set; }
    }
}