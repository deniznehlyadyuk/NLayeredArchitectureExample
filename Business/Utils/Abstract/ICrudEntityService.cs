using Business.Utils.Abstract.Base;
using Core.Business.DTOs.Abstract;

namespace Business.Utils.Abstract
{
    public interface ICrudEntityService<TEntityGetDto, TEntityCreateDto, TEntityUpdateDto> :
        IAddEntityService<TEntityGetDto, TEntityCreateDto>,
        IUpdateEntityService<TEntityGetDto, TEntityUpdateDto>,
        IDeleteEntityService,
        IGetEntityService<TEntityGetDto>
        
        where TEntityGetDto : IEntityGetDto, new()
        where TEntityCreateDto : IDto, new()
        where TEntityUpdateDto : IDto, new()
    {
    }
}