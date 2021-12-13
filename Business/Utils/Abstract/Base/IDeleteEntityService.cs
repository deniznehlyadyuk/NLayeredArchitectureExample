using System;
using System.Threading.Tasks;
using Core.Utils.Result;

namespace Business.Utils.Abstract.Base
{
    public interface IDeleteEntityService
    {
        Task<IResult> DeleteByIdAsync(Guid id);
    }
}