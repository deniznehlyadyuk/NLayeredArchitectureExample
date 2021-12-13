using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Business.DTOs;
using Core.Business.DTOs.Abstract;
using Core.Utils.Result;

namespace Business.Utils.Abstract.Base
{
    public interface IAddEntityService<TEntityGetDto, TEntityCreateDto>
        where TEntityGetDto : IEntityGetDto, new()
        where TEntityCreateDto : IDto, new()
    {
        Task<IDataResult<TEntityGetDto>> AddAsync(TEntityCreateDto input, IDictionary<string, object> extraProperties = null);
    }
}