using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Business.DTOs;
using Core.Business.DTOs.Student;

namespace Business.Utils
{
    public interface ICrudEntityService<TEntityGetDto, TEntityCreateDto, TEntityUpdateDto>
        where TEntityGetDto : IEntityGetDto, new()
        where TEntityCreateDto : IDto, new()
        where TEntityUpdateDto : IDto, new()
    {
        Task<TEntityGetDto> AddAsync(TEntityCreateDto input);
        Task<TEntityGetDto?> UpdateAsync(Guid id, TEntityUpdateDto input);
        Task DeleteByIdAsync(Guid id);
        Task<TEntityGetDto> GetByIdAsync(Guid id);
        Task<ICollection<TEntityGetDto>> GetAllAsync();
    }
}