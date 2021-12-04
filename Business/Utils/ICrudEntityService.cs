using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Business.DTOs;
using Core.Business.DTOs.Student;

namespace Business.Utils
{
    public interface ICrudEntityService<TEntityGetDto, TEntityDto>
        where TEntityGetDto : IEntityGetDto, new()
        where TEntityDto : IDto, new()
    {
        Task<TEntityGetDto> Add(TEntityDto input);
        Task<TEntityGetDto?> Update(Guid id, TEntityDto input);
        Task DeleteById(Guid id);
        Task<TEntityGetDto> GetById(Guid id);
        Task<ICollection<TEntityGetDto>> GetAll();
    }
}