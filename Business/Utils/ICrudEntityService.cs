using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Business.DTOs;
using Core.Utils.Result;

namespace Business.Utils
{
    public interface ICrudEntityService<TEntityGetDto, TEntityCreateDto, TEntityUpdateDto>
        where TEntityGetDto : IEntityGetDto, new()
        where TEntityCreateDto : IDto, new()
        where TEntityUpdateDto : IDto, new()
    {
        Task<IDataResult<TEntityGetDto>> AddAsync(TEntityCreateDto input);
        Task<IDataResult<TEntityGetDto>> UpdateAsync(Guid id, TEntityUpdateDto input);
        Task<IResult> DeleteByIdAsync(Guid id);
        Task<IDataResult<TEntityGetDto>> GetByIdAsync(Guid id);
        Task<IDataResult<ICollection<TEntityGetDto>>> GetListAsync();
    }
}