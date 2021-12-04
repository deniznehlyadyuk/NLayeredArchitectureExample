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
        Task<TEntityGetDto> AddAsync(TEntityDto input);
        Task<TEntityGetDto?> UpdateAsync(Guid id, TEntityDto input);
        Task DeleteByIdAsync(Guid id);
        Task<TEntityGetDto> GetByIdAsync(Guid id);
        Task<ICollection<TEntityGetDto>> GetAllAsync();
    }
}