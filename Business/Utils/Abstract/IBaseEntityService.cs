using Business.Utils.Abstract.Base;
using Core.Business.DTOs.Abstract;

namespace Business.Utils.Abstract
{
    public interface IBaseEntityService<TEntityGetDto> : IGetEntityService<TEntityGetDto>
        where TEntityGetDto : IEntityGetDto, new()
    {
        
    }
}