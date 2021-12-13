using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Business.DTOs;
using Core.Business.DTOs.Abstract;
using Core.Utils.Result;

namespace Business.Utils.Abstract.Base
{
    public interface IGetEntityService<TEntityGetDto>
        where TEntityGetDto : IEntityGetDto, new()
    {
        Task<IDataResult<TEntityGetDto>> GetByIdAsync(Guid id);
        Task<IDataResult<ICollection<TEntityGetDto>>> GetListAsync();
    }
}