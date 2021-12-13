using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Business.DTOs.Abstract;
using Core.Utils.Result;

namespace Business.Utils.Abstract.Base
{
    public interface IUpdateEntityService<TEntityGetDto, TEntityUpdateDto>
        where TEntityGetDto : IEntityGetDto, new()
        where TEntityUpdateDto : IDto, new()
    {
        Task<IDataResult<TEntityGetDto>> UpdateAsync(Guid id, TEntityUpdateDto input, IDictionary<string, object> extraProperties = null);
    }
}